using Cypisek.Data.Infrastructure;
using Cypisek.Data.Repositories;
using Cypisek.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cypisek.Services
{
    public interface IMediaStorageService
    {
        IEnumerable<MediaFile> GetMediaFiles();
        MediaFile GetMediaFile(int fileID);
        void AddFile(Stream s, string fileName);
        bool DeleteFile(int fileID);

        void RefreshFileDB();

        IEnumerable<ClientScheduleMediaFilesList> GetMediaFileSchedules(int fileID);
    }

    public class MediaStorageService : IMediaStorageService
    {
        private readonly string StorageDirPath;
        private readonly IMediaFileRepository mediaFilesRepository;
        private readonly IClientScheduleMediaFilesListRepository filesToSchedulesRepository;
        private readonly IUnitOfWork unitOfWork;

        public MediaStorageService(string storageDirPath, IMediaFileRepository mediaFileRepository, IClientScheduleMediaFilesListRepository csmfl, IUnitOfWork uow)
        {
            this.StorageDirPath = storageDirPath;
            this.mediaFilesRepository = mediaFileRepository;
            this.filesToSchedulesRepository = csmfl;
            this.unitOfWork = uow;
        }

        public void RefreshFileDB()
        {
            var filesOnDisk = ReadFiles();
            var filesInDB = GetMediaFiles();

            mediaFilesRepository.Delete(f => true);

            //filesInDB.Except(filesOnDisk, )
            
            foreach (MediaFile f in filesOnDisk)
            {
                mediaFilesRepository.Add(f);
            }

            unitOfWork.Commit();
        }

        public void AddFile(Stream s, string fileName)
        {
            string path = Path.Combine(StorageDirPath,
                                       Path.GetFileName(fileName));

            var saveStream = File.Create(path);
            s.Seek(0, SeekOrigin.Begin);
            s.CopyTo(saveStream);
            saveStream.Close();

            mediaFilesRepository.Add(new MediaFile()
            {
                Name = fileName,
                Path = path,
                Size = s.Length
            });
            unitOfWork.Commit();
        }

        private IEnumerable<MediaFile> ReadFiles()
        {
            DirectoryInfo dir = new DirectoryInfo(StorageDirPath);
            var files = dir.GetFiles();

            List<MediaFile> mediaFiles = new List<MediaFile>();
            foreach (FileInfo fi in files)
            {
                mediaFiles.Add(
                    new MediaFile() { Name = fi.Name, Path = fi.FullName, Size = fi.Length }
                    );
            }

            return mediaFiles;
        }

        public IEnumerable<MediaFile> GetMediaFiles()
        {
            return mediaFilesRepository.GetAll();
        }

        public bool DeleteFile(int fileID)
        {
            var file = mediaFilesRepository.GetById(fileID);

            if (file != null)
            {
                if (file.ClientSchedulesList.Count > 0)
                    return false;
                else
                {
                    File.Delete(file.Path);
                    mediaFilesRepository.Delete(file);
                }

            }

            unitOfWork.Commit();
            return true;
        }

        public MediaFile GetMediaFile(int fileID)
        {
            return mediaFilesRepository.GetById(fileID);
        }

        public IEnumerable<ClientScheduleMediaFilesList> GetMediaFileSchedules(int fileID)
        {
            return filesToSchedulesRepository.GetManyIncludeSchedules(fs => fs.MediaFileID == fileID);
        }
    }
}
