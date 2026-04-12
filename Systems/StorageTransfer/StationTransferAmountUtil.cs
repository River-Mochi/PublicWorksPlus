// File: Systems/StorageTransfer/StationTransferAmountUtil.cs
// Purpose: Helpers for capacity-aware station / OC storage-transfer amounts.

namespace PublicWorksPlus
{
    using Game.Economy;
    using Game.Prefabs;
    using Unity.Mathematics;

    internal static class StationTransferAmountUtil
    {
        internal static bool TryGetMaxTruckCapacity(
            DeliveryTruckSelectData truckSelectData,
            Resource resource,
            out int maxCapacity)
        {
            maxCapacity = 0;

            if (resource == Resource.NoResource)
            {
                return false;
            }

            truckSelectData.GetCapacityRange(resource, out _, out maxCapacity);
            return maxCapacity > 0;
        }

        internal static bool TryPromoteToAtLeastOneFullTruck(
            DeliveryTruckSelectData truckSelectData,
            Resource resource,
            int originalAmount,
            out int adjustedAmount)
        {
            adjustedAmount = originalAmount;

            if (originalAmount == 0 || resource == Resource.NoResource)
            {
                return false;
            }

            if (!TryGetMaxTruckCapacity(truckSelectData, resource, out int maxCapacity))
            {
                return false;
            }

            int absAmount = math.abs(originalAmount);
            if (absAmount >= maxCapacity)
            {
                return false;
            }

            adjustedAmount = originalAmount < 0 ? -maxCapacity : maxCapacity;
            return adjustedAmount != originalAmount;
        }
    }
}
