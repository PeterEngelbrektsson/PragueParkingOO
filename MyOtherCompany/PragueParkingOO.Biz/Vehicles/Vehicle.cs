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
        private int _size;          // Size of vehicle
        private string _typeName;   // User friendly name of vehicle type
        private string _registrationNumber;
        private DateTime _timestamp;

        public int Size
        {
            get { return _size; }
            set { _size = value; }
        }        
        public string RegistrationNumber {
            get
            {
                return _registrationNumber;
            }
             set
            {
                if (VehicleValidator.ValidRegistrationNumber(value, out string[] errorMessages))
                {
                    _registrationNumber = value;
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
        
        public string TypeName
        {
            get { return _typeName;  }
            set { _typeName = value; }
        }

        public Vehicle()
        {
            
        }
        public Vehicle(string registrationNumber)
        {
            RegistrationNumber = registrationNumber;
        }
        public Vehicle(string registrationNumber,int size)
        {
            RegistrationNumber = registrationNumber;
            Size = size;
        }
        public Vehicle(string registrationNumber, int size, string typeName)
        {
            RegistrationNumber = registrationNumber;
            Size = size;
            TypeName = typeName;
        }
        public abstract object Clone();
    }
    
}
