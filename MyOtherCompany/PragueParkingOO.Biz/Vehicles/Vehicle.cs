using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCompany.Storage.Biz;
using MyOtherCompany.Common;
namespace MyOtherCompany.PragueParkingOO.Biz.Vehicles
{
    /// <summary>
    /// A Vehcle that can be stored.
    /// </summary>
    [Serializable]
    public abstract class Vehicle : IStoreable
    {
        public int Size { get; set; }           // Size of vehicle
        private string _registraionNumber;
        private DateTime _timestamp;
        public string RegistrationNumber {
            get
            {
                return _registraionNumber;
            }
             set
            {
                if (VehicleValidator.ValidRegistrationNumber(value, out string[] errorMessages))
                {
                    _registraionNumber = value;
                }
                else
                {
                    string errorMessage = string.Join("\n", errorMessages);
                    throw new RegistrationNumberInvalid(errorMessage);
                }
            }
        }
        public DateTime TimeStamp              // Timestamp set at time the vehicle check in to the parking place
        {
            get
            {
                return _timestamp;
            }
            set
            {
                this._timestamp = DateTime.Now; 
            }
        }     
        public string TypeName { get; set; }

        public Vehicle()
        {
            
        }
        public Vehicle(string registrationNumber)
        {
            RegistrationNumber = registrationNumber;
        }

        public abstract object Clone();
    }
    
}
