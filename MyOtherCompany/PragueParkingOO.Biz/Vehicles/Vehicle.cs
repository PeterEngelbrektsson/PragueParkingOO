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
    public class Vehicle : IStoreable
    {
        public int Size { get;}           // Size of vehicle
        private string _registraionNumber;
        public string RegistrationNumber {
            get
            {
                return _registraionNumber;
            }
             set
            {
                string[] errorMessages;
                if (VehicleValidator.ValidRegistrationNumber(value,out errorMessages))
                {
                    _registraionNumber = value;
                }
                else
                {
                    string errorMessage= string.Join("\n",errorMessages);
                    throw new RegistrationNumberInvalid(errorMessage);
                }
            }
        }
        public DateTime TimeStamp { get; set; }          // Timestamp set at time the vehicle check in to the parking place
        public string Description { get; }
        public string TypeName { get; set; }

        public Vehicle()
        {
            
        }
        public Vehicle(string registrationNumber)
        {
            RegistrationNumber = registrationNumber;
        }
    }
    
}
