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
        private readonly ICampaignService campaignService;
        private readonly IClientScheduleService clientScheduleService;
        private readonly IMediaFileService mediaFileService;

        public SchedulesController(IClientScheduleService csS, IMediaFileService mfS, ICampaignService cS)
        {
            this.campaignService = cS;
            this.clientScheduleService = csS;
            this.mediaFileService = mfS;
        }

        // GET: Schedules
        public ActionResult Index()
        {
            var campaigns = campaignService.GetAllCampaignsIncludeSchedules();
            var model = Mapper.Map<IEnumerable<Campaign>, IEnumerable<CampaignsIndexViewModel>>(campaigns);

            return View(model);
        }

        public ActionResult CampaignCalendar(int id)
        {
            var schedules = campaignService.GetCampaign(id).Schedules;
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
        public ActionResult Create(ClientScheduleFormViewModel formSchedule)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ClientSchedule newSchedule = 
                        Mapper.Map<ClientScheduleFormViewModel, ClientSchedule>(formSchedule);

                    var playlist = formSchedule.MediaFileList.Where(f => f.IsSelected == true)
                        .Select(f => new ClientScheduleMediaFilesList
                        {
                            MediaFileID = f.ID,
                            PlayTime = f.PlayTime
                        })
                        .ToList();

                    newSchedule.MediaPlaylist = playlist;
                    clientScheduleService.CreateClientSchedule(newSchedule);
                    clientScheduleService.SaveClientSchedule();

                }
                else
                {
                    return View(formSchedule);
                }
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag["error"] = ex.Message;
                return View(formSchedule);
            }
        }

        public ActionResult CreateCampaign()
        { 
            return View();
        }

        [HttpPost]
        public ActionResult CreateCampaign(CampaignFormViewModel formCampaign)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Campaign newCampaign =
                        Mapper.Map<CampaignFormViewModel, Campaign>(formCampaign);

                    campaignService.CreateCampaign(newCampaign);
                    campaignService.CommitChanges();

                }
                else
                {
                    return View(formCampaign);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag["error"] = ex.Message;
                return View(formCampaign);
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
