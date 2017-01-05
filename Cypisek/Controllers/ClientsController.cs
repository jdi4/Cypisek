using AutoMapper;
using Cypisek.Models;
using Cypisek.Services;
using Cypisek.ViewModels;
using Cypisek.ViewModels.Clients;
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
        private readonly IClientScheduleService clientScheduleService;

        public ClientsController(IClientGroupService cgS, IEndPlayerClientService epcS, IClientScheduleService csS)
        {
            clientGroupService = cgS;
            endPlayerClientService = epcS;
            clientScheduleService = csS;
        }

        // GET: Clients
        public ActionResult Index()
        {
            var model = new ClientManagerViewModel();

            var groups = clientGroupService.GetClientGroupsIncludeClients();
            model.ClientsGroups =
                Mapper.Map<IEnumerable<ClientGroup>, ICollection<ClientGroupViewModel>>(groups);

            var clientsWithoutGroup = endPlayerClientService.GetEndPlayerClientsWithoutGroup();
            model.ClientsWithoutGroup = Mapper
                .Map<IEnumerable<EndPlayerClient>, List<EndPlayerClientViewModel>>(clientsWithoutGroup);

            model.ClientsSchedulesSL = clientScheduleService.GetClientSchedules()
                .Select(s => new SelectListItem { Value = s.ID.ToString(), Text = s.Name })
                .ToList();


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
        public ActionResult SetGroup(List<EndPlayerClientViewModel> clientlist)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var groups = clientGroupService.GetClientGroups();
                }

                return RedirectToAction("Index");
                //return View("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Clients/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Clients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

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

        // GET: Clients/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Clients/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
