// File: Systems/IndustrySystem.cs
// Purpose: Apply industry/logistics tuning based on current settings:
//          - Extractor fleet max trucks (TransportCompanyData.m_MaxTransports for industrial companies)
//          - Cargo station max fleet (TransportCompanyData.m_MaxTransports for CargoTransportStationData)
//          - Delivery truck cargo capacity (DeliveryTruckData.m_CargoCapacity)
// Notes:
// - SystemAPI queries.
// - PrefabBase authoring data is the vanilla source of truth for delivery truck capacities.
// - Delivery sliders are now stored as percent values (100..500), so they are converted to scalars here.
// - Cargo station / extractor fleet sliders still use direct scalar values (1..5).
// - Scales all delivery-truck prefabs by bucket (Semi / Van / Raw / Motorbike).
// - Tags changed prefab entities with Updated via ECB (structural change safe).

namespace PublicWorksPlus
{
    using Colossal.Serialization.Entities;
    using Game;
    using Game.Common;
    using Game.Companies;
    using Game.Prefabs;
    using Game.SceneFlow;
    using System;
    using System.Collections.Generic;
    using Unity.Collections;
    using Unity.Entities;

    public sealed partial class IndustrySystem : GameSystemBase
    {
        private PrefabSystem m_PrefabSystem = null!;

        // Prefab base caches (per city/session) — prevents stacking when sliders are moved multiple times.
        private Dictionary<Entity, int> m_CargoStationBaseMaxTransports = null!;
        private Dictionary<Entity, int> m_DeliveryTruckBaseCargoCapacity = null!;
        private Dictionary<Entity, int> m_ExtractorCompanyBaseMaxTransports = null!;

        private static readonly HashSet<string> s_KnownIndustrialCompanies = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "Industrial_FishExtractor", "Industrial_ForestryExtractor", "Industrial_GrainExtractor", "Industrial_OreExtractor", "Industrial_OilExtractor",
            "Industrial_VegetableExtractor", "Industrial_LivestockExtractor", "Industrial_CottonExtractor", "Industrial_CoalMine",
            "Industrial_StoneQuarry", "Industrial_MineralPlant", "Industrial_WarehouseStone", "Industrial_WarehouseCoal", "Industrial_WarehouseMinerals",
        };

        protected override void OnCreate()
        {
            base.OnCreate();

            m_PrefabSystem = World.GetOrCreateSystemManaged<PrefabSystem>();

            m_CargoStationBaseMaxTransports = new Dictionary<Entity, int>();
            m_DeliveryTruckBaseCargoCapacity = new Dictionary<Entity, int>();
            m_ExtractorCompanyBaseMaxTransports = new Dictionary<Entity, int>();

            EntityQuery anyRelevantPrefabQuery = SystemAPI.QueryBuilder()
                .WithAll<PrefabData>()
                .WithAny<TransportCompanyData, DeliveryTruckData>()
                .Build();

            RequireForUpdate(anyRelevantPrefabQuery);

            Enabled = false;
        }

        protected override void OnGameLoadingComplete(Purpose purpose, GameMode mode)
        {
            base.OnGameLoadingComplete(purpose, mode);

            bool isRealGame =
                mode == GameMode.Game &&
                (purpose == Purpose.NewGame || purpose == Purpose.LoadGame);

            if (!isRealGame)
            {
                return;
            }

            m_CargoStationBaseMaxTransports.Clear();
            m_DeliveryTruckBaseCargoCapacity.Clear();
            m_ExtractorCompanyBaseMaxTransports.Clear();

#if DEBUG
            LogUtils.Info(Mod.s_Log, () => $"{Mod.ModTag} City Loading Complete -> applying Industry settings");
#endif
            Enabled = true;
        }

        protected override void OnUpdate()
        {
            GameManager gm = GameManager.instance;
            if (gm == null || !gm.gameMode.IsGame())
            {
                Enabled = false;
                return;
            }

            if (Mod.Settings == null)
            {
                Enabled = false;
                return;
            }

            Setting settings = Mod.Settings;
#if DEBUG
            bool verbose = settings.EnableDebugLogging;
#else
            bool verbose = false;
#endif

            ComponentLookup<CarTractorData> tractorLookup = SystemAPI.GetComponentLookup<CarTractorData>(isReadOnly: true);
            ComponentLookup<CarTrailerData> trailerLookup = SystemAPI.GetComponentLookup<CarTrailerData>(isReadOnly: true);

            EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.Temp);
            bool anyPrefabTaggedUpdated = false;

