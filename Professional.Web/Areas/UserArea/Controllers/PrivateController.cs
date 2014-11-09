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

namespace Professional.Web.Areas.UserArea.Controllers
{
    public class PrivateController : UserController
    {
        // GET: UserArea/Private/Profile
        public ActionResult Profile()
        {
            var currentUserId = User.Identity.GetUserId();
            var currentUser = this.data.Users.All()
                .FirstOrDefault(u => u.Id == currentUserId);

            Mapper.CreateMap<User, UserViewModel>();
            var userInfoForView = Mapper.Map<UserViewModel>(currentUser);

            var userFields = (List<FieldOfExpertise>)this.GetUserFields(currentUserId);

            var topPostPanel = new ListPanelViewModel();
            topPostPanel.Items = (List<Post>)this.GetTopPosts(currentUserId);
            topPostPanel.Fields = userFields;

            var recentPostPanel = new ListPanelViewModel();
            recentPostPanel.Items = (List<Post>)this.GetRecentPosts(currentUserId);
            recentPostPanel.Fields = userFields;

            var publicProfileInfo = new PublicProfileViewModel();
            publicProfileInfo.UserInfo = userInfoForView;
            publicProfileInfo.BtnNavigatePosts = "See post's page";
            publicProfileInfo.BtnNavigateEndorsements = "See post's page";
            publicProfileInfo.TopPostsList = topPostPanel;
            publicProfileInfo.RecentPostsList = recentPostPanel;

            return View(publicProfileInfo);
        }
    }
}