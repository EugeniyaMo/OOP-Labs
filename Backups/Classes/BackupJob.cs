using System.Collections.Generic;
using System.IO;

namespace Backups.Classes
{
    public class BackupJob
    {
        public BackupJob()
        {
            JobObjects = new List<JobObject>();
            RestorePoints = new List<RestorePoint>();
        }

        public List<JobObject> JobObjects { get; }
        public List<RestorePoint> RestorePoints { get; }
    }
}