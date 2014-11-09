using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Professional.Web.Models
{
    public class HorizontalNavbarViewModel
    {
        public string Title { get; set; }
        public IEnumerable<string> ListItems { get; set; }
    }
}