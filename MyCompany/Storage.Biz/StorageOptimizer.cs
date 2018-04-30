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
        /// Optimize one pakable of one specified size
        /// </summary>
        /// <param name="storage">Parking place</param>
        /// <param name="size">Size of Vehicle to optimize</param>
        /// <returns></returns>
        public OptimizeMovementDetail GetOneOptimizeInstruction(Storage<T> storage, int size)
        {
             OptimizeMovementDetail instruction = new OptimizeMovementDetail(); 

             var freePlaces = storage.FindFreeSlots(size);

            var availableSlots =
                 (from freePlace in freePlaces
                 orderby freePlace.FreeSpace ascending, freePlace.Size ascending, freePlace.SlotNumber ascending
                 where freePlace.FreeSpace != 0
                 select freePlace );

             // Smallest free place is of size Size. 

             // Try to find optimization.
             // Find and do one optimization
             var firstStorageSlotToMoveTo = availableSlots.First();
             var storeablesToMoveTo = firstStorageSlotToMoveTo.StorageItemDetails;
             if (storeablesToMoveTo.Count() == 0)
             {
                 // Empty parking slot
                 // break
                 return instruction;
             }
             var ToDoublePark = storeablesToMoveTo[0]; // There can only be one onesize parked here in this version.

             var storeablesToMoveFrom = storage.FindAll();
             var storageSlotsToMoveFrom = storage.FindAllSlots();
             // query sort on descening parking slot number
             // where size=size and free space = size
             var result = from slot in storageSlotsToMoveFrom
                          join storeable in storeablesToMoveFrom
                             on slot.SlotNumber equals storeable.StorageSlotNumber
                          select new { SlotNumner = slot.SlotNumber, FreeSpace = slot.FreeSpace,
                              SizeOfStoreable = storeable.Size, SizeOfSlot = slot.Size,
                              RegistrationNumner = storeable.RegistrationNumber,
                              TimeStamp = storeable.TimeStamp, TypeName=storeable.TypeName,
                              Description =storeable.Description};

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
             if (lastofSizeN.SlotNumner.CompareTo(ToDoublePark.StorageSlotNumber) == 0)
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
                NewStorageSlotNumber = ToDoublePark.StorageSlotNumber,
                OldStorageSlotNumber = lastofSizeN.SlotNumner,
                Description = lastofSizeN.Description
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
            List<OptimizeMovementDetail> instructions = new List<OptimizeMovementDetail>();
//            Dictionary<ParkingSlot, int> freePlaces;
            bool loop = true;
            do
            {
                var freePlaces = storage.FindFreeSlots();
                // Query sort in ascending free space, descending parking slot number
                var availableSlots =
                from freePlace in freePlaces
                orderby freePlace.FreeSpace ascending, freePlace.Size ascending, freePlace.SlotNumber ascending
                where freePlace.FreeSpace != 0
                select new { freePlace.FreeSpace, freePlace.SlotNumber};

                if (availableSlots.Count() <= 0)
                {
                    // The parking slot is full
                    // break loop
                    break;
                }
                if (availableSlots.First().FreeSpace== storage.MaxSize)
                {
                    // Size= size of car, the largest parkable allowed
                    // The parking slot is optimized
                    // break loop
                    break;
                }

                // Get one optimize instruction
                OptimizeMovementDetail myInstruction = GetOneOptimizeInstruction(storage, availableSlots.First().FreeSpace);

                // No omptize instructions. break loop
                if (string.IsNullOrEmpty(myInstruction.RegistrationNumber))
                {
                    loop = false;
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
