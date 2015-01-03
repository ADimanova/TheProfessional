namespace Professional.Web.Areas.UserArea.Models.Profile.Private
{
    using System.Collections.Generic;

    public class ShortListingViewModel
    {
        public string Title { get; set; }

        public string Type { get; set; }

        public string ItemsId 
        { 
            get
            {
                return "items" + this.Type;
            }
        }

        public string FormId
        {
            get
            {
                return "form" + this.Type;
            }
        }

        public string InputId
        {
            get
            {
                return "input" + this.Type;
            }
        }

        public IEnumerable<string> Items { get; set; }
    }
}