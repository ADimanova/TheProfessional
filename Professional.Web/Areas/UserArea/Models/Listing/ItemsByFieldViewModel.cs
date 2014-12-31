using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Professional.Models;
using Professional.Web.Models;
using Professional.Web.Models.Shared;

namespace Professional.Web.Areas.UserArea.Models
{
    public class ItemsByFieldViewModel
    {
        public string Name { get; set; }
        public IList<NavigationItemWithImage> Items { get; set; } 
    }
}