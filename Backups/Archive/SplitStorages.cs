using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace Backups.Classes
{
    public class SplitStorages : IAlgorithmCreateCopy
    {
        public List<Storage> CreateStorages(List<JobObject> jobObjects)
        {
            var storages = new List<Storage>();

            foreach (JobObject jobObject in jobObjects)
            {
                var storage = new Storage();
                storage.JobObjects.Add(jobObject);
                storages.Add(storage);
            }

            return storages;
        }
    }
}