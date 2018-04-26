using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.Storage.Biz
{
    
    /// <summary>
    /// Storage handling system. Stores storable items that implements the 
    /// IStorable interface.
    /// </summary>
    /// <typeparam name="T">Class that implements IStoreable</typeparam>
    public class Storage<T>:IEnumerable<T>,IEnumerable<StorageSlotDetail<T>> where T : IStoreable
    {
        private List<StorageSlot<T>> _storageSlots = new List<StorageSlot<T>>();

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
        /// <returns>Slot number the storeable has been parked in</returns>
        public int Add(T item)
        {
            // find a slot with free place.
            // set timestamp if not already set
            // store item in slot
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
        /// Counts the number of free spaces for a specifik storeable size
        /// </summary>
        /// <param name="size">The size of the storeable item</param>
        /// <returns></returns>
        public int FreeSpacesCount(int size)
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
        public List<StorageSlotDetail<T>> Occupied()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Returns the content of a storage slot
        /// </summary>
        /// <returns></returns>
        public StorageSlotDetail<T> Occupied(int SlotNumber)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Returns the content of all partially or fully free storage slot
        /// </summary>
        /// <returns></returns>
        public StorageSlotDetail<T> FindFreeSlots()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Returns the content of the storage place with registration number that matches the searchstring
        /// </summary>
        /// <returns></returns>
        public List<StorageItemDetail<T>> Find(string SearchString)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Returns all storables stored in the storage place 
        /// </summary>
        /// <returns></returns>
        public List<StorageItemDetail<T>> FindAll( )
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
        public T Peek(string registrationNumber)
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

        public IEnumerator<T> GetEnumerator()
        {
            foreach(StorageSlot<T> slot in _storageSlots)
            {
                foreach(T item in slot)
                {
                    yield return item;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator<Biz.StorageSlotDetail<T>> IEnumerable<Biz.StorageSlotDetail<T>>.GetEnumerator()
        {
            foreach(StorageSlot<T> slot in _storageSlots)
            {
                StorageSlotDetail<T> item = slot.GetSlotDetails();
                yield return item;
            }
        }
        public StorageItemDetail<T> this[int index]
        {

            get
            {
                List<StorageItemDetail<T>> storableReports = FindAll();
                if(index<0 || index > storableReports.Count)
                {
                    throw new IndexOutOfRangeException();
                }
                return storableReports[index];
            }
            // Should only be get able. No set functionality.
        }
    }
}
