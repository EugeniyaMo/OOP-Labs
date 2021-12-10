using System.Collections.Generic;
using System.IO;
using Backups.Classes;
using Backups.Services;
using BackupsExtra.ChooseAlgorithm;
using BackupsExtra.Services;
using NUnit.Framework;

namespace BackupsExtra.Tests
{
    public class BackupExtraTest
    {
        private BackupExtra _backupExtraManager;
        private BackupManager _backupManager;
        
        [SetUp]
        public void Setup()
        {
            _backupExtraManager = new BackupExtra(@"../../../Repository");
            _backupManager = new BackupManager(@"../../../Repository");
        }

        [Test]
        public void ChooseRestorePoints()
        {
            var jobObject1 = new JobObject(new FileInfo(@"../../../../BackupsExtra.Tests/Files/FileA"));
            var jobObject2 = new JobObject(new FileInfo(@"../../../../BackupsExtra.Tests/Files/FileB"));
            _backupManager.AddObjectBackupJob(jobObject1);
            _backupManager.AddObjectBackupJob(jobObject2);
            _backupManager.RunBackup(new SplitStorages());
            _backupManager.RemoveJobObject(jobObject2);
            _backupManager.RunBackup(new SplitStorages());
            _backupManager.AddObjectBackupJob(jobObject2);
            _backupManager.RunBackup(new SplitStorages());
            BackupJob backupJob = _backupManager.GetBackupJob();
            CountCleaner choosePoints = new CountCleaner();
            int limitCount = 2;
            List<RestorePoint> points = choosePoints.GetPoints(backupJob, limitCount);
            Assert.AreEqual(points.Count, limitCount);
        }

        [Test]
        public void SerializeBackupJob()
        {
            var jobObject1 = new JobObject(new FileInfo(@"../../../../BackupsExtra.Tests/Files/FileA"));
            var jobObject2 = new JobObject(new FileInfo(@"../../../../BackupsExtra.Tests/Files/FileB"));
            _backupManager.AddObjectBackupJob(jobObject1);
            _backupManager.AddObjectBackupJob(jobObject2);
            _backupManager.RunBackup(new SplitStorages());
            BackupJob backupJob = _backupManager.GetBackupJob();
            string path = @"../../../Repository/backupJob";
            _backupExtraManager.SaveBackupJob(backupJob, path);
            BackupJob returnBackupJob = _backupExtraManager.GetBackupJob(path);
            Assert.AreEqual(backupJob.JobObjects.Count, returnBackupJob.JobObjects.Count);
            Assert.AreEqual(backupJob.RestorePoints.Count, returnBackupJob.RestorePoints.Count);
        }

        [Test]
        public void CallLogger()
        {
            var jobObject1 = new JobObject(new FileInfo(@"../../../../Backups.Tests/Files/FileA"));
            var jobObject2 = new JobObject(new FileInfo(@"../../../../Backups.Tests/Files/FileB"));
            _backupExtraManager.AddObjectBackupJob(jobObject1);
            _backupExtraManager.AddObjectBackupJob(jobObject2);
            string path = "@../../../log.txt";
            Assert.IsNotEmpty(path);
        }
    }
}