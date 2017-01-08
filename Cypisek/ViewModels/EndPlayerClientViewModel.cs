using Cypisek.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cypisek.ViewModels
{
    public class EndPlayerClientViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime LastConnectionDate { get; set; }
        public bool IsConnected { get; set; }
        public bool IsSynchronized { get; set; }

        public int ClientGroupID { get; set; }
        public int ClientScheduleID { get; set; }

        public ClientGroup ClientGroup { get; set; }
        public ClientSchedule ClientSchedule { get; set; }

        // extra properties
        public string ScheduleName
        {
            get { return ClientSchedule.Name; }
        }
        public bool IsSelected { get; set; }


    }
}