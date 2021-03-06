﻿using System;
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
        private string manufacturer;
        public string Manufacturer
        {
            get
            {
                return manufacturer;
            }
            set
            {
                manufacturer = value;
            }
        }
        public Trike(string registrationnumber, string manufactuer) : base(registrationnumber,3,"Trike")
        {
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
        public override string ToString()
        {
            return string.Format("Trike with registration number {0} of Manufacturer {1}", RegistrationNumber, Manufacturer);
        }
    }
}
