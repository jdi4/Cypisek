using Cypisek.Data.Infrastructure;
using Cypisek.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cypisek.Data.Repositories
{
    public interface IClientScheduleRepository : IRepository<ClientSchedule>
    {

    }

    public class ClientScheduleRepository : RepositoryBase<ClientSchedule>, IClientScheduleRepository
    {
        public ClientScheduleRepository(IDbFactory dbFactory)
            : base(dbFactory) { }
    }
}
