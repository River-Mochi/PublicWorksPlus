// File: Systems/Probes/LaneWearProbeSystem.cs
// Purpose: Verbose probe for LaneCondition.m_Wear to validate lane wear slider behavior.
// Notes:
// - Samples ONLY lanes in the current UpdateFrame group (matches NetDeteriorationSystem cadence).
// - Keeps a small stable sample set per group so deltas become meaningful when that group repeats.
// - Uses EntityQuery shared-component filtering (UpdateFrame) instead of obsolete shared-component APIs.
// - GetUpdateInterval must return a power of 2 for any phase; settings gating belongs in OnUpdate.

namespace PublicWorksPlus
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
        // NetDeteriorationSystem updates lane wear 16 times/day, split into 16 UpdateFrame groups.
        // These constants are used only for computing the current group index.
        private const int kNetUpdatesPerDay = 16;
        private const int kGroupCount = 16;

        // Small sample size per group to avoid log spam.
        private const int kSamplesPerGroup = 3;

        // Probe frequency:
        // 262144 sim frames/day; 262144 / 256 = 1024 frames (power of 2).
        private const int kProbeUpdatesPerDay = 256;

        // Wear quantization for logs: steps of 1/64.
        private const float kWearQuantize = 64f;

        private SimulationSystem m_Sim = null!;
        private EntityQuery m_LanesByFrameQuery;

        // Per-group samples (group * kSamplesPerGroup + i).
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

            // Defensive: power-of-2 and non-zero.
            return 1;
        }

        protected override void OnCreate()
        {
            base.OnCreate();

            m_Sim = World.GetOrCreateSystemManaged<SimulationSystem>();

            // Query all live lanes with wear + prefab reference + UpdateFrame.
            // UpdateFrame is a shared component; filtering is done via SetSharedComponentFilter.
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
            // Runtime work is gated here (not in GetUpdateInterval).
            if (Mod.Settings == null || !Mod.Settings.EnableDebugLogging)
                return;

            uint frame = m_Sim.frameIndex;

            // Determine the UpdateFrame group being processed by the deterioration system.
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

                if (!condLookup.TryGetComponent(lane, out LaneCondition cond))
                    continue;

                if (!prLookup.TryGetComponent(lane, out PrefabRef pr))
                    continue;

                float tf = float.NaN;
                float traf = float.NaN;

                if (detLookup.TryGetComponent(pr.m_Prefab, out LaneDeteriorationData det))
                {
                    tf = det.m_TimeFactor;
                    traf = det.m_TrafficFactor;
                }

                bool had = m_HasLast[idx];

                float wear = cond.m_Wear;
                int wearQ64 = (int)math.round(wear * kWearQuantize);

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

                // Per-lane log is printed only when informative:
                // - first time sample is seen, or
                // - quantized wear step changed (deltaQ != 0).
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

            // Keep existing sample set if all lanes still exist.
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

            // Reset group slots and history.
            for (int i = 0; i < kSamplesPerGroup; i++)
            {
                int idx = baseIndex + i;
                m_Samples[idx] = Entity.Null;
                m_LastWear[idx] = 0f;
                m_LastWearQ64[idx] = 0;
                m_HasLast[idx] = false;
            }

            // Filter lanes to ONLY the current UpdateFrame group.
            m_LanesByFrameQuery.ResetFilter();
            m_LanesByFrameQuery.SetSharedComponentFilter(target);

            try
            {
                using (NativeArray<Entity> lanes = m_LanesByFrameQuery.ToEntityArray(Allocator.Temp))
                {
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
            }
            finally
            {
                // Always clear shared-component filters after use.
                m_LanesByFrameQuery.ResetFilter();
            }
        }

        private static string Fmt(float v)
        {
            return float.IsNaN(v) ? "n/a" : v.ToString("0.###");
        }
    }
}
