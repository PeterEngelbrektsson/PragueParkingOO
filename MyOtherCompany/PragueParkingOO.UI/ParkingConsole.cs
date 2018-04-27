using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyOtherCompany.PragueParkingOO.Biz;
using MyOtherCompany.PragueParkingOO.Biz.Vehicles;
using MyCompany.Storage.Biz;
using MyOtherCompany.Common;

namespace MyOtherCompany.PragueParkingOO.UI
{
    public static class ParkingConsole
    {
        public const int NumberOfParkinPlaces = 100;
        public const string ParkingPlaceFileName = "ParkinPlace2_0.bin";

        /// <summary>
        /// Writes the main menu 
        /// </summary>
        /// <param name="parkingPlace"></param>
        public static void WriteMenu(Storage<Vehicle> parkingPlace)
        {
            Console.WriteLine();
            Console.WriteLine("  Prague Parking v1.0");
            Console.WriteLine("-----------------------");
            Console.WriteLine("1. Add a vehicle");
            Console.WriteLine("2. Display overview");
            Console.WriteLine("3. Move a vehicle");
            Console.WriteLine("4. Find a vehicle");
            Console.WriteLine("5. Remove a vehicle");
            Console.WriteLine("6. Find free place");
            Console.WriteLine("7. Optimize parking lot");
            Console.WriteLine("8. Display all parked vehicles");
            Console.WriteLine("9. Display statistics");
            Console.WriteLine("10. Save");
            Console.WriteLine("11. Load");
            Console.WriteLine("0. EXIT");
            DisplayIfCanBeOptimized(parkingPlace);
            Console.WriteLine();
            Console.Write("Please input number : ");

        }

