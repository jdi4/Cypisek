using Cypisek.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cypisek.ViewModels.Clients
{
    public class ClientManagerViewModel
    {
        public ICollection<ClientGroupViewModel> ClientsGroups { get; set; }
        public ICollection<EndPlayerClientViewModel> ClientsWithoutGroup { get; set; }

        public ICollection<SelectListItem> ClientsGroupsSL
        {
            get
            {
                //List<SelectListItem> slilist = new List<SelectListItem>();

                //foreach (var group in this.ClientsGroups)
                //{
                //    slilist.Add(new SelectListItem() { Text = group.Name, Value = group.ID.ToString() });
                //}
                return this.ClientsGroups
                    .Select( g => new SelectListItem { Text = g.Name, Value = g.ID.ToString() })
                    .ToList();
            }
        }

        public ICollection<SelectListItem> ClientsSchedulesSL { get; set; }
    }
}