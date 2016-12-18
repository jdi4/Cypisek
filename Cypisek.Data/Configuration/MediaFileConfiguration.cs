using Cypisek.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cypisek.Data.Configuration
{
    public class MediaFileConfiguration : EntityTypeConfiguration<MediaFile>
    {
        public MediaFileConfiguration()
        {
            ToTable("MediaFile");
            Property(g => g.ID).IsRequired();
            Property(g => g.Path).IsRequired();
        }

    }
}
