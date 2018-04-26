using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.Storage.Biz
{
    public struct StorageItemDetail
    {

        public string RegistrationNumber;
        public int Size;
        public DateTime TimeStamp;
        public string Description;
        public IStoreable Storeable;
    }
}
