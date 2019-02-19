using Roznamah.Model.Entities;
using Roznamah.Model.Theme;
using Roznamah.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core;
using Umbraco.Core.Services;

namespace Roznamah.Services
{
    public class ThemeService : IThemeService
    {
        private readonly IContentService _contentService;

        public ThemeService() {
            _contentService = ApplicationContext.Current.Services.ContentService; ;
        }

        public IEnumerable<Theme> GetAllThemes()
        {
            var root = _contentService.GetRootContent().FirstOrDefault();
            var Themes = _contentService.GetDescendants(root.Id).Where(x => x.ContentType.Alias == "Theme");

            List<Theme> ThemesList = new List<Theme>();
            foreach (var item in Themes)
            {
                ThemesList.Add(new Theme
                {
                    Id = item.Id,
                    Name = item.Name,
                    PageBannerEn = getMediaUrl(Convert.ToString(item.GetValue("PageBanner"))),
                    PageBannerAr = Convert.ToString(item.GetValue("PageBannerArabic")),
                    PageContentEn = Convert.ToString(item.GetValue("PageContents")),
                    PageContentAr = Convert.ToString(item.GetValue("PageContentsArabic")),
                    BrochureEn = getMediaUrl(Convert.ToString(item.GetValue("UploadBrochure"))),
                    BrochureAr = getMediaUrl(Convert.ToString(item.GetValue("UploadBrochureArabic"))),
                    PageEvents = getDocumentByUId(Convert.ToString(item.GetValue("PageEvents")))
                });
            }

            return ThemesList;
        }

        public Theme GetTheme(int themeId)
        {
            var themeData = _contentService.GetById(themeId);
            Theme ThemeItem = new Theme
            {
                Id = themeData.Id,
                Name = themeData.Name,
                PageBannerEn = Convert.ToString(themeData.GetValue("PageBanner")),
                PageBannerAr = Convert.ToString(themeData.GetValue("PageBannerArabic")),
                PageContentEn = Convert.ToString(themeData.GetValue("PageContents")),
                PageContentAr = Convert.ToString(themeData.GetValue("PageContentsArabic")),
                BrochureEn = Convert.ToString(themeData.GetValue("UploadBrochure")),
                BrochureAr = Convert.ToString(themeData.GetValue("UploadBrochureArabic")),
                PageEvents = Convert.ToString(themeData.GetValue("PageEvents"))
            };
            return ThemeItem;
        }

        #region Helper Methods
        public string getDocumentByUId(string UId)
        {

            if (UId.Contains("umb://") == false)
            {
                return UId;
            }

            var UIdArray = UId.Split(',');
            List<string> DocIdList = new List<string>();
            foreach (var item in UIdArray)
            {
                DocIdList.Add(_umbracoHelper.TypedContent(Udi.Parse(item)).ToString());
            }

            return string.Join(",", DocIdList.ToArray());
        }

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
