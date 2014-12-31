using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Professional.Web.Areas.UserArea.Models.ListingViewModels
{
    public class ShortListingViewModel
    {
        public string Title { get; set; }
        public string Type { get; set; }
        public IEnumerable<string> Items { get; set; }
    }
}