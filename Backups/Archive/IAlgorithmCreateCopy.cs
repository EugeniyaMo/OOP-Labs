using System.Collections.Generic;

namespace Backups.Classes
{
    public interface IAlgorithmCreateCopy
    {
        List<Storage> CreateStorages(List<JobObject> objects);
    }
}