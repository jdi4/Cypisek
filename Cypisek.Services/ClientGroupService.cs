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
    public interface IClientGroupService
    {
        IEnumerable<ClientGroup> GetClientGroups();
        IEnumerable<ClientGroup> GetClientGroupsIncludeClients();
        ClientGroup GetClientGroup(int id);
        void CreateClientGroup(ClientGroup ClientGroup);
        void DeleteClientGroup(int id);
        void SaveChanges();
    }

    public class ClientGroupService : IClientGroupService
    {
        private readonly IClientGroupRepository ClientGroupsRepository;
        private readonly IEndPlayerClientRepository endPlayerClientRepository;
        private readonly IUnitOfWork unitOfWork;

        public ClientGroupService(IClientGroupRepository ClientGroupsRepository, IEndPlayerClientRepository endPlayerClientRepository, IUnitOfWork unitOfWork)
        {
            this.ClientGroupsRepository = ClientGroupsRepository;
            this.endPlayerClientRepository = endPlayerClientRepository;
            this.unitOfWork = unitOfWork;
        }

        #region IClientGroupService Members

        public IEnumerable<ClientGroup> GetClientGroups()
        {
            var ClientGroups = ClientGroupsRepository.GetAll();
            return ClientGroups;
        }

        public ClientGroup GetClientGroup(int id)
        {
            var ClientGroup = ClientGroupsRepository.GetById(id);
            return ClientGroup;
        }

        public void CreateClientGroup(ClientGroup ClientGroup)
        {
            ClientGroupsRepository.Add(ClientGroup);
        }

        public void SaveChanges()
        {
            unitOfWork.Commit();
        }

        public IEnumerable<ClientGroup> GetClientGroupsIncludeClients()
        {
            return ClientGroupsRepository.GetAllIncludeClients();
        }

        public void DeleteClientGroup(int id)
        {
            var clientGroup = ClientGroupsRepository.GetById(id);
            clientGroup.EndPlayerClients.ToList().ForEach(c => 
                {
                    c.ClientGroupID = null;
                    endPlayerClientRepository.Edit(c);
                });
            ClientGroupsRepository.Delete(clientGroup);
        }

        #endregion

    }
}
