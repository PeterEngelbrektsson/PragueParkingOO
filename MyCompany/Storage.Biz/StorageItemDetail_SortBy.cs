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
        public class StorageItemDetail_SortByStorageSlotAscendingOrder<T> : IComparer<StorageItemDetail<T>> where T:IStoreable
        {
            public int Compare(StorageItemDetail<T> x, StorageItemDetail<T> y)
            {
                if (x.StorageSlotNumber < y.StorageSlotNumber) return -1;
                else if (x.StorageSlotNumber > y.StorageSlotNumber) return 1;
                else return 0;
            }
        }
    /// <summary>
    /// Sort on storage slot number in descending order.
    /// </summary>
    public class StorageItemDetail_SortByStorageSlotDescendingOrder<T> : IComparer<StorageItemDetail<T>> where T : IStoreable
    {
        public int Compare(StorageItemDetail<T> x, StorageItemDetail<T> y)
        {
            if (x.StorageSlotNumber > y.StorageSlotNumber) return -1;
            else if (x.StorageSlotNumber < y.StorageSlotNumber) return 1;
            else return 0;
        }
    }
    /// <summary>
    /// Sort on registratiomnumber  in ascending order.
    /// </summary>
    public class StorageItemDetail_SortByRegistrationNumberAscendingOrder<T> : IComparer<StorageItemDetail<T>> where T : IStoreable
    {
        public int Compare(StorageItemDetail<T> x, StorageItemDetail<T> y)
        {
            return x.RegistrationNumber.CompareTo(y.RegistrationNumber);
        }
    }
    /// <summary>
    /// Sort on registratiomnumber in descending order.
    /// </summary>
    public class StorageItemDetail_SortByRegistrationNumberDescendingOrder<T> : IComparer<StorageItemDetail<T>> where T : IStoreable
    {
        public int Compare(StorageItemDetail<T> x, StorageItemDetail<T> y)
        {
            return -x.RegistrationNumber.CompareTo(y.RegistrationNumber);
        }
    }
    /// <summary>
    /// Sort on timestamp in ascending order.
    /// </summary>
    public class StorageItemDetail_SortByTimeStampAscendingOrder<T> : IComparer<StorageItemDetail<T>> where T : IStoreable
    {
        public int Compare(StorageItemDetail<T> x, StorageItemDetail<T> y)
        {
            if (x.TimeStamp < y.TimeStamp) return -1;
            else if (x.TimeStamp > y.TimeStamp) return 1;
            else return 0;
        }
    }
    /// <summary>
    /// Sort on timestampin descending order.
    /// </summary>
    public class StorageItemDetail_SortByTimeStampDescendingOrder<T> : IComparer<StorageItemDetail<T>> where T : IStoreable
    {
        public int Compare(StorageItemDetail<T> x, StorageItemDetail<T> y)
        {
            if (x.TimeStamp > y.TimeStamp) return -1;
            else if (x.TimeStamp < y.TimeStamp) return 1;
            else return 0;
        }
    }
    /// <summary>
    /// Sort on Size in ascending order.
    /// </summary>
    public class StorageItemDetail_SortBySizeAscendingOrder<T> : IComparer<StorageItemDetail<T>> where T : IStoreable
    {
        public int Compare(StorageItemDetail<T> x, StorageItemDetail<T> y)
        {
            if (x.Size < y.Size) return -1;
            else if (x.Size > y.Size) return 1;
            else return 0;
        }
    }
    /// <summary>
    /// Sort on Size in descending order.
    /// </summary>
    public class StorageItemDetail_SortBySizeDescendingOrder<T> : IComparer<StorageItemDetail<T>> where T : IStoreable
    {
        public int Compare(StorageItemDetail<T> x, StorageItemDetail<T> y)
        {
            if (x.Size > y.Size) return -1;
            else if (x.Size < y.Size) return 1;
            else return 0;
        }
    }
    /// <summary>
    /// Sort on type name in ascending order.
    /// </summary>
    public class StorageItemDetail_SortByTypeNameAscendingOrder<T> : IComparer<StorageItemDetail<T>> where T : IStoreable
    {
        public int Compare(StorageItemDetail<T> x, StorageItemDetail<T> y)
        {
            return x.TypeName.CompareTo(y.TypeName);
        }
    }
    /// <summary>
    /// Sort on type name in descending order.
    /// </summary>
    public class StorageItemDetail_SortByTypeNameDescendingOrder<T> : IComparer<StorageItemDetail<T>> where T : IStoreable
    {
        public int Compare(StorageItemDetail<T> x, StorageItemDetail<T> y)
        {
            return -x.TypeName.CompareTo(y.TypeName);
        }
    }
}
