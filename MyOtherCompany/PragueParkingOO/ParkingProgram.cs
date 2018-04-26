using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyOtherCompany.PragueParkingOO.UI;
using MyOtherCompany.PragueParkingOO.Biz;
using MyOtherCompany.PragueParkingOO.Biz.Vehicles;
using MyCompany.Storage.Biz;

namespace MyOtherCompany.PragueParkingOO
{
    public class ParkingProgram
    {
        public const int NumberOfParkinPlaces = 100;
        /// <summary>
        /// Adding some test data
        /// </summary>
        /// <returns></returns>
        public static void PopulateTestData(Storage<Vehicle> parkingPlace)
        {
            
            // Testdata
            parkingPlace.Add("ABC123",typeof(Car));
            parkingPlace.Add("CAR432", typeof(Car));
            parkingPlace.Add("CUSTOMNAME", typeof(Car));
            parkingPlace.Add("MYNAME", typeof(Car));
            parkingPlace.Add("MC3", typeof(MotorBike));
            parkingPlace[22] = ":OIU988";
            parkingPlace[24] = ":MC1";
            parkingPlace[45] = "MC4:MC2";
            parkingPlace[54] = ":MC5";
            parkingPlace[55] = ":MC6";
            parkingPlace[85] = "CAR987" + "," + DateTime.Now;
            parkingPlace[86] = "CAR123" + "," + DateTime.Now;
            parkingPlace[88] = ":MC7";
            parkingPlace[99] = ":MC8";

        }
        static void Main(string[] args)
        {
            //Main file

            // String array with elements of parking 
            Storage<Vehicle> parkingPlace = new Storage<Vehicle>(100);

            // Setup demo with testdata.  FIXME remove this in production code.
            PopulateTestData(parkingPlace);

            ParkingConsole.DisplayMenu(parkingPlace);
            Console.ReadLine();
        }
    }
}
