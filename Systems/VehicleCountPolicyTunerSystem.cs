// File: Systems/VehicleCountPolicyTunerSystem.cs
// Purpose: Optional toggle to adjust VehicleCountPolicy VehicleInterval modifier so the vanilla transit line panel
//          can reach as low as ~1 vehicle, while keeping max from going too high.
// Notes:
// - Global policy edit (affects all transit line types using VehicleCountPolicy).
// - One-shot: runs after city load, and whenever Settings.Apply enables it.
// - Toggle OFF restores the original policy values captured at first run (per session).

namespace DispatchBoss
{
    using Colossal.Mathematics;              // Bounds1 (min/max float range)
    using Colossal.Serialization.Entities;   // Purpose
    using Game;                              // GameMode
    using Game.Prefabs;                      // PrefabSystem, UITransportConfiguration*
    using Game.Routes;                       // RouteModifierData, RouteModifierType
    using Unity.Entities;                    // EntityQuery, ComponentType, DynamicBuffer

    public sealed partial class VehicleCountPolicyTunerSystem : GameSystemBase
    {
        private PrefabSystem m_PrefabSystem = null!;
        private EntityQuery m_ConfigQuery;

        // Captured original (vanilla) VehicleInterval modifier data.
        // Static so it persists for the whole play session (even if the system runs multiple times).
        private static bool s_HasOriginal;
        private static Bounds1 s_OriginalVehicleIntervalRange;
        private static ModifierValueMode s_OriginalVehicleIntervalMode;

        // Targets are in "applied effect" space (what the policy ultimately applies),
        // not in "slider input" space. For InverseRelative mode we must convert applied->input.
        private const float kFewerVehiclesApplied = 22f;
        private const float kMoreVehiclesApplied = -0.84f;

        protected override void OnCreate()
        {
            base.OnCreate();

            m_PrefabSystem = World.GetOrCreateSystemManaged<PrefabSystem>();
            m_ConfigQuery = GetEntityQuery(ComponentType.ReadOnly<UITransportConfigurationData>());

            RequireForUpdate(m_ConfigQuery);
            Enabled = false;
        }

        protected override void OnGameLoadingComplete(Purpose purpose, GameMode mode)
        {
            base.OnGameLoadingComplete(purpose, mode);

            bool isRealGame =
                mode == GameMode.Game &&
                (purpose == Purpose.NewGame || purpose == Purpose.LoadGame);

            if (!isRealGame)
                return;

            Enabled = true;
        }

