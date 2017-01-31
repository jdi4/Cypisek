using Cypisek.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cypisek.ViewModels.Clients
{
    public class ClientManagerViewModel
    {
        public ICollection<ClientGroupViewModel> ClientsGroups { get; set; }
        public ICollection<EndPlayerClientViewModel> ClientsWithoutGroup { get; set; }

        public ICollection<SelectListItem> ClientsGroupsSL { get; set; }
        public ICollection<SelectListItem> ClientsCampaignsSL { get; set; }
    }
}