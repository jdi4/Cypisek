using Cypisek.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cypisek.ViewModels.Clients
{
    public class ClientManagerViewModel
    {
        public ICollection<ClientGroupViewModel> ClientsGroups { get; set; }
        public ICollection<EndPlayerClientViewModel> ClientsWithoutGroup { get; set; }
    }
}