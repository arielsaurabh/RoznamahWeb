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
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Roznamah.Web.Controllers
{
    [Route("api/[controller]")]


    public class EventsController : UmbracoApiController
    {
        private readonly IContentService _contentService;
        private readonly UmbracoHelper _umbracoHelper;

        public EventsController()
        {
            _contentService = Services.ContentService;
            _umbracoHelper = new UmbracoHelper(UmbracoContext.Current);
        }

        /// <summary>
        /// Get all events
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Event> GetAllEvents()
        {
            //var contentServices = Services.ContentService;
            var root = _contentService.GetRootContent().FirstOrDefault();
            var events = _contentService.GetDescendants(root.Id).Where(x => x.ContentType.Alias == "Event");

            List<Event> EventList = new List<Event>();
            foreach (var item in events)
            {
                EventList.Add(new Event
                {
                    Id = item.Id,
                    Name = item.Name,
                    NameEng = Convert.ToString(item.GetValue("EventName")),
                    NameAr = Convert.ToString(item.GetValue("EventNameArabic")),
                    Thumbnail = Convert.ToString(item.GetValue("EventThumbnail")),
                    VideoEng = Convert.ToString(item.GetValue("EventVideo")),
                    VideoAr = Convert.ToString(item.GetValue("EventVideoArabic")),
                    Website = Convert.ToString(item.GetValue("EventWebsite")),
                    Orgnizer = Convert.ToString(item.GetValue("EventOrganizer")),
                    Tags = Convert.ToString(item.GetValue("EventTags")),
                    ContentsEng = Convert.ToString(item.GetValue("EventContents")),
                    ContentsAr = Convert.ToString(item.GetValue("EventContentsArabic")),
                    Audience = Convert.ToString(item.GetValue("Audiences")),
                    Status = Convert.ToString(item.GetValue("EventStatus")),
                    Occurrences = Convert.ToString(item.GetValue("EventOccurance")),
                    Tickets = Convert.ToString(item.GetValue("Tickets")),
                    Promotion = new Promotions
                    {
                        Logo = Convert.ToString(item.GetValue("PromotionLogo")),
                        NameEng = Convert.ToString(item.GetValue("PName")),
                        NameAr = Convert.ToString(item.GetValue("PNameArabic")),
                        OfferDescriptionEng = Convert.ToString(item.GetValue("OfferDescription")),
                        OfferDescriptionAr = Convert.ToString(item.GetValue("OfferDescriptionArabic")),
                        Duration = Convert.ToString(item.GetValue("PromotionDuration"))
                    },
                    AdditionalInfo = new AdditionalInformation
                    {
                        ActivitiesEng = Convert.ToString(item.GetValue("Activities")),
                        ActivitiesAr = Convert.ToString(item.GetValue("ActivitiesArabic")),
                        BeforYouComeEng = Convert.ToString(item.GetValue("BeforeYouCome")),
                        BeforYouComeAr = Convert.ToString(item.GetValue("BeforeYouComeArabic")),
                        AmenitiesEng = Convert.ToString(item.GetValue("BeforeYouCome")),
                        AmenitiesAr = Convert.ToString(item.GetValue("BeforeYouComeArabic"))
                    },
                    SocialImages = new SocialImages
                    {
                        ImageEng = Convert.ToString(item.GetValue("UploadImageComponent")),
                        ImageAr = Convert.ToString(item.GetValue("UploadImageComponentArabic")),
                        TwitterImages = Convert.ToString(item.GetValue("RetrieveFromTwitter"))
                    }

                });

            }

            return EventList;
        }

        /// <summary>
        /// Get Event detail by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public Event GetEvent(int eventId)
        {

            var eventData = _contentService.GetById(eventId);
            if (eventData == null)
            {
                return new Event();
            }
            Event eventItem = new Event
            {
                Id = eventData.Id,
                Name = eventData.Name,
                NameEng = Convert.ToString(eventData.GetValue("EventName")),
                NameAr = Convert.ToString(eventData.GetValue("EventNameArabic")),
                Thumbnail = getMediaUrl(Convert.ToString(eventData.GetValue("EventThumbnail"))),
                VideoEng = getMediaUrl(Convert.ToString(eventData.GetValue("EventVideo"))),
                VideoAr = getMediaUrl(Convert.ToString(eventData.GetValue("EventVideoArabic"))),
                Website = Convert.ToString(eventData.GetValue("EventWebsite")),
                Orgnizer = getDocumentByUId(Convert.ToString(eventData.GetValue("EventOrganizer"))),
                Tags = Convert.ToString(eventData.GetValue("EventTags")),
                ContentsEng = Convert.ToString(eventData.GetValue("EventContents")),
                ContentsAr = Convert.ToString(eventData.GetValue("EventContentsArabic")),
                Audience = Convert.ToString(eventData.GetValue("Audiences")),
                Status = Convert.ToString(eventData.GetValue("EventStatus")),
                Occurrences = Convert.ToString(eventData.GetValue("EventOccurance")),
                Tickets = Convert.ToString(eventData.GetValue("Tickets")),
                Promotion = new Promotions
                {
                    Logo = getMediaUrl(Convert.ToString(eventData.GetValue("PromotionLogo"))),
                    NameEng = Convert.ToString(eventData.GetValue("PName")),
                    NameAr = Convert.ToString(eventData.GetValue("PNameArabic")),
                    OfferDescriptionEng = Convert.ToString(eventData.GetValue("OfferDescription")),
                    OfferDescriptionAr = Convert.ToString(eventData.GetValue("OfferDescriptionArabic")),
                    Duration = Convert.ToString(eventData.GetValue("PromotionDuration"))
                },
                AdditionalInfo = new AdditionalInformation
                {
                    ActivitiesEng = Convert.ToString(eventData.GetValue("Activities")),
                    ActivitiesAr = Convert.ToString(eventData.GetValue("ActivitiesArabic")),
                    BeforYouComeEng = Convert.ToString(eventData.GetValue("BeforeYouCome")),
                    BeforYouComeAr = Convert.ToString(eventData.GetValue("BeforeYouComeArabic")),
                    AmenitiesEng = Convert.ToString(eventData.GetValue("BeforeYouCome")),
                    AmenitiesAr = Convert.ToString(eventData.GetValue("BeforeYouComeArabic"))
                },
                SocialImages = new SocialImages
                {
                    ImageEng = getMediaUrl(Convert.ToString(eventData.GetValue("UploadImageComponent"))),
                    ImageAr = getMediaUrl(Convert.ToString(eventData.GetValue("UploadImageComponentArabic"))),
                    TwitterImages = Convert.ToString(eventData.GetValue("RetrieveFromTwitter"))
                }

            };

            return eventItem;
        }

        //api done by mahendra 14 feb 2019
        [HttpGet]
       
        public IEnumerable<Event> GetUpcomingEvent()
        {

            var root = _contentService.GetRootContent().FirstOrDefault();
            var events = _contentService.GetDescendants(root.Id).Where(x => x.ContentType.Alias == "Event");

            List<Event> EventList = new List<Event>();
            foreach (var item in events)
            {
                var Occurrences = Convert.ToString(item.GetValue("EventOccurance"));
                var data = JsonConvert.DeserializeObject(Occurrences);
                var result = getEventStatus(Occurrences);
                var isUpcomingEvent = false;

                if (isUpcomingEvent)
                {
                    EventList.Add(new Event
                    {
                        Id = item.Id,
                        Name = item.Name,
                        NameEng = Convert.ToString(item.GetValue("EventName")),
                        NameAr = Convert.ToString(item.GetValue("EventNameArabic")),
                        Thumbnail = Convert.ToString(item.GetValue("EventThumbnail")),
                        VideoEng = Convert.ToString(item.GetValue("EventVideo")),
                        VideoAr = Convert.ToString(item.GetValue("EventVideoArabic")),
                        Website = Convert.ToString(item.GetValue("EventWebsite")),
                        Orgnizer = Convert.ToString(item.GetValue("EventOrganizer")),
                        Tags = Convert.ToString(item.GetValue("EventTags")),
                        ContentsEng = Convert.ToString(item.GetValue("EventContents")),
                        ContentsAr = Convert.ToString(item.GetValue("EventContentsArabic")),
                        Audience = Convert.ToString(item.GetValue("Audiences")),
                        Status = Convert.ToString(item.GetValue("EventStatus")),
                        Occurrences = Convert.ToString(item.GetValue("EventOccurance")),
                        Tickets = Convert.ToString(item.GetValue("Tickets")),
                        Promotion = new Promotions
                        {
                            Logo = Convert.ToString(item.GetValue("PromotionLogo")),
                            NameEng = Convert.ToString(item.GetValue("PName")),
                            NameAr = Convert.ToString(item.GetValue("PNameArabic")),
                            OfferDescriptionEng = Convert.ToString(item.GetValue("OfferDescription")),
                            OfferDescriptionAr = Convert.ToString(item.GetValue("OfferDescriptionArabic")),
                            Duration = Convert.ToString(item.GetValue("PromotionDuration"))
                        },
                        AdditionalInfo = new AdditionalInformation
                        {
                            ActivitiesEng = Convert.ToString(item.GetValue("Activities")),
                            ActivitiesAr = Convert.ToString(item.GetValue("ActivitiesArabic")),
                            BeforYouComeEng = Convert.ToString(item.GetValue("BeforeYouCome")),
                            BeforYouComeAr = Convert.ToString(item.GetValue("BeforeYouComeArabic")),
                            AmenitiesEng = Convert.ToString(item.GetValue("BeforeYouCome")),
                            AmenitiesAr = Convert.ToString(item.GetValue("BeforeYouComeArabic"))
                        },
                        SocialImages = new SocialImages
                        {
                            ImageEng = Convert.ToString(item.GetValue("UploadImageComponent")),
                            ImageAr = Convert.ToString(item.GetValue("UploadImageComponentArabic")),
                            TwitterImages = Convert.ToString(item.GetValue("RetrieveFromTwitter"))
                        }

                    });
                }

            }

            return EventList;
        }
        [HttpGet]
        public IEnumerable<Event> GetDailyNewlyEvent()
        {

            var root = _contentService.GetRootContent().FirstOrDefault();
            var events = _contentService.GetDescendants(root.Id).Where(x => x.ContentType.Alias == "Event");

            List<Event> EventList = new List<Event>();
            foreach (var item in events)
            {
                EventList.Add(new Event
                {
                    Id = item.Id,
                    Name = item.Name,
                    NameEng = Convert.ToString(item.GetValue("EventName")),
                    NameAr = Convert.ToString(item.GetValue("EventNameArabic")),
                    Thumbnail = Convert.ToString(item.GetValue("EventThumbnail")),
                    VideoEng = Convert.ToString(item.GetValue("EventVideo")),
                    VideoAr = Convert.ToString(item.GetValue("EventVideoArabic")),
                    Website = Convert.ToString(item.GetValue("EventWebsite")),
                    Orgnizer = Convert.ToString(item.GetValue("EventOrganizer")),
                    Tags = Convert.ToString(item.GetValue("EventTags")),
                    ContentsEng = Convert.ToString(item.GetValue("EventContents")),
                    ContentsAr = Convert.ToString(item.GetValue("EventContentsArabic")),
                    Audience = Convert.ToString(item.GetValue("Audiences")),
                    Status = Convert.ToString(item.GetValue("EventStatus")),
                    Occurrences = Convert.ToString(item.GetValue("EventOccurance")),
                    Tickets = Convert.ToString(item.GetValue("Tickets")),
                    Promotion = new Promotions
                    {
                        Logo = Convert.ToString(item.GetValue("PromotionLogo")),
                        NameEng = Convert.ToString(item.GetValue("PName")),
                        NameAr = Convert.ToString(item.GetValue("PNameArabic")),
                        OfferDescriptionEng = Convert.ToString(item.GetValue("OfferDescription")),
                        OfferDescriptionAr = Convert.ToString(item.GetValue("OfferDescriptionArabic")),
                        Duration = Convert.ToString(item.GetValue("PromotionDuration"))
                    },
                    AdditionalInfo = new AdditionalInformation
                    {
                        ActivitiesEng = Convert.ToString(item.GetValue("Activities")),
                        ActivitiesAr = Convert.ToString(item.GetValue("ActivitiesArabic")),
                        BeforYouComeEng = Convert.ToString(item.GetValue("BeforeYouCome")),
                        BeforYouComeAr = Convert.ToString(item.GetValue("BeforeYouComeArabic")),
                        AmenitiesEng = Convert.ToString(item.GetValue("BeforeYouCome")),
                        AmenitiesAr = Convert.ToString(item.GetValue("BeforeYouComeArabic"))
                    },
                    SocialImages = new SocialImages
                    {
                        ImageEng = Convert.ToString(item.GetValue("UploadImageComponent")),
                        ImageAr = Convert.ToString(item.GetValue("UploadImageComponentArabic")),
                        TwitterImages = Convert.ToString(item.GetValue("RetrieveFromTwitter"))
                    }

                });

            }

            return EventList;
        }

        [HttpGet]
        public TimeSchedule GetPlaceAndTimeOfEvent(int eventId)
        {

            var eventData = _contentService.GetById(eventId);

            TimeSchedule eventItem = new TimeSchedule
            {
              
                Occurrences = Convert.ToString(eventData.GetValue("EventOccurance")),
            };

            return eventItem;
        }

        [HttpGet]
        public AdditionalInformationActivites GetActivitiesofEvent(int eventId)
        {

            var eventData = _contentService.GetById(eventId);


            AdditionalInformationActivites AdditionalInfo = new AdditionalInformationActivites
            {
                ActivitiesEng = Convert.ToString(eventData.GetValue("Activities")),
                ActivitiesAr = Convert.ToString(eventData.GetValue("ActivitiesArabic")),

            };


          

            return AdditionalInfo;
        }

        [HttpGet]
        public AdditionalInformationAmenities GetAminities(int eventId)
        {
            var eventData = _contentService.GetById(eventId);

            AdditionalInformationAmenities AdditionalInfo = new AdditionalInformationAmenities
            {
                AmenitiesEng = Convert.ToString(eventData.GetValue("BeforeYouCome")),
                AmenitiesAr = Convert.ToString(eventData.GetValue("BeforeYouComeArabic"))
            };

            return AdditionalInfo;
        }
        [HttpGet]
        public TimeSchedule GetTimeScheduleofEvent(int eventId)
        {

            var eventData = _contentService.GetById(eventId);

            TimeSchedule eventItem = new TimeSchedule
            {
              
                Occurrences = Convert.ToString(eventData.GetValue("EventOccurance")),
            };

            return eventItem;
        }
        
        [HttpGet]

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

        [ApiExplorerSettings(IgnoreApi = true)]
        public string getEventStatus(string Occurrences)
        {
          
           var data = JArray.Parse(Occurrences);
           
            for (int i = 3; i < data.Count - 1; i++)
            {
                // var occurance = JObject.Parse(data[i]["EventOccurance"]);
            }
            return "";
        }

        #endregion
    }


}