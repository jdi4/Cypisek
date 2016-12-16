using Cypisek.Models;
using Cypisek.ViewModels;
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

        private IHubContext SignalRHubContext = GlobalHost.ConnectionManager.GetHubContext<HelloTestHub>();

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FileManager()
        {
            //init
            Directory.CreateDirectory(Server.MapPath(imagesStorageDirLocation)); // only if dirs does not exist

            //get data
            DirectoryInfo dir = new DirectoryInfo(
                Server.MapPath(imagesStorageDirLocation));
            var files = dir.GetFiles();

            //mapping ? use AutoMapper instead
            List<MediaFileViewModel> mediaFiles = new List<MediaFileViewModel>();
            foreach (FileInfo fi in files)
            {
                mediaFiles.Add(
                    new MediaFileViewModel() { FileName = fi.Name, Bytes = fi.Length }
                    );
            }

            return View(mediaFiles);
        }

        [HttpPost]
        public ActionResult FileManager(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
                try
                {
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

            return RedirectToAction("FileManager");
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
            //var context = GlobalHost.ConnectionManager.GetHubContext<HelloTestHub>();

            SignalRHubContext.Clients.All.addNewMessageToPage("Ktos", "tu jest");
            SignalRHubContext.Clients.All.setImage("Rumcajs", "https://i.ytimg.com/vi/Qkiqqkz22nw/hqdefault.jpg");
            return View("HelloAdmin");
        }

        public ActionResult ClientManager()
        {
            //var clients = SignalRHubContext.Clients.All;
            List<Client> clientlist = new List<Client>();

            var clients = SignalRHubContext.Clients;
            //foreach (var litem in clients)
            //{
            //    clientlist.Add( new Client(litem.ToString())
            //        );
            //}
            clientlist.Add(new Client(clients.ToString())
                    );
            return View(clientlist);
        }
    }
}