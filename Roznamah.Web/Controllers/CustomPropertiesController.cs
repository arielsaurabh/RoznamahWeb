using Google.Apis.Urlshortener.v1;
using Google.Apis.Urlshortener.v1.Data;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TweetSharp;
using System.Threading.Tasks;
using Umbraco.Core.Services;
using Umbraco.Web;
using Roznamah.Web.ViewModel;

namespace Roznamah.Web.Controllers
{
    public class CustomPropertiesController : Umbraco.Web.WebApi.UmbracoApiController
    {
        // Global variable for custom propery list
        public List<Items> objListItems = new List<Items>();

        private readonly UrlshortenerService service;
        private readonly IContentService _contentService;
        private readonly UmbracoHelper _umbracoHelper;

        public CustomPropertiesController() {
            service = new UrlshortenerService(new BaseClientService.Initializer
            {
                ApplicationName = "UrlShortener.ShortenURL sample",
                ApiKey = "AIzaSyAMzN-xoUrT15H_0LPJxuwKVV5Sq44acXk"
            });

            _contentService = Services.ContentService;
            _umbracoHelper = new UmbracoHelper(UmbracoContext.Current);
        }

        #region Global methods

        /// <summary>
        /// get property id and name
        /// </summary>
        /// <returns></returns>
        public List<Items> getItems()
        {
            var contentService = Services.ContentService;
            var nodeId = contentService.GetById(2068);
            var items = contentService.GetDescendants(45);
            foreach (var item in items)
            {
                Items objItems = new Items();
                objItems.Name = item.Name;
                objItems.Id = item.Id;
                objListItems.Add(objItems);
            }
            return objListItems;
        }

        /// <summary>
        /// Get list of all shops for events
        /// </summary>
        /// <returns>Items list</returns>
        public List<Items> GetAllShops()
        {
            var contentService = Services.ContentService;
            var root = contentService.GetRootContent().Where(x => x.ContentType.Alias == "Home").FirstOrDefault();
            var shops = contentService.GetDescendants(root).Where(x => x.ContentType.Alias == "Shop");
            foreach (var item in shops)
            {
                Items objItems = new Items();
                objItems.Name = item.Name;
                objItems.Id = item.Id;
                objListItems.Add(objItems);
            }
            return objListItems;
        }

        /// <summary>
        /// Get all cities list
        /// </summary>
        /// <returns>cities list</returns>
        public List<Items> GetAllCities()
        {
            var contentService = Services.ContentService;
            var nodeId = contentService.GetById(1082);
            var items = contentService.GetDescendants(nodeId.Id);
            foreach (var item in items)
            {
                Items objItems = new Items();
                objItems.Name = item.Name;
                objItems.Id = item.Id;
                objListItems.Add(objItems);
            }
            return objListItems;
        }
        #endregion

        #region events

