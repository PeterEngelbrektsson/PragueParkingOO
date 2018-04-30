using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCompany.Storage.Biz;

namespace MyOtherCompany.PragueParkingOO.Biz.Vehicles
{
    /// <summary>
    /// A Trike that can be stored.
    /// </summary>
    [Serializable]
    public class Trike:Vehicle
    {
        public Trike()
        {
            Size = 3;
            TypeName = "Trike";
        }
        public override object Clone()
        {
            Trike newTrike = new Trike
            {
                Size = this.Size,
                RegistrationNumber = this.RegistrationNumber,
                TypeName = this.TypeName,
                TimeStamp = this.TimeStamp
            };
            return newTrike;
        }
    }
}
