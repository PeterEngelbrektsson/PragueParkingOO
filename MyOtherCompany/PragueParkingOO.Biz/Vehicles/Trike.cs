using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCompany.Storage.Biz;

namespace MyOtherCompany.PragueParkingOO.Biz.Vehicles
{
    /// <summary>
    /// A Trike that can be stored.
    /// </summary>
    [Serializable]
    public class Trike : Vehicle
    {
        public Trike():base()
        {
            Size = 3;
            TypeName = "Trike";
        }
        private string _manufacturer;
        public string Manufacturer
        {
            get
            {
                return _manufacturer;
            }
            set
            {
                _manufacturer = value;
            }
        }
        public Trike(string registrationnumber, string manufactuer) : base()
        {
            this.Size = 3;
            this.TypeName = "Trike";
            this.RegistrationNumber = registrationnumber;
            this.Manufacturer = manufactuer;
        }
        public override object Clone()
        {
            Trike newTrike = new Trike
            {
                Size = this.Size,
                RegistrationNumber = this.RegistrationNumber,
                TypeName = this.TypeName,
                TimeStamp = this.TimeStamp
            };
            return newTrike;
        }
        public override string Description
        {
            get
            {
                return string.Format("Trike with registration number {0} of Manufacturer {1}", RegistrationNumber, Manufacturer);
            }
        }
    }
}
