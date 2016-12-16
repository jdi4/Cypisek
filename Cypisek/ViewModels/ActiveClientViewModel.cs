using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cypisek.ViewModels
{
    public class ActiveClientViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsConnected { get; set; }
        public bool IsSynchronized { get; set; }

        //?? nr/id/nazwa harmonogramu

        public ActiveClientViewModel()
        { }
    }
}