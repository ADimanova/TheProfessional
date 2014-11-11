using Professional.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Professional.Web.Helpers
{
    public static class WebConstants
    {
        public static string PostPageRoute = "/PostDisplay/Post/";
        public static string PostsPageRoute = "/UserArea/Public/Posts/";
        public const int PostsPerPage = 5;

        public static List<NavigationItem> SiteSections = new List<NavigationItem> {
            new NavigationItem
            {
                Content = "Users",
                Url = PostsPageRoute
            }, 
            new NavigationItem
            {
                Content = "Posts",
                Url = PostsPageRoute
            }
        };
    }
}