            // -----------------------------------------------------------------
            // Cargo Stations: max trucks (TransportCompanyData.m_MaxTransports)
            // These sliders still use direct scalar values (1x..5x).
            // -----------------------------------------------------------------
            {
                float scalar = ScalarMath.ClampScalar(
                    settings.CargoStationMaxTrucksScalar,
                    Setting.CargoStationMinScalar,
                    Setting.CargoStationMaxScalar);

                foreach ((RefRW<TransportCompanyData> companyRef, Entity prefabEntity) in SystemAPI
                             .Query<RefRW<TransportCompanyData>>()
                             .WithAll<CargoTransportStationData, PrefabData>()
                             .WithEntityAccess())
                {
                    ref TransportCompanyData company = ref companyRef.ValueRW;

                    int baseMax = GetOrCacheCargoStationBase(prefabEntity, company.m_MaxTransports);

                    if (baseMax <= 0 && company.m_MaxTransports <= 0)
                    {
                        continue;
                    }

                    int newMax = ScalarMath.ScaleIntRoundedAllowZeroMin1(baseMax, scalar);

                    if (newMax != company.m_MaxTransports)
                    {
                        string prefabName = PrefabNameUtil.GetNameSafe(m_PrefabSystem, prefabEntity);

                        if (verbose)
                        {
                            LogUtils.Info(
                                Mod.s_Log,
                                () => $"{Mod.ModTag} CargoStation max trucks: '{prefabName}' " +
                                      $"Base={baseMax} x{scalar:0.##} -> {newMax}");
                        }

                        company.m_MaxTransports = newMax;
                        TagPrefabUpdatedIfMissing(prefabEntity, ref ecb, ref anyPrefabTaggedUpdated);
                    }
                }
            }

            // -------------------------------------------------------------------
            // Delivery trucks: buckets (semi / vans / raw materials / motorbikes)
            // Delivery sliders are now stored as percent values (100..500).
            // Convert those percent values back into a scalar here.
            // -------------------------------------------------------------------
            {
                float semiScalar = ScalarMath.PercentToScalarClamped(settings.SemiTruckCargoScalar, Setting.DeliveryMinPercent, Setting.DeliveryMaxPercent);
                float vanScalar = ScalarMath.PercentToScalarClamped(settings.DeliveryVanCargoScalar, Setting.DeliveryMinPercent, Setting.DeliveryMaxPercent);
                float rawScalar = ScalarMath.PercentToScalarClamped(settings.CoalTruckScalar, Setting.DeliveryMinPercent, Setting.DeliveryMaxPercent);
                float mbikeScalar = ScalarMath.PercentToScalarClamped(settings.MotorbikeDeliveryCargoScalar, Setting.DeliveryMinPercent, Setting.DeliveryMaxPercent);

                foreach ((RefRW<DeliveryTruckData> truckRef, Entity prefabEntity) in SystemAPI
                             .Query<RefRW<DeliveryTruckData>>()
                             .WithAll<PrefabData>()
                             .WithEntityAccess())
                {
                    ref DeliveryTruckData data = ref truckRef.ValueRW;

                    int baseCap = GetOrCacheDeliveryTruckBase(prefabEntity, data.m_CargoCapacity);

                    if (baseCap <= 0 && data.m_CargoCapacity <= 0)
                    {
                        continue;
                    }

                    string prefabName = PrefabNameUtil.GetNameSafe(m_PrefabSystem, prefabEntity);

                    VehicleHelpers.GetTrailerTypeInfo(
                        in tractorLookup,
                        in trailerLookup,
                        prefabEntity,
                        out bool hasTractor,
                        out CarTrailerType tractorType,
                        out bool hasTrailer,
                        out CarTrailerType trailerType);

                    VehicleHelpers.DeliveryBucket bucket = VehicleHelpers.ClassifyDeliveryTruckPrefab(
                        prefabName,
                        baseCap,
                        data.m_TransportedResources,
                        hasTractor,
                        tractorType,
                        hasTrailer,
                        trailerType);

                    if (bucket == VehicleHelpers.DeliveryBucket.Other)
                    {
                        continue;
                    }

                    float scalar =
                        bucket == VehicleHelpers.DeliveryBucket.Semi ? semiScalar :
                        bucket == VehicleHelpers.DeliveryBucket.Van ? vanScalar :
                        bucket == VehicleHelpers.DeliveryBucket.RawMaterials ? rawScalar :
                        mbikeScalar;

                    int newCap = ScalarMath.ScaleIntRoundedAllowZeroMin1(baseCap, scalar);

                    if (newCap != data.m_CargoCapacity)
                    {
                        if (verbose)
                        {
                            string resources = data.m_TransportedResources.ToString();

                            LogUtils.Info(
                                Mod.s_Log,
                                () => $"{Mod.ModTag} Delivery cargo: '{prefabName}' Bucket={bucket} Base={baseCap} x{scalar:0.##} -> {newCap} " +
                                      $"Resources={resources}");
                        }

                        data.m_CargoCapacity = newCap;
                        TagPrefabUpdatedIfMissing(prefabEntity, ref ecb, ref anyPrefabTaggedUpdated);
                    }
                }
            }

