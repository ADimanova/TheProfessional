namespace Professional.Web.Areas.UserArea.Models.Profile.Private
{
    using Professional.Web.Models.Shared;

    public class PrivateProfileViewModel
    {
        public UserViewModel UserInfo { get; set; }

        public UpdatesViewModel UpdatesInfo { get; set; }

        public HorizontalNavbarViewModel NavigationList { get; set; }

        public PrivateProInfoViewModel ProInfo { get; set; }
    }
}