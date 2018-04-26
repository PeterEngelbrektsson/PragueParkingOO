using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.Storage.Biz
{
    public class StorageSlot<T>
    {
        /// <summary>
        /// Adds a storeable to the storage slot.
        /// Throws exeption if registrationnumber already exists
        /// </summary>
        /// <param name="item">Item to store</param>
        public void Add(IStoreable item)
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
        public IStoreable Peek(string registrationNumber)
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
        public StorageSlotDetail Occupied()
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
    }
}
