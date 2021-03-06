﻿using Cypisek.App_Start;
using Cypisek.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Cypisek
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            System.Data.Entity.Database.SetInitializer(new CypisekSeedData());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Application["mediaStorageDirLocation"] = Server.MapPath("~/MediaStorage");
            Bootstrapper.mediaStorageDirLocation = Server.MapPath("~/MediaStorage");

            Bootstrapper.Run();
        }
    }
}
