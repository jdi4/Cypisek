using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.SignalR;
using Cypisek.Data.Infrastructure;
using Cypisek.Data.Repositories;
using Cypisek.Mappings;
using Cypisek.Services;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Cypisek.App_Start
{
    public static class Bootstrapper
    {
        public static string mediaStorageDirLocation { get; set; }

        public static void Run()
        {
            SetAutofacContainer();
            //Configure AutoMapper
            AutoMapperConfiguration.Configure();
        }

        private static void SetAutofacContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();

            // Repositories
            builder.RegisterAssemblyTypes(typeof(EndPlayerClientRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(MediaFileRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerRequest();

            // Services
            builder.RegisterAssemblyTypes(typeof(EndPlayerClientService).Assembly)
               .Where(t => t.Name.EndsWith("Service"))
               .AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(MediaStorageService).Assembly)
               .Where(t => t.Name.EndsWith("Service"))
               .AsImplementedInterfaces().InstancePerRequest()
               .WithParameter("storageDirPath", mediaStorageDirLocation);

            //signalR - reqs. nuget for integraiton
            //builder.RegisterHubs(Assembly.GetExecutingAssembly());
            builder.RegisterType<Hubs.ContentHub>().ExternallyOwned();
            //container = builder.Build();

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new Autofac.Integration.Mvc.AutofacDependencyResolver(container));

            //var builder2 = new ContainerBuilder();
            //builder2.RegisterHubs(Assembly.GetExecutingAssembly());
            //IContainer container2 = builder2.Build();
            GlobalHost.DependencyResolver = new Autofac.Integration.SignalR.AutofacDependencyResolver(container);

            // refresh file db - move to uniform service
            //using (var scope = container.BeginLifetimeScope())
            //{
            //}
            //var service = scope.Resolve<IMediaStorageService>();
            var service = DependencyResolver.Current.GetService<IMediaStorageService>();
            service.RefreshFileDB();
        }
    }
}