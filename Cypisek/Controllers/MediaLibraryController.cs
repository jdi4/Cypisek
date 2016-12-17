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
        private readonly IMediaFileService filesService;
        private readonly IMediaStorageService storageService;

        public MediaLibraryController(IMediaFileService imfs, IMediaStorageService imss)
        {
            this.filesService = imfs;
            this.storageService = imss;
        }

        //public ActionResult Refresh()
        //{
        //    var dbfiles = filesService.GetMediaFiles();

        //    foreach (MediaFile mf in dbfiles)
        //    {

        //    }

        //    return View();
        //}

        // GET: MediaLibrary
        public ActionResult FileBrowser()
        {
            var dbfiles = filesService.GetMediaFiles();

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

        // GET: MediaLibrary/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MediaLibrary/Edit/5
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

        // GET: MediaLibrary/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MediaLibrary/Delete/5
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

        // GET: MediaLibrary/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}
    }
}
