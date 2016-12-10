using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cypisek.ViewModels
{
    public class MediaFileViewModel
    {
        public string FileName { get; set; }
        public long Bytes { get; set; }

        public string FileSize
        {
            get { return Utilities.BytesToString(this.Bytes); }
        }
    }
}