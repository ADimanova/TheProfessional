﻿namespace Professional.Web.Models.Shared
{
    using System.Collections.Generic;

    /// <summary>
    /// A horizontal navigation bar
    /// </summary>
    public class HorizontalNavbarViewModel
    {
        public string Title { get; set; }

        public IEnumerable<NavigationItem> ListItems { get; set; }

        /// <summary>
        /// URL that leads to a page to be displayed if more info is required.
        /// It would not be displayed if left empty
        /// </summary>
        public string MoreInfoUrl { get; set; }
    }
}