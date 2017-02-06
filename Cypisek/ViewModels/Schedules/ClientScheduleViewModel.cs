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

        public int CampaignID { get; set; }

        [DisplayName("Kampania")]
        public string CampaignName { get; set; }

        [DisplayName("Data rozpoczęcia")]
        public DateTime StartDate { get; set; }

        [DisplayName("Data wygaśnięcia")]
        public DateTime ExpirationDate { get; set; }

        [DisplayName("Liczba plików")]
        public int PlaylistCount
        {
            get
            {
                if (MediaPlaylist != null)
                    return MediaPlaylist.Count;
                else return 0;
            }
        }

        public virtual ICollection<MediaFileSelectViewModel> MediaPlaylist { get; set; }

        public bool IsCurrent { get; set; } = false;
    }
}