namespace Professional.Web.Areas.UserArea.Models.Profile.Private
{
    using System.Collections.Generic;

    public class ChatsListingViewModel
    {
        public bool LoadMore { get; set; }
        public IEnumerable<ChatListedViewModel> ChatsListing { get; set; }
    }
}