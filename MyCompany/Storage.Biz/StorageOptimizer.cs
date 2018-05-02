using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.Storage.Biz
{
    public class StorageOptimizer<T> where T:IStoreable
    {
        /// <summary>
        /// Optimize one storable of one specified size
        /// </summary>
        /// <param name="storage">Parking place</param>
        /// <param name="size">Size of Vehicle to optimize</param>
        /// <returns></returns>
        public OptimizeMovementDetail GetOneOptimizeInstruction(Storage<T> storage, int size)
        {
             OptimizeMovementDetail instruction = new OptimizeMovementDetail(); 

             var freePlaces = storage.FindFreeSlots(size);

            //----------------------------------
            // find a place to move something to
            // sort on freespace, size and slotnr
            var availableSlots =
                 (from freePlace in freePlaces
                 orderby freePlace.FreeSpace ascending, freePlace.Size ascending, freePlace.SlotNumber ascending
                 where freePlace.FreeSpace != 0
                 select freePlace );

             // Smallest free place of size Size. 
             var firstStorageSlotToMoveTo = availableSlots.First();
             var storeablesToMoveTo = firstStorageSlotToMoveTo.StorageItemDetails;
            
             //----------------------------
             // Find a vehicle to double park or move to empty parking slot
             var storeablesToMoveFrom = storage.FindAll();
             var storageSlotsToMoveFrom = storage.FindAllSlots();
             // query sort on descening parking slot number
             // where size=size and free space = size
             var result = from slot in storageSlotsToMoveFrom
                          join storeable in storeablesToMoveFrom
                             on slot.SlotNumber equals storeable.StorageSlotNumber
                          select new { SlotNumner = slot.SlotNumber, slot.FreeSpace,
                              SizeOfStoreable = storeable.Size, SizeOfSlot = slot.Size,
                              RegistrationNumner = storeable.RegistrationNumber,
                               storeable.TimeStamp,storeable.TypeName};

              var found= from slotStorable in result
              orderby slotStorable.FreeSpace descending, slotStorable.SizeOfStoreable descending, slotStorable.SlotNumner descending
              where slotStorable.SizeOfStoreable == size & slotStorable.FreeSpace >= 1
              select slotStorable;
             if (found.Count() <= 0)
             {
                 // No optimization can be done
                 // break recursion
                 return instruction;
             }
             var lastofSizeN = found.First();
            if (lastofSizeN.SlotNumner.CompareTo(firstStorageSlotToMoveTo.SlotNumber) == 0)
            {
                // First and last n size are the same object
                // No optimization can be done
                // break recursion
                return instruction;

            }
            
            // Create a movement instruction
            instruction = new OptimizeMovementDetail
            {
                RegistrationNumber = lastofSizeN.RegistrationNumner,
                TimeStamp = lastofSizeN.TimeStamp,
                TypeName = lastofSizeN.TypeName,
                NewStorageSlotNumber = firstStorageSlotToMoveTo.SlotNumber,
                OldStorageSlotNumber = lastofSizeN.SlotNumner,
            };


            // Do the move in the test parking place
            storage.Move(instruction.RegistrationNumber, instruction.NewStorageSlotNumber);


             return instruction;
             
            throw new NotImplementedException();

        }

        /// <summary>
        /// Optimizes the parking place.
        /// Calls a recursive function that does the actual optimization.
        /// </summary>
        /// <param name="storage"></param>
        /// <returns>Instruction how to optimze the parking place.</returns>
        public List<OptimizeMovementDetail> GetOptimzeInstructionsModifying(Storage<T> storage)
        {
            int size = 1;
            List<OptimizeMovementDetail> instructions = new List<OptimizeMovementDetail>();
            bool loop = true;
            do
            {
                var freePlaces = storage.FindFreeSlots();
                // Query sort in ascending free space, descending parking slot number
                var availableSlots =
                from freePlace in freePlaces
                orderby freePlace.FreeSpace ascending, freePlace.Size ascending, freePlace.SlotNumber ascending
                where freePlace.FreeSpace != 0 & freePlace.FreeSpace >= size
                select new { freePlace.FreeSpace, freePlace.SlotNumber};

                if (availableSlots.Count() <= 0)
                {
                    // The parking slot is full
                    // break loop
                    break;
                }
 

                // Get one optimize instruction
                OptimizeMovementDetail myInstruction = GetOneOptimizeInstruction(storage,size);

                // No optimize instructions. break loop
                if (string.IsNullOrEmpty(myInstruction.RegistrationNumber))
                {
                    size++;
                    if (size > storage.MaxSizeOfStoredItems)
                    {
                        loop = false;
                    }
                }
                else
                {
                    instructions.Add(myInstruction);
                }

            } while (loop);


            return instructions;
        }
        /// <summary>
        /// Optimizes the parking place.
        /// Calls a function that does the actual optimization.
        /// The optimization is done on a copy of the parking place.
        /// The instructions to optimize is returned.
        /// </summary>
        /// <param name="storage"></param>
        /// <returns>Instruction how to optimze the parking place.</returns>
        public List<OptimizeMovementDetail> GetOptimzeInstructions(Storage<T> storage)
        {
            List<OptimizeMovementDetail> instructions = new List<OptimizeMovementDetail>();
            Storage<T> testStorageSpace = (Storage<T>)storage.Clone();

            instructions = GetOptimzeInstructionsModifying(testStorageSpace);

            return instructions;
        }
        /// <summary>
        /// Do an optimization of the parking place.
        /// </summary>
        /// <param name="storage"></param>
        public void DoOptimization(Storage<T> storage)
        {
            // Call the optimize function that modifies the parking place.
            GetOptimzeInstructionsModifying(storage);
        }
    }
}
