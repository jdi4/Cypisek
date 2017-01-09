using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cypisek.ViewModels.Schedules
{
    public class MediaFileSelectViewModel
    {
        public bool IsSelected { get; set; }
        public int ID { get; set; }
        public long Bytes { get; set; }

        [DisplayName("Nazwa pliku")]
        public string FileName { get; set; }

        [DisplayName("Rozmiar pliku")]
        public string FileSize
        {
            get { return Utilities.BytesToString(this.Bytes); }
        }

        [DisplayName("Czas wyświetlania (sekundy)")]
        //[Range(1, 86400, ErrorMessage = "Czas wyświetlania musi zawierać się w przedziale 1-86400")]
        public int PlayTime { get; set; }
    }
}