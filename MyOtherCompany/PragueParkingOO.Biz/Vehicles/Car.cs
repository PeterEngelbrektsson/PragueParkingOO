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
        public Car(string registrationNumber):base()  // Used for popluate the data But we can use it anywhere.
        {
            this.Size= 4;
            this.RegistrationNumber = registrationNumber;
            this.TypeName = "Car";
            this.Colour = "Black"; // All cars are by default black.
        }
        public Car(string registrationNumber,string colour) : base()  // Used for popluate the data But we can use it anywhere.
        {
            this.Size = 4;
            this.RegistrationNumber = registrationNumber;
            this.TypeName = "Car";
            this.Colour = colour;
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
        public override string Description
        {
            get
            {
                return string.Format("Car with registration number {0} of colour {1}", RegistrationNumber, Colour);
            }
        }
    }
}
