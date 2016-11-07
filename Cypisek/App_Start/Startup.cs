using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Owin;
using Microsoft.Owin;
using Microsoft.Extensions.DependencyInjection;

[assembly: OwinStartup(typeof(Cypisek.Startup))]

namespace Cypisek
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
        }
    }
}