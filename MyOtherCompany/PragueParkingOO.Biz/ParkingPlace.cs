using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCompany.Storage.Biz;
using MyOtherCompany.PragueParkingOO.Biz;
using MyOtherCompany.PragueParkingOO.Biz.Vehicles;

namespace MyOtherCompany.PragueParkingOO.Biz
{
    /// <summary>
    /// Encapsulates the storage
    /// </summary>
    [Serializable]
    public class ParkingPlace : IEnumerable<StorageSlotDetail>, ICloneable
    {

        private Storage<Vehicle> storage;
        public ParkingPlace(int parkingPlaceSize, int slotSize)
        {
            Dictionary<int, int> SlotSizeCounts = new Dictionary<int, int>
            {
                { slotSize, parkingPlaceSize }
            };

            storage = new Storage<Vehicle>(SlotSizeCounts);
        }
        /// <summary>
        /// Instanciates the storage with a number of slots of default size
        /// </summary>
        /// <param name="Size"></param>
        public ParkingPlace(int size)
        {
            Dictionary<int, int> SlotSizeCounts = new Dictionary<int, int>
            {
                { 1, size / 8 },
                { 2, size / 8 },
                { 3, size / 4 },
                { 4, size / 4 },
                { 5, size / 8 },
                { 6, size-((size / 8)*3+(size/4)*2)}    // The rest of the parking places
            };

            storage = new Storage<Vehicle>(SlotSizeCounts);
        }
        /// <summary>
        /// Creates the parkingplace with a custom number of slots of different sizes.
        /// </summary>
        /// <param name="SlotSizeCounts"></param>
        public ParkingPlace(Dictionary<int, int> SlotSizeCounts)
        {
            storage = new Storage<Vehicle>(SlotSizeCounts);
        }
        /// <summary>
        /// Adds a vehicel to the storage place.
        /// Throws exeption if registrationnumber already exists
        /// </summary>
        /// <param name="item">Item to be stored</param>
        /// <returns>Slot number the storeable has been parked in</returns>
        public int Add(Vehicle item)
        {
            return storage.Add(item);
        }

        /// <summary>
        /// Returns the content of the vehicle place with registration number that matches the searchstring
        /// </summary>
        /// <returns></returns>
        public List<StorageItemDetail> Find(string SearchString)
        {
            return storage.Find(SearchString);
        }
        /// <summary>
        /// Returns all vehicles stored in the storage place 
        /// </summary>
        /// <returns></returns>
        public List<StorageItemDetail> FindAll()
        {
            return storage.FindAll();
        }

        /// <summary>
        /// Finds all parking slot reports
        /// </summary>
        /// <returns></returns>
        public List<StorageSlotDetail> FindAllSlots()
        {
            return storage.FindAllSlots();
        }
 
        /// <summary>
        /// Finds a stored vehicel and returns the slot number
        /// </summary>
        /// <param name="registrationNumber"></param>
        /// <returns></returns>
        public int FindDistinctSlotNumber(string registrationNumber)
        {
            return storage.FindDistinctSlotNumber(registrationNumber);
        }

        /// <summary>
        /// Finds a free storage slot for a specific size
        /// </summary>
        /// <param name="size">Size of the storeable item</param>
        /// <returns></returns>
        public int FindFreePlace(int size)
        {
            return storage.FindFreePlace(size);
        }
 
        /// <summary>
        /// Returns the content of all partially or fully free vehicle slot there a specific size fits
        /// </summary>
        /// <returns></returns>
        public List<StorageSlotDetail> FindFreeSlots(int size)
        {
            return storage.FindFreeSlots(size);
        }

        /// <summary>
        /// Returns the content of all partially or fully free vehicle slot
        /// </summary>
        /// <returns></returns>
        public List<StorageSlotDetail> FindFreeSlots()
        {
            return storage.FindFreeSlots(1);
        }

        /// <summary>
        /// Counts the number of free spaces for a specifik vehicle size
        /// </summary>
        /// <param name="size">The size of the storeable item</param>
        /// <returns></returns>
        public int FreeSpacesCount(int size)
        {
            return storage.FreeSpacesCount(size);
        }

        /// <summary>
        /// Moves a storeable to a new storage slot
        /// </summary>
        /// <param name="registrationNumber"></param>
        /// <param name="newPlace"></param>
        public void Move(string registrationNumber, int newPlace)
        {
            storage.Move(registrationNumber, newPlace);
        }

        /// <summary>
        /// Returns the content of the storage place
        /// </summary>
        /// <returns></returns>
        public List<StorageSlotDetail> Occupied()
        {
            return storage.Occupied();
        }
 
