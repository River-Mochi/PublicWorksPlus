// File: Systems/LaneWearProbeSystem.cs
// Purpose: Verbose probe for LaneCondition.m_Wear to validate lane wear slider behavior.
// Notes:
// - Samples ONLY lanes in the current UpdateFrame group (matches NetDeteriorationSystem cadence).
// - Keeps a tiny stable sample set per group so deltas become meaningful when that group repeats.
// - Avoids obsolete EntityManager.GetSharedComponentData by using EntityQuery shared-component filtering.
// - GetUpdateInterval must return a power of 2 for any phase; DO NOT gate settings here.

namespace DispatchBoss
{
    using Game;
    using Game.Net;
    using Game.Prefabs;
    using Game.Simulation;
    using Unity.Collections;
    using Unity.Entities;
    using Unity.Mathematics;

    public sealed partial class LaneWearProbeSystem : GameSystemBase
    {
        // NetDeteriorationSystem updates lanes in groups; these match common vanilla constants.
        // These are NOT the probe frequency; they are used to compute the current UpdateFrame group.
        private const int kNetUpdatesPerDay = 16;
        private const int kGroupCount = 16;

        // Keep small to avoid log spam.
        private const int kSamplesPerGroup = 3;

        // Probe frequency:
        // 262144 “simulation frames” per day; 262144 / 256 = 1024 frames (power of 2).
        private const int kProbeUpdatesPerDay = 256;

        private SimulationSystem m_Sim = null!;
        private EntityQuery m_LanesByFrameQuery;

        // Per-group samples (group * kSamplesPerGroup + i)
        private Entity[] m_Samples = null!;
        private float[] m_LastWear = null!;
        private int[] m_LastWearQ64 = null!;
        private bool[] m_HasLast = null!;

        public override int GetUpdateInterval(SystemUpdatePhase phase)
        {
            if (phase == SystemUpdatePhase.GameSimulation)
            {
                return 262144 / kProbeUpdatesPerDay;
            }

            // Defensive: must be power-of-2 and non-zero.
            return 1;
        }

        protected override void OnCreate()
        {
            base.OnCreate();

            m_Sim = World.GetOrCreateSystemManaged<SimulationSystem>();

            // Query all lanes that can have wear and are assigned to an UpdateFrame group.
            m_LanesByFrameQuery = SystemAPI.QueryBuilder()
                .WithAll<LaneCondition, PrefabRef, UpdateFrame>()
                .Build();

            RequireForUpdate(m_LanesByFrameQuery);

            int totalSlots = kGroupCount * kSamplesPerGroup;
            m_Samples = new Entity[totalSlots];
            m_LastWear = new float[totalSlots];
            m_LastWearQ64 = new int[totalSlots];
            m_HasLast = new bool[totalSlots];

            for (int i = 0; i < totalSlots; i++)
            {
                m_Samples[i] = Entity.Null;
                m_LastWear[i] = 0f;
                m_LastWearQ64[i] = 0;
                m_HasLast[i] = false;
            }
        }

