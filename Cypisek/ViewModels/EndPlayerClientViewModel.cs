﻿using Cypisek.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Cypisek.ViewModels
{
    public class EndPlayerClientViewModel
    {
        public int ID { get; set; }

        [DisplayName("Nazwa")]
        public string Name { get; set; }

        [DisplayName("Ostatnie połączenie")]
        public DateTime LastConnectionDate { get; set; }

        [DisplayName("Jest podłączony")]
        public bool IsConnected { get; set; }

        [DisplayName("Jest zsynchronizowany")]
        public bool IsSynchronized { get; set; }

        public int ClientGroupID { get; set; }
        public int ClientScheduleID { get; set; }

        public ClientGroup ClientGroup { get; set; }
        public ClientSchedule ClientSchedule { get; set; }

        // extra properties

        [DisplayName("Harmonogram")]
        public string ScheduleName
        {
            get { return ClientSchedule.Name; }
        }
        public bool IsSelected { get; set; }


    }
}