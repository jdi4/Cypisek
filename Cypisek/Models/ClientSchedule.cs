using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cypisek.Models
{
    public class ClientSchedule
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public DateTime ExpirationDate { get; set; }

        public ICollection<MediaFile> Files { get; set; }
    }
}