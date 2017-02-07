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
    public interface ICampaignRepository : IRepository<Campaign>
    {
        IEnumerable<Campaign> GetAllIncludeSchedules();
        IEnumerable<Campaign> GetAllIncludeSchedulesWithPlaylists();
        Campaign GetByIdIncludeSchedules(int id);
    }

    public class CampaignRepository : RepositoryBase<Campaign>, ICampaignRepository
    {
        public CampaignRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        public IEnumerable<Campaign> GetAllIncludeSchedules()
        {
            return dbSet.Include(cmp => cmp.Schedules).ToList();
        }

        public IEnumerable<Campaign> GetAllIncludeSchedulesWithPlaylists()
        {
            return dbSet//.Include(cmp => cmp.Schedules)
                .Include(cmp => cmp.Schedules.Select(s => s.MediaPlaylist))
                .ToList();
        }

        public Campaign GetByIdIncludeSchedules(int id)
        {
            return dbSet.Where(c => c.ID == id).Include(cmp => cmp.Schedules).FirstOrDefault();
        }
    }
}
