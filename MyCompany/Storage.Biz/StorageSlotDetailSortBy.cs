﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.Storage.Biz
{
    /// <summary>
    /// Sort on storage slot number in ascending order.
    /// </summary>
    //public class StorageSlotDetail_SortByStorageSlotNumberAscendingOrder : Comparer<StorageSlotDetail>    // Old name to long and with underscore
    public class StorageSlotDetailSortByStorageSlotNrAsc : Comparer<StorageSlotDetail> 
    {
        public override int Compare(StorageSlotDetail x, StorageSlotDetail y)
        {
            if (x.SlotNumber < y.SlotNumber) return -1;
            else if (x.SlotNumber > y.SlotNumber) return 1;
            else return 0;
        }
    }
    /// <summary>
    /// Sort on storage slot number in descending order.
    /// </summary>
    public class StorageSlotDetailSortByStorageSlotNrDesc: Comparer<StorageSlotDetail>
    {
        public override int Compare(StorageSlotDetail x, StorageSlotDetail y)
        {
            if (x.SlotNumber > y.SlotNumber) return -1;
            else if (x.SlotNumber < y.SlotNumber) return 1;
            else return 0;
        }
    }
    /// <summary>
    /// Sort on occupied space in ascending order.
    /// </summary>
    public class StorageSlotDetailSortByOccupiedSpaceAsc : Comparer<StorageSlotDetail> 
    {
        public override int Compare(StorageSlotDetail x, StorageSlotDetail y)
        {
            if (x.OccupiedSpace < y.OccupiedSpace) return -1;
            else if (x.OccupiedSpace > y.OccupiedSpace) return 1;
            else return x.SlotNumber.CompareTo(y.SlotNumber);
        }
    }
    /// <summary>
    /// Sort on occupied space in ascending order.
    /// </summary>
    public class StorageSlotDetailSortByOccupiedSpaceDesc : Comparer<StorageSlotDetail> 
    {
        public override int Compare(StorageSlotDetail x, StorageSlotDetail y)
        {
            if (x.OccupiedSpace > y.OccupiedSpace) return -1;
            else if (x.OccupiedSpace < y.OccupiedSpace) return 1;
            else return x.SlotNumber.CompareTo(y.SlotNumber);
        }
    }

    /// <summary>
    /// Sort on free space in ascending order.
    /// </summary>
    public class StorageSlotDetailSortByFreeSpaceAsc : Comparer<StorageSlotDetail> 
    {
        public override int Compare(StorageSlotDetail x, StorageSlotDetail y)
        {
            if (x.FreeSpace> y.FreeSpace) return -1;
            else if (x.FreeSpace < y.FreeSpace) return 1;
            else return x.SlotNumber.CompareTo(y.SlotNumber);
        }
    }
    /// <summary>
    /// Sort on free space in ascending order.
    /// </summary>
    public class StorageSlotDetailSortByFreeSpaceDesc : Comparer<StorageSlotDetail> 
    {
        public override int Compare(StorageSlotDetail x, StorageSlotDetail y)
        {
            if (x.FreeSpace < y.FreeSpace) return -1;
            else if (x.FreeSpace > y.FreeSpace) return 1;
            else return x.SlotNumber.CompareTo(y.SlotNumber);
        }
    }
 
}
