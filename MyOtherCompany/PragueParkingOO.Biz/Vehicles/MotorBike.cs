using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCompany.Storage.Biz;

namespace MyOtherCompany.PragueParkingOO.Biz.Vehicles
{
    /// <summary>
    /// A Motorbike that can be stored.
    /// </summary>
    [Serializable]
    public class MotorBike:Vehicle
    {
        public string Mark;

        public MotorBike()
        {
            Size = 2;
        }
        public override object Clone()
        {
            MotorBike newMotorBike = new MotorBike
            {
                Size = this.Size,
                RegistrationNumber = this.RegistrationNumber,
                TypeName = this.TypeName,
                TimeStamp = this.TimeStamp
            };
            return newMotorBike;
        }
    }
}
