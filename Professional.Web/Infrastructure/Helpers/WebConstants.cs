namespace Professional.Web.Helpers
{
    using System.Collections.Generic;

    using Professional.Web.Models.Shared;

    public static class WebConstants
    {
        public const string DefaultHistory = "No personal history has been added.";
        public const string DefaultImage = "/Images/default-profile-pic.png";

        // Visualization counts
        public const int PostsPerPage = 5;
        public const int TitleLength = 20;
        public const int ContentLength = 100;
        public const int ListPanelCount = 5;

        // Area names
        public const string AdminArea = "Admin";
        public const string KendoAdminArea = "KendoAdmin";
        public const string UserArea = "UserArea";

        // Routes
        public const string PostsPageRoute = "/UserArea/Listing/Posts/";
        public const string UserPostsPageRoute = "/UserArea/Listing/Posts/User/";

        public const string UsersPageRoute = "/UserArea/Listing/Users/";

        public const string UserEndorsementsPageRoute = "/UserArea/Listing/UserEndorsements/";
        public const string UserConnectionsPageRoute = "/UserArea/Listing/Connections/User/";

        public const string PublicProfilePageRoute = "/UserArea/Profile/Public/";
        public const string PrivateProfilePageRoute = "/UserArea/Profile/Private/";

        public const string GetImagePageRoute = "/Image/ImageById/";

        public const string OnRegistrationPageRoute = "/UserArea/AddInfo/OnRegistration/";
        public const string AddPersonalInfoPageRoute = "/UserArea/AddInfo/Personal/";
        public const string AddProfessionalInfoPageRoute = "/UserArea/AddInfo/Professional/";

        // Create
        public const string CreatePostPageRoute = "/UserArea/CreateItem/Post/";
        public const string CreateEndorsementOfUserPageRoute = "/UserArea/CreateItem/EndorsementOfUser/";
        public const string CreateEndorsementOfPostPageRoute = "/UserArea/CreateItem/EndorsementOfPost/";

        // Administration
        public const string AdminPostsPageRoute = "/Admin/Posts/PostsAdmin/";
        public const string AdminUsersPageRoute = "/Admin/Users/UsersAdmin/";
        public const string AdminFieldsPageRoute = "/Admin/Fields/FieldsAdmin/";
        public const string AdminNotificationsPageRoute = "/Admin/Notifications/NotificationsAdmin/";

        // Updates
        public const string UpdateConnectPageRoute = "/UserArea/Updates/Connect/";
        public const string UpdateAcceptConnectionPageRoute = "/UserArea/Updates/AcceptConnection/";
        public const string UpdateDeclineConnectionPageRoute = "/UserArea/Updates/DeclineConnection/";
        public const string UpdateDeclineUserPageRoute = "/UserArea/Updates/DeclineUser/";

        // Public 
        public const string PostPageRoute = "/Post/Info/";
        public const string FieldInfoPageRoute = "/Field/Info/";
        public const string FieldsListingPageRoute = "/Field/FieldsListing/";     

        // Navbars and Dropdowns data
        public static List<NavigationItem> SiteSections = new List<NavigationItem>
        {
            new NavigationItem
            {
                Content = "Users",
                Url = UsersPageRoute
            }, 
            new NavigationItem
            {
                Content = "Posts",
                Url = PostsPageRoute
            }
        };

        public static List<NavigationItem> AdminSections = new List<NavigationItem>
        {
            new NavigationItem
            {
                Content = "Users",
                Url = AdminUsersPageRoute
            }, 
            new NavigationItem
            {
                Content = "Posts",
                Url = AdminPostsPageRoute
            },
            new NavigationItem
            {
                Content = "Fields of Expertise",
                Url = AdminFieldsPageRoute
            },
            new NavigationItem
            {
                Content = "Notifications",
                Url = AdminNotificationsPageRoute
            }
        };
    }
}
