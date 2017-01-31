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

        // GET: Schedules/Create/5
        public ActionResult Create(int cId, string cName)
        {
            var files = mediaFileService.GetMediaFiles();

            var model = new ClientScheduleFormViewModel();

            //model.MediaFileList = (ICollection< MediaFileViewModel>) Mapper.Map<IEnumerable<MediaFile>, 
            //    IEnumerable<MediaFileViewModel>>(files);

            model.MediaFileList = (List<MediaFileSelectViewModel>) 
                Mapper.Map<IEnumerable<MediaFile>, IEnumerable<MediaFileSelectViewModel>>(files);

            model.CampaignID = cId;
            model.CampaignName = cName;

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

        // GET: Schedules/DeleteSchedule/5
        public ActionResult DeleteSchedule(int id)
        {
            var schedule = clientScheduleService.GetClientSchedule(id);

            if (schedule != null)
            {
                var model = Mapper.Map<ClientSchedule, ClientScheduleViewModel>(schedule);
                var playlist = clientScheduleService.GetSchedulePlaylist(id);
                model.MediaPlaylist = (List<MediaFileSelectViewModel>)
                        Mapper.Map<IEnumerable<ClientScheduleMediaFilesList>, IEnumerable<MediaFileSelectViewModel>>(playlist);
                return View(model);
            }
            else
                return RedirectToAction("Index");

        }

        // POST: Schedules/DeleteSchedule/5
        [HttpPost]
        public ActionResult DeleteSchedule(int id, FormCollection collection)
        {
            try
            {
                clientScheduleService.DeleteClientSchedule(id);
                clientScheduleService.SaveClientSchedule();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
