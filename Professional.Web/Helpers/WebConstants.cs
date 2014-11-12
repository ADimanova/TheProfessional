using Professional.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Professional.Web.Helpers
{
    public static class WebConstants
    {
        // Area names
        public const string AdminArea = "Administration";
        public const string UserArea = "UserArea";

        // Routes
        public const string PostPageRoute = "/PostDisplay/Post/";
        public const string PostsPageRoute = "/UserArea/Public/Posts/";

        public const string UsersPageRoute = "/UserArea/AllUsers/Index";

        public const string PublicProfilePageRoute = "/UserArea/Public/Profile/";
        public const string PrivateProfilePageRoute = "/UserArea/Private/Profile/";

        public const string OnRegistrationPageRoute = "/UserArea/Private/OnRegistration/";

        //Visualization
        public const int PostsPerPage = 5;

        public const int TitleLength = 20;
        public const int ContentLength = 100;

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
    }
}