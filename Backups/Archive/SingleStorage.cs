using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Threading;

namespace Backups.Classes
{
    public class SingleStorage : IAlgorithmCreateCopy
    {
        public List<Storage> CreateStorages(List<JobObject> jobObjects)
        {
            var storages = new List<Storage>();
            var storage = new Storage();
            foreach (JobObject jobObject in jobObjects)
            {
                storage.JobObjects.Add(jobObject);
            }

            storages.Add(storage);
            return storages;
        }
    }
}