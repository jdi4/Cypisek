using Cypisek.Data.Infrastructure;
using Cypisek.Data.Repositories;
using Cypisek.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cypisek.Services
{
    public interface IMediaFileService
    {
        IEnumerable<MediaFile> GetMediaFiles();
        MediaFile GetMediaFile(int id);
        void CreateMediaFile(MediaFile MediaFile);
        void SaveMediaFile();
    }

    public class MediaFileService : IMediaFileService
    {
        private readonly IMediaFileRepository MediaFilesRepository;
        private readonly IUnitOfWork unitOfWork;

        public MediaFileService(IMediaFileRepository MediaFilesRepository, IUnitOfWork unitOfWork)
        {
            this.MediaFilesRepository = MediaFilesRepository;
            this.unitOfWork = unitOfWork;
        }

        public void CreateMediaFile(MediaFile MediaFile)
        {
            MediaFilesRepository.Add(MediaFile);
        }

        public MediaFile GetMediaFile(int id)
        {
            return MediaFilesRepository.GetById(id);
        }

        public IEnumerable<MediaFile> GetMediaFiles()
        {
            return MediaFilesRepository.GetAll();
        }

        public void SaveMediaFile()
        {
            unitOfWork.Commit();
        }
    }
}
