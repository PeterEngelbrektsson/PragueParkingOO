﻿using MyCompany.Storage.Biz;
using MyOtherCompany.Common;
using MyOtherCompany.PragueParkingOO.Biz;
using MyOtherCompany.PragueParkingOO.Biz.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyOtherCompany.PragueParkingOO.UI
{
    public class ParkingConsole
    {
        public const int NumberOfParkinPlaces = 100;
        public string ParkingPlaceFileName = "ParkingPlace2_0.bin";
        private ParkingPlace parkingPlace;

        public ParkingConsole(string FileName, ParkingPlace parkingPlace)
        {
            ParkingPlaceFileName = FileName;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            this.parkingPlace = parkingPlace;

        }
        /// <summary>
        /// Writes the main menu 
        /// </summary>
        /// <param name="parkingPlace"></param>
        public void WriteMenu()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine();
            Console.WriteLine("  Prague Parking v2.0 Object oriented");     // Version updated
            Console.WriteLine("---------------------------------------");
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
            DisplayIfCanBeOptimized();
            Console.WriteLine();
            Console.Write("Please input number : ");

        }

        /// <summary>
        /// Display a message if the park can be optimized.
        /// </summary>
        /// <param name="parkingPlace">The parking place</param>
        public void DisplayIfCanBeOptimized()
        {
            
            List<OptimizeMovementDetail> OptimizationInstructions = parkingPlace.GetOptimzeInstructions();
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
        public void DisplayStatistics()
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
        public void DisplayMenu()
        {
            // Console.Clear(); -- Do we want to clear screen between repeat displays of the menu or not ? 
            bool keepLoop = true;
            int choice = 0;

            while (keepLoop) // Perpetual loop
            {
                WriteMenu();

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
                            ParkVehicle();
                            break;

                        case 2: // Move a vehicle
                            MoveVehicle();
                            break;

                        case 3: // Find a vehicle
                            FindVehicle();
                            break;

                        case 4: // Remove a vehicle
                            RemoveVehicle();
                            break;

                        case 5: // Find free parking spot
                            FindFreeSpot();
                            break;

                        case 6: // Optimize parking spot
                            Optimize(); // Optimize the parking place
                            break;

                        case 7: //Display short overview
                            DisplayParkingSlotsOverview();
                            break;

                        case 8: // Display overview
                            DisplayOverview();
                            break;

                        case 9: // List all vehicles in parking lot
                            DisplayParkedVehicels();
                            break;

                        case 10: //Display statistics
                            DisplayStatistics();
                            break;

                        case 11: //Display occupied places
                            DisplayOccupiedPlaces();
                            break;

                        case 12: //Save
                            parkingPlace.SaveToFile(ParkingPlaceFileName);
                            Messenger.WriteInformationMessage("Database saved to file.");
                            break;
                        case 13: //Load
                            parkingPlace.LoadFromFile(ParkingPlaceFileName);
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
        public void DisplayParkedVehicels()
        {
            var parkedVehicles = parkingPlace.FindAll();

            if (parkedVehicles == null || parkedVehicles.Count() < 1)
            {
                Messenger.WriteInformationMessage("The parkingplace is empty.");
                return;
            }

            bool loop = true;
            int choice = 1;    // default sorting order to display before giving options to resort
            do
            {
                switch (choice)
                {
                    case 1:
                        parkedVehicles.Sort(new StorageItemDetailSortByStorageSlotAsc());
                        break;
                    case 2:
                        parkedVehicles.Sort(new StorageItemDetailSortByStorageSlotDesc());
                        break;
                    case 3:
                        parkedVehicles.Sort(new StorageItemDetailSortByRegNrAsc());
                        break;
                    case 4:
                        parkedVehicles.Sort(new StorageItemDetailSortByRegNrDesc());
                        break;
                    case 5:
                        parkedVehicles.Sort(new StorageItemDetailSortByTypeNameAsc());
                        break;
                    case 6:
                        parkedVehicles.Sort(new StorageItemDetailSortByTypeNameDesc());
                        break;
                    case 7:
                        parkedVehicles.Sort(new StorageItemDetailSortByTimeStampAsc());
                        break;
                    case 8:
                        parkedVehicles.Sort(new StorageItemDetailSortByTimeStampDesc());
                        break;
                    case 9:
                        parkedVehicles.Sort(new StorageItemDetailSortBySizeAsc());
                        break;
                    case 10:
                        parkedVehicles.Sort(new StorageItemDetailSortBySizeDesc());
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
        public void WriteParkingSlotOverview(IEnumerable<StorageSlotDetail> storageSlotReports)
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
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                    }
                    Console.Write(" {0,1}/{1,1} ", report.OccupiedSpace, report.Size); // slotnumber convertion from 0 to 1 based
                    Console.ForegroundColor = ConsoleColor.Black;
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
        }
        /// <summary>
        /// Writes a report of all passed storageSlotReports to the console.
        /// </summary>
        /// <param name="storageSlotReports">Storage slot reports to write</param>
        public void WriteParkingSlotContent(List<StorageSlotDetail> storageSlotReports)
        {
            bool loop = true;
            int choice = 1;     // default sorting order to display before giving options to resort
            do
            {
                switch (choice)
                {
                    case 1:
                        storageSlotReports.Sort(new StorageSlotDetailSortByStorageSlotNrAsc());
                        break;
                    case 2:
                        storageSlotReports.Sort(new StorageSlotDetailSortByStorageSlotNrDesc());
                        break;
                    case 3:
                        storageSlotReports.Sort(new StorageSlotDetailSortByFreeSpaceAsc());
                        break;
                    case 4:
                        storageSlotReports.Sort(new StorageSlotDetailSortByFreeSpaceDesc());
                        break;
                    case 5:
                        storageSlotReports.Sort(new StorageSlotDetailSortByOccupiedSpaceAsc());
                        break;
                    case 6:
                        storageSlotReports.Sort(new StorageSlotDetailSortByOccupiedSpaceDesc());
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
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                    }
                    Console.Write(" {0,3} {1,1}/{2,1}", report.SlotNumber+1, report.OccupiedSpace, report.Size); // slotnumber convertion from 0 to 1 based
                    Console.ForegroundColor = ConsoleColor.Black;
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

        /// <summary>
        /// Display an short overivew of the parking slots
        /// </summary>
        /// <param name="parkingPlace"></param>
        public void DisplayParkingSlotsOverview()
        {
            WriteParkingSlotOverview(parkingPlace.FindAllSlots());
        }
            /// <summary>
            /// Displays overview of all parked vehicles in the parking place.
            /// </summary>
            /// <param name="parkingPlace"></param>
            public void DisplayOverview()
        {

            WriteParkingSlotContent(parkingPlace.FindAllSlots());
       
                        
        }
        /// <summary>
        /// Displays all free places in the parkingplace
        /// </summary>
        /// <param name="parkingPlace"></param>
        public void DisplayOccupiedPlaces()
        {
            WriteParkingSlotContent(parkingPlace.Occupied());
        }
        /// <summary>
        /// Optimizes the parking place. Moves single parked motorcycles together in the same slots.
        /// Displays a list of movements to be performed by the employees.
        /// </summary>
        /// <param name="parkingPlace"></param>

        public void Optimize()
        {
            
            List<OptimizeMovementDetail> OptimizeInstructions = parkingPlace.GetOptimzeInstructions();
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
                    if (int.TryParse(input, out int inputNumber))
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
                        parkingPlace.DoOptimization();
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
        public void Remove(string registrationNumber)
        {

            try
            {
                Vehicle v = parkingPlace.Peek(registrationNumber);
                DateTime timeStamp = v.TimeStamp;
                int pos = parkingPlace.Remove(registrationNumber);
                string message=String.Format("The {2} with registration number {0} successfully removed from position {1}.\nIt was parked {2}.\n", registrationNumber, pos + 1,timeStamp,v.TypeName); // Display of parking number should be one based
                if (v is Bike)
                {
                    message += string.Format("The brand of the bike is {0}.", (v as Bike).Brand);
                }
                if (v is MotorBike)
                {
                    message += string.Format("The mark of the motorbike is {0}.", (v as MotorBike).Mark);
                }
                if (v is Trike)
                {
                    message += string.Format("The manufacturer of the trike is {0}.", (v as Trike).Manufacturer);
                }
                if (v is Car)
                {
                    message += string.Format("The colour of the car is {0}.", (v as Car).Colour);
                }
                Messenger.WriteInformationMessage(message);

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
        public void FindFreeSpot()
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
        void FindVehicle()
        {
            // Console.WriteLine("Please enter the registration number of the vehicle : ");
            string registrationNumber = PromptForRegistrationNumber();
            if (registrationNumber == null)
            {
                // User aborted
                return;
            }
            // No exact match found
            var searchResult = parkingPlace.Find(registrationNumber);
                if (searchResult.Count > 0)
                {
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine("Slot    RegNo         Type    Checked in");
                    Console.WriteLine("-------------------------------------------------");
                    foreach (var detail in searchResult)
                    {
                        Console.WriteLine("{0,3} {1,10} {2,12} {3,16}", detail.StorageSlotNumber+ 1, detail.RegistrationNumber,detail.TypeName,detail.TimeStamp);
                    }
                }
                else
                {
                    Messenger.WriteErrorMessage("I am sorry to say you vehicle does not exist in our parking lot.");
                    Messenger.WriteErrorMessage("Perhaps someone has taken it for a joyride. Our apologies.");
                }

        }
        /// <summary>
        /// Moving Vehicle one place to another place.
        /// </summary>
        /// <param name="parkingPlace"></param>
        public void MoveVehicle()
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
        public string PromptForRegistrationNumber()
        {
            bool loop = true;
            string registrationNumber = null;
            do
            {
                Console.WriteLine("Please enter the registration number of the vehicle or 0 to abort: ");
                registrationNumber = Console.ReadLine().ToUpper();
                if (int.TryParse(registrationNumber, out int inputNumber))
                {
                    if (inputNumber == 0)
                    {
                        registrationNumber = null;
                        loop = false;
                    }
                }
                else if (!VehicleValidator.ValidRegistrationNumber(registrationNumber, out string[] errorMessages))
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
        public VehicleType PromptForVehicelType()
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
                if (!int.TryParse(input, out int typeNumber))
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
        /// Prompts the user for a bike manufacturer
        /// </summary>
        /// <returns></returns>
        public string PromptFormanufacturerTrike()
        {
            bool loop = true;
            string manufacturer = null;
            do
            {
                Console.WriteLine("Please enter the manufacturer of the trike: ");
                manufacturer = Console.ReadLine().ToUpper();
                if (string.IsNullOrEmpty(manufacturer))
                {
                    // Contiune looping
                }
                else
                {
                    // a string has been entered
                    loop = false;
                }
            } while (loop);
            return manufacturer;
        }
        /// <summary>
        /// Prompts the user for a bike brand
        /// </summary>
        /// <returns></returns>
        public string PromptForBrandBike()
        {
            bool loop = true;
            string brand = null;
            do
            {
                Console.WriteLine("Please enter the brand of the bike: ");
                brand = Console.ReadLine().ToUpper();
                if (string.IsNullOrEmpty(brand))
                {
                    // Contiune looping
                }
                else
                {
                    // a string has been entered
                    loop = false;
                }
            } while (loop);
            return brand;
        }
        /// <summary>
        /// Prompts the user for a Car mark
        /// </summary>
        /// <returns></returns>
        public string PromptForColourCar()
        {
            bool loop = true;
            string colour = null;
            do
            {
                Console.WriteLine("Please enter the colour of the car: ");
                colour = Console.ReadLine().ToUpper();
                if (string.IsNullOrEmpty(colour))
                {
                    // Contiune looping
                }
                else
                {
                    // a string has been entered
                    loop = false;
                }
            } while (loop);
            return colour;
        }
        /// <summary>
        /// Prompts the user for a motorbike mark
        /// </summary>
        /// <returns></returns>
        public string PromptForMarkMotorbike()
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
        public void ParkVehicle()
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
                    Bike newBike = new Bike
                    {
                        RegistrationNumber = registrationNumber
                    };
                    newVehicle = newBike;
                    // Should per specification use the specialized properties of the class Bike
                    // Ask the user for input and set properties
                    newBike.Brand = PromptForBrandBike();
                    break;
                case VehicleType.MotorBike:
                    MotorBike newMotorBike = new MotorBike
                    {
                        RegistrationNumber = registrationNumber
                    };
                    newVehicle = newMotorBike;
                    // Should per specification use the specialized properties of the class MotorBike
                    // Ask the user for input and set properties
                    string mark=PromptForMarkMotorbike();
                    newMotorBike.Mark = mark;
                    break;

                case VehicleType.Trike:
                    Trike newTrike = new Trike
                    {
                        RegistrationNumber = registrationNumber
                    };
                    newVehicle = newTrike;
                    // Should per specification use the specialized properties of the class Trike
                    // Ask the user for input and set properties
                    newTrike.Manufacturer = PromptFormanufacturerTrike();
                    break;
               case VehicleType.Car:
                    Car newCar = new Car
                    {
                        RegistrationNumber = registrationNumber
                    };
                    newVehicle = newCar;
                    // Should per specification use the specialized properties of the class Car
                    // Ask the user for input and set properties
                    string colour=PromptForColourCar();
                    newCar.Colour = colour;
                    break;
     
            }
            try
            {
                int position = parkingPlace.Add(newVehicle); // Park at suitable position (if any)
                                                             // Throws exceptions.
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
        void RemoveVehicle()
        {
            string registrationNumber = PromptForRegistrationNumber();
            if (registrationNumber != null)
            {
                Remove(registrationNumber); // Remove the vehicle with the specificed registration number (if it exists in the parking lot)
            }
        }
    
  
    }
}
