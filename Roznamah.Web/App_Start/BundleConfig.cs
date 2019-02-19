using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace WebApp.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/Script/Bundles")
            //     .Include(
            //         "~/bundles/runtime.*",
            //         "~/bundles/polyfills.*",
            //         "~/bundles/scripts.*",
            //         "~/bundles/vendor.*",
            //         "~/bundles/main.*",
            //         "~/bundles/login-login-module.*"
            //         ));

            ////bundles.Add(new StyleBundle("~/Bundles/AppStyles")
            ////    .Include("~/Portal/src/assets/font-awesome.min.css",
            ////    "~/Portal/src/assets/material-design-iconic-font.min.css",
            ////    "~/Portal/src/assets/util.css"
            ////    ));
            //bundles.Add(new StyleBundle("~/Bundles/AppStyles")
            //    .Include("~/Portal/src/styles.css"));

            //bundles.Add(new StyleBundle("~/Bundles/FontAwesomeStyles")
            //               .Include("~/Portal/node_modules/@angular/material/prebuilt-themes/pink-bluegrey.css"));

            //bundles.Add(new StyleBundle("~/Bundles/BootstrapStyles")
            //    .Include("~/Portal/node_modules/bootstrap/dist/css/bootstrap.min.css", "~/assets/bootstrap-social.css", "~/assets/font-awesome.min.css"));
        }
    }
}