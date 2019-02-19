using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Roznamah.Web.ViewModel
{
    public class Entities
    {
    }

    public class GovernmentEntities
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string DescriptionEn { get; set; }
        public string DescriptionAr { get; set; }
        public string Website { get; set; }
        public string RelatedVenues { get; set; }
    }

    public class EventOrgnizer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string DescriptionEn { get; set; }
        public string DescriptionAr { get; set; }
        public string Website { get; set; }
        public string ContactInformation { get; set; }
    }

    public class VenueOwners
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string DescriptionEn { get; set; }
        public string DescriptionAr { get; set; }
        public string Website { get; set; }
        public string ContactInformation { get; set; }
    }
}