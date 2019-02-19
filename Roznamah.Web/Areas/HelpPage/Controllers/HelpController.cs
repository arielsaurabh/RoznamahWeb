using System;
using System.Web.Http;
using System.Web.Mvc;
using Roznamah.Web.Areas.HelpPage.ModelDescriptions;
using Roznamah.Web.Areas.HelpPage.Models;

namespace Roznamah.Web.Areas.HelpPage.Controllers
{
    /// <summary>
    /// The controller that will handle requests for the help page.
    /// </summary>
    public class HelpController : System.Web.Mvc.Controller
    {
        private const string ErrorViewName = "Error";

        public HelpController()
            : this(GlobalConfiguration.Configuration)
        {
        }

        public HelpController(HttpConfiguration config)
        {
            Configuration = config;
        }

        public HttpConfiguration Configuration { get; private set; }

        public ActionResult Index()
        {
            ViewBag.DocumentationProvider = Configuration.Services.GetDocumentationProvider();
            var apiDescriptions = Configuration.Services.GetApiExplorer().ApiDescriptions;
            var apiDescriptionData = new System.Collections.ObjectModel.Collection<System.Web.Http.Description.ApiDescription>();
            if (apiDescriptions.Count > 100)
            {
                foreach (var description in apiDescriptions)
                {
                    if (description.ID.IndexOf("umbraco/Api/") != -1 && description.ID.IndexOf("umbraco/Api/Tags/") == -1 && description.ID.IndexOf("umbraco/Api/CanvasDesigner/") == -1)
                    {
                        if (description.ParameterDescriptions.Count >= 1 && description.ParameterDescriptions[description.ParameterDescriptions.Count-1].Name == "id")
                        {
                            description.ParameterDescriptions.RemoveAt(description.ParameterDescriptions.Count - 1);
                        }
                        //else
                        //{
                        //    description.ParameterDescriptions.Clear();
                        //}
                        description.RelativePath = description.RelativePath.Replace("/{id}", "");
                        apiDescriptionData.Add(description);
                    }
                }
            }
            return View(apiDescriptionData);
        }

        public ActionResult Api(string apiId)
        {
            if (!String.IsNullOrEmpty(apiId))
            {
                HelpPageApiModel apiModel = Configuration.GetHelpPageApiModel(apiId);
                if (apiModel != null)
                {
                    return View(apiModel);
                }
            }

            return View(ErrorViewName);
        }

        public ActionResult ResourceModel(string modelName)
        {
            if (!String.IsNullOrEmpty(modelName))
            {
                ModelDescriptionGenerator modelDescriptionGenerator = Configuration.GetModelDescriptionGenerator();
                ModelDescription modelDescription;
                if (modelDescriptionGenerator.GeneratedModels.TryGetValue(modelName, out modelDescription))
                {
                    return View(modelDescription);
                }
            }

            return View(ErrorViewName);
        }
    }
}