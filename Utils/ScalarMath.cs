// File: Utils/ScalarMath.cs
// Purpose: Centralized clamping + scaling helpers to keep math consistent across systems.
// Notes:
// - Rounds to int (AwayFromZero) so slider results feel stable.
// - AllowZero scaling returns 0 only when base value is <= 0.

namespace AdjustTransit
{
    using System;
    using Unity.Mathematics;

    internal static class ScalarMath
    {
        internal static float PercentToScalarClamped(float percent, float minPercent, float maxPercent)
        {
            percent = SanitizeFinite(percent);
            percent = math.clamp(percent, minPercent, maxPercent);
            return percent / 100f;
        }

        internal static float ClampScalar(float scalar, float minScalar, float maxScalar)
        {
            scalar = SanitizeFinite(scalar);
            return math.clamp(scalar, minScalar, maxScalar);
        }

        internal static int ScaleIntRoundedMin1(int baseValue, float scalar)
        {
            int basePositive = math.max(baseValue, 1);

            float s = SanitizeFinite(scalar);
            if (s < 0f) s = 0f;

            double raw = basePositive * (double)s;
            if (raw >= int.MaxValue)
                return int.MaxValue;

            int v = (int)Math.Round(raw, MidpointRounding.AwayFromZero);
            if (v < 1) v = 1;

            return v;
        }

        internal static int ScaleIntRoundedAllowZeroMin1(int baseValue, float scalar)
        {
            if (baseValue <= 0)
                return 0;

            float s = SanitizeFinite(scalar);
            if (s < 0f) s = 0f;

            double raw = baseValue * (double)s;
            if (raw >= int.MaxValue)
                return int.MaxValue;

            int v = (int)Math.Round(raw, MidpointRounding.AwayFromZero);
            if (v < 1) v = 1;

            return v;
        }

        private static float SanitizeFinite(float v)
        {
            if (math.isnan(v) || math.isinf(v))
                return 0f;

            return v;
        }
    }
}
