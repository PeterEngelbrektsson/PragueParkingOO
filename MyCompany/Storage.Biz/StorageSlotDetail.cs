﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.Storage.Biz
{
    /// <summary>
    /// Details about a storage slot
    /// </summary>
    [Serializable]
    public struct StorageSlotDetail
    {
        public int SlotNumber;
        public int FreeSpace;
        public int OccupiedSpace;
        public int Size;
        public List<StorageItemDetail> StorageItemDetails;
    }
}
