using Cypisek.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cypisek.Data.Configuration
{
    public class ClientGroupConfiguration : EntityTypeConfiguration<ClientGroup>
    {
        public ClientGroupConfiguration()
        {
            ToTable("ClientGroup");
            Property(g => g.ID).IsRequired();
            Property(g => g.Name).IsRequired();
        }
    }
}
