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
        IEnumerable<EndPlayerClient> GetEndPlayerClientsIncludeGroups();
    }

    public class EndPlayerClientService : IEndPlayerClientService
    {
        private readonly IEndPlayerClientRepository endPlayerClientRepository;
        private readonly IUnitOfWork unitOfWork;

        public EndPlayerClientService(IEndPlayerClientRepository endPlayerClientsRepository, IUnitOfWork unitOfWork)
        {
            this.endPlayerClientRepository = endPlayerClientsRepository;
            this.unitOfWork = unitOfWork;
        }

        public void CreateEndPlayerClient(EndPlayerClient EndPlayerClient)
        {
            endPlayerClientRepository.Add(EndPlayerClient);
        }

        public void EditEndPlayerClient(EndPlayerClient endPlayerClient)
        {
            endPlayerClientRepository.Edit(endPlayerClient);
        }

        public EndPlayerClient GetEndPlayerClient(int id)
        {
            return endPlayerClientRepository.GetById(id);
        }

        public IEnumerable<EndPlayerClient> GetEndPlayerClients()
        {
            var clients = endPlayerClientRepository.GetAll();
            return clients;
        }

        public IEnumerable<EndPlayerClient> GetEndPlayerClientsIncludeGroups()
        {
            //endPlayerClientRepository.GetAll()
            throw new NotImplementedException();
        }

        public IEnumerable<EndPlayerClient> GetEndPlayerClientsWithoutGroup()
        {
            var noGroupClients = endPlayerClientRepository.GetMany(c => c.ClientGroupID == null);
            return noGroupClients;
        }

        public void SaveEndPlayerClient()
        {
            unitOfWork.Commit();
        }
    }
}
