﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cypisek.Models
{
    public class EndPlayerClient
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime LastConnectionDate { get; set; }
        public bool IsSynchronized { get; set; }
        public bool IsConnected { get; set; }

        // FK
        public int? ClientGroupID { get; set; }
        public int? CampaignID { get; set; }

        // Nav.properties
        public ClientGroup ClientGroup { get; set; }
        public Campaign ClientCampaign { get; set; }
    }
}