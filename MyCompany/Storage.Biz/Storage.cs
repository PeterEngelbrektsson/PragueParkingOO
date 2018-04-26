using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.Storage.Biz
{
    public class Storage<T> where T : IStoreable
    {
        private StorageSlot<T>[] _storageSlots;

        /// <summary>
        /// Instanciates the storage with a number of slots of default size
        /// </summary>
        /// <param name="Size"></param>
        public Storage(int Size)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Adds a storeable to the storage place.
        /// Throws exeption if registrationnumber already exists
        /// </summary>
        /// <param name="item">Item to be stored</param>
        public void Add(IStoreable item)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Removes a storeable from the storeage place
        /// </summary>
        /// <param name="registrationNumber"></param>
        public void Remove(string registrationNumber)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Counts the number of free spaces for a specifik storeable type
        /// </summary>
        /// <param name="storageType"></param>
        /// <returns></returns>
        public int FreeSpacesCount(Type storeableType)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Counts the number of partially or fully occupied storeable slots
        /// </summary>
        /// <returns></returns>
        public int OccupiedCount()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Returns the content of the storage place
        /// </summary>
        /// <returns></returns>
        public List<StorageSlotDetail> Occupied()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Returns the content of a storage slot
        /// </summary>
        /// <returns></returns>
        public StorageSlotDetail Occupied(int SlotNumber)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Returns the content of the storage place with registration number that matches the searchstring
        /// </summary>
        /// <returns></returns>
        public List<StorageItemDetail> Find(string SearchString)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Returns all storables stored in the storage place 
        /// </summary>
        /// <returns></returns>
        public List<StorageItemDetail> FindAll( )
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Finds a stored storeable and returns the slot number
        /// </summary>
        /// <param name="registrationNumber"></param>
        /// <returns></returns>
        public int FindDistinctSlotNumber(string registrationNumber)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Retrieves an item from storage without removing it
        /// </summary>
        /// <param name="registrationNumber"></param>
        /// <returns></returns>
        public IStoreable Peek(string registrationNumber)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Finds a free storage slot for a specific type
        /// </summary>
        /// <param name="size">Size of the storeable item</param>
        /// <returns></returns>
        public int FindFreePlace(int size)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Moves a storeable to a new storage slot
        /// </summary>
        /// <param name="registrationNumber"></param>
        /// <param name="newPlace"></param>
        public void Move(string registrationNumber, int newPlace)
        {
            throw new NotImplementedException();
        }
    }
}
