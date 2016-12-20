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
    public interface IEndPlayerClientService
    {
        IEnumerable<EndPlayerClient> GetEndPlayerClients();
        EndPlayerClient GetEndPlayerClient(int id);
        void CreateEndPlayerClient(EndPlayerClient EndPlayerClient);
        void EditEndPlayerClient(EndPlayerClient endPlayerClient);
        void SaveEndPlayerClient();

        IEnumerable<EndPlayerClient> GetEndPlayerClientsWithoutGroup();
    }

    public class EndPlayerClientService : IEndPlayerClientService
    {
        private readonly IEndPlayerClientRepository EndPlayerClientsRepository;
        private readonly IUnitOfWork unitOfWork;

        public EndPlayerClientService(IEndPlayerClientRepository endPlayerClientsRepository, IUnitOfWork unitOfWork)
        {
            this.EndPlayerClientsRepository = endPlayerClientsRepository;
            this.unitOfWork = unitOfWork;
        }

        public void CreateEndPlayerClient(EndPlayerClient EndPlayerClient)
        {
            EndPlayerClientsRepository.Add(EndPlayerClient);
        }

        public void EditEndPlayerClient(EndPlayerClient endPlayerClient)
        {
            EndPlayerClientsRepository.Edit(endPlayerClient);
        }

        public EndPlayerClient GetEndPlayerClient(int id)
        {
            return EndPlayerClientsRepository.GetById(id);
        }

        public IEnumerable<EndPlayerClient> GetEndPlayerClients()
        {
            var clients = EndPlayerClientsRepository.GetAll();
            return clients;
        }

        public IEnumerable<EndPlayerClient> GetEndPlayerClientsWithoutGroup()
        {
            var noGroupClients = EndPlayerClientsRepository.GetMany(c => c.ClientGroupID == null);
            return noGroupClients;
        }

        public void SaveEndPlayerClient()
        {
            unitOfWork.Commit();
        }
    }
}
