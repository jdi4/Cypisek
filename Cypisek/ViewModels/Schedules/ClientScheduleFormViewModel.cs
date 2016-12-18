using Cypisek.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cypisek.ViewModels.Schedules
{
    public class ClientScheduleFormViewModel
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public virtual ICollection<ClientScheduleMediaFilesList> MediaPlaylist { get; set; }
        public virtual ICollection<EndPlayerClient> EndPlayerClients { get; set; }
    }
}