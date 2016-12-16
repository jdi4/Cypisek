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
        void SaveEndPlayerClient();
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

        public EndPlayerClient GetEndPlayerClient(int id)
        {
            return EndPlayerClientsRepository.GetById(id);
        }

        public IEnumerable<EndPlayerClient> GetEndPlayerClients()
        {
            throw new NotImplementedException();
        }

        public void SaveEndPlayerClient()
        {
            unitOfWork.Commit();
        }
    }
}
