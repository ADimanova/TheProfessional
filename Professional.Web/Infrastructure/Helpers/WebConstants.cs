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
        public const string PostsPageRoute = "/UserArea/Public/Posts/";

        public const string UsersPageRoute = "/UserArea/AllUsers/Index/";

        public const string PublicProfilePageRoute = "/UserArea/Public/Profile/";
        public const string PrivateProfilePageRoute = "/UserArea/Private/Profile/";

        public const string OnRegistrationPageRoute = "/UserArea/Private/OnRegistration/";
        public const string AddUserInfoPageRoute = "/UserArea/Private/AddUserInfo/";

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

 
 
