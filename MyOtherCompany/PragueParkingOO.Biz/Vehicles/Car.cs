using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCompany.Storage.Biz;

namespace MyOtherCompany.PragueParkingOO.Biz.Vehicles
{
    /// <summary>
    /// A Car that can be stored.
    /// </summary>
    [Serializable]
    public class Car:Vehicle
    {
        public string Colour;

        public Car():base()
        {
            this.Size = 4;
        }
        public Car(string registrationNumber):base()
        {
            this.Size= 4;
            this.RegistrationNumber = registrationNumber;
            this.TypeName = "Car";
        }
        public override string ToString()
        {
            return string.Format("Car {0}",RegistrationNumber);
        }
        public override object Clone()
        {
            Car newCar = new Car();
            newCar.Size = this.Size;
            newCar.RegistrationNumber = this.RegistrationNumber;
            newCar.TypeName = this.TypeName;
            newCar.Colour = this.Colour;
            newCar.TimeStamp = this.TimeStamp;
            return newCar;
        }
    }
}
