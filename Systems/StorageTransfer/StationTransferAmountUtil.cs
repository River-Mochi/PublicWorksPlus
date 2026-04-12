// File: Systems/StorageTransfer/StationTransferAmountUtil.cs
// Purpose: Helpers for promoting storage-transfer and company-shopping requests
//          up to a safe currently selectable truck capacity.

namespace PublicWorksPlus
{
    using Game.Economy;
    using Game.Prefabs;
    using System;
    using Unity.Mathematics;

    internal static class StationTransferAmountUtil
    {
        private const int SelectionProbeCount = 8;

        internal static bool IsEligibleOutgoingCarRequest(Game.Companies.StorageTransferFlags flags)
        {
            return (flags & Game.Companies.StorageTransferFlags.Incoming) == 0 &&
                   (flags & Game.Companies.StorageTransferFlags.Car) != 0;
        }

        internal static bool TryGetSafeSelectedTruckCapacity(
            DeliveryTruckSelectData truckSelectData,
            Resource resource,
            int requestedAmount,
            out int selectedCapacity)
        {
            selectedCapacity = 0;

            if (resource == Resource.NoResource || requestedAmount <= 0)
            {
                return false;
            }

            truckSelectData.GetCapacityRange(resource, out int minCapacity, out int maxCapacity);
            if (maxCapacity <= 0)
            {
                return false;
            }

            int safeCapacity = int.MaxValue;

            for (int i = 0; i < SelectionProbeCount; i++)
            {
                Unity.Mathematics.Random random = CreateProbeRandom(resource, requestedAmount, i);

                if (truckSelectData.TrySelectItem(ref random, resource, requestedAmount, out DeliveryTruckSelectItem item) &&
                    item.m_Capacity > 0)
                {
                    safeCapacity = math.min(safeCapacity, item.m_Capacity);
                }
            }

            if (safeCapacity == int.MaxValue)
            {
                // Conservative fallback. Better to under-promote than overfill a live truck.
                selectedCapacity = minCapacity;
                return selectedCapacity > 0;
            }

            selectedCapacity = safeCapacity;
            return true;
        }

        internal static bool TryPromoteToAtLeastOneFullTruck(
            DeliveryTruckSelectData truckSelectData,
            Resource resource,
            int originalAmount,
            out int adjustedAmount)
        {
            adjustedAmount = originalAmount;

            if (originalAmount <= 0 || resource == Resource.NoResource)
            {
                return false;
            }

            if (!TryGetSafeSelectedTruckCapacity(truckSelectData, resource, originalAmount, out int safeCapacity))
            {
                return false;
            }

            if (originalAmount >= safeCapacity)
            {
                return false;
            }

            adjustedAmount = safeCapacity;
            return adjustedAmount != originalAmount;
        }

        private static Unity.Mathematics.Random CreateProbeRandom(Resource resource, int requestedAmount, int salt)
        {
            ulong raw = Convert.ToUInt64(resource);
            uint low = (uint)(raw & 0xFFFFFFFFu);
            uint high = (uint)(raw >> 32);

            uint seed = low ^ high ^ (uint)requestedAmount ^ (uint)(0x9E3779B9u * (uint)(salt + 1));
            seed ^= seed << 13;
            seed ^= seed >> 17;
            seed ^= seed << 5;

            if (seed == 0)
            {
                seed = 1;
            }

            return new Unity.Mathematics.Random(seed);
        }
    }
}
