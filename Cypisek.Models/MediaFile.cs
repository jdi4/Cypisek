using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cypisek.Models
{
    public class MediaFile
    {
        public int ID { get; set; }
        public string Path { get; set; }
        public int Size { get; set; }

        public virtual ICollection<ClientScheduleMediaFilesList> ClientSchedulesList { get; set; }
    }
}