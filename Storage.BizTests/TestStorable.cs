using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCompany.Storage.Biz;

namespace MyCompany.Storage.BizTests
{
    public class TestStorable : IStoreable
    {
        public string Description
        {
            get
            {
                return RegistrationNumber;
            }
        }
        public string RegistrationNumber { get; set; }
        public DateTime TimeStamp { get; set; }
        public int Size { get; set; }
        public string TypeName { get; set; }

    }
}
