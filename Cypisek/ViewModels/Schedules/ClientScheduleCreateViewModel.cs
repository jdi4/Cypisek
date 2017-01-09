using Cypisek.Models;
using Cypisek.ViewModels.MediaLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cypisek.ViewModels.Schedules
{
    public class ClientScheduleCreateViewModel
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public ICollection<MediaFileViewModel> MediaFileList { get; set; }
    }
}