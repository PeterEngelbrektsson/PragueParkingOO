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
        private string _mark;
        public string Mark {
        get {
                return _mark;
            }
            set
            {
                _mark = value;
            }
        }

        public MotorBike():base()
        {
            Size = 2;
            TypeName="MotorBike";
            this.Mark = "BMW";
        }
        public MotorBike(string registrationNumber, string mark):base()
        {
            Size = 2;
            TypeName = "MotorBike";
            RegistrationNumber = registrationNumber;
            Mark = mark;
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
        public override string ToString()
        {
            return string.Format("Motorbike with registration number {0} of mark {1}", RegistrationNumber, Mark);
        }
    }
}
