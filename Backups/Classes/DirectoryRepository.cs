using System.Collections.Generic;
using System.IO;
using Ionic.Zip;

namespace Backups.Classes
{
    public class DirectoryRepository : IRepository
    {
        public DirectoryRepository(string path)
        {
            RepositoryPath = path;
        }

        public string RepositoryPath { get; }

        public List<Storage> BackupLocalStorages(
            IAlgorithmCreateCopy algorithmCreateCopy,
            List<JobObject> jobObjects,
            string directoryName,
            uint id)
        {
            List<Storage> storages = algorithmCreateCopy.CreateStorages(jobObjects);
            var newStorages = new List<Storage>();
            var directoryInfo = new DirectoryInfo($@"{RepositoryPath}/{directoryName}");
            directoryInfo.Create();
            foreach (Storage storage in storages)
            {
                var zip = new ZipFile();
                var newStorage = new Storage();
                foreach (JobObject jobObject in storage.JobObjects)
                {
                    zip.AddFile($"{jobObject.JobObjectFile.DirectoryName}/{jobObject.JobObjectFile.Name}", "/");
                    var fileInfo = new FileInfo($@"{RepositoryPath}/
                        {directoryName}/storage_{id}.zip/{jobObject.JobObjectFile.Name}_{id}");
                    var newJobObject = new JobObject(fileInfo);
                    newStorage.JobObjects.Add(newJobObject);
                }

                newStorages.Add(newStorage);
                zip.Save($@"{RepositoryPath}/{directoryName}/storage_{storage.Id}.zip");
            }

            return newStorages;
        }

        public List<Storage> BackupVirtualStorages(
            IAlgorithmCreateCopy algorithmCreateCopy,
            List<JobObject> jobObjects,
            string directoryName,
            uint id)
        {
            List<Storage> storages = algorithmCreateCopy.CreateStorages(jobObjects);
            var newStorages = new List<Storage>();
            foreach (Storage storage in storages)
            {
                var newStorage = new Storage();
                foreach (JobObject jobObject in storage.JobObjects)
                {
                    var fileInfo = new FileInfo($@"{RepositoryPath}
                        /{directoryName}/storage_{storage.Id}.zip/{jobObject.JobObjectFile.Name}_{id}");
                    var newJobObject = new JobObject(fileInfo);
                    newStorage.JobObjects.Add(newJobObject);
                }

                newStorages.Add(newStorage);
            }

            return newStorages;
        }
    }
}