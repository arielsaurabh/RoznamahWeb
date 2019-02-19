using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roznamah.Model.Theme
{
    public class Theme
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PageBannerEn { get; set; }
        public string PageBannerAr { get; set; }
        public string PageContentEn { get; set; }
        public string PageContentAr { get; set; }
        public string BrochureEn { get; set; }
        public string BrochureAr { get; set; }
        public string PageEvents { get; set; }
    }
}
