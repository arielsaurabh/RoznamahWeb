using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roznamah.Model.Event
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
}
