using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.Storage.Biz
{
    /// <summary>
    /// If the registration number already exists
    /// </summary>
    public class RegistrationNumberAlreadyExistsException : Exception
    {

        const string message = "The registration number already exists.";
        public RegistrationNumberAlreadyExistsException() : base(message) { }
        public RegistrationNumberAlreadyExistsException(string msg) : base(msg) { }

    }
    /// <summary>
    /// Exception in case a storable item is not found where it's expected to be.
    /// </summary>
    public class StoreableNotFoundException : Exception
    {

        const string message = "The storable item could not be found.";
        public StoreableNotFoundException() : base(message) { }
        public StoreableNotFoundException(string msg) : base(msg) { }

    }
    /// <summary>
    /// Exception in case the storage slot is to full for a item to be stored in it.
    /// /// The storage can still have room for a smaller items
    /// </summary>
    public class StorageSlotToFullForStoreableException : Exception
    {

        const string message = "The stotage has not room for the item";
        public StorageSlotToFullForStoreableException() : base(message) { }
        public StorageSlotToFullForStoreableException(string msg) : base(msg) { }

    }
    /// <summary>
    /// Exception in case the storage is to full for a item to be stored in it.
    /// /// The storage can still have room for a smaller items
    /// </summary>
    public class StorageToFullForStoreableException : Exception
    {

        const string message = "The parkingplace has not room for the vehicle";
        public StorageToFullForParkableException() : base(message) { }
        public StorageToFullForParkableException(string msg) : base(msg) { }

    }
    public class RegistrationNumberInvalid : Exception
    {

        const string message = "The registration number is not valid.";
        public RegistrationNumberInvalid() : base(message) { }
        public RegistrationNumberInvalid(string msg) : base(msg) { }

    }
}
