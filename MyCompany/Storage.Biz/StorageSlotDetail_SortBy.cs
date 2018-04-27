using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.Storage.Biz
{
    /// <summary>
    /// Sort on storage slot number in ascending order.
    /// </summary>
    public class StorageSlotDetail_SortByStorageSlotNumberAscendingOrder<T> : IComparer<StorageSlotDetail> where T : IStoreable
    {
        public int Compare(StorageSlotDetail x, StorageSlotDetail y)
        {
            if (x.SlotNumber < y.SlotNumber) return -1;
            else if (x.SlotNumber > y.SlotNumber) return 1;
            else return 0;
        }
    }
    /// <summary>
    /// Sort on storage slot number in descending order.
    /// </summary>
    public class StorageSlotDetail_SortByStorageSlotNumberDescendingOrder<T> : IComparer<StorageSlotDetail> where T : IStoreable
    {
        public int Compare(StorageSlotDetail x, StorageSlotDetail y)
        {
            if (x.SlotNumber > y.SlotNumber) return -1;
            else if (x.SlotNumber < y.SlotNumber) return 1;
            else return 0;
        }
    }
    /// <summary>
    /// Sort on occupied space in ascending order.
    /// </summary>
    public class StorageSlotDetail_SortByOccupiedSpaceAscendingOrder<T> : IComparer<StorageSlotDetail> where T : IStoreable
    {
        public int Compare(StorageSlotDetail x, StorageSlotDetail y)
        {
            if (x.OccupiedSpace < y.OccupiedSpace) return -1;
            else if (x.OccupiedSpace > y.OccupiedSpace) return 1;
            else return 0;
        }
    }
    /// <summary>
    /// Sort on occupied space in ascending order.
    /// </summary>
    public class StorageSlotDetail_SortByOccupiedSpaceDescendingOrder<T> : IComparer<StorageSlotDetail> where T : IStoreable
    {
        public int Compare(StorageSlotDetail x, StorageSlotDetail y)
        {
            if (x.OccupiedSpace > y.OccupiedSpace) return -1;
            else if (x.OccupiedSpace < y.OccupiedSpace) return 1;
            else return 0;
        }
    }

    /// <summary>
    /// Sort on free space in ascending order.
    /// </summary>
    public class StorageSlotDetail_SortByFreeSpaceAscendingOrder<T> : IComparer<StorageSlotDetail> where T : IStoreable
    {
        public int Compare(StorageSlotDetail x, StorageSlotDetail y)
        {
            if (x.FreeSpace< y.FreeSpace) return -1;
            else if (x.FreeSpace > y.FreeSpace) return 1;
            else return 0;
        }
    }
    /// <summary>
    /// Sort on free space in ascending order.
    /// </summary>
    public class StorageSlotDetail_SortByFreeSpaceDescendingOrder<T> : IComparer<StorageSlotDetail> where T : IStoreable
    {
        public int Compare(StorageSlotDetail x, StorageSlotDetail y)
        {
            if (x.FreeSpace > y.FreeSpace) return -1;
            else if (x.FreeSpace < y.FreeSpace) return 1;
            else return 0;
        }
    }
 
}
