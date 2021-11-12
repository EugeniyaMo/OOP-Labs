using System;
using System.Collections.Generic;
using System.IO;
using Backups.Classes;

namespace Backups.Services
{
    public class BackupManager : IBackup
    {
        private DirectoryRepository _repository;
        private BackupJob _backupJob = new BackupJob();

        public BackupManager(string repositoryPath)
        {
            _repository = new DirectoryRepository(repositoryPath);
        }

        public JobObject AddObjectBackupJob(JobObject jobObject)
        {
            _backupJob.JobObjects.Add(jobObject);
            return jobObject;
        }

        public void RemoveJobObject(JobObject jobObject)
        {
            _backupJob.JobObjects.Remove(jobObject);
        }

        public void RunBackup(IAlgorithmCreateCopy algorithmCreateCopy)
        {
            var restorePoint = new RestorePoint();
            string newDirectory = @$"Backup_{restorePoint.Id}";
            restorePoint.RestorePointDirectory = newDirectory;
            List<Storage> storages = _repository.BackupLocalStorages(algorithmCreateCopy, _backupJob.JobObjects, newDirectory, restorePoint.Id);
            restorePoint.Storages.AddRange(storages);
            _backupJob.RestorePoints.Add(restorePoint);
        }

        public RestorePoint RemoveRestorePoint(RestorePoint restorePoint)
        {
            _backupJob.RestorePoints.Remove(restorePoint);
            return restorePoint;
        }

        public BackupJob GetBackupJob()
        {
            return _backupJob;
        }
    }
}