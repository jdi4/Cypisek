using Cypisek.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cypisek.ViewModels.Schedules
{
    public class ClientScheduleFormViewModel
    {
        public string Name { get; set; }
        [Range(typeof(DateTime), "1/1/2001", "1/1/2112", ErrorMessage = "Date is out of Range")]
        public DateTime StartDate { get; set; }
        [Range(typeof(DateTime), "1/1/2001", "1/1/2112", ErrorMessage = "Date is out of Range")]
        public DateTime ExpirationDate { get; set; }

        public List<MediaFileSelectViewModel> MediaFileList { get; set; }
    }
}