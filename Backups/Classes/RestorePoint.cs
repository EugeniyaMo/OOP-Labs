using System;
using System.Collections.Generic;
using System.IO;

namespace Backups.Classes
{
    public class RestorePoint
    {
        private static uint _id;
        public RestorePoint()
        {
            Id = ++_id;
            RestorePointCreationTime = DateTime.Now;
            Storages = new List<Storage>();
        }

        public uint Id { get; }
        public DateTime RestorePointCreationTime { get; }
        public List<Storage> Storages { get; }
        public string RestorePointDirectory { get; set; }
    }
}