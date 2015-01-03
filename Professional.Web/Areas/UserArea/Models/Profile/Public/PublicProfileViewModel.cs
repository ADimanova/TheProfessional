namespace Professional.Web.Areas.UserArea.Models.Profile.Public
{
    using Professional.Web.Areas.UserArea.Models.CreateItem;
    using Professional.Web.Models.Shared;

    public class PublicProfileViewModel
    {
        public UserViewModel UserInfo { get; set; }

        public ContactViewModel ContactInfo { get; set; }

        public EndorsementInputModel EndorseFunctionality { get; set; }

        public NavigationItem BtnNavigatePosts { get; set; }

        public NavigationItem BtnNavigateEndorsements { get; set; }

        public ListPanelViewModel TopPostsList { get; set; }

        public ListPanelViewModel RecentPostsList { get; set; }
    }
}