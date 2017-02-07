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
            SetAutoFacSignalrContatiner();

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

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new Autofac.Integration.Mvc.AutofacDependencyResolver(container));

            // refresh file db - move to uniform service
            //using (var scope = container.BeginLifetimeScope())
            //{
            //}
            //var service = scope.Resolve<IMediaStorageService>();

            //var service = DependencyResolver.Current.GetService<IMediaStorageService>();
            //service.RefreshFileDB();
        }

        private static void SetAutoFacSignalrContatiner()
        {
            var builderSignalR = new ContainerBuilder();
            builderSignalR.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builderSignalR.RegisterType<DbFactory>().As<IDbFactory>().InstancePerLifetimeScope();
            builderSignalR.RegisterAssemblyTypes(typeof(EndPlayerClientRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();

            builderSignalR.RegisterAssemblyTypes(typeof(EndPlayerClientService).Assembly)
               .Where(t => t.Name.EndsWith("Service"))
               .AsImplementedInterfaces().InstancePerLifetimeScope();

            builderSignalR.RegisterHubs(Assembly.GetExecutingAssembly());


            IContainer container2 = builderSignalR.Build();
            GlobalHost.DependencyResolver = new Autofac.Integration.SignalR.AutofacDependencyResolver(container2);

        }
    }
}