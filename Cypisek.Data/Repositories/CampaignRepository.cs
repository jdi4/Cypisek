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
    }

    public class CampaignRepository : RepositoryBase<Campaign>, ICampaignRepository
    {
        public CampaignRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        public IEnumerable<Campaign> GetAllIncludeSchedules()
        {
            return dbSet.Include(cmp => cmp.Schedules).ToList();
        }
    }
}
