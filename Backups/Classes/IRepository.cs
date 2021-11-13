using System.Collections.Generic;

namespace Backups.Classes
{
    public interface IRepository
    {
        List<Storage> BackupLocalStorages(
            IAlgorithmCreateCopy algorithmCreateCopy,
            List<JobObject> jobObjects,
            string directoryName,
            uint id);

        List<Storage> BackupVirtualStorages(
            IAlgorithmCreateCopy algorithmCreateCopy,
            List<JobObject> jobObjects,
            string directoryName,
            uint id);
    }
}