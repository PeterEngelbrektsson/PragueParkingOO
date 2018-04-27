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
    [Serializable]
    public class Storage<T>:IEnumerable<StorageSlotDetail>,ICloneable where T : IStoreable
    {
        private StorageSlot<T>[] _storageSlots;
        public int MaxSize = 4;

        /// <summary>
        /// Instanciates the storage with a number of slots of default size
        /// </summary>
        /// <param name="Size"></param>
        public Storage(int Size)
        {
            _storageSlots = new StorageSlot<T>[Size];
            for (int i = 0; i < Size; i++)
            {
                _storageSlots[i] = new StorageSlot<T>(i);
            }
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
            // first find all free slots.
            var freeSpaces = FindFreeSlots(item.Size);
            // Prioritize smallest available with lowest slot number.
            var availableSlots =
                from freePlace in freeSpaces
                where (freePlace.Size >= item.Size)
                orderby freePlace.FreeSpace ascending, freePlace.Size ascending, freePlace.SlotNumber ascending
                select new { freePlace};
            if (availableSlots.Count() < 1)
            {
                throw new StorageSlotToFullForStoreableException();
            }
            int availableSlotNumber = availableSlots.First().freePlace.SlotNumber;

            // set timestamp if not already set
            if (item.TimeStamp == null)
            {
                item.TimeStamp = DateTime.Now;
            }

            // store item in slot
            _storageSlots[availableSlotNumber].Add(item);

            return availableSlotNumber;
        }
        /// <summary>
        /// Removes a storeable from the storeage place
        /// </summary>
        /// <param name="registrationNumber"></param>
        /// <returns>number of storage slot</returns>    
        public int Remove(string registrationNumber)
        {
            int slotNumber = FindDistinctSlotNumber(registrationNumber);
            if (slotNumber < 0)
            {
                throw new StoreableNotFoundException();
            }
            _storageSlots[slotNumber].Remove(registrationNumber);
            return slotNumber;
        }
        /// <summary>
        /// Counts the number of free spaces for a specifik storeable size
        /// </summary>
        /// <param name="size">The size of the storeable item</param>
        /// <returns></returns>
        public int FreeSpacesCount(int size)
        {
            var slots = FindFreeSlots(size);
            return slots.Count();
        }
        /// <summary>
        /// Counts the number of partially or fully occupied storeable slots
        /// </summary>
        /// <returns></returns>
        public int OccupiedCount()
        {
            var slots = Occupied();
            return slots.Count();
        }
        /// <summary>
        /// Returns the content of the storage place
        /// </summary>
        /// <returns></returns>
        public List<StorageSlotDetail> Occupied()
        {
            List<StorageSlotDetail> slots = new List<StorageSlotDetail>();
            foreach (StorageSlotDetail item in (List<StorageSlotDetail>)this.GetEnumerator())
            {
                if (item.OccupiedSpace > 0)
                {
                    slots.Add(item); // only include occupied slots
                }
            }
            return slots;

        }
        /// <summary>
        /// Returns the content of a storage slot
        /// </summary>
        /// <returns></returns>
        public StorageSlotDetail Occupied(int slotNumber)
        {
            if(slotNumber<0 || slotNumber > _storageSlots.Length)
            {
                throw new ArgumentException();
            }
            return _storageSlots[slotNumber].GetSlotDetails();
        }
        /// <summary>
        /// Returns the content of all partially or fully free storage slot there a sspecific size fits
        /// </summary>
        /// <returns></returns>
        public List<StorageSlotDetail> FindFreeSlots(int size)
        {
            List<StorageSlotDetail> freeSlots = new List<StorageSlotDetail>();
            foreach (StorageSlotDetail item in (List<StorageSlotDetail>)this.GetEnumerator())
            {
                if (item.FreeSpace > size)
                {
                    freeSlots.Add(item); /// only include free slots
                }
            }
            return freeSlots;
        }
        /// <summary>
        /// Returns the content of all partially or fully free storage slot
        /// </summary>
        /// <returns></returns>
        public List<StorageSlotDetail> FindFreeSlots()
        {
            return FindFreeSlots(1);
        }
        /// <summary>
        /// Returns the content of the storage place with registration number that matches the searchstring
        /// </summary>
        /// <returns></returns>
        public List<StorageItemDetail> Find(string SearchString)
        {
            List<StorageItemDetail> items = this.FindAll();
            List<StorageItemDetail> matches = new List<StorageItemDetail>(); ;

            foreach (StorageItemDetail item in items)
            {
                if (item.RegistrationNumber.IndexOf(SearchString) > -1)
                {
                    matches.Add(item);
                }
            }
            return matches;
        }
        /// <summary>
        /// Returns all storables stored in the storage place 
        /// </summary>
        /// <returns></returns>
        public List<StorageItemDetail> FindAll( )
        {
            List<StorageItemDetail> matches = new List<StorageItemDetail>(); ;
            foreach (StorageItemDetail item in (List<StorageItemDetail>)this.GetEnumerator())
            {
                matches.Add(item); // return all
            }
            return matches;
        }
        public List<StorageSlotDetail> FindAllSlots()
        {
            List<StorageSlotDetail> matches = new List<StorageSlotDetail>(); ;
            foreach (StorageSlotDetail item in (List<StorageSlotDetail>)this.GetEnumerator())
            {
                matches.Add(item); // return all
            }
            return matches;
        }
        /// <summary>
        /// Finds a stored storeable and returns the slot number
        /// </summary>
        /// <param name="registrationNumber"></param>
        /// <returns></returns>
        public int FindDistinctSlotNumber(string registrationNumber)
        {
            int found = -1;
            for(int i=0; i<_storageSlots.Length;i++)
            {
                if (_storageSlots[i].Contains(registrationNumber))
                {
                    found = i;
                    break;
                }
            }
            return found;
        }
        /// <summary>
        /// Retrieves an item from storage without removing it
        /// </summary>
        /// <param name="registrationNumber"></param>
        /// <returns></returns>
        public T Peek(string registrationNumber)
        {
            int found = -1;
            for (int i = 0; i < _storageSlots.Length; i++)
            {
                if (_storageSlots[i].Contains(registrationNumber))
                {
                    found = i;
                    break;
                }
            }
            T storeable = default(T);
            if (found > -1) { 
                storeable = _storageSlots[found].Peek(registrationNumber);
            }
            return storeable;
        }
        /// <summary>
        /// Finds a free storage slot for a specific size
        /// </summary>
        /// <param name="size">Size of the storeable item</param>
        /// <returns></returns>
        public int FindFreePlace(int size)
        {
            int freeSlot = -1;
            var freePlaces = FindFreeSlots(size);

            if (freePlaces.Count > 0)
            {
                // Select first
                // Prepared for in the future to prompt user to select one of the available places.
                freeSlot = freePlaces.First().SlotNumber;
            }
            else
            {
                throw new StorageToFullForStoreableException();
            }
            return freeSlot;
        }
        /// <summary>
        /// Moves a storeable to a new storage slot
        /// </summary>
        /// <param name="registrationNumber"></param>
        /// <param name="newPlace"></param>
        public void Move(string registrationNumber, int newPlace)
        {
            if(newPlace<0 || newPlace > _storageSlots.Length)
            {
                throw new ArgumentOutOfRangeException();
            }
            
            int slot = FindDistinctSlotNumber(registrationNumber);
            if (slot < 0)
            {
                throw new StoreableNotFoundException();
            }
            T storeable = Peek(registrationNumber);
            if (_storageSlots[newPlace].FreeSpaces(storeable.Size) < 1)
            {
                throw new StorageSlotToFullForStoreableException();
            }
            _storageSlots[slot].Remove(registrationNumber);
            _storageSlots[newPlace].Add(storeable);
        }

        /// <summary>
        /// Private enumerator for T
        /// </summary>
        /// <returns></returns>
        private IEnumerator<T> GetEnumerator()
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
            return (IEnumerator<StorageSlotDetail>) GetEnumerator();
        }

        IEnumerator<StorageSlotDetail> IEnumerable<StorageSlotDetail>.GetEnumerator()
        {
            foreach(StorageSlot<T> slot in _storageSlots)
            {
                StorageSlotDetail item = slot.GetSlotDetails();
                yield return item;
            }
        }
        public StorageItemDetail this[int index]
        {

            get
            {
                List<StorageItemDetail> storableReports = FindAll();
                if(index<0 || index > storableReports.Count)
                {
                    throw new IndexOutOfRangeException();
                }
                return storableReports[index];
            }
            // Should only be get able. No set functionality.
        }
        public override string ToString()
        {
            return (string.Format("Storage with {0}/{1} slots full.",_storageSlots.Length,OccupiedCount()));
        }

        public object Clone()
        {
            Storage<T> newStorage = new Storage<T>(_storageSlots.Length);
            newStorage._storageSlots = (StorageSlot<T>[])_storageSlots.Clone();
            return newStorage;
        }
    }
}
