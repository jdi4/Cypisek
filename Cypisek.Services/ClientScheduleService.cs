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
        void SaveClientSchedule();
    }

    public class ClientScheduleService : IClientScheduleService
    {
        private readonly IClientScheduleRepository ClientSchedulesRepository;
        private readonly IUnitOfWork unitOfWork;

        public ClientScheduleService(IClientScheduleRepository ClientSchedulesRepository, IUnitOfWork unitOfWork)
        {
            this.ClientSchedulesRepository = ClientSchedulesRepository;
            this.unitOfWork = unitOfWork;
        }

        #region IClientScheduleService Members

        public IEnumerable<ClientSchedule> GetClientSchedules()
        {
            var ClientSchedules = ClientSchedulesRepository.GetAll();
            return ClientSchedules;
        }

        public ClientSchedule GetClientSchedule(int id)
        {
            var ClientSchedule = ClientSchedulesRepository.GetById(id);
            return ClientSchedule;
        }

        public void CreateClientSchedule(ClientSchedule ClientSchedule)
        {
            ClientSchedulesRepository.Add(ClientSchedule);
        }

        public void SaveClientSchedule()
        {
            unitOfWork.Commit();
        }

        #endregion

    }
}
