using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Cypisek.ViewModels
{
    public class ClientGroupViewModel
    {
        public int ID { get; set; }

        [DisplayName("Nazwa")]
        public string Name { get; set; }

        public virtual List<EndPlayerClientViewModel> EndPlayerClients { get; set; }
    }
}