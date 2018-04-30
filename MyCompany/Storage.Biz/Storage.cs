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
    public class Storage<T> : IEnumerable, ICloneable where T : IStoreable
    {
        const int defaultSlotSize = 4;
        const int defaultSlotCount = 100;
        private StorageSlot<T>[] _storageSlots;
        public int MaxSizeOfStoredItems = 4;
        public int Length
        {
            get
            {
                return _storageSlots.Length;
            }
        }
        /// <summary>
        /// Constructor that takes a dictionary with slot sizes and counts
        /// </summary>
        /// <param name="SizeCountOfSlots">Size and number of slots</param>
        public Storage(Dictionary<int,int> SizeCountOfSlots)
        {
            int count = 0;
            // Start with counting the total number of slots
            foreach(var slotSet in SizeCountOfSlots)
            {
                count += slotSet.Value;
            }
            // Create slots array
            _storageSlots = new StorageSlot<T>[count];
            int i = 0;
            foreach(var slotSet in SizeCountOfSlots)
            {
                for(int j = 0; j < slotSet.Value; j++)
                {
                    _storageSlots[i] = new StorageSlot<T>(i, slotSet.Key);
                    i++;
                }
               
            }
        }
        /// <summary>
        /// Instanciates the storage with a number of slots of default sizes
        /// </summary>
        /// <param name="size"></param>
        public Storage(int size)
        {  
            _storageSlots = new StorageSlot<T>[size];
            for (int i = 0; i < size; i++)
            {
                _storageSlots[i] = new StorageSlot<T>(i,defaultSlotSize);
            }
         
        }
        /// <summary>
        /// Creates the storage with default slot count of default slot size
        /// </summary>
        public Storage()
        {
            _storageSlots = new StorageSlot<T>[defaultSlotCount];
            for (int i = 0; i < defaultSlotCount; i++)
            {
                _storageSlots[i] = new StorageSlot<T>(i, defaultSlotSize);
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
                throw new StorageToFullForStoreableException();
            }
            int availableSlotNumber = availableSlots.First().freePlace.SlotNumber;

            // set timestamp if not already set
            if (item.TimeStamp.Equals(default(DateTime)))
            {
                item.TimeStamp = DateTime.Now;
            }

            // store item in slot
            _storageSlots[availableSlotNumber].Add(item);

            return availableSlotNumber;
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
            foreach (StorageSlot<T> slot in _storageSlots)
            {
                matches.AddRange(slot.GetStorageItemDetailsReport()); // return all
            }
            return matches;
        }
   
        /// <summary>
        /// Returns reports for all slots
        /// </summary>
        /// <returns></returns>
        public List<StorageSlotDetail> FindAllSlots()
        {
            List<StorageSlotDetail> matches = new List<StorageSlotDetail>(); ;
            foreach (StorageSlotDetail item in this)
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
        /// Returns the content of all partially or fully free storage slot there a sspecific size fits
        /// </summary>
        /// <returns></returns>
        public List<StorageSlotDetail> FindFreeSlots(int size)
        {
            List<StorageSlotDetail> freeSlots = new List<StorageSlotDetail>();
            foreach (StorageSlotDetail item in this)
            {
                if (item.FreeSpace >= size)
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
        /// Returns report over the content of the storage place
        /// </summary>
        /// <returns></returns>
        public List<StorageSlotDetail> Occupied()
        {
            List<StorageSlotDetail> slots = new List<StorageSlotDetail>();
            foreach (StorageSlotDetail item in this)
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
            if (slotNumber < 0 || slotNumber > _storageSlots.Length)
            {
                throw new ArgumentException();
            }
            return _storageSlots[slotNumber].GetSlotDetails();
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
        /// Counts the number of partially  occupied storeable slots
        /// </summary>
        /// <returns></returns>
        public int PartiallyOccupiedCount()
        {
            var slots = PartiallyOccupied();
            return slots.Count();
        }

        /// <summary>
        /// Returns report over the paritally filled slots of the storage place
        /// </summary>
        /// <returns></returns>
        public List<StorageSlotDetail> PartiallyOccupied()
        {
            List<StorageSlotDetail> slots = new List<StorageSlotDetail>();
            foreach (StorageSlotDetail item in this)
            {
                if (item.OccupiedSpace > 0 && item.FreeSpace > 0)
                {
                    slots.Add(item); // only include partially occupied slots
                }
            }
            return slots;

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
            if (found > -1)
            {
                storeable = _storageSlots[found].Peek(registrationNumber);
            }
            else
            {
                throw new StoreableNotFoundException();
            }
            return storeable;
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


        IEnumerator IEnumerable.GetEnumerator()
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
            return (string.Format("Storage with {0}/{1} slots full.", OccupiedCount(),_storageSlots.Length));
        }

        public object Clone()
        {
            Storage<T> newStorage = new Storage<T>(_storageSlots.Length);
            for(int i=0;i<_storageSlots.Length;i++)
            {
                newStorage._storageSlots[i] = (StorageSlot<T>)_storageSlots[i].Clone();
            }
            
            return newStorage;
        }
    }
}
