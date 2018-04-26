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
    public class StorageSlot<T>:IEnumerable<StorageItemDetail<T>>,IEnumerable<T> where T:IStoreable
    {
        private List<T> _storables = new List<T>();
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
        /// Retrieves an item from storage without removing it
        /// </summary>
        /// <param name="registrationNumber"></param>
        /// <returns></returns>
        public T Peek(string registrationNumber)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Checks if the storage slot is partially or fully occupied
        /// </summary>
        /// <returns></returns>
        public bool OccupiedSlots()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Returns the content of the parking place
        /// </summary>
        /// <returns></returns>
        public StorageSlotDetail<T> Occupied()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        IEnumerator<StorageItemDetail<T>> IEnumerable<StorageItemDetail<T>>.GetEnumerator()
        {
            foreach(T item in _storables)
            {
                StorageItemDetail<T> itemDetail = new StorageItemDetail<T>();
                itemDetail.Size = item.Size;
                itemDetail.TimeStamp = item.TimeStamp;
                itemDetail.RegistrationNumber = item.RegistrationNumber;
                itemDetail.Storeable = item;
                yield return itemDetail;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)_storables).GetEnumerator();
        }
    }
}
