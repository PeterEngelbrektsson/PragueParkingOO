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
        private string _colour;
        public string Colour {
        get {
                return _colour;
            }
            set
            {
                _colour = value;
            }
        }

        public Car():base()
        {
            this.Size = 4;
            this.TypeName = "Car";
            this.Colour= "Black"; // All cars are by default black.
        }
        public Car(string registrationNumber):base(registrationNumber,4,"Car")  
        {
            this.Colour = "Black"; // All cars are by default black.
        }
        public Car(string registrationNumber,string colour) : base(registrationNumber,4,"Car")  
        {
            this.Colour = colour;
        }
        public override string ToString()
        {
            return string.Format("Car with registration number {0} of colour {1}", RegistrationNumber, Colour);
        }
        public override object Clone()
        {
            Car newCar = new Car
            {
                Size = this.Size,
                RegistrationNumber = this.RegistrationNumber,
                TypeName = this.TypeName,
                Colour = this.Colour,
                TimeStamp = this.TimeStamp
            };
            return newCar;
        }
    }
}
