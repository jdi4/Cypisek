using AutoMapper;
using Cypisek.Models;
using Cypisek.Services;
using Cypisek.ViewModels.MediaLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cypisek.Controllers
{
    public class MediaLibraryController : Controller
    {
        private readonly IMediaStorageService storageService;

        public MediaLibraryController(IMediaStorageService imss)
        {
            this.storageService = imss;
        }

        public ActionResult Refresh()
        {
            storageService.RefreshFileDB();
            return RedirectToAction("FileBrowser");
        }

        // GET: MediaLibrary
        public ActionResult FileBrowser()
        {
            var dbfiles = storageService.GetMediaFiles();

            List<MediaFileViewModel> model = new List<MediaFileViewModel>();

            foreach (MediaFile mf in dbfiles)
            {
                model.Add(new MediaFileViewModel()
                {
                    ID = mf.ID,
                    FileName = mf.Name,
                    Bytes = mf.Size
                });
            }
            return View(model);
        }

        // GET: MediaLibrary/UploadFile
        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
                try
                {
                    storageService.AddFile(file.InputStream, file.FileName);
                    ViewBag.Message = "File uploaded successfully";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }

            return RedirectToAction("FileBrowser");
        }

        // GET: MediaLibrary/Delete/5
        public ActionResult Delete(int id)
        {
            var file = storageService.GetMediaFile(id);

            if (file != null)
            {
                var model = Mapper.Map<MediaFile, MediaFileViewModel>(file);
                var playlist = storageService.GetMediaFileSchedules(id);
                model.ScheduleNames = playlist.Select(p => p.ClientSchedule.Name).ToList();

                if (TempData["Message"] != null)
                    ViewBag.Message = TempData["Message"];
                return View(model);
            }
            else
                return RedirectToAction("Index");
        }

        // POST: MediaLibrary/Delete/5
        [HttpPost]
        public ActionResult Delete(MediaFileViewModel model)
        {
            try
            {
                if (storageService.DeleteFile(model.ID))
                    return RedirectToAction("FileBrowser");
                else
                {
                    TempData["Message"] = "Nie można usunąć pliku, ponieważ jest on używany w harmonogramach";
                    return RedirectToAction("Delete", new { id = model.ID });
                }


            }
            catch
            {
                return View(model);
            }
        }

    }
}
