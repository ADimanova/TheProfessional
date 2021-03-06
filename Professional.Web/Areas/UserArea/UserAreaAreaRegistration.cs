﻿namespace Professional.Web.Areas.UserArea
{
    using System.Web.Mvc;

    public class UserAreaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "UserArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "PostsOfUserListings",
                "UserArea/Listing/Posts/User/{user}/{id}",
                new { controller = "Listing", action = "Posts", id = UrlParameter.Optional });

            context.MapRoute(
                "ConnectionsOfUserListings",
                "UserArea/Listing/Connections/User/{user}/{id}",
                new { controller = "Listing", action = "Users", id = UrlParameter.Optional });

            context.MapRoute(
                "UserArea_default",
                "UserArea/{controller}/{action}/{id}",
                new { controller = "Profile", action = "PublicProfile", id = UrlParameter.Optional });
        }
    }
}