using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Roznamah.Web.ViewModel
{
    public class Venue
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string City { get; set; }
        public string DescriptionEn { get; set; }
        public string DescriptionAR { get; set; }
        public string Location { get; set; }
        public string TagsEn { get; set; }
        public string TagsAr { get; set; }
        public string RepresentativeEn { get; set; }
        public string RepresentativeAr { get; set; }
        public string OpeningTime { get; set; }
        public string FloorMapEn { get; set; }
        public string FloorMapAr { get; set; }
        public string Owner { get; set; }
        public string ContactNumber { get; set; }
        public string SpecialOccasions { get; set; }
    }

    public class VenueDetails
    {
        public string OpeningTime { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string DescriptionEn { get; set; }
        public string DescriptionAR { get; set; }
    }

    public class VenueLocation
    {
        public string Location { get; set; }
    }

    public class FloorMap
    {
        public string FloorMapEn { get; set; }
        public string FloorMapAr { get; set; }
    }

}