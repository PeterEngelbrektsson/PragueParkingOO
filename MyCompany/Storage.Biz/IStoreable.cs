﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.Storage.Biz
{
    public interface IStoreable
    {
        int Size { get; }                           // Size of stored thing
        string RegistrationNumber { get; set; }     // ID for the stored thing
        DateTime TimeStamp { get; }                 // Timestamp set at time the stored thing is checked in to the storage place
        string Description { get;  }                // Description to help find the stored thing
    }
}
