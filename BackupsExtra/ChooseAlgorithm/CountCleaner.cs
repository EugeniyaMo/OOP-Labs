using System;
using System.Collections.Generic;
using Backups.Classes;
using BackupsExtra.Tools;

namespace BackupsExtra.ChooseAlgorithm
{
    public class CountCleaner : IChoosePoints
    {
        public List<RestorePoint> GetPoints(BackupJob backupJob, object parameter)
        {
            int limitCount = (int)parameter;
            List<RestorePoint> returnRestorePoints = new List<RestorePoint>();
            foreach (var currentRestorePoint in backupJob.RestorePoints)
            {
                if (backupJob.RestorePoints.IndexOf(currentRestorePoint) < limitCount)
                    returnRestorePoints.Add(currentRestorePoint);
            }

            if (returnRestorePoints.Count == backupJob.RestorePoints.Count)
                throw new BackupExtraException("Error: Attempt to delete all restore points");
            return returnRestorePoints;
        }
    }
}