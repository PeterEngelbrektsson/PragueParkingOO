using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCompany.Storage.Biz;

namespace MyOtherCompany.PragueParkingOO.Biz.Vehicles
{
    /// <summary>
    /// A bike that can be stored
    /// </summary>
    [Serializable]
    public class Bike:Vehicle
    {
        public Bike()
        {
            this.Size = 1;
        }
        public Bike(string registrationNumber)
        {
            this.Size = 1;
            this.RegistrationNumber = registrationNumber;
            this.TypeName = "Bike";
        }
        public override string ToString()
        {
            return string.Format("Bike {0}", RegistrationNumber);
        }
        public override object Clone()
        {
            Bike newBike = new Bike
            {
                Size = this.Size,
                RegistrationNumber = this.RegistrationNumber,
                TypeName = this.TypeName,
                TimeStamp = this.TimeStamp
            };
            return newBike;
        }
    }
}
