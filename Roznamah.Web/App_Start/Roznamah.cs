using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Roznamah.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Umbraco.Core;
using Umbraco.Core.Services;
using Umbraco.Web;

namespace Roznamah.Web.App_Start
{
    public class Roznamah : IApplicationEventHandler
    {
        public void OnApplicationStarted(
            UmbracoApplicationBase httpApplication, Umbraco.Core.ApplicationContext applicationContext)
        {
            var builder = new ContainerBuilder();

            // register all controllers found in this assembly
            builder.RegisterControllers(typeof(Roznamah).Assembly);
            builder.RegisterApiControllers(typeof(Roznamah).Assembly);

            // register Umbraco MVC + web API controllers used by the admin site
            builder.RegisterControllers(typeof(Roznamah).Assembly);
            builder.RegisterApiControllers(typeof(Roznamah).Assembly);

            // add custom class to the container as Transient instance
            //builder.RegisterType<ServiceContext>();
            //builder.RegisterType<IEntitiesService>();
            builder.RegisterType<EntitiesService>();


            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        public void OnApplicationInitialized(UmbracoApplicationBase httpApplication, Umbraco.Core.ApplicationContext applicationContext)
        {
        }

        public void OnApplicationStarting(UmbracoApplicationBase httpApplication, Umbraco.Core.ApplicationContext applicationContext)
        {
        }

    }
}