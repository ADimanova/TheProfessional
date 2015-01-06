namespace Professional.Web.Areas.UserArea.Models.Profile.Private
{
    using System.Collections.Generic;

    public class AddMassagesViewModel
    {
        public bool LoadMore { get; set; }
        public IEnumerable<MessageViewModel> ChatsListing { get; set; }
    }
}