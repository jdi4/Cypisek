using Cypisek.Models;
using Cypisek.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cypisek.Data.Infrastructure;

namespace Cypisek.Services
{
    public interface IClientScheduleService
    {
        IEnumerable<ClientSchedule> GetClientSchedules();
        ClientSchedule GetClientSchedule(int id);
        void CreateClientSchedule(ClientSchedule ClientSchedule);
        void CreateClientSchedule(ClientSchedule ClientSchedule, IEnumerable<int> filesIDs);
        void SaveClientSchedule();
    }

    public class ClientScheduleService : IClientScheduleService
    {
        private readonly IClientScheduleRepository clientSchedulesRepository;
        private readonly IClientScheduleMediaFilesListRepository clientScheduleMediaFilesListRepository;
        private readonly IUnitOfWork unitOfWork;

        public ClientScheduleService(IClientScheduleRepository csR, IClientScheduleMediaFilesListRepository csmfR, IUnitOfWork unitOfWork)
        {
            this.clientSchedulesRepository = csR;
            this.clientScheduleMediaFilesListRepository = csmfR;
            this.unitOfWork = unitOfWork;
        }

        #region IClientScheduleService Members

        public IEnumerable<ClientSchedule> GetClientSchedules()
        {
            var ClientSchedules = clientSchedulesRepository.GetAll();
            return ClientSchedules;
        }

        public ClientSchedule GetClientSchedule(int id)
        {
            var ClientSchedule = clientSchedulesRepository.GetById(id);
            return ClientSchedule;
        }

        public void CreateClientSchedule(ClientSchedule ClientSchedule)
        {
            //ClientSchedulesRepository.Add(ClientSchedule);
        }

        public void SaveClientSchedule()
        {
            unitOfWork.Commit();
        }

        public void CreateClientSchedule(ClientSchedule ClientSchedule, IEnumerable<int> filesIDs)
        {
            clientSchedulesRepository.Add(ClientSchedule);

            foreach (int i in filesIDs)
            {
                clientScheduleMediaFilesListRepository.Add( new ClientScheduleMediaFilesList()
                {
                    ClientScheduleID = ClientSchedule.ID,
                    MediaFileID = i,
                    PlayTime = 10
                });
            }
        }

        #endregion

    }
}
