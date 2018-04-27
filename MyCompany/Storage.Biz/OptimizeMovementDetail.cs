using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.Storage.Biz
{
    /// <summary>
    /// Details about an movement instruction to optimize the storage
    /// </summary>
    [Serializable]
    public struct OptimizeMovementDetail
    {

        public string RegistrationNumber;
        public DateTime TimeStamp;
        public string Description;
        public string TypeName;
        public int OldStorageSlotNumber;
        public int NewStorageSlotNumber;
        
    }
}
