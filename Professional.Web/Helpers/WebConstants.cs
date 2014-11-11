﻿using Professional.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Professional.Web.Helpers
{
    public static class WebConstants
    {
        // Routes
        public const string PostPageRoute = "/PostDisplay/Post/";
        public const string PostsPageRoute = "/UserArea/Public/Posts/";
        public const string PublicProfilePageRoute = "/UserArea/Public/Profile/";

        //Visualization
        public const int PostsPerPage = 5;

        public const int TitleLength = 20;
        public const int ContentLength = 100;

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