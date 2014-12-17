using Professional.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Professional.Web.Helpers
{
    public static class WebConstants
    {
        public static readonly IList<string> EmptyList = new List<string> { "no values specified" };

        // Visualization counts
        public const int PostsPerPage = 5;
        public const int TitleLength = 20;
        public const int ContentLength = 100;
        public const int ListPanelCount = 5;

        // Area names
        public const string AdminArea = "Administration";
        public const string UserArea = "UserArea";

        // Routes
        public const string PostsPageRoute = "/UserArea/Listing/Posts/";
        public const string UserPostsPageRoute = "/UserArea/Listing/Posts/User/";
        
        public const string UsersPageRoute = "/UserArea/Listing/Users/";

        public const string UserEndorsementsPageRoute = "/UserArea/Listing/UserEndorsements/";

        public const string PublicProfilePageRoute = "/UserArea/Profile/Public/";
        public const string PrivateProfilePageRoute = "/UserArea/Profile/Private/";

        public const string OnRegistrationPageRoute = "/UserArea/AddInfoController/OnRegistration/";
        public const string AddPersonalInfoPageRoute = "/UserArea/AddInfoController/Personal/";
        public const string AddProfessionalInfoPageRoute = "/UserArea/AddInfoController/Professional/";

        // Create
        public const string CreatePostPageRoute = "/UserArea/CreateItem/Post/";
        public const string CreateEndorsementOfUserPageRoute = "/UserArea/CreateItem/EndorsementOfUser/";
        public const string CreateEndorsementOfPostPageRoute = "/UserArea/CreateItem/EndorsementOfPost/";

        // Administration
        public const string AdminPostsPageRoute = "/Administration/PostsAdmin/Index/";
        public const string AdminUsersPageRoute = "/Administration/UsersAdmin/Index/";
        public const string AdminFieldsPageRoute = "/Administration/FieldsAdmin/Index/";

        // Public 
        public const string PostPageRoute = "/Post/Info/";
        public const string FieldInfoPageRoute = "/Field/Info/";

        // Navbars and Dropdowns data
        public static List<NavigationItem> SiteSections = new List<NavigationItem> {
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

        public static List<NavigationItem> AdminSections = new List<NavigationItem> {
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
            }
        };
    }
}

 
 
