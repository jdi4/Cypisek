using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cypisek.Models
{
    public class ClientScheduleMediaFilesList
    {
        public int ID { get; set; }
        public int PlayTime { get; set; }

        // FK
        public int MediaFileID { get; set; }
        public int ClientScheduleID { get; set; }

        // Nav.properties
        public MediaFile MediaFile { get; set; }
        public ClientSchedule ClientSchedule { get; set; }
    }
}