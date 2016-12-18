using Cypisek.Data.Configuration;
using Cypisek.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cypisek.Data
{
    public class CypisekEntities : DbContext
    {
        public CypisekEntities() : base("CypisekEntities") { }

        public DbSet<ClientGroup> ClientGroups { get; set; }
        public DbSet<ClientSchedule> ClientSchedule { get; set; }
        public DbSet<ClientScheduleMediaFilesList> ClientScheduleMediaFilesLists { get; set; }
        public DbSet<EndPlayerClient> EndPlayerClients { get; set; }
        public DbSet<MediaFile> MediaFiles { get; set; }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ClientGroupConfiguration());
            modelBuilder.Configurations.Add(new ClientScheduleConfiguration());
            modelBuilder.Configurations.Add(new ClientScheduleMediaFilesListConfiguration());
            modelBuilder.Configurations.Add(new EndPlayerClientConfiguration());
            modelBuilder.Configurations.Add(new MediaFileConfiguration());
        }
    }
}
