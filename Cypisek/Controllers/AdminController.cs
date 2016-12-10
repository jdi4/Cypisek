using Microsoft.AspNet.SignalR;
using SignalRTest;
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
            //var files = Directory.EnumerateFiles(Server.MapPath(imagesStorageDirLocation));
            string test = Server.MapPath(imagesStorageDirLocation);
            ViewData["t1"] = test;
            var files = Directory.EnumerateFiles(Server.MapPath(imagesStorageDirLocation));
            ViewData["filename"] = files.First();
            //return View(files);
            return View();
        }

        [HttpPost]
        public ActionResult FileManager(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
                try
                {
                    Directory.CreateDirectory(Server.MapPath(imagesStorageDirLocation)); // only if dirs does not exist

                    // assign MapPath result to variable?

                    string path = Path.Combine(Server.MapPath(imagesStorageDirLocation),
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

        public ActionResult HelloAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult HelloAdmin(string name, string imageurl)
        {
            if (name.Length > 0 && imageurl.Length > 0)
            {
                try
                {
                    var context = GlobalHost.ConnectionManager.GetHubContext<HelloTestHub>();
                    //tmp
                    context.Clients.All.addNewMessageToPage(name, imageurl);
                    context.Clients.All.setImage(name, imageurl);
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
                ViewBag.Message = "Image display request sent.";
            }
            else
            {
                ViewBag.Message = "You have not specified an image.";
            }
            return View();
        }

        public ActionResult HelloImage()
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<HelloTestHub>();

            context.Clients.All.addNewMessageToPage("Ktos", "tu jest");
            context.Clients.All.setImage("Rumcajs", "https://i.ytimg.com/vi/Qkiqqkz22nw/hqdefault.jpg");
            return View("HelloAdmin");
        }
    }
}