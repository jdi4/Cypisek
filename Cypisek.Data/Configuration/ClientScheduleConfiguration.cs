using Cypisek.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cypisek.Data.Configuration
{
    class ClientScheduleConfiguration : EntityTypeConfiguration<ClientSchedule>
    {
        public ClientScheduleConfiguration()
        {
            ToTable("ClientSchedule");
            Property(g => g.ID).IsRequired();
            Property(g => g.Name).IsRequired();
            Property(g => g.StartDate).IsRequired();
            Property(g => g.ExpirationDate).IsRequired();
        }
    }
}
