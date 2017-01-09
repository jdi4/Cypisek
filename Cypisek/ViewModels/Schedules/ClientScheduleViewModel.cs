using Cypisek.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Cypisek.ViewModels.Schedules
{
    public class ClientScheduleViewModel
    {
        public int ID { get; set; }

        [DisplayName("Nazwa")]
        public string Name { get; set; }

        [DisplayName("Data rozpoczęcia")]
        public DateTime StartDate { get; set; }

        [DisplayName("Data wygaśnięcia")]
        public DateTime ExpirationDate { get; set; }

        [DisplayName("Liczba plików")]
        public int PlaylistCount
        {
            get { return MediaPlaylist.Count; }
        }

        [DisplayName("Liczba końcówek")]
        public int ClientsCount
        {
            get { return EndPlayerClients.Count; }
        }

        public virtual ICollection<ClientScheduleMediaFilesList> MediaPlaylist { get; set; }
        public virtual ICollection<EndPlayerClient> EndPlayerClients { get; set; }
    }
}