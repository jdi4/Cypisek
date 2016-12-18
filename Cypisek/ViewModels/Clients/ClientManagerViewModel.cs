using Cypisek.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cypisek.ViewModels.Clients
{
    public class ClientManagerViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public ICollection<EndPlayerClientViewModel> EndPlayerClientsVM { get; set; }
    }
}