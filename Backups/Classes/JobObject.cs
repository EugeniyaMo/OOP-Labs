using System.IO;

namespace Backups.Classes
{
    public class JobObject
    {
        public JobObject(FileInfo jobObjectFile)
        {
            JobObjectFile = jobObjectFile;
        }

        public FileInfo JobObjectFile { get; }
    }
}