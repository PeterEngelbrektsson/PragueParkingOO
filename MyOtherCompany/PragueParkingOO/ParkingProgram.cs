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
            parkingPlace.Add(new Bike("BIKE1"));
            parkingPlace.Add(new Bike("BIKE2"));
            parkingPlace.Add(new Bike("BIKE3"));
            parkingPlace.Add(new Bike("BIKE4"));
            parkingPlace.Add(new Bike("BIKE5"));
            parkingPlace.Add(new Bike("BIKE6"));
            parkingPlace.Add(new Bike("BIKE7"));
            parkingPlace.Add(new Bike("BIKE8"));
            parkingPlace.Add(new Bike("BIKE9"));
            parkingPlace.Add(new Bike("BIKE10"));
            parkingPlace.Add(new Bike("BIKE11"));
            parkingPlace.Add(new Bike("BIKE12"));
            parkingPlace.Add(new Bike("BIKE13"));
            parkingPlace.Add(new Bike("BIKE14"));
            parkingPlace.Add(new Bike("BIKE15"));
            parkingPlace.Add(new Bike("BIKE16"));
            parkingPlace.Add(new Bike("BIKE17"));
            parkingPlace.Add(new Bike("BIKE18"));
            parkingPlace.Add(new Bike("BIKE19"));

        }
        static void Main(string[] args)
        {
            //Main file

            //Create the parking place with elements of parking 
            //ParkingPlace parkingPlace = new ParkingPlace(100,4); // 100 slots with a size of 4
            ParkingPlace parkingPlace = new ParkingPlace(100); // 100 slots with different sizes from 1 to 6

            // Setup demo with testdata.  FIXME remove this in production code.
            PopulateTestData(parkingPlace);

            ParkingConsole.DisplayMenu(parkingPlace);
            Console.ReadLine();
        }
    }
}
