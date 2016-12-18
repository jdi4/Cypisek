using AutoMapper;
using Cypisek.Models;
using Cypisek.Services;
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

        public ClientsController(IClientGroupService cgS, IEndPlayerClientService epcS)
        {
            this.clientGroupService = cgS;
            this.endPlayerClientService = epcS;
        }

        // GET: Clients
        public ActionResult Index()
        {
            var groups = clientGroupService.GetClientGroups();

            IEnumerable<ClientManagerViewModel> model =
                Mapper.Map<IEnumerable<ClientGroup>, IEnumerable<ClientManagerViewModel>>(groups);

            //var clientsWithoutGroup = endPlayerClientService.GetEndPlayerClientsWithoutGroup();
            //model. Mapper.Map<IEnumerable<EndPlayerClient>, IEnumerable<EndPlayerClientViewModel>>(clientsWithoutGroup);

            return View(model);
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