        /// <summary>
        /// Returns the content of a vehicle slot
        /// </summary>
        /// <returns></returns>
        public StorageSlotDetail Occupied(int slotNumber)
        {
            return storage.Occupied(slotNumber);
        }

        /// <summary>
        /// Counts the number of partially or fully occupied vehicle slots
        /// </summary>
        /// <returns></returns>
        public int OccupiedCount()
        {
            return storage.OccupiedCount();
        }

        /// <summary>
        /// Counts the number of partially  occupied vehicle slots
        /// </summary>
        /// <returns></returns>
        public int PartiallyOccupiedCount()
        {
            return storage.PartiallyOccupiedCount();
        }
 
        /// <summary>
        /// Returns the content of the storage place
        /// </summary>
        /// <returns></returns>
        public List<StorageSlotDetail> PartiallyOccupied()
        {
            return storage.PartiallyOccupied();
        }
 
        /// <summary>
        /// Retrieves an vehicle from storage without removing it
        /// </summary>
        /// <param name="registrationNumber"></param>
        /// <returns></returns>
        public Vehicle Peek(string registrationNumber)
        {
            return storage.Peek(registrationNumber);
        }
  
        /// <summary>
        /// Removes a vehicle from the storeage place
        /// </summary>
        /// <param name="registrationNumber"></param>
        /// <returns>number of storage slot</returns>    
        public int Remove(string registrationNumber)
        {
            return storage.Remove(registrationNumber);
        }

        public IEnumerator<StorageSlotDetail> GetEnumerator()
        {
            foreach (StorageSlotDetail slot in storage.FindAllSlots())
            {
                yield return slot;
            }
        }

        IEnumerator<StorageSlotDetail> IEnumerable<StorageSlotDetail>.GetEnumerator()
        {
            foreach (StorageSlotDetail slot in storage.FindAllSlots())
            {
                yield return slot;
            }
        }

        public StorageItemDetail this[int index]
        {

            get
            {
                List<StorageItemDetail> storableReports = FindAll();
                if (index < 0 || index > storableReports.Count)
                {
                    throw new IndexOutOfRangeException();
                }
                return storableReports[index];
            }
            // Should only be get able. No set functionality.
        }

        public override string ToString()
        {
            return (string.Format("Vehicle parking with {0}/{1} slots full.", OccupiedCount(), storage.Length));
        }

        public object Clone()
        {
            Storage<Vehicle> newStorage = (Storage<Vehicle>)storage.Clone();
            return newStorage;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<StorageSlotDetail>)storage).GetEnumerator();
        }
        /// <summary>
        /// Optimize one pakable of one specified size
        /// </summary>
        /// <param name="parkingPlace">Parking place</param>
        /// <param name="size">Size of Vehicle to optimize</param>
        /// <returns></returns>
        public OptimizeMovementDetail GetOneOptimizeInstruction(ParkingPlace parkingPlace, int size)
        {
            StorageOptimizer<Vehicle> optimizer = new StorageOptimizer<Vehicle>();
            return optimizer.GetOneOptimizeInstruction(parkingPlace.storage, size);

        }


        /// <summary>
        /// Optimizes the parking place.
        /// Calls a function that does the actual optimization.
        /// The optimization is done on a copy of the parking place.
        /// The instructions to optimize is returned.
        /// </summary>
        /// <param name="storage"></param>
        /// <returns>Instruction how to optimze the parking place.</returns>
        public List<OptimizeMovementDetail> GetOptimzeInstructions()
        {
            StorageOptimizer<Vehicle> optimizer = new StorageOptimizer<Vehicle>();
            return optimizer.GetOptimzeInstructions(storage);
        }
        /// <summary>
        /// Do an optimization of the parking place.
        /// </summary>
        /// <param name="storage"></param>
        public void DoOptimization()
        {
            // Call the optimize function that modifies the parking place.
            StorageOptimizer<Vehicle> optimizer = new StorageOptimizer<Vehicle>();
            optimizer.GetOptimzeInstructionsModifying(storage);
        }

        /// <summary>
        /// Saves the parking placce to file
        /// </summary>
        /// <param name="storage"></param>
        /// <param name="fileName"></param>
        public void SaveToFile(string fileName)
        {
            StorageRepository<Vehicle>.SaveToFile(storage, fileName);
        }
        /// <summary>
        /// Loads the parkin place from file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public void LoadFromFile(string fileName)
        {
            Storage<Vehicle> newStorage= (Storage<Vehicle>)StorageRepository<Vehicle>.LoadFromFile(fileName);
            storage = newStorage;
        }
    }
}
