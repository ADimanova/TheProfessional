using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Professional.Web.Controllers;
using AutoMapper;
using Professional.Models;
using Professional.Web.Areas.UserArea.Models;
using System.Collections;
using Professional.Common;
using Professional.Web.Models;
using Professional.Web.Helpers;
using Professional.Data;

namespace Professional.Web.Areas.UserArea.Controllers
{
    public class PrivateController : UserController
    {
        public PrivateController(IApplicationData data)
            : base(data)
        {

        }

        // GET: UserArea/Private/Profile
        public ActionResult Profile()
        {
            var currentUserId = User.Identity.GetUserId();
            var currentUser = this.data.Users.All()
                .FirstOrDefault(u => u.Id == currentUserId);

            Mapper.CreateMap<User, UserViewModel>();
            var userInfoForView = Mapper.Map<UserViewModel>(currentUser);

            var privateProfileInfo = new PrivateProfileViewModel();

            IList<NavigationItem> navItems = this.GetNavItems();
            var navList = new HorizontalNavbarViewModel();
            navList.Title = "Navigation";
            navList.ListItems = navItems;
            privateProfileInfo.UserInfo = userInfoForView;
            privateProfileInfo.NavigationList = navList;

            return View(privateProfileInfo);
        }

        public ActionResult OnRegistration()
        {
            var userId = User.Identity.GetUserId();
            var profilePath = WebConstants.PrivateProfilePageRoute + userId;
            ViewBag.Profile = profilePath;
            ViewBag.AddInfo = "#";

            return View();
        }

        private IList<NavigationItem> GetNavItems()
        {
            return new List<NavigationItem>
            {
                new NavigationItem { 
                    Content = "Create Post",
                    Url = "/UserArea/CreateItem/Post"
                },
                new NavigationItem { 
                    Content = "Go to post's page",
                    Url = ""
                },
                new NavigationItem { 
                    Content = "Go to endorsements's page",
                    Url = ""
                },
            };
        }
    }
}