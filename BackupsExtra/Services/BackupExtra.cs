using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using Backups.Classes;
using Backups.Services;
using BackupsExtra.ChooseAlgorithm;
using BackupsExtra.Logger;
using Newtonsoft.Json;

namespace BackupsExtra.Services
{
    public class BackupExtra : IBackupExtra
    {
        private BackupManager _backupManager = new BackupManager(@"../../../Repository");
        private ConsoleLogger _logger = new ConsoleLogger();
        public BackupExtra(string repositoryPath)
        {
            BackupManager backupManager = new BackupManager(repositoryPath);
        }

        public void SaveBackupJob(BackupJob backupJob, string path)
        {
            string data = JsonConvert.SerializeObject(backupJob, new BackupJsonConverter());
            FileStream fileStream = File.OpenWrite(path);
            fileStream.Write(System.Text.Encoding.Default.GetBytes(data));
            fileStream.Close();
        }

        public BackupJob GetBackupJob(string path)
        {
            return JsonConvert.DeserializeObject<BackupJob>(File.ReadAllText(path), new BackupJsonConverter());
        }

        public void AddObjectBackupJob(JobObject jobObject)
        {
            _backupManager.AddObjectBackupJob(jobObject);
            _logger.SendMessage("Add new JobObject in BackupJob");
        }

        public void RemoveJobObject(JobObject jobObject)
        {
            _backupManager.RemoveJobObject(jobObject);
            _logger.SendMessage("Remove JobObject from BackupJob");
        }

        public void RunBackup(IAlgorithmCreateCopy algorithmCreateCopy)
        {
            _backupManager.RunBackup(algorithmCreateCopy);
            _logger.SendMessage("Backup runs");
        }

        public void RemoveRestorePoint(RestorePoint restorePoint)
        {
            _backupManager.RemoveRestorePoint(restorePoint);
            _logger.SendMessage("Remove RestorePoint from BackupJob");
        }

        public void MergePoints(BackupJob backupJob, object parameter, IChoosePoints choosePoints)
        {
            List<RestorePoint> points = choosePoints.GetPoints(backupJob, parameter);
            if (points.Count != backupJob.RestorePoints.Count)
            {
                foreach (var currentRestorePoint in backupJob.RestorePoints)
                {
                    if (!points.Contains(currentRestorePoint))
                    {
                        if (currentRestorePoint.Storages.Count == 1)
                        {
                            backupJob.RestorePoints.Remove(currentRestorePoint);
                        }
                        else if (currentRestorePoint.Storages.Any() && points.Last().Storages.Any())
                        {
                            backupJob.RestorePoints.Remove(currentRestorePoint);
                        }
                        else if (currentRestorePoint.Storages.Any() && !points.Last().Storages.Any())
                        {
                            points.Last().Storages.AddRange(currentRestorePoint.Storages);
                            backupJob.RestorePoints.Remove(currentRestorePoint);
                        }
                    }
                }
            }
        }
    }
}