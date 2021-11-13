using System;
using System.IO;
using Backups.Classes;
using Backups.Services;
using NUnit.Framework;

namespace Backups.Tests
{
    public class BackupTest
    {
        private BackupManager _backupManager;

        [SetUp]
        public void Setup()
        {
            _backupManager = new BackupManager(@"../../../Repository");
        }

        [Test]
        public void CreateNewBackupJob_AddJobObjects()
        {
            var jobObject1 = new JobObject(new FileInfo(@"../../../../Backups.Tests/Files/FileA"));
            var jobObject2 = new JobObject(new FileInfo(@"../../../../Backups.Tests/Files/FileB"));
            _backupManager.AddObjectBackupJob(jobObject1);
            _backupManager.AddObjectBackupJob(jobObject2);
            _backupManager.RunBackup(new SplitStorages());
            _backupManager.RemoveJobObject(jobObject2);
            _backupManager.RunBackup(new SplitStorages());
            Assert.AreEqual(_backupManager.GetBackupJob().RestorePoints.Count, 2);
            Assert.AreEqual(_backupManager.GetBackupJob().RestorePoints[0].Storages.Count, 2);
            Assert.AreEqual(_backupManager.GetBackupJob().RestorePoints[1].Storages.Count, 1);
        }
    }
}