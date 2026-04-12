// File: Systems/StorageTransfer/StationTransferAmountUtil.cs
// Purpose: Helpers for promoting station / OC car storage-transfer requests
//          up to a full currently selectable truck.

namespace PublicWorksPlus
{
    using Game.Economy;
    using Game.Prefabs;
    using Unity.Mathematics;

    internal static class StationTransferAmountUtil
    {
        internal static bool IsEligibleOutgoingCarRequest(Game.Companies.StorageTransferFlags flags)
        {
            return (flags & Game.Companies.StorageTransferFlags.Incoming) == 0 &&
                   (flags & Game.Companies.StorageTransferFlags.Car) != 0;
        }

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

            if (originalAmount <= 0 || resource == Resource.NoResource)
            {
                return false;
            }

            if (!TryGetMaxTruckCapacity(truckSelectData, resource, out int maxCapacity))
            {
                return false;
            }

            if (originalAmount >= maxCapacity)
            {
                return false;
            }

            adjustedAmount = maxCapacity;
            return adjustedAmount != originalAmount;
        }
    }
}

