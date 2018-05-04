using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCompany.Storage.Biz;

namespace MyCompany.Storage.Biz
{
    /// <summary>
    /// Sort on storage slot number in ascending order.
    /// </summary>
    //public class StorageItemDetail_SortByStorageSlotAscendingOrder: Comparer<StorageItemDetail>  // Old name to long and with underscore.
    public class StorageItemDetailSortByStorageSlotAsc : Comparer<StorageItemDetail>
    {
            public override int Compare(StorageItemDetail x, StorageItemDetail y)
            {
                if (x.StorageSlotNumber < y.StorageSlotNumber) return -1;
                else if (x.StorageSlotNumber > y.StorageSlotNumber) return 1;
                else return x.RegistrationNumber.CompareTo(y.RegistrationNumber);
            }
        }
    /// <summary>
    /// Sort on storage slot number in descending order.
    /// </summary>
    public class StorageItemDetailSortByStorageSlotDesc : Comparer<StorageItemDetail>
    {
        public override int Compare(StorageItemDetail x, StorageItemDetail y)
        {
            if (x.StorageSlotNumber > y.StorageSlotNumber) return -1;
            else if (x.StorageSlotNumber < y.StorageSlotNumber) return 1;
            else return x.RegistrationNumber.CompareTo(y.RegistrationNumber);
        }
    }
    /// <summary>
    /// Sort on registratiomnumber  in ascending order.
    /// </summary>
    public class StorageItemDetailSortByRegNrAsc : Comparer<StorageItemDetail> 
    {
        public override int Compare(StorageItemDetail x, StorageItemDetail y)
        {
            if (x.RegistrationNumber.CompareTo(y.RegistrationNumber) != 0)
            {
                return x.RegistrationNumber.CompareTo(y.RegistrationNumber);
            }
            else
            {
                return x.StorageSlotNumber.CompareTo(y.StorageSlotNumber);
            }
            
        }
    }
    /// <summary>
    /// Sort on registratiomnumber in descending order.
    /// </summary>
    public class StorageItemDetailSortByRegNrDesc : Comparer<StorageItemDetail> 
    {
        public override int Compare(StorageItemDetail x, StorageItemDetail y)
        {
            if (x.RegistrationNumber.CompareTo(y.RegistrationNumber) != 0)
            {
                return -x.RegistrationNumber.CompareTo(y.RegistrationNumber);
            }
            else
            {
                return x.StorageSlotNumber.CompareTo(y.StorageSlotNumber);
            }
        }
    }
    /// <summary>
    /// Sort on timestamp in ascending order.
    /// </summary>
    public class StorageItemDetailSortByTimeStampAsc: Comparer<StorageItemDetail> 
    {
        public override int Compare(StorageItemDetail x, StorageItemDetail y)
        {
            if (x.TimeStamp < y.TimeStamp) return -1;
            else if (x.TimeStamp > y.TimeStamp) return 1;
            else return x.RegistrationNumber.CompareTo(y.RegistrationNumber);
        }
    }
    /// <summary>
    /// Sort on timestampin descending order.
    /// </summary>
    public class StorageItemDetailSortByTimeStampDesc : Comparer<StorageItemDetail> 
    {
        public override int Compare(StorageItemDetail x, StorageItemDetail y)
        {
            if (x.TimeStamp > y.TimeStamp) return -1;
            else if (x.TimeStamp < y.TimeStamp) return 1;
            else return x.RegistrationNumber.CompareTo(y.RegistrationNumber);
        }
    }
    /// <summary>
    /// Sort on Size in ascending order.
    /// </summary>
    public class StorageItemDetailSortBySizeAsc: Comparer<StorageItemDetail> 
    {
        public override int Compare(StorageItemDetail x, StorageItemDetail y)
        {
            if (x.Size < y.Size) return -1;
            else if (x.Size > y.Size) return 1;
            else return x.RegistrationNumber.CompareTo(y.RegistrationNumber); ;
        }
    }
    /// <summary>
    /// Sort on Size in descending order.
    /// </summary>
    public class StorageItemDetailSortBySizeDesc : Comparer<StorageItemDetail> 
    {
        public override int Compare(StorageItemDetail x, StorageItemDetail y)
        {
            if (x.Size > y.Size) return -1;
            else if (x.Size < y.Size) return 1;
            else return x.RegistrationNumber.CompareTo(y.RegistrationNumber); ;
        }
    }
    /// <summary>
    /// Sort on type name in ascending order.
    /// </summary>
    public class StorageItemDetailSortByTypeNameAsc : Comparer<StorageItemDetail> 
    {
        public override int Compare(StorageItemDetail x, StorageItemDetail y)
        {
            if (x.TypeName.CompareTo(y.TypeName) != 0)
            {
                return x.TypeName.CompareTo(y.TypeName);
            }
            else

            {
                return x.RegistrationNumber.CompareTo(y.RegistrationNumber);
            }
        }
    }
    /// <summary>
    /// Sort on type name in descending order.
    /// </summary>
    public class StorageItemDetailSortByTypeNameDesc : Comparer<StorageItemDetail> 
    {
        public override int Compare(StorageItemDetail x, StorageItemDetail y)
        {
            if (x.TypeName.CompareTo(y.TypeName) != 0)
            {
                return -x.TypeName.CompareTo(y.TypeName);
            }
            else

            {
                return x.RegistrationNumber.CompareTo(y.RegistrationNumber);
            }
        }
    }
}
