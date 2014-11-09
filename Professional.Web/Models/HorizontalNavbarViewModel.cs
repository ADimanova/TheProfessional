using System;
using System.Collections.Generic;
using System.Linq;

namespace Professional.Web.Models
{
    public class HorizontalNavbarViewModel
    {
        public string Title { get; set; }
        public IEnumerable<NavigationItem> ListItems { get; set; }
    }
}