// File: Utils/ScalarMath.cs
// Purpose: Centralized clamping + scaling helpers to keep math consistent across systems.
// Notes:
// - Rounds to int (slider results feel better than truncation).
// - "AllowZero" scaling returns 0 only when the base value is <= 0.

namespace DispatchBoss
{
    using System;
    using Unity.Mathematics;

    internal static class ScalarMath
    {
        internal static float PercentToScalarClamped(float percent, float minPercent, float maxPercent)
        {
            percent = SanitizeFinite(percent);
            minPercent = SanitizeFinite(minPercent);
            maxPercent = SanitizeFinite(maxPercent);

            percent = math.clamp(percent, minPercent, maxPercent);
            return percent / 100f;
        }

        internal static float ClampScalar(float scalar, float minScalar, float maxScalar)
        {
            scalar = SanitizeFinite(scalar);
            minScalar = SanitizeFinite(minScalar);
            maxScalar = SanitizeFinite(maxScalar);

            return math.clamp(scalar, minScalar, maxScalar);
        }

        /// <summary>
        /// Scales an integer base value by a scalar, rounds to int, then clamps to a minimum.
        /// If allowZero=true and baseValue<=0, returns 0.
        /// </summary>
        internal static int ScaleIntRounded(int baseValue, float scalar, int minIfBasePositive, bool allowZero)
        {
            if (allowZero && baseValue <= 0)
                return 0;

            // Keep “base 0 stays 0” only when allowZero is enabled; otherwise treat as 1.
            baseValue = math.max(baseValue, 1);

            scalar = SanitizeFinite(scalar);
            scalar = math.max(0f, scalar);

            // Ensure minimum clamp isn't negative (defensive; your callsites use 1).
            minIfBasePositive = math.max(0, minIfBasePositive);

            double raw = baseValue * (double)scalar;
            if (raw >= int.MaxValue)
                return int.MaxValue;

            // AwayFromZero gives better “slider feel” than banker's rounding.
            int v = (int)Math.Round(raw, MidpointRounding.AwayFromZero);

            if (v < minIfBasePositive)
                v = minIfBasePositive;

            return v;
        }

        internal static int ScaleIntRoundedMin1(int baseValue, float scalar)
        {
            return ScaleIntRounded(baseValue, scalar, minIfBasePositive: 1, allowZero: false);
        }

        internal static int ScaleIntRoundedAllowZeroMin1(int baseValue, float scalar)
        {
            return ScaleIntRounded(baseValue, scalar, minIfBasePositive: 1, allowZero: true);
        }

        private static float SanitizeFinite(float v)
        {
            if (math.isnan(v) || math.isinf(v))
                return 0f;

            return v;
        }
    }
}
