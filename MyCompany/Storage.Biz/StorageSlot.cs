using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.Storage.Biz
{
    /// <summary>
    /// An storage slot that can contain several items.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class StorageSlot<T> : IEnumerable<StorageItemDetail<T>>, IEnumerable<T> where T : IStoreable
    {
        private List<T> _storables = new List<T>();
        public int Size = 4;
        public int SlotNumber { get; private set; }

        public StorageSlot(int slotNumber)
        {
            SlotNumber = slotNumber;
        }
        /// <summary>
        /// Adds a storeable to the storage slot.
        /// Throws exeption if registrationnumber already exists
        /// </summary>
        /// <param name="item">Item to store</param>
        public void Add(T item)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Removes a storeable from the storage slot
        /// </summary>
        /// <param name="registrationNumber"></param>
        public void Remove(string registrationNumber)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Counts the number of free spaces for a specifik storable size
        /// </summary>
        /// <param name="size">Size of item to store</param>
        /// <returns></returns>
        public int FreeSpaces(int size)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Counts the number of free spaces 
        /// </summary>
        /// <returns></returns>
        public int FreeSpaces()
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
        /// Returns the amount of occupied space in the slot
        /// </summary>
        /// <returns></returns>
        public int Occupied()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Generates a storables details report for the slot;
        /// </summary>
        public List<StorageItemDetail<T>> GetStorageItemDetailsReport()
        {
            List<StorageItemDetail<T>> details = new List<StorageItemDetail<T>>();
            
            foreach(T item in _storables)
            {
                StorageItemDetail<T> detail = new StorageItemDetail<T>();
                detail.Size = item.Size;
                detail.TimeStamp = item.TimeStamp;
                detail.RegistrationNumber = item.RegistrationNumber;
                detail.Description = item.Description;
                details.Add(detail);
            }
            return details;
        }
        /// <summary>
        /// Returns the content of the parking place
        /// </summary>
        /// <returns></returns>
        public StorageSlotDetail<T> GetSlotDetails()
        {
            StorageSlotDetail<T> item = new StorageSlotDetail<T>();
            item.FreeSpace = FreeSpaces();
            item.OccupiedSpace = Occupied();
            item.SlotNumber = SlotNumber;
            item.StorageItemDetails = GetStorageItemDetailsReport();
            return item;
        }
        /// <summary>
        /// Finds a stored storeables  slot number
        /// </summary>
        /// <param name="registrationNumber"></param>
        /// <returns></returns>
        public bool Contains(string registrationNumber)
        {
            throw new NotImplementedException();
        }

    
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)_storables).GetEnumerator();
        }

        /// <summary>
        /// Enumerates storageItemDetailsReports for all T's in slot
        /// </summary>
        /// <returns></returns>
        IEnumerator<StorageItemDetail<T>> IEnumerable<StorageItemDetail<T>>.GetEnumerator()
        {
            foreach(StorageItemDetail<T> item in GetStorageItemDetailsReport())
            {
                yield return item;
            }
        }
        /// <summary>
        ///  Enumerates all T in slot
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)_storables).GetEnumerator();
        }
        public T this[int index]
        {
            get
            {
                if(index<0 || index > _storables.Count)
                {
                    throw new IndexOutOfRangeException();
                }
                return _storables[index];
            }
        }
    }
}
