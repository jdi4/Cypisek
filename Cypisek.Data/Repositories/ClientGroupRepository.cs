using Cypisek.Data.Infrastructure;
using Cypisek.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cypisek.Data.Repositories
{
    public interface IClientGroupRepository : IRepository<ClientGroup>
    {

    }

    public class ClientGroupRepository : RepositoryBase<ClientGroup>, IClientGroupRepository
    {
        public ClientGroupRepository(IDbFactory dbFactory)
            : base(dbFactory) { }
    }
}
