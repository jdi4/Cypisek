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
        private readonly IMediaStorageService mediaStorageService;

        public SchedulesController(IClientScheduleService csS, IMediaStorageService mfS, ICampaignService cS)
        {
            this.campaignService = cS;
            this.clientScheduleService = csS;
            this.mediaStorageService = mfS;
        }

        // GET: Schedules
        public ActionResult Index()
        {
            var campaigns = campaignService.GetAllCampaignsIncludeSchedules();
            var model = Mapper.Map<IEnumerable<Campaign>, IEnumerable<CampaignsIndexViewModel>>(campaigns)
                .ToList();

            //clientScheduleService.GetCurrentSchedule();

            //model.ForEach(c =>
            //{
            //    var cur = clientScheduleService.GetCurrentSchedule(c.ID);
            //    if (cur != null)
            //    {
            //        c.Schedules.First(s => s.ID == cur.ID).IsCurrent = true;
            //    }
            //});

            return View(model);
        }

        public ActionResult CampaignCalendar(int id)
        {
            var schedules = campaignService.GetCampaign(id).Schedules;
            var model = Mapper.Map<IEnumerable<ClientSchedule>, IEnumerable<ClientScheduleViewModel>>(schedules);

            return View(model);
        }

        // GET: Schedules/Create/5
        public ActionResult Create(int cId)
        {
            var files = mediaStorageService.GetMediaFiles();

            var model = new ClientScheduleFormViewModel();

            //model.MediaFileList = (ICollection< MediaFileViewModel>) Mapper.Map<IEnumerable<MediaFile>, 
            //    IEnumerable<MediaFileViewModel>>(files);

            model.MediaFileList = (List<MediaFileSelectViewModel>) 
                Mapper.Map<IEnumerable<MediaFile>, IEnumerable<MediaFileSelectViewModel>>(files);

            //model.CampaignID = cId;
            //model.CampaignName = cName;

            var campaign = campaignService.GetCmpaignIncludeSchedules(cId);

            model.CampaignID = cId;
            model.CampaignName = campaign.Name;
            model.OtherSchedules = Mapper.Map<IEnumerable<ClientSchedule>, IEnumerable<ClientScheduleViewModel>>
                (campaign.Schedules).ToList();

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

        // GET: Schedules/EditCampaign/5
        public ActionResult EditCampaign(int id)
        {
            var campaign = campaignService.GetCampaign(id);
            var model = Mapper.Map<Campaign, CampaignSchedulesFormViewModel>(campaign);

            return View(model);
        }

        // POST: Schedules/EditCampaign/5
        [HttpPost]
        public ActionResult EditCampaign(CampaignSchedulesFormViewModel formCampaign)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var edited = Mapper.Map<CampaignSchedulesFormViewModel, Campaign>(formCampaign);

                    campaignService.EditCampaign(edited);
                    campaignService.CommitChanges();
                }
                else
                {
                    return RedirectToAction("Create", new { id = formCampaign.ID });
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(formCampaign);
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
