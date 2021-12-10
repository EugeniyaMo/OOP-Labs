using Backups.Classes;
using BackupsExtra.ChooseAlgorithm;

namespace BackupsExtra.Services
{
    public interface IBackupExtra
    {
        void SaveBackupJob(BackupJob backupJob, string path);
        BackupJob GetBackupJob(string path);
        void AddObjectBackupJob(JobObject jobObject);
        void RemoveJobObject(JobObject jobObject);
        void RunBackup(IAlgorithmCreateCopy algorithmCreateCopy);
        void RemoveRestorePoint(RestorePoint restorePoint);
        void MergePoints(BackupJob backupJob, object parameter, IChoosePoints choosePoints);
    }
}