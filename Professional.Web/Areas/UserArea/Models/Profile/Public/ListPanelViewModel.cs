using Professional.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Professional.Web.Models
{
    /// <summary>
    /// Displaying items that fullfill a certain criteria,
    /// fillterable by their respective fields.
    /// </summary>
    public class ListPanelViewModel
    {
        public string Title { get; set; }

        /// <summary>
        /// Fields filter items
        /// </summary>
        public IEnumerable<string> Fields { get; set; }

        /// <summary>
        /// Resulting items to be displayed
        /// </summary>
        public IList<NavigationItem> Items { get; set; }

        /// <summary>
        /// Criteria for extracting items
        /// </summary>
        public string UniqueIdentificator { get; set; }
    }
}