        /// <summary>
        /// Get opening time for the event
        /// </summary>
        /// <param name="pageId"></param>
        /// <returns>venue opening time</returns>
        public String GetOpeningTimeForEvent(string pageId)
        {
            var contentService = Services.ContentService;
            var eventData = contentService.GetById(int.Parse(pageId));
            if (eventData != null)
            {
                return Convert.ToString(eventData.GetValue("venueOpeningTime"));
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Get tickets for event
        /// </summary>
        /// <param name="pageId"></param>
        /// <returns>tickets details</returns>
        public String GetTickets(string pageId)
        {
            var contentService = Services.ContentService;
            var ticketData = contentService.GetById(int.Parse(pageId));
            if (ticketData != null)
            {
                var eventPage = Convert.ToString(contentService.GetById(int.Parse(pageId)).GetValue("tickets"));
                return eventPage;
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// Get Special occasions for event
        /// </summary>
        /// <param name="pageId"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public dynamic GetSpecialOccasionsForEvent(string pageId, string key)
        {
            var contentService = Services.ContentService;
            var eventPage = Convert.ToString(contentService.GetById(int.Parse(pageId)).GetValue("venueSpecialOccasion"));
            if (String.IsNullOrEmpty(eventPage))
            {
                return null;
            }
            else
            {
                return eventPage;
            }

        }

        /// <summary>
        /// Get event occurances for details
        /// </summary>
        /// <param name="pageId"></param>
        /// <returns></returns>
        public dynamic GetEventOccurance(string pageId)
        {
            var contentService = Services.ContentService;
            var eventData = contentService.GetById(int.Parse(pageId));

            if (eventData != null)
            {
                var eventPage = Convert.ToString(contentService.GetById(int.Parse(pageId)).GetValue("eventOccurance"));
                return string.IsNullOrEmpty(eventPage) ? null : eventPage;
            }
            else
            {
                return null;
            }

        }


        /// <summary>
        /// Get Twitter Images data for current page/event
        /// </summary>
        /// <param name="pageId"></param>
        /// <returns></returns>
        public dynamic GetTwitterImagesData(string pageId)
        {
            var contentService = Services.ContentService;
            var eventData = contentService.GetById(int.Parse(pageId));

            if (eventData != null)
            {
                var eventPage = Convert.ToString(contentService.GetById(int.Parse(pageId)).GetValue("retrieveFromTwitter"));
                return string.IsNullOrEmpty(eventPage) ? null : eventPage;
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// Fetch Twitter images by tag name
        /// </summary>
        /// <param name="pageId"></param>
        /// <returns></returns>
        public List<PropertyItem> GetTwitterImages(string pageId, string hashTags)
        {
            var contentService = Services.ContentService;
            List<PropertyItem> Images = new List<PropertyItem>();
            var eventData = contentService.GetById(int.Parse(pageId));

            if (eventData != null)
            {
                //var eventPage = Convert.ToString(eventData.GetValue("eventTags"));
                //var tags = new JavaScriptSerializer().Deserialize<string[]>(eventPage);
                List<String> tags = new List<String>();
                if (hashTags.Contains(','))
                {
                    tags = hashTags.Split(',').ToList();
                }
                else
                {
                    tags.Add(hashTags);
                }
                if (tags.Count > 0)
                {
                    var twitterKey = ConfigurationManager.AppSettings["Twitter_Key"];
                    var twitterSecret = ConfigurationManager.AppSettings["Twitter_Secret"];
                    var twitterAccessToken = ConfigurationManager.AppSettings["Twitter_accessToken"];
                    var twitterAccessTokenSecret = ConfigurationManager.AppSettings["Twitter_accessTokenSecret"];
                    var twitterQueryForSearch = ConfigurationManager.AppSettings["Twitter_accountName"];

                    TwitterService twitterService = new TwitterService(twitterKey, twitterSecret);
                    twitterService.AuthenticateWith(twitterAccessToken, twitterAccessTokenSecret);

                    var tweets_search = twitterService.Search(new SearchOptions { Q = twitterQueryForSearch, Resulttype = TwitterSearchResultType.Recent, Count = 1000 });

                    List<TwitterStatus> resultList = new List<TwitterStatus>(tweets_search.Statuses);
                    foreach (var tweet in tweets_search.Statuses)
                    {
                        foreach (var tag in tags)
                        {
                            var isHastTagPresent = tweet.Entities.HashTags.Where(s => s.Text == tag).ToList();
                            if (isHastTagPresent.Count > 0)
                            {
                                foreach (var media in tweet.Entities.Media)
                                {
                                    if (media.MediaType == TwitterMediaType.Photo)
                                    {
                                        Images.Add(new PropertyItem { Name = media.IdAsString, Value = media.MediaUrlHttps });
                                    }
                                }
                            }
                        }

                    }

                }
            }

            return Images;

        }

        #endregion

        #region Test Functions
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageId"></param>
        /// <returns></returns>
        public String Test(string pageId)
        {
            var contentService = Services.ContentService;
            var eventPage = Convert.ToString(contentService.GetById(int.Parse(pageId)).GetValue("test"));
            return eventPage;
        }

        #endregion

        #region Venue

        /// <summary>
        /// Get all venues by city id
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public List<Items> GetAllVelues(string cityId)
        {
            var cityID = Convert.ToInt32(cityId);
            var contentService = Services.ContentService;
            var cityName = contentService.GetById(cityID).Name;
            var nodeId = contentService.GetById(4131);
            var items2 = contentService.GetDescendants(nodeId.Id).Where(x => x.GetValue<int>("venueCity") == cityID);
            var items = contentService.GetDescendants(nodeId.Id);
            foreach (var item in items2)
            {
                Items objItems = new Items();
                objItems.Name = item.Name;
                objItems.Id = item.Id;
                objListItems.Add(objItems);
            }
            return objListItems;
        }

        /// <summary>
        /// Get location, time and special occasion for venues
        /// </summary>
        /// <param name="venueId"></param>
        /// <returns></returns>
        public List<PropertyItem> GetLocationForVenue(string venueId)
        {
            var contentService = Services.ContentService;
            var venueLocation = contentService.GetById(int.Parse(venueId));
            List<PropertyItem> VenueDetailsList = new List<PropertyItem>();
            VenueDetailsList.Add(new PropertyItem { Name = "venueLocation", Value = (venueLocation.GetValue("venueLocation") != null ? venueLocation.GetValue("venueLocation").ToString() : "") });
            VenueDetailsList.Add(new PropertyItem { Name = "venueSpecialOccasion", Value = (venueLocation.GetValue("venueSpecialOccasion") != null ? venueLocation.GetValue("venueSpecialOccasion").ToString() : "") });
            VenueDetailsList.Add(new PropertyItem { Name = "venueOpeningTime", Value = (venueLocation.GetValue("venueOpeningTime") != null ? venueLocation.GetValue("venueOpeningTime").ToString() : "") });
            return VenueDetailsList;
        }

        /// <summary>
        /// Get location, time and special occasion for venues
        /// </summary>
        /// <param name="venueId"></param>
        /// <returns></returns>
        public string GetVenueLocation(string pageId)
        {
            var contentService = Services.ContentService;
            var eventData = contentService.GetById(int.Parse(pageId));
            if (eventData != null)
            {
                return Convert.ToString(eventData.GetValue("venueLocation"));
            }
            else
            {
                return "";
            }

        }
        #endregion

        #region Google Api

        [HttpGet]
        public string GetURLShorten(string Longurl)
        {
            var apiKey = ConfigurationManager.AppSettings["Google_Key"];
            string post = "{\"longUrl\": \"" + Longurl + "\"}";
            string MyshortUrl = Longurl;
            HttpWebRequest Myrequest = (HttpWebRequest)WebRequest
               .Create("https://www.googleapis.com/urlshortener/v1/url?key=" + apiKey);
            try
            {
                Myrequest.ServicePoint.Expect100Continue = false;
                Myrequest.Method = "POST";
                Myrequest.ContentLength = post.Length;
                Myrequest.ContentType = "application/json";
                Myrequest.Headers.Add("Cache-Control", "no-cache");

                using (Stream requestStream =
                   Myrequest.GetRequestStream())
                {
                    byte[] postBuffer = Encoding.ASCII.GetBytes(post);
                    requestStream.Write(postBuffer, 0,
                       postBuffer.Length);
                }

                using (HttpWebResponse response =
                   (HttpWebResponse)Myrequest.GetResponse())
                {
                    using (Stream responseStream =
                       response.GetResponseStream())
                    {
                        using (StreamReader responseReader = new
                           StreamReader(responseStream))
                        {
                            string json = responseReader.ReadToEnd();
                            MyshortUrl = Regex.Match(json, @"""id"":
                        ?""(?<id>.+)""").Groups["id"].Value;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            return MyshortUrl;
        }

        public void GetShortenURL(string longUrl) {
            string urlToShorten = "http://maps.google.com/";
            //Console.Write("\tEnter a URL to shorten[{0}]: ", urlToShorten);
            //var input = Console.ReadLine();
            //if (!string.IsNullOrEmpty(input))
            //{
            //     urlToShorten = input;
            //}
            //Console.WriteLine();

            // Shorten URL
            
            Url response = service.Url.Insert(new Url { LongUrl = urlToShorten }).Execute();

            // Display response
            //Console.WriteLine("\tShort URL:{0}", response.Id);
            //return response.Id;
        }

        #endregion

        #region Custom Dashboard

        [HttpGet]
        public DashboardData GetDashboardCountsData() {

            var root = _contentService.GetRootContent().FirstOrDefault();
            var contentsData = _contentService.GetDescendants(root.Id);
            DashboardData DashboardData = new DashboardData();
            if (contentsData != null) {
                var venues = contentsData.Where(x => x.ContentType.Alias == "Venue");
                var organizer = contentsData.Where(x => x.ContentType.Alias == "Organizer");
                var events = contentsData.Where(x => x.ContentType.Alias == "Event");
                var entities = contentsData.Where(x => x.ContentType.Alias == "GovernmentEntity");

                DashboardData.VenueCount = venues != null ? venues.Count() : 0;
                //DashboardData.EventCount = events != null ? events.Count() : 0;
                DashboardData.OrganizerCount = organizer != null ? organizer.Count() : 0;
                DashboardData.EntityCount = entities != null ? entities.Count() : 0;
                List<Event> EventList = new List<Event>();
                foreach (var item in events)
                {
                    EventList.Add(new Event
                    {
                        Id = item.Id,
                        Name = item.Name,
                        NameEng = Convert.ToString(item.GetValue("EventName")),
                        NameAr = Convert.ToString(item.GetValue("EventNameArabic")),
                        Status = Convert.ToString(item.GetValue("EventStatus")),
                        Occurrences = Convert.ToString(item.GetValue("EventOccurance")),
                    });

                }
                DashboardData.EventData = EventList;
            }

            return DashboardData;
        }

        [HttpGet]
        public int GetVenuesCount()
        {
            var root = _contentService.GetRootContent().FirstOrDefault();
            var venues = _contentService.GetDescendants(root.Id).Where(x => x.ContentType.Alias == "Venue");
            return venues != null ? venues.Count() : 0;
        }

        [HttpGet]
        public int GetOrganizersCount()
        {
            var root = _contentService.GetRootContent().FirstOrDefault();
            var eventOrganizers = _contentService.GetDescendants(root.Id).Where(x => x.ContentType.Alias == "Organizer");

            return eventOrganizers != null ? eventOrganizers.Count() : 0;
        }

        [HttpGet]
        public int GetRegisteredUsersCount()
        {

            return 0;
        }

        [HttpGet]
        public int GetEntitiesCount()
        {
            var root = _contentService.GetRootContent().FirstOrDefault();
            var gvtEntity = _contentService.GetDescendants(root.Id).Where(x => x.ContentType.Alias == "GovernmentEntity");

            return gvtEntity != null ? gvtEntity.Count() : 0;
        }

        [HttpGet]
        public string GetChartData()
        {

            return "";
        }

        #endregion
    }

    #region helper classes
    public class Items
    {
        public string Name { get; set; }
        public int Id { get; set; }

    }

    public class PropertyItem
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class DashboardData
    {
        public List<Event> EventData { get; set; }
        public int VenueCount { get; set; }
        public int OrganizerCount { get; set; }
        public int EntityCount { get; set; }
        public int RegisteredUserCount { get; set; }
    }
    #endregion
}