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
        public static void PopulateTestData(ParkingPlace parkingPlace)
        {

            // Testdata
            parkingPlace.Add(new Car("ABC123"));
            parkingPlace.Add(new Car("ABC210"));
            parkingPlace.Add(new Car("ABC321"));
            parkingPlace.Add(new Car("ABC432"));
            parkingPlace.Add(new Car("ABC543"));


        }
        static void Main(string[] args)
        {
            //Main file

            //with elements of parking 
            ParkingPlace parkingPlace = new ParkingPlace(100);

            // Setup demo with testdata.  FIXME remove this in production code.
            PopulateTestData(parkingPlace);

            ParkingConsole.DisplayMenu(parkingPlace);
            Console.ReadLine();
        }
    }
}
