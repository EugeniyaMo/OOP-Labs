using System.Collections.Generic;
using Backups.Classes;

namespace BackupsExtra.ChooseAlgorithm
{
    public interface IChoosePoints
    {
        List<RestorePoint> GetPoints(BackupJob backupJob, object parameter);
    }
}