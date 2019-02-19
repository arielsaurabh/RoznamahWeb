using Our.Umbraco.AuthU.Web.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Umbraco.Web.WebApi;
using Umbraco.Core.Services;
using Umbraco.Web;

namespace Roznamah.Web.Controller
{
    [Route("api/[controller]")]
    [OAuth]
    public class HomeController : UmbracoApiController
    {

        /// <summary>
        /// Get home page properties
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public IEnumerable<pageProperties> Home()
        {
            var contentServices = Services.ContentService;
            var root = contentServices.GetRootContent().FirstOrDefault();
            var events = contentServices.GetDescendants(root.Id).Where(x => x.ContentType.Alias == "event");
            var asdfasdf = contentServices.GetById(1062);
            foreach (var item in events)
            {
                var eventName = item.GetValue("eventName");
                var eventThumbnail = item.GetValue("eventThumbnail");
                var eventVideo = item.GetValue("eventVideo");
                var eventWebsite = item.GetValue("eventWebsite");
                var eventOrganizer = item.GetValue("eventOrganizer");
                var eventTags = item.GetValue("eventTags");
                var eventContents = item.GetValue("eventContents");
                var audiences = item.GetValue("audiences");
                var eventStatus = item.GetValue("eventStatus");
                var addCity = item.GetValue("addCity");
            }
            //DynamicRecordList. GetRecordsFromPage(int pageId)

            var node = Umbraco.TypedContent(1058);
            var fieldValue = node.Properties.ToList();
            List<pageProperties> prop = new List<pageProperties> { };
            for (int i = 0; i < fieldValue.Count; i++)
            {
                prop.Add(new pageProperties
                {
                    DataValue = fieldValue[i].DataValue.ToString(),
                    HasValue = fieldValue[i].HasValue,
                    NodeAlias = fieldValue[i].PropertyTypeAlias.ToString()
                });
            }
            return prop;
        }
    }

    #region helper classes

    public class pageProperties
    {
        public string DataValue { get; set; }
        public bool HasValue { get; set; }
        public string NodeAlias { get; set; }
    }

    #endregion
}