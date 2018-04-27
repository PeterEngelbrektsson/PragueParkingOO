using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCompany.Storage.Biz;
using MyOtherCompany.PragueParkingOO.Biz;
using MyOtherCompany.PragueParkingOO.Biz.Vehicles;

namespace MyCompany.PragueParkingOO.Biz
{
    public class ParkingPlaceOptimizer
    {
        /// <summary>
        /// Optimize one pakable of one specified size
        /// </summary>
        /// <param name="parkingPlace">Parking place</param>
        /// <param name="size">Size of Vehicle to optimize</param>
        /// <returns></returns>
        public OptimizeMovementDetail GetOneOptimizeInstruction(ParkingPlace parkingPlace, int size)
        {
            StorageOptimizer<Vehicle> optimizer = new StorageOptimizer<Vehicle>();
            return optimizer.GetOneOptimizeInstruction(parkingPlace.Storage, size);

        }

        /// <summary>
        /// Optimizes the parking place.
        /// Calls a recursive function that does the actual optimization.
        /// </summary>
        /// <param name="parkingPlace"></param>
        /// <returns>Instruction how to optimze the parking place.</returns>
        public List<OptimizeMovementDetail> GetOptimzeInstructionsModifying(ParkingPlace parkingPlace)
        {
            StorageOptimizer<Vehicle> optimizer = new StorageOptimizer<Vehicle>();
            return optimizer.GetOptimzeInstructionsModifying(parkingPlace.Storage);
            
        }
        /// <summary>
        /// Optimizes the parking place.
        /// Calls a function that does the actual optimization.
        /// The optimization is done on a copy of the parking place.
        /// The instructions to optimize is returned.
        /// </summary>
        /// <param name="storage"></param>
        /// <returns>Instruction how to optimze the parking place.</returns>
        public List<OptimizeMovementDetail> GetOptimzeInstructions(ParkingPlace parkingPlace)
        {
            StorageOptimizer<Vehicle> optimizer = new StorageOptimizer<Vehicle>();
            return optimizer.GetOptimzeInstructions(parkingPlace.Storage);
        }
        /// <summary>
        /// Do an optimization of the parking place.
        /// </summary>
        /// <param name="storage"></param>
        public void DoOptimization(ParkingPlace parkingPlace)
        {
            // Call the optimize function that modifies the parking place.
            GetOptimzeInstructionsModifying(parkingPlace);
        }
    }

}

