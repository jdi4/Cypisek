using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cypisek.Controllers
{
    public class SchedulesController : Controller
    {
        // GET: Schedules
        public ActionResult Index()
        {
            return View();
        }

        // GET: Schedules/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Schedules/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Schedules/Create
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

        // GET: Schedules/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Schedules/Edit/5
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

        // GET: Schedules/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Schedules/Delete/5
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
