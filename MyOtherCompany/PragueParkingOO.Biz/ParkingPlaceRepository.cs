using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCompany.Storage.Biz;
using MyOtherCompany.PragueParkingOO.Biz.Vehicles;

namespace MyOtherCompany.PragueParkingOO.Biz
{
    public class ParkingPlaceRepository
    {
        /// <summary>
        /// Saves the parking placce to file
        /// </summary>
        /// <param name="storage"></param>
        /// <param name="fileName"></param>
        public static void SaveToFile(ParkingPlace place, string fileName)
        {

            StorageRepository<Vehicle>.SaveToFile(place.Storage, fileName);
        }
        /// <summary>
        /// Loads the parkin place from file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static ParkingPlace LoadFromFile(string fileName)
        {
            Storage<Vehicle> storage = (Storage<Vehicle>)StorageRepository<Vehicle>.LoadFromFile(fileName);
            ParkingPlace newPlace = new ParkingPlace(storage.Length);
            newPlace.Storage = storage;

            return newPlace;
        }
    }
}
