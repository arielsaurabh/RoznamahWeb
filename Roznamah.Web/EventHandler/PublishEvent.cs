using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Umbraco.Core;
using Umbraco.Web.UI;
using Umbraco.Web.UI.Pages;
using Umbraco.Core.Events;
using Umbraco.Core.Models;
using Umbraco.Core.Services;


namespace Roznamah.Web.EventHandler
{
    public class PublishEvent : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            Umbraco.Core.Services.ContentService.Publishing += ContentService_Publishing;
        }

        //Override the publish button event for custom events/validation
        void ContentService_Publishing(Umbraco.Core.Publishing.IPublishingStrategy sender, Umbraco.Core.Events.PublishEventArgs<Umbraco.Core.Models.IContent> e)
        {

            if (e.PublishedEntities.FirstOrDefault().ContentType.Alias == "Event")
            {
                var eventPage = e.PublishedEntities.FirstOrDefault();
                var promotionLogo = Convert.ToString(eventPage.GetValue("promotionLogo"));
                var pNameEnglish = Convert.ToString(eventPage.GetValue("pName"));
                var pNameArabic = Convert.ToString(eventPage.GetValue("pNameArabic"));
                if (promotionLogo != null && promotionLogo != "")
                {
                    //  var clientTool = new ClientTools((Page)HttpContext.Current.CurrentHandler);
                    if (pNameEnglish == "" && pNameArabic == "")
                    {
                        eventPage.SetValue("promotionLogo", "");
                        e.CancelOperation(new EventMessage("Warning", "Name (English) field is mendatory and Name (Arabic) field is mendatory.", EventMessageType.Warning));

                    }
                    if (pNameEnglish == "" && pNameArabic != "")
                    {
                        eventPage.SetValue("promotionLogo", "");
                        e.CancelOperation(new EventMessage("Warning", "Name (English) field is mendatory.", EventMessageType.Warning));

                    }
                    if (pNameEnglish != "" && pNameArabic == "")
                    {
                        eventPage.SetValue("promotionLogo", "");
                        e.CancelOperation(new EventMessage("Warning", "Name (Arabic) field is mendatory.", EventMessageType.Warning));

                    }

                }
            }
        }

    }


}

