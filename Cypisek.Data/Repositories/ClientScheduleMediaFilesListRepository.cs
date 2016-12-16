using Cypisek.Data.Infrastructure;
using Cypisek.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cypisek.Data.Repositories
{
    public interface IClientScheduleMediaFilesListRepository : IRepository<ClientScheduleMediaFilesList>
    {

    }

    public class ClientScheduleMediaFilesListRepository : RepositoryBase<ClientScheduleMediaFilesList>, IClientScheduleMediaFilesListRepository
    {
        public ClientScheduleMediaFilesListRepository(IDbFactory dbFactory)
            : base(dbFactory) { }
    }
}
