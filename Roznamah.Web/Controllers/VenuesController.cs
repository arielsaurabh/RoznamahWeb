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
    public class VenuesController : UmbracoApiController
    {
        private readonly IContentService _contentService;
        private readonly UmbracoHelper _umbracoHelper;

        public VenuesController()
        {
            _contentService = Services.ContentService;
            _umbracoHelper = new UmbracoHelper(UmbracoContext.Current);
        }

        [HttpGet]
        public IEnumerable<Venue> GetAllVenues()
        {
            //var contentServices = Services.ContentService;
            var root = _contentService.GetRootContent().FirstOrDefault();
            var venues = _contentService.GetDescendants(root.Id).Where(x => x.ContentType.Alias == "Venue");

            List<Venue> VenueList = new List<Venue>();
            foreach (var item in venues)
            {
                VenueList.Add(new Venue
                {
                    Id = item.Id,
                    Name = item.Name,
                    NameEn = Convert.ToString(item.GetValue("VenueName")),
                    NameAr = Convert.ToString(item.GetValue("VenueNameArabic")),
                    City = Convert.ToString(item.GetValue("VenueCity")),
                    DescriptionEn = Convert.ToString(item.GetValue("VenueDescription")),
                    DescriptionAR = Convert.ToString(item.GetValue("VenueDescriptionArabic")),
                    Location = Convert.ToString(item.GetValue("VenueLocation")),
                    TagsEn = Convert.ToString(item.GetValue("Tags")),
                    TagsAr= Convert.ToString(item.GetValue("TagsArabic")),
                    RepresentativeEn = Convert.ToString(item.GetValue("VenueRepresentative")),
                    RepresentativeAr = Convert.ToString(item.GetValue("VenueRepresentativeArabic")),
                    OpeningTime = Convert.ToString(item.GetValue("VenueOpeningTime")),
                    FloorMapEn = getMediaUrl(Convert.ToString(item.GetValue("FloorMap"))),
                    FloorMapAr = getMediaUrl(Convert.ToString(item.GetValue("FloorMapArabic"))),
                    Owner = Convert.ToString(item.GetValue("VenueOwner")),
                    ContactNumber = Convert.ToString(item.GetValue("VenueContactNumber")),
                    SpecialOccasions = Convert.ToString(item.GetValue("VenueSpecialOccasion"))
                });

            }

            return VenueList;
        }

        [HttpGet]
        public Venue GetVenue(int venueId)
        {

            var eventData = _contentService.GetById(venueId);
            Venue venueItem = new Venue
            {
                Id = eventData.Id,
                Name = eventData.Name,
                NameEn = Convert.ToString(eventData.GetValue("VenueName")),
                NameAr = Convert.ToString(eventData.GetValue("VenueNameArabic")),
                City = Convert.ToString(eventData.GetValue("VenueCity")),
                DescriptionEn = Convert.ToString(eventData.GetValue("VenueDescription")),
                DescriptionAR = Convert.ToString(eventData.GetValue("VenueDescriptionArabic")),
                Location = Convert.ToString(eventData.GetValue("VenueLocation")),
                TagsEn = Convert.ToString(eventData.GetValue("Tags")),
                TagsAr = Convert.ToString(eventData.GetValue("TagsArabic")),
                RepresentativeEn = Convert.ToString(eventData.GetValue("VenueRepresentative")),
                RepresentativeAr = Convert.ToString(eventData.GetValue("VenueRepresentativeArabic")),
                OpeningTime = Convert.ToString(eventData.GetValue("VenueOpeningTime")),
                FloorMapEn = getMediaUrl(Convert.ToString(eventData.GetValue("FloorMap"))),
                FloorMapAr = getMediaUrl(Convert.ToString(eventData.GetValue("FloorMapArabic"))),
                Owner = Convert.ToString(eventData.GetValue("VenueOwner")),
                ContactNumber = Convert.ToString(eventData.GetValue("VenueContactNumber")),
                SpecialOccasions = Convert.ToString(eventData.GetValue("VenueSpecialOccasion"))
            };
            return venueItem;
        }
        [HttpGet]

        //done by mahendra 
        public VenueDetails GetVenueDetails(int venueId)
        {

            var eventData = _contentService.GetById(venueId);
            VenueDetails venueItem = new VenueDetails
            {
             
                DescriptionEn = Convert.ToString(eventData.GetValue("VenueDescription")),
                DescriptionAR = Convert.ToString(eventData.GetValue("VenueDescriptionArabic")),
                OpeningTime = Convert.ToString(eventData.GetValue("VenueOpeningTime")),
         
            };
            return venueItem;
        }
        [HttpGet]
        public VenueLocation GetVenueLocation(int venueId)
        {
            var eventData = _contentService.GetById(venueId);
            VenueLocation venueItem = new VenueLocation
            {
              
                Location = Convert.ToString(eventData.GetValue("VenueLocation")),
              
            };
            return venueItem;
        }

        //public FloorMap FloorMap (int venueId)
        //{
        //    var eventData = _contentService.GetById(venueId);
        //    FloorMap venueItem = new FloorMap
        //    {
        //        FloorMapEn = Convert.ToString(eventData.GetValue("FloorMap")),
        //        FloorMapAr=Convert.ToString(eventData.GetValue("FloorMapArabic"))

        //    };
        //    return venueItem;
        //}

        public IEnumerable<FloorMap> FloorMap()
        {
            //var contentServices = Services.ContentService;
            var root = _contentService.GetRootContent().FirstOrDefault();
            var venues = _contentService.GetDescendants(root.Id).Where(x => x.ContentType.Alias == "Venue");

            List<FloorMap> VenueList = new List<FloorMap>();
            foreach (var item in venues)
            {
                VenueList.Add(new FloorMap
                {
                    FloorMapEn = getMediaUrl(Convert.ToString(item.GetValue("FloorMap"))),
                    FloorMapAr = getMediaUrl(Convert.ToString(item.GetValue("FloorMapArabic"))),
             });

            }

            return VenueList;
        }
        #region Helper Methods
        [ApiExplorerSettings(IgnoreApi = true)]
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