namespace Professional.Web.Areas.UserArea.Models.Listing
{
    using System.Collections.Generic;

    public class ItemsByFieldViewModel
    {
        public string Name { get; set; }

        public IList<NavigationItemWithImage> Items { get; set; } 
    }
}