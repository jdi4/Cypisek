using Cypisek.Data.Infrastructure;
using Cypisek.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cypisek.Data.Repositories
{
    public interface IClientGroupRepository : IRepository<ClientGroup>
    {
        IEnumerable<ClientGroup> GetAllIncludeClients();
    }

    public class ClientGroupRepository : RepositoryBase<ClientGroup>, IClientGroupRepository
    {
        public ClientGroupRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        public IEnumerable<ClientGroup> GetAllIncludeClients()
        {
            return dbSet.Include(cg => cg.EndPlayerClients).ToList();
        }
    }
}
