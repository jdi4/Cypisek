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
        IEnumerable<MediaFile> GetFiles();
        void AddFile(Stream s, string fileName);
    }

    public class MediaStorageService : IMediaStorageService
    {
        private readonly string StorageDirPath;
        private readonly IMediaFileRepository mediaFileRepository;
        private readonly IUnitOfWork unitOfWork;

        public MediaStorageService(string storageDirPath, IMediaFileRepository mediaFileRepository, IUnitOfWork uow)
        {
            this.StorageDirPath = storageDirPath;
            this.mediaFileRepository = mediaFileRepository;
            this.unitOfWork = uow;
        }

        public void AddFile(Stream s, string fileName)
        {
            string path = Path.Combine(StorageDirPath,
                                       Path.GetFileName(fileName));

            var saveStream = File.Create(path);
            s.Seek(0, SeekOrigin.Begin);
            s.CopyTo(saveStream);
            saveStream.Close();

            mediaFileRepository.Add(new MediaFile()
            {
                Name = fileName,
                Path = path,
                Size = s.Length
            });
            unitOfWork.Commit();
        }

        public IEnumerable<MediaFile> GetFiles()
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
    }
}
