using AutoMapper;
using Cypisek.Hubs;
using Cypisek.Models;
using Cypisek.Services;
using Cypisek.ViewModels;
using Cypisek.ViewModels.Clients;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cypisek.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IClientGroupService clientGroupService;
        private readonly IEndPlayerClientService endPlayerClientService;
        private readonly IClientScheduleService clientScheduleService; // remove
        private readonly ICampaignService campaignService;

        public ClientsController(IClientGroupService cgS, IEndPlayerClientService epcS, IClientScheduleService csS, ICampaignService cS)
        {
            clientGroupService = cgS;
            endPlayerClientService = epcS;
            clientScheduleService = csS;
            campaignService = cS;
        }

        // GET: Clients
        public ActionResult Index()
        {
            var model = new ClientManagerViewModel();

            //clients->campaigns nav. properties loaded from here
            var campaigns = campaignService.GetAllCampaigns();

            model.ClientsCampaignsSL = campaigns
                .Select(s => new SelectListItem { Value = s.ID.ToString(), Text = s.Name })
                .ToList();

            var groups = clientGroupService.GetClientGroups();
            model.ClientsGroups =
                Mapper.Map<IEnumerable<ClientGroup>, ICollection<ClientGroupViewModel>>(groups);
           
            model.ClientsGroupsSL = groups
                    .Select(g => new SelectListItem { Text = g.Name, Value = g.ID.ToString() })
                    .ToList();
            //model.ClientsGroupsSL.Add(new SelectListItem { Text = "Brak grupy", Value = "-1" });

            var clientsWithoutGroup = endPlayerClientService.GetEndPlayerClientsWithoutGroup();
            model.ClientsWithoutGroup = Mapper
                .Map<IEnumerable<EndPlayerClient>, List<EndPlayerClientViewModel>>(clientsWithoutGroup);

            //var epClients = endPlayerClientService.GetEndPlayerClients()
            //    .GroupBy(c => c.ClientGroupID);
            //.GroupBy(c => c.ClientGroupID == null ? -1 : c.ClientGroupID);

            //ClientManagerViewModel model2 = new ClientManagerViewModel();
            //foreach (var epClientGorup in epClients)
            //{
            //    if (epClientGorup.Key == null)
            //    {
            //        model2.ClientsWithoutGroup =
            //             Mapper.Map<IEnumerable<EndPlayerClient>, ICollection<EndPlayerClientViewModel>>(epClientGorup.ToList());
            //    }
            //    else
            //    {
            //        model2.ClientsGroups.Add(new ClientGroupViewModel()
            //        {
            //            ID = epClientGorup.First().ClientGroup.ID,
            //            Name = epClientGorup.First().ClientGroup.Name,
            //            EndPlayerClients = Mapper
            //            .Map<IEnumerable<EndPlayerClient>, ICollection<EndPlayerClientViewModel>>
            //            (epClientGorup.ToList())
            //    });
            //    }
            //}
            return View(model);
        }

        [HttpPost]
        public ActionResult SetGroup(ClientManagerFormModel formModel)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    foreach (var client in formModel.ClientsList)
                    {
                        if (client.IsSelected)
                        {
                            var toEdit = endPlayerClientService.GetEndPlayerClient(client.ID);
                            if (toEdit != null)
                            {
                                toEdit.ClientGroupID = formModel.ClientsGroupsSL;
                                endPlayerClientService.EditEndPlayerClient(toEdit);
                            }
                        }
                    }
                    endPlayerClientService.SaveEndPlayerClient();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");//return View("Index");
            }
        }

        [HttpPost]
        public ActionResult SetCampaign(ClientManagerFormModel formModel)
        {
            try
            {
                var clientsToNotify = new List<int>();

                if (ModelState.IsValid)
                {
                    foreach (var client in formModel.ClientsList)
                    {
                        if (client.IsSelected)
                        {
                            var toEdit = endPlayerClientService.GetEndPlayerClient(client.ID);
                            if (toEdit != null)
                            {
                                toEdit.CampaignID = formModel.ClientsCampaignsSL;
                                toEdit.IsSynchronized = false;
                                endPlayerClientService.EditEndPlayerClient(toEdit);

                                clientsToNotify.Add(client.ID);
                            }
                        }
                    }
                }
                endPlayerClientService.SaveEndPlayerClient();

                if (formModel.ClientsCampaignsSL != null)
                {
                    NotifyCampaignUpdate(clientsToNotify, (int)formModel.ClientsCampaignsSL);

                        //if (!NotifyCampaignUpdate(toEdit.ID))
                        //    TempData["message"] = "Brak podlaczonego klienta";
                    //clientsToNotify.ForEach(c => NotifyCampaignUpdate(c));

                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private void NotifyCampaignUpdate(List<int> clientIds, int campaignId)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<ContentHub>();

            var schedule = clientScheduleService.GetCurrentSchedule(campaignId);
            string message = clientScheduleService.GetScheduleAsString(schedule);

            foreach (int id in clientIds)
            {
                string connID = ContentHub.GetClientConnection(id);
                if (connID != null)
                {
                    context.Clients.Client(connID).test1(message);
                    //return true;
                }
                //else
                //    return false;
            }




        }

        public ActionResult Test()
        {
            var clients = endPlayerClientService.GetEndPlayerClients();

            IEnumerable<EndPlayerClientViewModel> model =
                Mapper.Map<IEnumerable<EndPlayerClient>, IEnumerable<EndPlayerClientViewModel>>(clients);
            if (TempData["message"] != null)
                ViewBag.Error = TempData["message"].ToString();
            return View(model);
        }

        [HttpPost]
        public ActionResult Test(int? id, string message)
        {
            if (ModelState.IsValid)
            {
                if (id != null)
                {
                    var context = GlobalHost.ConnectionManager.GetHubContext<ContentHub>();
                    string connID = ContentHub.GetClientConnection((int)id);
                    if (connID != null)
                        context.Clients.Client(connID).test1(message);
                    else
                    {
                        TempData["message"] = "Brak podlaczonego klienta";
                    }

                }

            }
            return RedirectToAction("Test");
        }

        // GET: Clients/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Clients/CreateGroup
        public ActionResult CreateGroup()
        {
            return View();
        }

        // POST: Clients/CreateGroup
        [HttpPost]
        public ActionResult CreateGroup(ClientGroupFormModel formModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newGroup = Mapper.Map< ClientGroupFormModel, ClientGroup>(formModel);

                    clientGroupService.CreateClientGroup(newGroup);
                    clientGroupService.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Clients/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Clients/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Clients/DeleteGroup
        public ActionResult DeleteGroup(int id)
        {
            var model = Mapper.Map<ClientGroup, ClientGroupViewModel>(
                clientGroupService.GetClientGroup(id));
            return View(model);
        }

        // POST: Clients/DeleteGroup
        [HttpPost]
        public ActionResult DeleteGroup(int id, FormCollection collection)
        {
            try
            {
                clientGroupService.DeleteClientGroup(id);
                clientGroupService.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
