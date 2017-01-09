using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Cypisek.ViewModels.Clients
{
    public class ClientGroupFormModel
    {
        [DisplayName("Nazwa grupy")]
        public string Name { get; set; }
    }
}