using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.Storage.Biz
{
    /// <summary>
    /// Details about an item in storage.
    /// </summary>
    [Serializable]
    public struct StorageItemDetail
    {

        public string RegistrationNumber;
        public int Size;
        public DateTime TimeStamp;
        public int StorageSlotNumber;
        public string TypeName;
    }
}
