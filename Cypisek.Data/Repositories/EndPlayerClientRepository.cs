using Cypisek.Data.Infrastructure;
using Cypisek.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cypisek.Data.Repositories
{
    public interface IEndPlayerClientRepository : IRepository<EndPlayerClient>
    {

    }

    public class EndPlayerClientRepository : RepositoryBase<EndPlayerClient>, IEndPlayerClientRepository
    {
        public EndPlayerClientRepository(IDbFactory dbFactory)
            : base(dbFactory) { }
    }
}
