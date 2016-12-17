using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cypisek.Models
{
    public class ClientGroup
    {
        public int ID { get; set; }
        public string Name { get; set; }

        // Nav. properties
        public virtual ICollection<EndPlayerClient> EndPlayerClients { get; set; }
    }
}