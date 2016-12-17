using Cypisek.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cypisek.Data.Configuration
{
    public class EndPlayerClientConfiguration : EntityTypeConfiguration<EndPlayerClient>
    {
        public EndPlayerClientConfiguration()
        {
            ToTable("EndPlayerClient");
            Property(g => g.ID).IsRequired();
            Property(g => g.Name).IsRequired();
        }
    }
}
