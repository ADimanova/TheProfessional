using Professional.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Professional.Web.Models
{
    public class ListPanelViewModel
    {
        public string Title { get; set; }

        // TODO: ViewModel
        public IEnumerable<FieldOfExpertise> Fields { get; set; }
        public IList<NavigationItem> Items { get; set; }
        public string UniqueIdentificator { get; set; }
    }
}