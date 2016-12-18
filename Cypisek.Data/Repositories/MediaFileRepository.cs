using Cypisek.Data.Infrastructure;
using Cypisek.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cypisek.Data.Repositories
{
    public interface IMediaFileRepository : IRepository<MediaFile>
    {

    }

    public class MediaFileRepository : RepositoryBase<MediaFile>, IMediaFileRepository
    {
        public MediaFileRepository(IDbFactory dbFactory)
            : base(dbFactory) { }
    }
}
