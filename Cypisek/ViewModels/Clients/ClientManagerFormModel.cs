using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cypisek.ViewModels.Clients
{
    public class ClientManagerFormModel
    {
        public int? ClientsGroupsSL { get; set; }
        public int? ClientsCampaignsSL { get; set; }
        public List<EndPlayerClientViewModel> ClientsList { get; set; }
    }
}