namespace Professional.Web.Areas.UserArea.Models.Profile.Public
{
    using System.Collections.Generic;

    using Professional.Web.Models.Shared;

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

        public string ItemsId
        {
            get
            {
                return "items" + this.UniqueIdentificator;
            }
        }

        public string FormId
        {
            get
            {
                return "form" + this.UniqueIdentificator;
            }
        }

        public string InputId
        {
            get
            {
                return "input" + this.UniqueIdentificator;
            }
        }
    }
}