using System;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;
using Backups.Classes;
using BackupsExtra.Tools;

namespace BackupsExtra.ChooseAlgorithm
{
    public class HybridCleaner : IChoosePoints
    {
        public List<RestorePoint> GetPoints(BackupJob backupJob, object parameter)
        {
            (char hybridRule, int limitCount, DateTime limitDate) = ((char, int, DateTime))parameter;
            List<RestorePoint> returnRestorePoints = new List<RestorePoint>();
            switch (hybridRule)
            {
                case '&':
                    foreach (var currentRestorePoint in backupJob.RestorePoints)
                    {
                        if (backupJob.RestorePoints.Count < limitCount &&
                            currentRestorePoint.RestorePointCreationTime < limitDate)
                            returnRestorePoints.Add(currentRestorePoint);
                    }

                    break;
                case '|':
                    foreach (var currentRestorePoint in backupJob.RestorePoints)
                    {
                        if (backupJob.RestorePoints.Count < limitCount ||
                            currentRestorePoint.RestorePointCreationTime < limitDate)
                            returnRestorePoints.Add(currentRestorePoint);
                    }

                    break;
            }

            if (returnRestorePoints.Count == backupJob.RestorePoints.Count)
                throw new BackupExtraException("Error: Attempt to delete all restore points");
            return returnRestorePoints;
        }
    }
}