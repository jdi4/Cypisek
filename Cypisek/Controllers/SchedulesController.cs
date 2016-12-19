using AutoMapper;
using Cypisek.Models;
using Cypisek.Services;
using Cypisek.ViewModels.MediaLibrary;
using Cypisek.ViewModels.Schedules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cypisek.Controllers
{
    public class SchedulesController : Controller
    {
        //Cypisek.Services.
        private readonly IClientScheduleService clientScheduleService;
        private readonly IMediaFileService mediaFileService;

        public SchedulesController(IClientScheduleService csS, IMediaFileService mfS)
        {
            this.clientScheduleService = csS;
            this.mediaFileService = mfS;
        }

        // GET: Schedules
        public ActionResult Index()
        {
            var schedules = clientScheduleService.GetClientSchedules();
            var model = Mapper.Map<IEnumerable<ClientSchedule>, IEnumerable<ClientScheduleViewModel>>(schedules);

            return View(model);
        }

        // GET: Schedules/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Schedules/Create
        public ActionResult Create()
        {
            var files = mediaFileService.GetMediaFiles();

            var model = new ClientScheduleFormViewModel();

            //model.MediaFileList = (ICollection< MediaFileViewModel>) Mapper.Map<IEnumerable<MediaFile>, 
            //    IEnumerable<MediaFileViewModel>>(files);

            model.MediaFileList = (List<MediaFileSelectViewModel>) 
                Mapper.Map<IEnumerable<MediaFile>, IEnumerable<MediaFileSelectViewModel>>(files);

            return View(model);
        }

        // POST: Schedules/Create
        // t public ActionResult Create(FormCollection collection)
        [HttpPost]
        public ActionResult Create(ClientScheduleFormViewModel newSchedule)
        {
            try
            {
                if (ModelState.IsValid)
                {

                }
                else
                {
                    return View(newSchedule);
                }
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