        protected override void OnUpdate()
        {
            // One-shot behavior.
            Enabled = false;

            if (m_ConfigQuery.IsEmptyIgnoreFilter)
                return;

            Setting settings = Mod.Settings;
            if (settings == null)
                return;

            bool verbose = settings.EnableDebugLogging;
            bool enable = settings.EnableLineVehicleCountTuner;

            // UITransportConfigurationPrefab holds references to various UI policy prefabs.
            UITransportConfigurationPrefab config =
                m_PrefabSystem.GetSingletonPrefab<UITransportConfigurationPrefab>(m_ConfigQuery);

            // VehicleCountPolicy is an entity that contains RouteModifierData entries.
            Entity policyEntity = m_PrefabSystem.GetEntity(config.m_VehicleCountPolicy);

            if (policyEntity == Entity.Null || !SystemAPI.Exists(policyEntity))
            {
                Mod.s_Log.Warn($"{Mod.ModTag} VehicleCountPolicyTuner: could not resolve VehicleCountPolicy entity.");
                return;
            }

            if (!SystemAPI.HasBuffer<RouteModifierData>(policyEntity))
            {
                Mod.s_Log.Warn($"{Mod.ModTag} VehicleCountPolicyTuner: VehicleCountPolicy has no RouteModifierData buffer.");
                return;
            }

            DynamicBuffer<RouteModifierData> buf = SystemAPI.GetBuffer<RouteModifierData>(policyEntity);

            bool found = false;
            bool changed = false;

            // Find the VehicleInterval modifier entry and update just that one.
            for (int i = 0; i < buf.Length; i++)
            {
                RouteModifierData item = buf[i];
                if (item.m_Type != RouteModifierType.VehicleInterval)
                    continue;

                found = true;

                // Capture vanilla policy once per session (so toggle OFF can restore it later).
                if (!s_HasOriginal)
                {
                    s_HasOriginal = true;
                    s_OriginalVehicleIntervalRange = item.m_Range;
                    s_OriginalVehicleIntervalMode = item.m_Mode;

                    Mod.s_Log.Info(
                        $"{Mod.ModTag} VehicleCountPolicyTuner: captured original VehicleInterval " +
                        $"mode={item.m_Mode} range={item.m_Range.min:F3}..{item.m_Range.max:F3} (input space)");
                }

                // Toggle OFF -> restore original values.
                if (!enable)
                {
                    bool needRestore =
                        item.m_Range.min != s_OriginalVehicleIntervalRange.min ||
                        item.m_Range.max != s_OriginalVehicleIntervalRange.max ||
                        item.m_Mode != s_OriginalVehicleIntervalMode;

                    if (needRestore)
                    {
                        Bounds1 oldRange = item.m_Range;
                        ModifierValueMode oldMode = item.m_Mode;

                        item.m_Range = s_OriginalVehicleIntervalRange;
                        item.m_Mode = s_OriginalVehicleIntervalMode;

                        buf[i] = item;
                        changed = true;

                        Mod.s_Log.Info(
                            $"{Mod.ModTag} VehicleCountPolicyTuner: DISABLED -> restore VehicleInterval " +
                            $"mode {oldMode} -> {item.m_Mode}, " +
                            $"range {oldRange.min:F3}..{oldRange.max:F3} -> {item.m_Range.min:F3}..{item.m_Range.max:F3} (input space)");
                    }
                    else if (verbose)
                    {
                        Mod.s_Log.Info($"{Mod.ModTag} VehicleCountPolicyTuner: DISABLED -> already original (no change).");
                    }

                    break;
                }

                // Toggle ON -> only supported safely when the policy uses InverseRelative.
                if (item.m_Mode == ModifierValueMode.InverseRelative)
                {
                    // Build a new input-range that produces our desired applied effects.
                    Bounds1 desired = BuildInverseRelativeInputRange(kFewerVehiclesApplied, kMoreVehiclesApplied);

                    if (item.m_Range.min != desired.min || item.m_Range.max != desired.max)
                    {
                        Bounds1 oldRange = item.m_Range;

                        item.m_Range = desired;
                        buf[i] = item;
                        changed = true;

                        float appliedAtMinInput = InverseRelativeAppliedFromInput(desired.min);
                        float appliedAtMaxInput = InverseRelativeAppliedFromInput(desired.max);

                        Mod.s_Log.Info(
                            $"{Mod.ModTag} VehicleCountPolicyTuner: ENABLED -> VehicleInterval input range " +
                            $"{oldRange.min:F3}..{oldRange.max:F3} -> {desired.min:F3}..{desired.max:F3} (InverseRelative). " +
                            $"Applied endpoints: inputMin→{appliedAtMinInput:F3}, inputMax→{appliedAtMaxInput:F3}. " +
                            $"Targets: fewer={kFewerVehiclesApplied:F1}, more={kMoreVehiclesApplied:F2}");
                    }
                    else if (verbose)
                    {
                        Mod.s_Log.Info($"{Mod.ModTag} VehicleCountPolicyTuner: ENABLED -> already in desired state (no change).");
                    }
                }
                else if (item.m_Mode == ModifierValueMode.Relative)
                {
                    // Different math model; safer to leave unchanged.
                    Mod.s_Log.Warn(
                        $"{Mod.ModTag} VehicleCountPolicyTuner: VehicleInterval mode is Relative; leaving unchanged. " +
                        $"Range={item.m_Range.min:F3}..{item.m_Range.max:F3}");
                }
                else
                {
                    Mod.s_Log.Warn(
                        $"{Mod.ModTag} VehicleCountPolicyTuner: VehicleInterval mode is {item.m_Mode}; not modifying. " +
                        $"Range={item.m_Range.min:F3}..{item.m_Range.max:F3}");
                }

                // Only one VehicleInterval entry expected; stop after handling it.
                break;
            }

            if (!found)
            {
                Mod.s_Log.Warn($"{Mod.ModTag} VehicleCountPolicyTuner: no VehicleInterval RouteModifierData entry found.");
            }
            else if (!changed && verbose)
            {
                Mod.s_Log.Info($"{Mod.ModTag} VehicleCountPolicyTuner: completed (no change). enable={enable}");
            }
        }

        private static Bounds1 BuildInverseRelativeInputRange(float fewerVehiclesApplied, float moreVehiclesApplied)
        {
            // Convert the "applied effect" endpoints into the "input" endpoints used by InverseRelative mode.
            float inputForFewer = InverseRelativeInputFromApplied(fewerVehiclesApplied);
            float inputForMore = InverseRelativeInputFromApplied(moreVehiclesApplied);

            float inputMin = inputForFewer;
            float inputMax = inputForMore;

            // Ensure min <= max (readable swap; ignore IDE0180 if preferred).
            if (inputMin > inputMax)
            {
                float t = inputMin;
                inputMin = inputMax;
                inputMax = t;
            }

            // InverseRelative has a divide by (1 + input). Input near -1 explodes.
            // Clamp slightly above -1 to avoid infinity/NaN.
            if (inputMin <= -0.999f)
                inputMin = -0.999f;

            return new Bounds1(inputMin, inputMax);
        }

        private static float InverseRelativeInputFromApplied(float applied)
        {
            // input = (-applied) / (1 + applied)
            float denom = 1f + applied;
            if (denom == 0f)
                return 0f;

            return (-applied) / denom;
        }

        private static float InverseRelativeAppliedFromInput(float input)
        {
            // applied = (-input) / (1 + input)
            float denom = 1f + input;
            if (denom == 0f)
                return 0f;

            return (-input) / denom;
        }
    }
}
