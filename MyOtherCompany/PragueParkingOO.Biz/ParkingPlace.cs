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

        public Storage<Vehicle> Storage;
        /// <summary>
        /// Instanciates the storage with a number of slots of default size
        /// </summary>
        /// <param name="Size"></param>
        public ParkingPlace(int size)
        {
            Storage = new Storage<Vehicle>(size);
        }
        /// <summary>
        /// Adds a vehicel to the storage place.
        /// Throws exeption if registrationnumber already exists
        /// </summary>
        /// <param name="item">Item to be stored</param>
        /// <returns>Slot number the storeable has been parked in</returns>
        public int Add(Vehicle  item)
        {
            return Storage.Add(item);
        }
        /// <summary>
        /// Removes a vehicle from the storeage place
        /// </summary>
        /// <param name="registrationNumber"></param>
        /// <returns>number of storage slot</returns>    
        public int Remove(string registrationNumber)
        {
            return Storage.Remove(registrationNumber);
        }
        /// <summary>
        /// Counts the number of free spaces for a specifik vehicle size
        /// </summary>
        /// <param name="size">The size of the storeable item</param>
        /// <returns></returns>
        public int FreeSpacesCount(int size)
        {
            return Storage.FreeSpacesCount(size);
        }
        /// <summary>
        /// Counts the number of partially or fully occupied vehicle slots
        /// </summary>
        /// <returns></returns>
        public int OccupiedCount()
        {
            return Storage.OccupiedCount();
        }
        /// <summary>
        /// Returns the content of the storage place
        /// </summary>
        /// <returns></returns>
        public List<StorageSlotDetail> Occupied()
        {
            return Storage.Occupied();
        }
        /// <summary>
        /// Returns the content of a vehicle slot
        /// </summary>
        /// <returns></returns>
        public StorageSlotDetail Occupied(int slotNumber)
        {
            return Storage.Occupied(slotNumber);
        }
        /// <summary>
        /// Returns the content of all partially or fully free vehicle slot there a specific size fits
        /// </summary>
        /// <returns></returns>
        public List<StorageSlotDetail> FindFreeSlots(int size)
        {
            return FindFreeSlots(size);
        }
        /// <summary>
        /// Returns the content of all partially or fully free vehicle slot
        /// </summary>
        /// <returns></returns>
        public List<StorageSlotDetail> FindFreeSlots()
        {
            return FindFreeSlots(1);
        }
        /// <summary>
        /// Returns the content of the vehicle place with registration number that matches the searchstring
        /// </summary>
        /// <returns></returns>
        public List<StorageItemDetail> Find(string SearchString)
        {
            return Storage.Find(SearchString);
        }
        /// <summary>
        /// Returns all vehicles stored in the storage place 
        /// </summary>
        /// <returns></returns>
        public List<StorageItemDetail> FindAll()
        {
            return Storage.FindAll();
        }
        public List<StorageSlotDetail> FindAllSlots()
        {
            return Storage.FindAllSlots();
        }
        /// <summary>
        /// Finds a stored vehicel and returns the slot number
        /// </summary>
        /// <param name="registrationNumber"></param>
        /// <returns></returns>
        public int FindDistinctSlotNumber(string registrationNumber)
        {
            return FindDistinctSlotNumber(registrationNumber);
        }
        /// <summary>
        /// Retrieves an vehicle from storage without removing it
        /// </summary>
        /// <param name="registrationNumber"></param>
        /// <returns></returns>
        public Vehicle Peek(string registrationNumber)
        {
            return Peek(registrationNumber);
        }
        /// <summary>
        /// Finds a free storage slot for a specific size
        /// </summary>
        /// <param name="size">Size of the storeable item</param>
        /// <returns></returns>
        public int FindFreePlace(int size)
        {
            return Storage.FindFreePlace(size);
        }
        /// <summary>
        /// Moves a storeable to a new storage slot
        /// </summary>
        /// <param name="registrationNumber"></param>
        /// <param name="newPlace"></param>
        public void Move(string registrationNumber, int newPlace)
        {
            Storage.Move(registrationNumber, newPlace);
        }


        public IEnumerator<StorageSlotDetail> GetEnumerator()
        {
            return this.GetEnumerator();
        }

        IEnumerator<StorageSlotDetail> IEnumerable<StorageSlotDetail>.GetEnumerator()
        {
            foreach (StorageSlotDetail slot in Storage.FindAllSlots())
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
            return (string.Format("Vehicle parking with {0}/{1} slots full.", Storage.Length, OccupiedCount()));
        }

        public object Clone()
        {
            Storage<Vehicle> newStorage = (Storage<Vehicle>)Storage.Clone();
            return newStorage;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<StorageSlotDetail>)Storage).GetEnumerator();
        }
    }
}
