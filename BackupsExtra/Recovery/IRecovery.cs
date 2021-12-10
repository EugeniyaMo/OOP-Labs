using Backups.Classes;

namespace BackupsExtra.Location
{
    public interface IRecovery
    {
        void RestoreFile(RestorePoint restorePoint, string path);
    }
}