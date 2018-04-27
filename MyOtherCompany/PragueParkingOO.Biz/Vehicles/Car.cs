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

        public Car()
        {
            throw new NotImplementedException();
        }
        public Car(string registrationNumber)
        {
            this.RegistrationNumber = registrationNumber;
        }
    }
}
