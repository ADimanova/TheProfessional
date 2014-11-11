using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Professional.Models;
using Professional.Web.Models;

namespace Professional.Web.Areas.UserArea.Models
{
    public class ItemsByFieldViewModel
    {
        public string Name { get; set; }
        public IList<NavigationItem> Items { get; set; } 
    }
}