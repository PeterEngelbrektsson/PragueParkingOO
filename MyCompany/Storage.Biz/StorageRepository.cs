using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.Storage.Biz
{
    public class StorageRepository<T> where T:IStoreable
    {
        /// <summary>
        /// Saves the storage to file
        /// </summary>
        /// <param name="storage"></param>
        /// <param name="fileName"></param>
        public static void SaveToFile(Storage<T> storage, string fileName)
        {

            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(fileName,
                                     FileMode.Create,
                                     FileAccess.Write, FileShare.None))
            {
                formatter.Serialize(stream, storage);
                stream.Close();
            }
        }
        /// <summary>
        /// Loads the storage from file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static Storage<T> LoadFromFile(string fileName)
        {
            Storage<T> storage;
            IFormatter formatter = new BinaryFormatter();
            using (Stream fromStream = new FileStream(fileName,
                                       FileMode.Open,
                                       FileAccess.Read,
                                       FileShare.Read))
            {
                storage = (Storage<T>)formatter.Deserialize(fromStream);
                fromStream.Close();
            }
            return storage;
        }
    }
}

