using Cypisek.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cypisek.ViewModels.Schedules
{
    public class ClientScheduleFormViewModel : IValidatableObject
    {
        [DisplayName("Nazwa harmonogramu")]
        [Required]
        public string Name { get; set; }

        [Required]
        public int CampaignID { get; set; }

        [DisplayName("Wybrana kampania")]
        public string CampaignName { get; set; }

        [DisplayName("Data rozpoczęcia")]
        [Range(typeof(DateTime), "1/1/2001", "1/1/2112", ErrorMessage = "Date is out of Range")]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [DisplayName("Data wygaśnięcia")]
        [Range(typeof(DateTime), "1/1/2001", "1/1/2112", ErrorMessage = "Date is out of Range")]
        public DateTime ExpirationDate { get; set; } = DateTime.Now.AddDays(7);

        public List<MediaFileSelectViewModel> MediaFileList { get; set; }

        public ClientScheduleFormViewModel()
        {
            Name = "Nowy harmonogram";
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ExpirationDate.CompareTo(StartDate) <= 0)
                yield return new ValidationResult("Data wygaśnięcia nie może być wcześniejsza niż data rozpoczęcia.");
        }
    }
}