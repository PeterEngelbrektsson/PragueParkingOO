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
    [Serializable]
    public class StorageSlot<T> : IEnumerable<T>,ICloneable where T : IStoreable
    {
        private List<T> storables = new List<T>();
        private int slotNumber;
        public int Size = 4;
        public int SlotNumber { get
            {
                return slotNumber;
            }
             private set
            {
                slotNumber = value;
            }
        }


        public StorageSlot(int slotNumber)
        {
            SlotNumber = slotNumber;
        }
        public StorageSlot(int slotNumber,int size)
        {
            SlotNumber = slotNumber;
            Size = size;
        }
        /// <summary>
        /// Adds a storeable to the storage slot.
        /// Throws exeption if registrationnumber already exists
        /// </summary>
        /// <param name="item">Item to store</param>
        public void Add(T item)
        {
            if (Contains(item.RegistrationNumber))
            {
                throw new RegistrationNumberAlreadyExistsException();
            }
            if (item.Size > FreeSpace())
            {
                throw new StorageSlotToFullForStoreableException();
            }
            if (item.TimeStamp.Equals(default(DateTime)))
            {
                item.TimeStamp = DateTime.Now; // Set timestamp if null. 
                //If not null it has already been checked in and is only moved around inside the storage
            }
            storables.Add(item);
        }

        /// <summary>
        /// Finds a stored storeables  slot number
        /// </summary>
        /// <param name="registrationNumber"></param>
        /// <returns></returns>
        public bool Contains(string registrationNumber)
        {
            bool found = false;
            foreach (T t in this)
            {
                if (t.RegistrationNumber.Equals(registrationNumber))
                {
                    found = true;
                    break;
                }
            }
            return found;
        }

        /// <summary>
        /// Counts the number of free spaces for a specifik storable size
        /// </summary>
        /// <param name="size">Size of item to store</param>
        /// <returns></returns>
        public int FreeSpaces(int size)
        {
            if (size < 0)
            {
                throw new ArgumentException();
            }
            int freeSpaces = FreeSpace() / size;    // The number of storabels of a specific size that fits in the slot
            return freeSpaces;
        }
 
        /// <summary>
        /// Counts the number of free spaces 
        /// </summary>
        /// <returns></returns>
        public int FreeSpace()
        {
            return Size - Occupied();
        }

        /// <summary>
        /// Returns the content of the parking place
        /// </summary>
        /// <returns></returns>
        public StorageSlotDetail GetSlotDetails()
        {
            StorageSlotDetail item = new StorageSlotDetail
            {
                FreeSpace = FreeSpace(),
                OccupiedSpace = Occupied(),
                SlotNumber = SlotNumber,
                Size = Size,
                StorageItemDetails = GetStorageItemDetailsReport()
            };
            return item;
        }

        /// <summary>
        /// Generates a storables details report for the slot;
        /// </summary>
        public List<StorageItemDetail> GetStorageItemDetailsReport()
        {
            List<StorageItemDetail> details = new List<StorageItemDetail>();

            foreach (T item in storables)
            {
                StorageItemDetail detail = new StorageItemDetail
                {
                    Size = item.Size,
                    TimeStamp = item.TimeStamp,
                    RegistrationNumber = item.RegistrationNumber,
                    StorageSlotNumber = this.SlotNumber,
                    TypeName = item.TypeName
                };
                details.Add(detail);
            }
            return details;
        }

 
        /// <summary>
        /// Returns the amount of occupied space in the slot
        /// </summary>
        /// <returns></returns>
        public int Occupied()
        {
            int OccupiedSpace = 0;
            foreach (T t in this)
            {
                OccupiedSpace += t.Size;
            }
            return OccupiedSpace;
        }

        /// <summary>
        /// Retrieves an item from storage without removing it
        /// </summary>
        /// <param name="registrationNumber"></param>
        /// <returns></returns>
        public T Peek(string registrationNumber)
        {
            T result = default(T);
            foreach (T item in storables)
            {
                if (item.RegistrationNumber.Equals(registrationNumber))
                {
                    result = item;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// Removes a storeable from the storage slot
        /// </summary>
        /// <param name="registrationNumber"></param>
        public void Remove(string registrationNumber)
        {
            if (!Contains(registrationNumber))
            {
                throw new RegistrationNumberAlreadyExistsException();
            }

            T item = Peek(registrationNumber);
            storables.Remove(item);

        }
 
   
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)storables).GetEnumerator();
        }

        /// <summary>
        ///  Enumerates all T in slot
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)storables).GetEnumerator();
        }


        public T this[int index]
        {
            get
            {
                if(index<0 || index > storables.Count)
                {
                    throw new IndexOutOfRangeException();
                }
                return storables[index];
            }
        }
        public override string ToString()
        {
            return string.Format("StorageSlot with {0}/{1}",Occupied(),Size);
        }

        public object Clone()
        {

            StorageSlot<T> newSlot = new StorageSlot<T>(SlotNumber);
            foreach(T storeable in storables)
            {
                newSlot.Add((T)storeable.Clone());
            }
            newSlot.Size = Size;
            return newSlot;

    }
}
}
