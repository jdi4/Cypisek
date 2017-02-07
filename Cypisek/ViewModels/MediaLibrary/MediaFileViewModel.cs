using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Cypisek.ViewModels.MediaLibrary
{
    public class MediaFileViewModel
    {
        public int ID { get; set; }

        [DisplayName("Nazwa pliku")]
        public string FileName { get; set; }
        public long Bytes { get; set; }

        [DisplayName("Rozmiar pliku")]
        public string FileSize
        {
            get { return Utilities.BytesToString(this.Bytes); }
        }

        public List<string> ScheduleNames { get; set; }

        public string Message { get; set; }
    }
}