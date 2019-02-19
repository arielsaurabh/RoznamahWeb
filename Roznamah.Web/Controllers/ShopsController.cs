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
using Roznamah.Web.ViewModel;
using Umbraco.Core;
using Umbraco.Core.Models;
using System.Web.Http.Description;

namespace Roznamah.Web.Controllers
{
    [Route("api/[controller]")]
    public class ShopsController : UmbracoApiController
    {
        private readonly IContentService _contentService;

        public ShopsController()
        {
            _contentService = Services.ContentService;
        }


        [HttpGet]
        public IEnumerable<Shop> GetAllShops()
        {
            var root = _contentService.GetRootContent().FirstOrDefault();
            var shops = _contentService.GetDescendants(root.Id).Where(x => x.ContentType.Alias == "Shop");

            List<Shop> ShopList = new List<Shop>();
            foreach (var item in shops)
            {
                var mediaId = item.Properties[10].Id;
                var PROP = ApplicationContext.Current.Services.MediaService;
                var asdfasd = PROP.GetById(mediaId);

                ShopList.Add(new Shop
                {
                    Id = item.Id,
                    Name = item.Name,
                    NameEn = Convert.ToString(item.GetValue("ShopName")),
                    NameAr = Convert.ToString(item.GetValue("ShopNameArabic")),
                    Logo = getMediaUrl(Convert.ToString(item.GetValue("logo"))),
                    LocationEn = Convert.ToString(item.GetValue("ShopLocation")),
                    LocationAr = Convert.ToString(item.GetValue("ShopLocationArabic"))
                });

            }

            return ShopList;
        }

        [HttpGet]
        public Shop GetShop(int shopId)
        {
            var shopData = _contentService.GetById(shopId);
            Shop ShopItem = new Shop
            {
                Id = shopData.Id,
                Name = shopData.Name,
                NameEn = Convert.ToString(shopData.GetValue("ShopName")),
                NameAr = Convert.ToString(shopData.GetValue("ShopNameArabic")),
                Logo = getMediaUrl(Convert.ToString(shopData.GetValue("Logo"))),
                LocationEn = Convert.ToString(shopData.GetValue("ShopLocation")),
                LocationAr = Convert.ToString(shopData.GetValue("ShopLocationArabic"))
            };
            return ShopItem;
        }

        #region Helper Methods
        [ApiExplorerSettings(IgnoreApi = true)]
        public string getMediaUrl(string mediaValue)
        {

            if (mediaValue.Contains("umb://media") == false)
            {
                return mediaValue;
            }

            var mediaArray = mediaValue.Split(',');
            List<string> MediaList = new List<string>();
            foreach (var item in mediaArray)
            {
                var imageGuidUdi = GuidUdi.Parse(item);
                // Get the ID of the node!
                var imageNodeId = ApplicationContext.Current.Services.EntityService.GetIdForKey(imageGuidUdi.Guid, (UmbracoObjectTypes)Enum.Parse(typeof(UmbracoObjectTypes), imageGuidUdi.EntityType, true));

                var imageNode = Umbraco.TypedMedia(imageNodeId.Result);
                MediaList.Add(imageNode.Url());
            }

            return string.Join(",", MediaList.ToArray());
        }

        #endregion
    }
}