        protected override void OnUpdate()
        {
            // Gate runtime behavior HERE (safe and expected).
            if (Mod.Settings == null || !Mod.Settings.EnableDebugLogging)
                return;

            uint frame = m_Sim.frameIndex;

            uint groupU = SimulationUtils.GetUpdateFrame(frame, kNetUpdatesPerDay, kGroupCount);
            int group = (int)groupU;
            UpdateFrame target = new UpdateFrame(groupU);

            RefillSamplesIfNeeded(group, target);

            ComponentLookup<LaneCondition> condLookup = SystemAPI.GetComponentLookup<LaneCondition>(isReadOnly: true);
            ComponentLookup<PrefabRef> prLookup = SystemAPI.GetComponentLookup<PrefabRef>(isReadOnly: true);
            ComponentLookup<LaneDeteriorationData> detLookup = SystemAPI.GetComponentLookup<LaneDeteriorationData>(isReadOnly: true);

            int baseIndex = group * kSamplesPerGroup;

            float sumDelta = 0f;
            int sumDeltaQ = 0;
            float maxAbsDelta = 0f;
            int maxAbsDeltaQ = 0;
            int observed = 0;

            for (int i = 0; i < kSamplesPerGroup; i++)
            {
                int idx = baseIndex + i;
                Entity lane = m_Samples[idx];

                if (lane == Entity.Null || !EntityManager.Exists(lane))
                    continue;

                if (!condLookup.HasComponent(lane) || !prLookup.HasComponent(lane))
                    continue;

                LaneCondition cond = condLookup[lane];
                PrefabRef pr = prLookup[lane];

                float tf = float.NaN;
                float traf = float.NaN;

                if (detLookup.HasComponent(pr.m_Prefab))
                {
                    LaneDeteriorationData det = detLookup[pr.m_Prefab];
                    tf = det.m_TimeFactor;
                    traf = det.m_TrafficFactor;
                }

                bool had = m_HasLast[idx];

                float wear = cond.m_Wear;
                int wearQ64 = (int)math.round(wear * 64f);

                float delta = had ? (wear - m_LastWear[idx]) : 0f;
                int deltaQ = had ? (wearQ64 - m_LastWearQ64[idx]) : 0;

                m_LastWear[idx] = wear;
                m_LastWearQ64[idx] = wearQ64;
                m_HasLast[idx] = true;

                observed++;
                sumDelta += delta;
                sumDeltaQ += deltaQ;
                maxAbsDelta = math.max(maxAbsDelta, math.abs(delta));
                maxAbsDeltaQ = math.max(maxAbsDeltaQ, math.abs(deltaQ));

                // Only print per-lane detail when it’s informative:
                // - first time we’ve seen this sample, or
                // - quantized step changed (deltaQ != 0)
                if (!had || deltaQ != 0)
                {
                    string tag = had ? "" : " (first)";
                    Mod.s_Log.Info(
                        $"{Mod.ModTag} [LaneWearProbe g={group}] lane={lane.Index}:{lane.Version} " +
                        $"wear={wear:0.######} (q64={wearQ64}) Δ={delta:0.######} (Δq64={deltaQ}){tag} " +
                        $"Prefab={pr.m_Prefab.Index}:{pr.m_Prefab.Version} TF={Fmt(tf)} TrF={Fmt(traf)}");
                }
            }

            if (observed == 0)
            {
                Mod.s_Log.Info($"{Mod.ModTag} [LaneWearProbe g={group}] no samples found Frame={frame}");
                return;
            }

            float avg = sumDelta / observed;

            Mod.s_Log.Info(
                $"{Mod.ModTag} [LaneWearProbe g={group}] summary: Observed={observed} " +
                $"AvgΔ={avg:0.######} AvgΔq64={(sumDeltaQ / (float)observed):0.###} " +
                $"Max|Δ|={maxAbsDelta:0.######} Max|Δq64|={maxAbsDeltaQ} Frame={frame}");
        }

        private void RefillSamplesIfNeeded(int group, UpdateFrame target)
        {
            int baseIndex = group * kSamplesPerGroup;

            bool allValid = true;
            for (int i = 0; i < kSamplesPerGroup; i++)
            {
                Entity e = m_Samples[baseIndex + i];
                if (e == Entity.Null || !EntityManager.Exists(e))
                {
                    allValid = false;
                    break;
                }
            }

            if (allValid)
                return;

            // Reset this group’s slots.
            for (int i = 0; i < kSamplesPerGroup; i++)
            {
                m_Samples[baseIndex + i] = Entity.Null;
                m_LastWear[baseIndex + i] = 0f;
                m_LastWearQ64[baseIndex + i] = 0;
                m_HasLast[baseIndex + i] = false;
            }

            // Filter lanes to ONLY the current UpdateFrame group.
            m_LanesByFrameQuery.ResetFilter();
            m_LanesByFrameQuery.SetSharedComponentFilter(target);

            try
            {
                using NativeArray<Entity> lanes = m_LanesByFrameQuery.ToEntityArray(Allocator.Temp);

                int filled = 0;
                for (int i = 0; i < lanes.Length && filled < kSamplesPerGroup; i++)
                {
                    Entity lane = lanes[i];
                    if (lane == Entity.Null)
                        continue;

                    m_Samples[baseIndex + filled] = lane;
                    filled++;
                }
            }
            finally
            {
                // Always clear filters.
                m_LanesByFrameQuery.ResetFilter();
            }
        }

        private static string Fmt(float v) => float.IsNaN(v) ? "n/a" : v.ToString("0.###");
    }
}
