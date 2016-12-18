using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cypisek.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        CypisekEntities dbContext;

        public CypisekEntities Init()
        {
            return dbContext ?? (dbContext = new CypisekEntities());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
