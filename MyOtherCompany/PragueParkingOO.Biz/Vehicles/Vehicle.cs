using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCompany.Storage.Biz;

namespace MyOtherCompany.PragueParkingOO.Biz.Vehicles
{
    /// <summary>
    /// A Vehcle that can be stored.
    /// </summary>
    [Serializable]
    public class Vehicle : IStoreable
    {
        public int Size { get;}           // Size of vehicle
        public string RegistrationNumber { get; set; }
        public DateTime TimeStamp { get; set; }          // Timestamp set at time the vehicle check in to the parking place
        public string Description { get; }
        public string TypeName { get; set; }

        public Vehicle()
        {
            throw new NotImplementedException();
        }
    }
    
}
