using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Roznamah.Web.ViewModel
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }
        public string NameAr { get; set; }
        public string Thumbnail { get; set; }
        public string VideoEng { get; set; }
        public string VideoAr { get; set; }
        public string Website { get; set; }
        public string Orgnizer { get; set; }
        public string Tags { get; set; }
        public string ContentsEng { get; set; }
        public string ContentsAr { get; set; }
        public string Audience { get; set; }
        public string Status { get; set; }
        public string Occurrences { get; set; }
        public string Tickets { get; set; }
        public AdditionalInformation AdditionalInfo { get; set; }
        public Promotions Promotion { get; set; }
        public SocialImages SocialImages { get; set; }
    }


    #region helper Classes

    public class Promotions
    {
        public string Logo { get; set; }
        public string NameEng { get; set; }
        public string NameAr { get; set; }
        public string OfferDescriptionEng { get; set; }
        public string OfferDescriptionAr { get; set; }
        public string Duration { get; set; }
    }

    public class AdditionalInformation
    {
        public string ActivitiesEng { get; set; }
        public string ActivitiesAr { get; set; }
        public string BeforYouComeEng { get; set; }
        public string BeforYouComeAr { get; set; }
        public string AmenitiesEng { get; set; }
        public string AmenitiesAr { get; set; }
    }

  public class Amenities
    {
        public string AmenitiesEng { get; set; }
        public string AmenitiesAr { get; set; }
      
    }
    public class Activites
    {
        public string ActivitiesEng { get; set; }
        public string ActivitiesAr { get; set; }
    }
    public class TimeSchedule
    {
        public string Occurrences { get; set; }
    }
    public class SocialImages
    {
        public string ImageEng { get; set; }
        public string ImageAr { get; set; }
        public string TwitterImages { get; set; }
    }

    public class ImagesFromTwitter
    {
        public string Tags { get; set; }
        public string[] Images { get; set; }
    }

    public class Ticket
    {
        public bool IsFreeTickets { get; set; }
        public bool TicketHasPrice { get; set; }
        public string SingleTicketPrice { get; set; }
        public string TicketPriceRangeFrom { get; set; }
        public string TicketPriceRangeTo { get; set; }
        public bool IsTicketOnline { get; set; }
        public string OnlineUrlForTicket { get; set; }
        public bool IsTicketOffline { get; set; }
        public bool IsAvailableAtGate { get; set; }
        public string AvailableAtShop { get; set; }

    }

    public class Occurrance
    {
        public string City { get; set; }
        public string Venue { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string TimeFrom { get; set; }
        public string TimeTo { get; set; }
        public string Location { get; set; }
    }

    #endregion
}