        /// <summary>
        /// Display a message if the park can be optimized.
        /// </summary>
        /// <param name="parkingPlace">The parking place</param>
        public static void DisplayIfCanBeOptimized(Storage<Vehicle> parkingPlace)
        {
            /*
            int singleMcs = Parking.NumberOfSingleParkedMcs(parkingPlace);
            if (singleMcs > 1)
            {
                Console.WriteLine();
                Messenger.WriteInformationMessage(String.Format("The parkingspace can be optimized. There are {0} single parked motorcycles.", singleMcs));
            }
            */
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// Displays statistics about the parking place.
        /// </summary>
        /// <param name="parkingPlace"></param>
        public static void DisplayStatistics(Storage<Vehicle> parkingPlace)
        {
            int freeParkingPlacesCar = parkingPlace.FreeSpacesCount(new Car().Size);
            int freeParkingPlacesBike = parkingPlace.FreeSpacesCount(new Bike().Size);
            int freeParkingPlacesMotorBike = parkingPlace.FreeSpacesCount(new MotorBike().Size);
            int freeParkingPlacesTrike = parkingPlace.FreeSpacesCount(new Trike().Size);
            int OccupiedParkingPlaces = parkingPlace.OccupiedCount();
            Console.WriteLine();
            Messenger.WriteInformationMessage(String.Format("The number of free parking places for cars {0}.", freeParkingPlacesCar));
            Messenger.WriteInformationMessage(String.Format("The number of free parking places for motorcycles {0}.", freeParkingPlacesMotorBike));
            Messenger.WriteInformationMessage(String.Format("The number of free parking places for trikes {0}.", freeParkingPlacesTrike));
            Messenger.WriteInformationMessage(String.Format("The number of free parking places for bikes{0}.", freeParkingPlacesBike));
            Messenger.WriteInformationMessage(String.Format("The number of occupied parking places {0}.", OccupiedParkingPlaces));
        }
        /// <summary>
        /// Display the menu bar.
        /// </summary>
        /// <param name="parkingPlace"></param>
        public static void DisplayMenu(Storage<Vehicle> parkingPlace)
        {
            // Console.Clear(); -- Do we want to clear screen between repeat displays of the menu or not ? 
            bool keepLoop = true;
            int choice = 0;

            while (keepLoop) // Perpetual loop
            {
                WriteMenu(parkingPlace);

                String Str = Console.ReadLine(); // Store user choice
                choice = 0;
                //int choice = int.Parse(Console.ReadLine()); // Store user choice                
                if (!int.TryParse(Str, out choice))
                {
                    Messenger.WriteErrorMessage("Invalid Input, Please enter number only");
                }
                else
                {

                    switch (choice) // Check user choice
                    {
                        case 0: // Leave menu permanently.
                            keepLoop = false;
                            break;

                        case 1: // Add a Vehicle
                            ParkVehicle(parkingPlace);
                            break;
                        case 2: // Display overview
                            DisplayOverview(parkingPlace);
                            break;

                        case 3: // Move a vehicle
                            MoveVehicle(parkingPlace);
                            break;

                        case 4: // Find a vehicle
                            FindVehicle(parkingPlace);
                            break;

                        case 5: // Remove a vehicle
                            RemoveVehicle(parkingPlace);
                            break;

                        case 6: // Find free parking spot
                            FindFreeSpot(parkingPlace);
                            break;
                            /*
                        case 7: // Optimize parking spot
                            Optimize(parkingPlace); // Optimize the parking place
                            break;
                            */
                        case 8: // List all vehicles in parking lot
                            DisplayParkedVehicels(parkingPlace);
                            break;
                        case 9: //Display statistics
                            DisplayStatistics(parkingPlace);
                            break;
                            /*
                        case 10: //Save
                            Parking.SaveToFile(parkingPlace, ParkingPlaceFileName);
                            Messenger.WriteInformationMessage("Database saved to file.");
                            break;
                        case 11: //Load
                            parkingPlace = Parking.LoadFromFile(ParkingPlaceFileName);
                            Messenger.WriteInformationMessage("Database loaded from file.");
                            break;
                            */
                        default: // None of the above

                            Console.WriteLine();
                            Messenger.WriteErrorMessage("That number does not exist. Please enter a correct number.");
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Displays a list of all parked vehicles in the parking place.
        /// </summary>
        /// <param name="parkingPlace"></param>
        public static void DisplayParkedVehicels(Storage<Vehicle> parkingPlace)
        {

            var parkedVehicles = parkingPlace.FindAll();
            if (parkedVehicles != null && parkedVehicles.Count > 0)
            {
                foreach (var vehicle in parkedVehicles)
                {
                    Console.WriteLine("{0} {1} ", vehicle.RegistrationNumber, vehicle.TimeStamp); 
                }
            }
            else
            {
                Messenger.WriteInformationMessage("The parkingplace is empty.");
            }
        }

        
        /// <summary>
        /// Displays overview of all parked vehicles in the parking place.
        /// </summary>
        /// <param name="parkingPlace"></param>
        public static void DisplayOverview(Storage<Vehicle> parkingPlace)
        {
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Slot Used Vehicles");
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");

            foreach (StorageSlotDetail report in parkingPlace)
            {
                if (report.FreeSpace == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else if (report.FreeSpace> 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                Console.Write(" {0,3} {1,1}/{2,1}", report.SlotNumber, report.OccupiedSpace, report.Size);
                Console.ForegroundColor = ConsoleColor.White;
                foreach (var itemDetail in report.StorageItemDetails)
                {
                    Console.Write(" {0,10} ", itemDetail.RegistrationNumber);
                }
                Console.WriteLine();
            }
                        
        }

        /// <summary>
        /// Optimizes the parking place. Moves single parked motorcycles together in the same slots.
        /// Displays a list of movements to be performed by the employees.
        /// </summary>
        /// <param name="parkingPlace"></param>

        public static void Optimize(string[] parkingPlace)
        {
            /*
                        string[] messages;
                        messages = Parking.Optimize(parkingPlace);

                        foreach (string message in messages)
                        {
                            Messenger.WriteInformationMessage(message);
                        }
                        if (messages.Length < 1)
                        {
                            Messenger.WriteInformationMessage("The parkingplace is alreadey optimized.");
                        }

                        */
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes a vehicle from the parking place.
        /// </summary>
        /// <param name="parkingPlace"></param>
        /// <param name="registrationNumber"></param>
        public static void Remove(Storage<Vehicle> parkingPlace, string registrationNumber)
        {

            try
            {
                Vehicle v = parkingPlace.Peek(registrationNumber);
                DateTime timeStamp = v.TimeStamp;
                int pos = parkingPlace.Remove(registrationNumber);
                Messenger.WriteInformationMessage(String.Format("The Vehicle with registration number {0} successfully removed from position {1}. It was parked {2}", registrationNumber, pos + 1,timeStamp)); // Display of parking number should be one based

            }
            catch (StoreableNotFoundException)
            {

                Messenger.WriteErrorMessage("The Vehicle with this number " + registrationNumber + " Not found. ");
                Messenger.WriteErrorMessage("The vehicle " + registrationNumber + " you are trying to remove can not be found in the parkingplace");

            }
        }
        /// <summary>
        /// Finding free parking place. 
        /// </summary>
        /// <param name="parkingPlace"></param>
        public static void FindFreeSpot(Storage<Vehicle> parkingPlace)
        {
            
            
            VehicleType vehicleType = PromptForVehicelType();
            if (vehicleType == VehicleType.Unspecified)
            {
                // user aborted registration
                return;
            }
            int position;
            int size=0;
            switch (vehicleType)
            {
                case VehicleType.Bike:
                    size = new Bike().Size;
                    break;
                case VehicleType.MotorBike:
                    size = new MotorBike().Size;
                    break;
                case VehicleType.Trike:
                    size = new Trike().Size;
                    break;
                case VehicleType.Car:
                    size = new Car().Size;
                    break;
            }


            position = parkingPlace.FindFreePlace(size); // Find a free position for car or mc, depending on user choice
            if (position < 0)
            {
                Messenger.WriteErrorMessage("The parking place is full.");
            }
            Messenger.WriteInformationMessage(String.Format("There is a free place for your vehicle at {0}.", position + 1));

        }
        /// <summary>
        /// Finding Vehicle
        /// </summary>
        /// <param name="parkingPlace"></param>
        static void FindVehicle(Storage<Vehicle> parkingPlace)
        {
            Console.WriteLine("Please enter the registration number of the vehicle : ");
            string registrationNumber = Console.ReadLine().ToUpper();

            int position = parkingPlace.FindDistinctSlotNumber(registrationNumber); // Position where vehicle is located (if any)

            if (position != -1)
            {
                // The exact match found
                Messenger.WriteInformationMessage(String.Format("Your vehicle is parked at spot number {0}.", position + 1)); // Parking spots numbered 1 - 100 !
            }
            else
            {
                // No exact match found
                var searchResult = parkingPlace.Find(registrationNumber);
                if (searchResult.Count > 0)
                {
                    foreach (var detail in searchResult)
                    {
                        Console.WriteLine("{0,3} {1,10}", detail.StorageSlotNumber+ 1, detail.RegistrationNumber);
                    }
                }
                else
                {
                    Messenger.WriteErrorMessage("I am sorry to say you vehicle does not exist in our parking lot.");
                    Messenger.WriteErrorMessage("Perhaps someone has taken it for a joyride. Our apologies.");
                }
            }
        }
        /// <summary>
        /// Moving Vehicle one place to another place.
        /// </summary>
        /// <param name="parkingPlace"></param>
        public static void MoveVehicle(Storage<Vehicle> parkingPlace)
        {
            Console.Write("Enter the registration number: ");
            string registrationNumber = Console.ReadLine().ToUpper();
            int oldPosition = parkingPlace.FindDistinctSlotNumber(registrationNumber);
            if (oldPosition < 0)
            {
                Messenger.WriteErrorMessage("The vehicle could not be found.");
                return;
            }
            Vehicle v = parkingPlace.Peek(registrationNumber);
            try
            {
                int newPosition = parkingPlace.FindFreePlace(v.Size);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Suggest parking position for your vehicle will be {0}", newPosition + 1); // zero to one based index
                Console.Write("Do you accept this ? Please choose YES or NO. : ");
                Console.ForegroundColor = ConsoleColor.White;

                string yesOrNo = Console.ReadLine().ToUpper();

                if (yesOrNo == "YES")
                {
                    // Move vehicle to new position
                    try
                    {
                        parkingPlace.Move(registrationNumber.ToUpper(), newPosition);// convert form one based to zerop based index
                        Messenger.WriteInformationMessage("The vehicle has been moved.");
                    }
                    catch (StoreableNotFoundException)
                    {
                        Messenger.WriteInformationMessage("The vehicle could not be found.");
                    }

                }

                else if (yesOrNo == "NO")
                {
                    Console.WriteLine("OK, lets try finding another parking place that is suitable for you");
                    Console.Write("Please choose a parking place and we shall see if it is available : ");
                    int userPosition = int.Parse(Console.ReadLine());
                    try
                    {
                        parkingPlace.Move(registrationNumber.ToUpper(), userPosition - 1);// convert form one based to zerop based index
                        Messenger.WriteInformationMessage("The vehicle has been moved.");
                    }
                    catch (StoreableNotFoundException)
                    {
                        Messenger.WriteErrorMessage("The vehicle could not be found.");
                    }
                    catch (StorageSlotToFullForStoreableException ex)
                    {
                        Messenger.WriteErrorMessage("The selected new position is already full.");
                        Messenger.WriteErrorMessage(ex.Message);
                    }
                    catch (StoreableAlreadyAtThePosition)
                    {
                        Messenger.WriteErrorMessage("The vehicle is already parked at that position.");
                    }
                }
                else
                {
                    Messenger.WriteErrorMessage("You have to make a proper choice.");
                }
            }
            catch (StorageToFullForStoreableException)
            {
                Messenger.WriteErrorMessage("The parking place is to full for the vehicle to be moved.");
            }


        }
        /// <summary>
        /// Prompts the user for a valid registration number or exit code 0
        /// </summary>
        /// <returns></returns>
        public static string PromptForRegistrationNumber()
        {
            bool loop = true;
            string registrationNumber = null;
            string[] errorMessages;
            do
            {
                Console.WriteLine("Please enter the registration number of the vehicle or 0 to bort: ");
                registrationNumber = Console.ReadLine().ToUpper();
                int inputNumber = 0;
                if (int.TryParse(registrationNumber, out inputNumber) && inputNumber == 0)
                {
                    registrationNumber = null;
                    loop = false;
                }
                else if (!VehicleValidator.ValidRegistrationNumber(registrationNumber, out errorMessages))
                {
                    Messenger.WriteErrorMessage(errorMessages);
                }
                else
                {
                    //valid registration number
                    loop = false;
                }
            } while (loop);
            return registrationNumber;
        }
        public static VehicleType PromptForVehicelType()
        {
            bool loop = true;
            VehicleType type= VehicleType.Unspecified;
            do
            {
                Console.WriteLine("Please select type of vehicle or 0 to exit ");
                
                Console.WriteLine("1. Bike");
                Console.WriteLine("2. MotorBike");
                Console.WriteLine("3. Trike");
                Console.WriteLine("4. Car");

                string input = Console.ReadLine().ToUpper();
                int typeNumber;
                if (!int.TryParse(input, out typeNumber))
                {
                    Messenger.WriteErrorMessage("Enter a valid number.");
                }
                else if (typeNumber == 0)
                {
                    loop = false;
                    type = VehicleType.Unspecified;
                }
                else if (typeNumber < 0 || typeNumber > 4)
                {
                    Messenger.WriteErrorMessage("Enter a number from the list.");
                }
                else
                {
                    switch (typeNumber)
                    {
                        case 1:
                            type = VehicleType.Bike;
                            break;
                        case 2:
                            type = VehicleType.MotorBike;
                            break;
                        case 3:
                            type = VehicleType.Trike;
                            break;
                        case 4:
                            type = VehicleType.Car;
                            break;
                    }
                    loop = false;
                }

            } while (loop);

            return type;
        }
        /// <summary>
        /// Prompts the user for a motorbike mark
        /// </summary>
        /// <returns></returns>
        public static string PromptForMark()
        {
            bool loop = true;
            string mark = null;
            do
            {
                Console.WriteLine("Please enter the mark of the motorbike: ");
                mark = Console.ReadLine().ToUpper();
                if (string.IsNullOrEmpty(mark))
                {
                    // Contiune looping
                }
                else
                {
                    // a string has been entered
                    loop = false;
                }
            } while (loop);
            return mark;
        }
        /// <summary>
        /// Park Vehicle
        /// </summary>
        /// <param name="parkingPlace"></param>
        /// <param name="registrationNumber"></param>
        /// <param name="vehicleType"></param>
        public static void ParkVehicle(Storage<Vehicle> parkingPlace)
        {
            VehicleType vehicleType = PromptForVehicelType();
            if (vehicleType == VehicleType.Unspecified)
            {
                // User has aborted registration
                return;
            }
            string registrationNumber = PromptForRegistrationNumber();
            if (registrationNumber == null)
            {
                //The user has aborted registration
                return;
            }
            Vehicle newVehicle = null;
            switch (vehicleType)
            {
                case VehicleType.Bike:
                    Bike newBike = new Bike();
                    newBike.RegistrationNumber = registrationNumber;
                    newVehicle = newBike;
                    // Should per specification use the specialized properties of the class Bike
                    // Ask the user for input and set properties
                    throw new NotImplementedException();
                    break;
                case VehicleType.MotorBike:
                    MotorBike newMotorBike = new MotorBike();
                    newMotorBike.RegistrationNumber = registrationNumber;
                    newVehicle = newMotorBike;
                    // Should per specification use the specialized properties of the class MotorBike
                    // Ask the user for input and set properties
                    string mark=PromptForMark();
                    newMotorBike.Mark = mark;
                    break;

                case VehicleType.Trike:
                    MotorBike newTrike = new MotorBike();
                    newTrike.RegistrationNumber = registrationNumber;
                    newVehicle = newTrike;
                    // Should per specification use the specialized properties of the class MotorBike
                    // Ask the user for input and set properties
                    throw new NotImplementedException();
                    break;
                    // more classes of vehicles
                    throw new NotImplementedException();
            }
            try
            {
                int position = parkingPlace.Add(newVehicle); // Park at suitable position (if any)
                Messenger.WriteInformationMessage(String.Format("Your vehicle has been parked at place number {0}.", position + 1));
            }
            catch (RegistrationNumberAlreadyExistsException)
            {
                Messenger.WriteErrorMessage("Registration number already exist. Cannot have two vehicles with same.");
            }
            catch (StorageToFullForStoreableException)
            {
                Messenger.WriteErrorMessage("The parking place has no room for the vehicel.");
            }
        }
     
        /// <summary>
        /// Revome Vehicle
        /// </summary>
        /// <param name="parkingPlace"></param>
        static void RemoveVehicle(Storage<Vehicle> parkingPlace)
        {
            string registrationNumber = PromptForRegistrationNumber();
            if (registrationNumber != null)
            {
                Remove(parkingPlace, registrationNumber); // Remove the vehicle with the specificed registration number (if it exists in the parking lot)
            }
        }
    
  
    }
}
