using System;
using System.Collections.Generic;
using Backups.Classes;
using BackupsExtra.Tools;

namespace BackupsExtra.ChooseAlgorithm
{
    public class DateCleaner : IChoosePoints
    {
        public List<RestorePoint> GetPoints(BackupJob backupJob, object parameter)
        {
            DateTime limitDate = (DateTime)parameter;
            List<RestorePoint> returnRestorePoints = new List<RestorePoint>();
            foreach (var currentRestorePoint in backupJob.RestorePoints)
            {
                if (currentRestorePoint.RestorePointCreationTime < limitDate)
                    returnRestorePoints.Add(currentRestorePoint);
            }

            if (returnRestorePoints.Count == backupJob.RestorePoints.Count)
                throw new BackupExtraException("Error: Attempt to delete all restore points");
            return returnRestorePoints;
        }
    }
}