using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.Storage.Biz
{
    public struct StorageSlotDetail
    {
        public int SlotNumber;
        public int FreeSpace;
        public int OccupiedSpace;
        public int Size;
        public List<StorageSlotDetail> StorageSlotDetails;
    }
}
