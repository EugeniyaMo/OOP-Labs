using Backups.Classes;

namespace Backups.Services
{
    public interface IBackup
    {
        JobObject AddObjectBackupJob(JobObject jobObject);
        void RemoveJobObject(JobObject jobObject);
        void RunBackup(IAlgorithmCreateCopy algorithmCreateCopy);
        RestorePoint RemoveRestorePoint(RestorePoint restorePoint);
        BackupJob GetBackupJob();
    }
}