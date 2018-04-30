using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyOtherCompany.PragueParkingOO.Biz;
using MyOtherCompany.PragueParkingOO.Biz.Vehicles;
using MyCompany.Storage.Biz;
using MyOtherCompany.Common;
using MyCompany.PragueParkingOO.Biz;

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
        public static void WriteMenu(ParkingPlace parkingPlace)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine("  Prague Parking v1.0");
            Console.WriteLine("-----------------------");
            Console.WriteLine("1. Add a vehicle");
            Console.WriteLine("2. Move a vehicle");
            Console.WriteLine("3. Find a vehicle");
            Console.WriteLine("4. Remove a vehicle");
            Console.WriteLine("5. Find free place");
            Console.WriteLine("6. Optimize parking lot");
            Console.WriteLine("7. Display short parking slots overview");
            Console.WriteLine("8. Display overview");
            Console.WriteLine("9. Display all parked vehicles");
            Console.WriteLine("10. Display statistics");
            Console.WriteLine("11. Display Occupied slots");
            Console.WriteLine("12. Save");
            Console.WriteLine("13. Load");
            Console.WriteLine("0. EXIT");
            DisplayIfCanBeOptimized(parkingPlace);
            Console.WriteLine();
            Console.Write("Please input number : ");

        }

        /// <summary>
        /// Display a message if the park can be optimized.
        /// </summary>
        /// <param name="parkingPlace">The parking place</param>
        public static void DisplayIfCanBeOptimized(ParkingPlace parkingPlace)
        {
            ParkingPlaceOptimizer optimizer = new ParkingPlaceOptimizer();
            List<OptimizeMovementDetail> OptimizationInstructions = optimizer.GetOptimzeInstructions(parkingPlace);
            if (OptimizationInstructions.Count() > 0)
            {
                Console.WriteLine();
                Messenger.WriteInformationMessage(String.Format("The parkingspace can be optimized. There are {0} vehicles that can be moved.", OptimizationInstructions.Count()));
            }
        }
        
        /// <summary>
        /// Displays statistics about the parking place.
        /// </summary>
        /// <param name="parkingPlace"></param>
        public static void DisplayStatistics(ParkingPlace parkingPlace)
        {
            int freeParkingPlacesCar = parkingPlace.FreeSpacesCount(new Car().Size);
            int freeParkingPlacesBike = parkingPlace.FreeSpacesCount(new Bike().Size);
            int freeParkingPlacesMotorBike = parkingPlace.FreeSpacesCount(new MotorBike().Size);
            int freeParkingPlacesTrike = parkingPlace.FreeSpacesCount(new Trike().Size);
            int OccupiedParkingPlaces = parkingPlace.OccupiedCount();
            int PartiallyOccupiedParkingPlaces = parkingPlace.PartiallyOccupiedCount();
            Console.WriteLine();
            string report="";
            report += String.Format("The number of free parking places for cars {0}. \n", freeParkingPlacesCar);
            report += String.Format("The number of free parking places for motorcycles {0}. \n", freeParkingPlacesMotorBike);
            report += String.Format("The number of free parking places for trikes {0}.\n", freeParkingPlacesTrike);
            report += String.Format("The number of free parking places for bikes {0}.\n", freeParkingPlacesBike);
            report += String.Format("The number of occupied parking places {0}.\n", OccupiedParkingPlaces);
            report += String.Format("The number of partially occupied parking places {0}.", PartiallyOccupiedParkingPlaces);
            Messenger.WriteInformationMessage(report);
        }
        /// <summary>
        /// Display the menu bar.
        /// </summary>
        /// <param name="parkingPlace"></param>
        public static void DisplayMenu(ParkingPlace parkingPlace)
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

                        case 2: // Move a vehicle
                            MoveVehicle(parkingPlace);
                            break;

                        case 3: // Find a vehicle
                            FindVehicle(parkingPlace);
                            break;

                        case 4: // Remove a vehicle
                            RemoveVehicle(parkingPlace);
                            break;

                        case 5: // Find free parking spot
                            FindFreeSpot(parkingPlace);
                            break;

                        case 6: // Optimize parking spot
                            Optimize(parkingPlace); // Optimize the parking place
                            break;

                        case 7: //Display short overview
                            DisplayParkingSlotsOverview(parkingPlace);
                            break;

                        case 8: // Display overview
                            DisplayOverview(parkingPlace);
                            break;

                        case 9: // List all vehicles in parking lot
                            DisplayParkedVehicels(parkingPlace);
                            break;

                        case 10: //Display statistics
                            DisplayStatistics(parkingPlace);
                            break;

                        case 11: //Display occupied places
                            DisplayOccupiedPlaces(parkingPlace);
                            break;

                        case 12: //Save
                            ParkingPlaceRepository.SaveToFile(parkingPlace, ParkingPlaceFileName);
                            Messenger.WriteInformationMessage("Database saved to file.");
                            break;
                        case 13: //Load
                            parkingPlace = ParkingPlaceRepository.LoadFromFile(ParkingPlaceFileName);
                            Messenger.WriteInformationMessage("Database loaded from file.");
                            break;

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
        public static void DisplayParkedVehicels(ParkingPlace parkingPlace)
        {
            var parkedVehicles = parkingPlace.FindAll();

            if (parkedVehicles == null || parkedVehicles.Count() < 1)
            {
                Messenger.WriteInformationMessage("The parkingplace is empty.");
                return;
            }

            bool loop = true;
            int choice = 1;
            do
            {
                switch (choice)
                {
                    case 1:
                        parkedVehicles.Sort(new StorageItemDetail_SortByStorageSlotAscendingOrder());
                        break;
                    case 2:
                        parkedVehicles.Sort(new StorageItemDetail_SortByStorageSlotDescendingOrder());
                        break;
                    case 3:
                        parkedVehicles.Sort(new StorageItemDetail_SortByRegistrationNumberAscendingOrder());
                        break;
                    case 4:
                        parkedVehicles.Sort(new StorageItemDetail_SortByRegistrationNumberDescendingOrder());
                        break;
                    case 5:
                        parkedVehicles.Sort(new StorageItemDetail_SortByTypeNameAscendingOrder());
                        break;
                    case 6:
                        parkedVehicles.Sort(new StorageItemDetail_SortByTypeNameDescendingOrder());
                        break;
                    case 7:
                        parkedVehicles.Sort(new StorageItemDetail_SortByTimeStampAscendingOrder());
                        break;
                    case 8:
                        parkedVehicles.Sort(new StorageItemDetail_SortByTimeStampDescendingOrder());
                        break;
                    case 9:
                        parkedVehicles.Sort(new StorageItemDetail_SortBySizeAscendingOrder());
                        break;
                    case 10:
                        parkedVehicles.Sort(new StorageItemDetail_SortBySizeDescendingOrder());
                        break;
                    default:
                        loop = false;
                        break;
                }
                Console.WriteLine("-----------------------------------------------------------------------------------");
                Console.WriteLine("Slot Regnr       Type         Checked in          Size");
                Console.WriteLine("-----------------------------------------------------------------------------------");
                foreach (StorageItemDetail vehicleDetail in parkedVehicles)
                {
                    Console.WriteLine("{0,3} {1,10} {2,12} {3,16} {4,2}", vehicleDetail.StorageSlotNumber + 1, vehicleDetail.RegistrationNumber, vehicleDetail.TypeName,vehicleDetail.TimeStamp, vehicleDetail.Size); // Display should be 1 based
                }
                Console.WriteLine();
                Console.WriteLine("1. Sort the list in ascending order on parking slot numner.");
                Console.WriteLine("2. Sort the list in descending order on parking slot  numner.");
                Console.WriteLine("3. Sort the list in ascending order on registration numner.");
                Console.WriteLine("4. Sort the list in descending order on registration numner.");
                Console.WriteLine("5. Sort the list in ascending order on vehicle type numner.");
                Console.WriteLine("6. Sort the list in descending order on vehicle type numner.");
                Console.WriteLine("7. Sort the list in ascending order on timestamp.");
                Console.WriteLine("8. Sort the list in descending order on timestamp.");
                Console.WriteLine("9. Sort the list in ascending order on vehicle size.");
                Console.WriteLine("10. Sort the list in descending order on vehicle size.");
                Console.WriteLine("Enter. Return to main menu.");
                string select = Console.ReadLine();
                if (!int.TryParse(select, out choice))
                {
                    loop = false;
                    return;
                }
                if (choice < 1 | choice > 10)
                {
                    // User has aborted
                    loop = false;
                }
            } while (loop);

        }
        /// <summary>
        /// Writes a report of all storageSlotReports to the console.
        /// </summary>
        /// <param name="storageSlotReports">Storage slot reports to write</param>
        public static void WriteParkingSlotOverview(List<StorageSlotDetail> storageSlotReports)
        {
            bool loop = true;
            int choice = 1;
            do
            {
    
                Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine("Slot Used Vehicles");
                Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
                int column = 0;
                foreach (StorageSlotDetail report in storageSlotReports)
                {
                    Console.Write(" {0,3} ", report.SlotNumber + 1);
                    if (report.FreeSpace == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if (report.OccupiedSpace > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    Console.Write(" {0,1}/{1,1}", report.OccupiedSpace, report.Size); // slotnumber convertion from 0 to 1 based
                    Console.ForegroundColor = ConsoleColor.White;
                    if (column >= 9)
                    {
                        Console.WriteLine();
                        column = 0;
                    }
                    else
                    {
                        column++;
                    }
                    
                }
                Console.WriteLine();
                Console.WriteLine("Enter. Return to main menu.");
                Console.WriteLine();
                string select = Console.ReadLine();
                if (!int.TryParse(select, out choice))
                {
                    loop = false;
                    return;
                }
            } while (loop);
        }
        /// <summary>
        /// Writes a report of all passed storageSlotReports to the console.
        /// </summary>
        /// <param name="storageSlotReports">Storage slot reports to write</param>
        public static void WriteParkingSlotContent(List<StorageSlotDetail> storageSlotReports)
        {
            bool loop = true;
            int choice = 1;
            do
            {
                switch (choice)
                {
                    case 1:
                        storageSlotReports.Sort(new StorageSlotDetail_SortByStorageSlotNumberAscendingOrder());
                        break;
                    case 2:
                        storageSlotReports.Sort(new StorageSlotDetail_SortByStorageSlotNumberDescendingOrder());
                        break;
                    case 3:
                        storageSlotReports.Sort(new StorageSlotDetail_SortByFreeSpaceAscendingOrder());
                        break;
                    case 4:
                        storageSlotReports.Sort(new StorageSlotDetail_SortByFreeSpaceDescendingOrder());
                        break;
                    case 5:
                        storageSlotReports.Sort(new StorageSlotDetail_SortByOccupiedSpaceAscendingOrder());
                        break;
                    case 6:
                        storageSlotReports.Sort(new StorageSlotDetail_SortByOccupiedSpaceDescendingOrder());
                        break;
                    default:
                        loop = false;
                        break;
                }
                Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine("Slot Used Vehicles");
                Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");

                foreach (StorageSlotDetail report in storageSlotReports)
                {
                    if (report.FreeSpace == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if (report.OccupiedSpace > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    Console.Write(" {0,3} {1,1}/{2,1}", report.SlotNumber+1, report.OccupiedSpace, report.Size); // slotnumber convertion from 0 to 1 based
                    Console.ForegroundColor = ConsoleColor.White;
                    foreach (var itemDetail in report.StorageItemDetails)
                    {
                        Console.Write(" [{0,12} {1,10}] ", itemDetail.TypeName, itemDetail.RegistrationNumber);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.WriteLine("1. Sort the list in ascending order on parking slot numner.");
                Console.WriteLine("2. Sort the list in descending order on parking slot  numner.");
                Console.WriteLine("3. Sort the list in ascending order on free space numner.");
                Console.WriteLine("4. Sort the list in descending order on free space numner.");
                Console.WriteLine("5. Sort the list in ascending order on occupied space numner.");
                Console.WriteLine("6. Sort the list in descending order on occupied space numner.");
                Console.WriteLine("Enter. Return to main menu.");
                Console.WriteLine();
                string select = Console.ReadLine();
                if (!int.TryParse(select, out choice))
                {
                    loop = false;
                    return;
                }
                if (choice < 1 | choice > 10)
                {
                    // User has aborted
                    loop = false;
                }
            } while (loop);
        }

        public static void DisplayParkingSlotsOverview(ParkingPlace parkingPlace)
        {
            WriteParkingSlotOverview(parkingPlace.FindAllSlots());
        }
            /// <summary>
            /// Displays overview of all parked vehicles in the parking place.
            /// </summary>
            /// <param name="parkingPlace"></param>
            public static void DisplayOverview(ParkingPlace parkingPlace)
        {

            WriteParkingSlotContent(parkingPlace.FindAllSlots());
       
                        
        }
        /// <summary>
        /// Displays all free places in the parkingplace
        /// </summary>
        /// <param name="parkingPlace"></param>
        public static void DisplayOccupiedPlaces(ParkingPlace parkingPlace)
        {
            WriteParkingSlotContent(parkingPlace.Occupied());
        }
        /// <summary>
        /// Optimizes the parking place. Moves single parked motorcycles together in the same slots.
        /// Displays a list of movements to be performed by the employees.
        /// </summary>
        /// <param name="parkingPlace"></param>

        public static void Optimize(ParkingPlace parkingPlace)
        {
            ParkingPlaceOptimizer optimizer = new ParkingPlaceOptimizer();
            List<OptimizeMovementDetail> OptimizeInstructions;
            OptimizeInstructions = optimizer.GetOptimzeInstructions(parkingPlace);
            string OptimzeMessage = "";
            foreach (var message in OptimizeInstructions)
            {
                OptimzeMessage += string.Format("Move {0} with registrationNumber {1} from parking slot {2} to {3} \n", message.TypeName, message.RegistrationNumber, message.OldStorageSlotNumber+1, message.NewStorageSlotNumber+1);
            }
            if (OptimizeInstructions.Count() < 1)
            {
                Messenger.WriteInformationMessage("The parkingplace is alreadey optimized.");
            }
            else
            {
                Messenger.WriteInformationMessage(OptimzeMessage);

                bool loop = true;
                string input = null;
                do
                {
                    Console.WriteLine("Please enter YES to confirm optimization 0 to bort: ");
                    input = Console.ReadLine().ToUpper();
                    int inputNumber = 0;
                    if (int.TryParse(input, out inputNumber))
                    {
                        if (inputNumber == 0)
                        {
                            // 0 means abort
                            input = null;
                            loop = false;
                        }


                    }
                    else if (input.Equals("YES"))
                    {
                        // optimization confirmed
                        loop = false;
                        optimizer.DoOptimization(parkingPlace);
                        Messenger.WriteInformationMessage("The database has been updated with the optimize instructions.");
                    }
                    else
                    {
                        Messenger.WriteErrorMessage("Type YES or 0 to abort.");
                    }
                } while (loop);
            }
        }

        /// <summary>
        /// Removes a vehicle from the parking place.
        /// </summary>
        /// <param name="parkingPlace"></param>
        /// <param name="registrationNumber"></param>
        public static void Remove(ParkingPlace parkingPlace, string registrationNumber)
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
        public static void FindFreeSpot(ParkingPlace parkingPlace)
        {
 
            VehicleType vehicleType = PromptForVehicelType();
            if (vehicleType == VehicleType.Unspecified)
            {
                // user aborted registration
                return;
            }
            int position;
            int size=0;  // Today the size could at the moment be derived from enum Vehicletype
                         // but in the future the size might not be the same as the enum number
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

            position = parkingPlace.FindFreePlace(size); // Find a free position for a vehicle, depending on user choice
                                                         // The size determines whitch parking slot is most suitable.
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
        static void FindVehicle(ParkingPlace parkingPlace)
        {
            // Console.WriteLine("Please enter the registration number of the vehicle : ");
            string registrationNumber = PromptForRegistrationNumber();
            if (registrationNumber == null)
            {
                // User aborted
                return;
            }
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
        public static void MoveVehicle(ParkingPlace parkingPlace)
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
                Console.WriteLine("Please enter the registration number of the vehicle or 0 to abort: ");
                registrationNumber = Console.ReadLine().ToUpper();
                int inputNumber = 0;
                if (int.TryParse(registrationNumber, out inputNumber))
                {
                    if (inputNumber == 0)
                    {
                        registrationNumber = null;
                        loop = false;
                    }
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
                else if (typeNumber < 0 || typeNumber > 4)
                {
                    Messenger.WriteErrorMessage("Enter a number from the list.");
                }
                else
                {
                    // includes 0 for abort that sets type to unspecified
                    type = (VehicleType)typeNumber;
                    loop = false;
                }

            } while (loop);

            return type;
        }
        /// <summary>
        /// Prompts the user for a Car mark
        /// </summary>
        /// <returns></returns>
        public static string PromptMarkForCar()
        {
            bool loop = true;
            string mark = null;
            do
            {
                Console.WriteLine("Please enter the mark of the car: ");
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
        public static void ParkVehicle(ParkingPlace parkingPlace)
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

            // Switches on different vehicle types selected by the user
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
                    Trike newTrike = new Trike();
                    newTrike.RegistrationNumber = registrationNumber;
                    newVehicle = newTrike;
                    // Should per specification use the specialized properties of the class Trike
                    // Ask the user for input and set properties
                    throw new NotImplementedException();
                    break;
               case VehicleType.Car:
                    Car newCar = new Car();
                    newCar.RegistrationNumber = registrationNumber;
                    newVehicle = newCar;
                    // Should per specification use the specialized properties of the class Car
                    // Ask the user for input and set properties
                    string colour=PromptMarkForCar();
                    newCar.Colour = colour;
                    break;
     
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
            catch(StorageSlotToFullForStoreableException)
            {
                // This should not happend. 
                Messenger.WriteErrorMessage("The parking place has no room for the vehicel.");
            }
        }
     
        /// <summary>
        /// Revome Vehicle
        /// </summary>
        /// <param name="parkingPlace"></param>
        static void RemoveVehicle(ParkingPlace parkingPlace)
        {
            string registrationNumber = PromptForRegistrationNumber();
            if (registrationNumber != null)
            {
                Remove(parkingPlace, registrationNumber); // Remove the vehicle with the specificed registration number (if it exists in the parking lot)
            }
        }
    
  
    }
}
