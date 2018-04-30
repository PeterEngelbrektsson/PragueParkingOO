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
            parkingPlace.Add(new Car("ABC123","BROWN"));
            parkingPlace.Add(new Car("ABC210","YELLOW"));
            parkingPlace.Add(new Car("ABC321","GREEN"));
            parkingPlace.Add(new Car("ABC432","BLUE"));
            parkingPlace.Add(new Car("ABC543","WHITE"));
            parkingPlace.Add(new Bike("BIKE1","BMW"));
            parkingPlace.Add(new Bike("BIKE2","TOYOTA"));
            parkingPlace.Add(new Bike("BIKE3","NISSAN"));
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
            parkingPlace.Add(new Trike("TRIKE1","FORD"));
            parkingPlace.Add(new Trike("TRIKE2", "SAAB"));
            parkingPlace.Add(new Trike("TRIKE3", "VOLVO"));
            parkingPlace.Add(new Trike("TRIKE4", "BMW"));
            parkingPlace.Add(new Trike("TRIKE5", "TOYOTA"));
            parkingPlace.Add(new Trike("TRIKE6", "NISSAN"));
            parkingPlace.Add(new MotorBike("MB1", "MINI"));
            parkingPlace.Add(new MotorBike("MB2", "MAXI"));
            parkingPlace.Add(new MotorBike("MB3", "MEDIUM"));
            parkingPlace.Add(new MotorBike("MB4", "SMALL"));



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
