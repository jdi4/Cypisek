using Cypisek.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Cypisek.ViewModels.Schedules
{
    public class CampaignFormViewModel
    {
        [DisplayName("Nazwa")]
        public string Name { get; set; }
    }
}