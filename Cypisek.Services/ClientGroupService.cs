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
        ClientGroup GetClientGroup(int id);
        void CreateClientGroup(ClientGroup ClientGroup);
        void SaveClientGroup();
    }

    public class ClientGroupService : IClientGroupService
    {
        private readonly IClientGroupRepository ClientGroupsRepository;
        private readonly IUnitOfWork unitOfWork;

        public ClientGroupService(IClientGroupRepository ClientGroupsRepository, IUnitOfWork unitOfWork)
        {
            this.ClientGroupsRepository = ClientGroupsRepository;
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

        public void SaveClientGroup()
        {
            unitOfWork.Commit();
        }

        #endregion

    }
}
