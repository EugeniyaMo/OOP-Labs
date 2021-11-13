using System.IO;
using Backups.Classes;
using Backups.Services;

namespace Backups
{
    internal class Program
    {
        private static void Main()
        {
            var backupManager = new BackupManager(@"../../../Repository");
            var jobObject1 = new JobObject(new FileInfo(@"../../../../Backups.Tests/Files/FileA"));
            var jobObject2 = new JobObject(new FileInfo(@"../../../../Backups.Tests/Files/FileB"));
            backupManager.AddObjectBackupJob(jobObject1);
            backupManager.AddObjectBackupJob(jobObject2);
            backupManager.RunBackup(new SingleStorage());
        }
    }
}
