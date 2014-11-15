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
using Professional.Web.Areas.UserArea.Models.InputModels;
using System.IO;

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
            ViewBag.AddInfo = WebConstants.AddUserInfoPageRoute;

            return View();
        }

        [HttpGet]
        public ActionResult AddUserInfo()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUserInfo(UserInputModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var user = this.data.Users.GetById(userId);

                user.PersonalHistory = model.PersonalHistory;
                user.IsMale = model.IsMale;
                user.DateOfBirth = model.DateOfBirth;

                if (model.ProfileImage != null)
                {
                    using (var memory = new MemoryStream())
                    {
                        model.ProfileImage.InputStream.CopyTo(memory);
                        var content = memory.GetBuffer();

                        user.ProfileImage = new Image
                        {
                            Content = content,
                            FileExtension = model.ProfileImage.FileName.Split(new[] { '.' }).Last()
                        };
                    }
                }

                try
                {
                    this.data.Users.Update(user);
                    this.data.SaveChanges();
                    return RedirectToAction("Index", "Home", new { Area = "" });
                }
                catch
                {
                    // Implement better error handling
                    return View("Error");
                }
                
            }

            // Something failed, redisplay form
            return View(model);
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