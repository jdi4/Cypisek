using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cypisek.ViewModels.Clients
{
    public class ClientManagerFormModel
    {
        public int? ClientsGroupsSL { get; set; }
        public int? ClientsSchedulesSL { get; set; }
        public List<EndPlayerClientViewModel> ClientsList { get; set; }
    }
}