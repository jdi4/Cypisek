using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cypisek.Data
{
    public class CypisekSeedData : DropCreateDatabaseIfModelChanges<CypisekEntities>
    {
        protected override void Seed(CypisekEntities context)
        {
            //seed if needed
            //GetCategories().ForEach(c => context.Categories.Add(c));
            //GetGadgets().ForEach(g => context.Gadgets.Add(g));

            //context.Commit();
        }
    }
}