            // -------------------------------------------------------------------------------
            // Extractor transport company: max fleet (TransportCompanyData.m_MaxTransports)
            // These sliders still use direct scalar values (1x..5x).
            // -------------------------------------------------------------------------------
            {
                float scalar = ScalarMath.ClampScalar(
                    settings.ExtractorMaxTrucksScalar,
                    Setting.CargoStationMinScalar,
                    Setting.CargoStationMaxScalar);

                int matched = 0;
                int changed = 0;
                int skippedZero = 0;

                foreach ((RefRW<TransportCompanyData> tcRef, Entity prefabEntity) in SystemAPI
                             .Query<RefRW<TransportCompanyData>>()
                             .WithAll<PrefabData>()
                             .WithEntityAccess())
                {
                    string name = PrefabNameUtil.GetNameSafe(m_PrefabSystem, prefabEntity);
                    if (!IsTargetIndustrialCompany(name))
                    {
                        continue;
                    }

                    ref TransportCompanyData tc = ref tcRef.ValueRW;

                    int baseMax = GetOrCacheExtractorCompanyBase(prefabEntity, tc.m_MaxTransports);

                    if (baseMax == 0 && tc.m_MaxTransports == 0)
                    {
                        skippedZero++;
                        continue;
                    }

                    matched++;

                    int desired = ScalarMath.ScaleIntRoundedAllowZeroMin1(baseMax, scalar);

                    if (tc.m_MaxTransports != desired)
                    {
                        tc.m_MaxTransports = desired;
                        changed++;

                        if (verbose)
                        {
                            LogUtils.Info(
                                Mod.s_Log,
                                () => $"{Mod.ModTag} Extractor trucks: '{name}' Base={baseMax} x{scalar:0.##} -> {desired}");
                        }

                        TagPrefabUpdatedIfMissing(prefabEntity, ref ecb, ref anyPrefabTaggedUpdated);
                    }
                }

                if (verbose)
                {
                    LogUtils.Info(
                        Mod.s_Log,
                        () => $"{Mod.ModTag} Extractor trucks: scalar={scalar:0.##} matched={matched} changed={changed} skippedZero={skippedZero}");
                }
            }

            if (anyPrefabTaggedUpdated)
            {
                ecb.Playback(EntityManager);
            }

            ecb.Dispose();

            Enabled = false;
        }

        // SystemAPI is valid in non-static SystemBase methods.
        // This helper tags only changed prefab entities.
        private void TagPrefabUpdatedIfMissing(Entity prefabEntity, ref EntityCommandBuffer ecb, ref bool anyPrefabTaggedUpdated)
        {
            if (!SystemAPI.HasComponent<Updated>(prefabEntity))
            {
                ecb.AddComponent<Updated>(prefabEntity);
                anyPrefabTaggedUpdated = true;
            }
        }

        private static bool IsTargetIndustrialCompany(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }

            if (s_KnownIndustrialCompanies.Contains(name))
            {
                return true;
            }

            if (name.StartsWith("Industrial_", StringComparison.OrdinalIgnoreCase) &&
                name.IndexOf("Extractor", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return true;
            }

            return false;
        }

        private int GetOrCacheCargoStationBase(Entity prefabEntity, int currentValue)
        {
            if (m_CargoStationBaseMaxTransports.TryGetValue(prefabEntity, out int baseMax))
            {
                return baseMax;
            }

            int vanilla;
            if (TryGetCargoStationVanillaMax(prefabEntity, out vanilla) && vanilla > 0)
            {
                baseMax = vanilla;
            }
            else
            {
                baseMax = currentValue;
            }

            m_CargoStationBaseMaxTransports[prefabEntity] = baseMax;
            return baseMax;
        }

        private bool TryGetCargoStationVanillaMax(Entity prefabEntity, out int baseMax)
        {
            baseMax = 0;

            if (!PrefabComponentUtil.TryGetComponent(m_PrefabSystem, prefabEntity, out CargoTransportStation station))
            {
                return false;
            }

            baseMax = station.transports;
            return true;
        }

        private int GetOrCacheDeliveryTruckBase(Entity prefabEntity, int currentValue)
        {
            if (m_DeliveryTruckBaseCargoCapacity.TryGetValue(prefabEntity, out int baseCap))
            {
                return baseCap;
            }

            int vanilla;
            if (TryGetDeliveryTruckVanillaCargo(prefabEntity, out vanilla) && vanilla >= 0)
            {
                baseCap = vanilla;
            }
            else
            {
                baseCap = currentValue;
            }

            m_DeliveryTruckBaseCargoCapacity[prefabEntity] = baseCap;
            return baseCap;
        }

        private bool TryGetDeliveryTruckVanillaCargo(Entity prefabEntity, out int baseCap)
        {
            baseCap = 0;

            if (!PrefabComponentUtil.TryGetComponent(m_PrefabSystem, prefabEntity, out Game.Prefabs.DeliveryTruck truck))
            {
                return false;
            }

            baseCap = truck.m_CargoCapacity;
            return true;
        }

        private int GetOrCacheExtractorCompanyBase(Entity prefabEntity, int currentValue)
        {
            if (m_ExtractorCompanyBaseMaxTransports.TryGetValue(prefabEntity, out int baseMax))
            {
                return baseMax;
            }

            baseMax = currentValue;
            m_ExtractorCompanyBaseMaxTransports[prefabEntity] = baseMax;
            return baseMax;
        }
    }
}
