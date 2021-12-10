using System.IO;
using System.IO.Compression;
using System.Linq;
using Backups.Classes;

namespace BackupsExtra.Location
{
    public class DifferentLocation
    {
        public void RestoreFile(RestorePoint restorePoint, string path)
        {
            foreach (Storage currentStorage in restorePoint.Storages)
            {
                foreach (JobObject jobObject in currentStorage.JobObjects)
                {
                    if (File.Exists(jobObject.JobObjectFile.FullName))
                    {
                        File.Delete(jobObject.JobObjectFile.FullName);
                    }

                    foreach (string fileName in Directory.EnumerateFiles(restorePoint.RestorePointDirectory))
                    {
                        using (ZipArchive zipArchive = ZipFile.OpenRead(fileName))
                        {
                            zipArchive.Entries.FirstOrDefault(x => x.Name == jobObject.JobObjectFile.Name)?.
                                ExtractToFile(path);
                        }
                    }
                }
            }
        }
    }
}