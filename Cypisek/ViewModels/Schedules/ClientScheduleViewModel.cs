using Cypisek.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cypisek.ViewModels.Schedules
{
    public class ClientScheduleViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public int PlaylistCount
        {
            get { return MediaPlaylist.Count; }
        }

        public int ClientsCount
        {
            get { return EndPlayerClients.Count; }
        }

        public virtual ICollection<ClientScheduleMediaFilesList> MediaPlaylist { get; set; }
        public virtual ICollection<EndPlayerClient> EndPlayerClients { get; set; }
    }
}