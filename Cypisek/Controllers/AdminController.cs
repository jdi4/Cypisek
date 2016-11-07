using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cypisek.Controllers
{
    public class AdminController : Controller
    {
        private const String imagesStorageDirLocation = "~/MediaStorage/Images";

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FileManager()
        {
            Directory.EnumerateFiles(imagesStorageDirLocation);
            return View();
        }

        [HttpPost]
        public ActionResult FileManager(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
                try
                {
                    Directory.CreateDirectory(Server.MapPath(imagesStorageDirLocation)); // only if dirs does not exist

                    string path = Path.Combine(imagesStorageDirLocation,
                                               Path.GetFileName(file.FileName));
                    file.SaveAs(path);
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
            return View();
        }
    }
}