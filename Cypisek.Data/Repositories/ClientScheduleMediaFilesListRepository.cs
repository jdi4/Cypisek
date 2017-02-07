using Cypisek.Data.Infrastructure;
using Cypisek.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cypisek.Data.Repositories
{
    public interface IClientScheduleMediaFilesListRepository : IRepository<ClientScheduleMediaFilesList>
    {
        IEnumerable<ClientScheduleMediaFilesList> GetManyIncludeMediaFiles(Expression<Func<ClientScheduleMediaFilesList, bool>> where);
        IEnumerable<ClientScheduleMediaFilesList> GetManyIncludeSchedules(Expression<Func<ClientScheduleMediaFilesList, bool>> where);
    }

    public class ClientScheduleMediaFilesListRepository : RepositoryBase<ClientScheduleMediaFilesList>, IClientScheduleMediaFilesListRepository
    {
        public ClientScheduleMediaFilesListRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        public IEnumerable<ClientScheduleMediaFilesList> GetManyIncludeMediaFiles(Expression<Func<ClientScheduleMediaFilesList, bool>> where)
        {
            return dbSet.Where(where).Include(mp => mp.MediaFile).ToList();
        }

        public IEnumerable<ClientScheduleMediaFilesList> GetManyIncludeSchedules(Expression<Func<ClientScheduleMediaFilesList, bool>> where)
        {
            return dbSet.Where(where).Include(mp => mp.ClientSchedule).ToList();
        }
    }
}
