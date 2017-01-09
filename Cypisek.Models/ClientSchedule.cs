using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cypisek.Models
{
    public class ClientSchedule
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        // Nav. properties
        public virtual ICollection<ClientScheduleMediaFilesList> MediaPlaylist { get; set; }

        public virtual ICollection<EndPlayerClient> EndPlayerClients { get; set; }

    }
}