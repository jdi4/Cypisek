using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cypisek.Models
{
    public class Campaign
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ClientSchedule> Schedules { get; set; }
        public virtual ICollection<EndPlayerClient> EndPlayerClients { get; set; }
    }
}
