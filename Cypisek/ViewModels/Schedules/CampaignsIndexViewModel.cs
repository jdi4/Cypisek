using Cypisek.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Cypisek.ViewModels.Schedules
{
    public class CampaignsIndexViewModel
    {
        public int ID { get; set; }

        [DisplayName("Nazwa")]
        public string Name { get; set; }

        [DisplayName("Harmonogramy")]
        public ICollection<ClientScheduleViewModel> Schedules { get; set; }

        [DisplayName("Końcówki")]
        public ICollection<EndPlayerClient> EndPlayerClients { get; set; }

        [DisplayName("Liczba końcówek")]
        public int ClientsCount
        {
            get { return EndPlayerClients.Count; }
        }
    }
}