using Cypisek.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cypisek.Data.Configuration
{
    public class ClientScheduleMediaFilesListConfiguration : EntityTypeConfiguration<ClientScheduleMediaFilesList>
    {
        public ClientScheduleMediaFilesListConfiguration()
        {
            ToTable("ClientScheduleMediaFilesList");
            Property(g => g.ID).IsRequired();
            Property(g => g.ClientScheduleID).IsRequired();
            Property(g => g.MediaFileID).IsRequired();
            Property(g => g.PlayTime).IsRequired();
        }
    }
}
