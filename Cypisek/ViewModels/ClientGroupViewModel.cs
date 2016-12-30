using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cypisek.ViewModels
{
    public class ClientGroupViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<EndPlayerClientViewModel> EndPlayerClients { get; set; }
    }
}