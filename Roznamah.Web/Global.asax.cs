using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using System.Web.Optimization;
using WebApp.App_Start;
using WebActivatorEx;
using Umbraco.Core;
using Umbraco.Web;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;

[assembly: PostApplicationStartMethod(typeof(UmbracoPluginInitializer), "PostApplicationStart")]
//namespace ConstructionStars.Web
//{
//    public class Global : Umbraco.Web.UmbracoApplication
//    {
//        protected override void OnApplicationStarted(object sender, EventArgs e)
//        {
//            base.OnApplicationStarted(sender, e);
//            // Code that runs on application startup
//            // AreaRegistration.RegisterAllAreas();
//            //GlobalConfiguration.Configure(WebApiConfig.Register);
//            //RouteConfig.RegisterRoutes(RouteTable.Routes);
//            BundleConfig.RegisterBundles(BundleTable.Bundles);
//        }
//    }
//}

namespace WebApp.App_Start
{
    public static class UmbracoPluginInitializer
    {
        /// 
        /// Runs right after the application has initialized.
        /// 
        public static void PostApplicationStart()
        {
            // Run bundle optimizer.
            // AreaRegistration.RegisterAllAreas();
            // GlobalConfiguration.Configure(WebApiConfig.Register);
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //   GlobalConfiguration.Configuration.EnsureInitialized();
            AreaRegistration.RegisterAllAreas();
        }
    }

    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
        }
    }
}