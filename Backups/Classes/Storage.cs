using System;
using System.Collections.Generic;
using System.IO;

namespace Backups.Classes
{
    public class Storage
    {
        private static uint _id;
        public Storage()
        {
            Id = ++_id;
            JobObjects = new List<JobObject>();
        }

        public Storage(string storageName, List<JobObject> jobObjects)
        {
            StorageName = storageName;
            JobObjects = jobObjects;
        }

        public uint Id { get; }
        public string StorageName { get; }
        public List<JobObject> JobObjects { get; }
        public static string RenameFile(string fileName, uint fileNumber)
        {
            return $"{fileName}_{fileNumber}";
        }
    }
}