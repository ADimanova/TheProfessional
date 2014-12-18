﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Professional.Data;
using Professional.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Professional.Web.Models.DatabaseViewModels;
using Professional.Web.Areas.UserArea.Models;
using Professional.Web.Models;
using Professional.Web.Helpers;
using Professional.Web.Areas.UserArea.Models.InputModels;
using Professional.Web.Models.InputViewModels;
using Professional.Web.Areas.UserArea.Models.ListingViewModels;

namespace Professional.Web.Areas.UserArea.Controllers
{
    public class ProfileController : UserController
    {
        private static User currentUser;
        private readonly IQueryable<Post> postsData;
        public ProfileController(IApplicationData data)
            : base(data)
        {
            this.postsData = this.data.Posts.All().OrderBy(p => p.Title);
        }

        // GET: UserArea/Profile
        public ActionResult Public(string id)
        {
            currentUser = this.data.Users.All()
                .FirstOrDefault(u => u.Id == id);

            Mapper.CreateMap<User, UserViewModel>();
            var userInfoForView = Mapper.Map<UserViewModel>(currentUser);

            var userFields = (List<FieldOfExpertise>)this.GetUserFields(id);

            var topPostPanel = new ListPanelViewModel();
            topPostPanel.Title = "Top Posts";
            topPostPanel.Items = this.GetTopPosts(id)
                .Select(p => new NavigationItem
                {
                    Content = p.Title,
                    Url = "#"
                }).ToList<NavigationItem>();
            topPostPanel.Fields = userFields;

            //var recentPostPanel = new ListPanelViewModel();
            //recentPostPanel.Title = "Recent Posts";
            //recentPostPanel.Items = this.GetRecentPosts(id)
            //    .Select(p => new NavigationItem
            //    {
            //        Content = p.Title,
            //        Url = "#"
            //    }).ToList<NavigationItem>();
            //recentPostPanel.Fields = userFields;

            var btnNavigatePosts = new NavigationItem();
            btnNavigatePosts.Content = "See post's page";
            btnNavigatePosts.Url = WebConstants.UserPostsPageRoute + currentUser.Id;

            var btnNavigateEndorsements = new NavigationItem();
            btnNavigateEndorsements.Content = "See endorsements's page";
            btnNavigateEndorsements.Url = WebConstants.UserEndorsementsPageRoute + currentUser.Id;

            var userID = User.Identity.GetUserId();
            var isEndorsed = this.data.EndorsementsOfUsers.All()
                .Where(e => e.EndorsingUserID == userID)
                .Any(e => e.EndorsedUserID == id);

            var endorsements = this.data.EndorsementsOfUsers.All()
                .Where(e => e.EndorsedUserID == id)
                .Select(e => new EndorsementViewModel
                {
                    Content = e.Comment,
                    ID = e.ID,
                    AuthorFirstName = e.EndorsingUser.FirstName,
                    AuthorLastName = e.EndorsingUser.LastName
                });

            var chatModel = new ChatViewModel();
            //chatModel.IsMessaged = true;
            chatModel.ToUserId = id;

            var publicProfileInfo = new PublicProfileViewModel();
            publicProfileInfo.UserInfo = userInfoForView;
            publicProfileInfo.ChatInfo = chatModel;
            if (!isEndorsed && userID != id)
            {
                var endorseInfo = new EndorsementInputModel();
                endorseInfo.EndorsedID = id;
                endorseInfo.EndorseAction = "EndorsementOfUser";
                publicProfileInfo.EndorseFunctionality = endorseInfo;
            }
            else
            {
                ViewBag.IsEndorsed = "true";
            }
            publicProfileInfo.BtnNavigatePosts = btnNavigatePosts;
            publicProfileInfo.BtnNavigateEndorsements = btnNavigateEndorsements;
            publicProfileInfo.TopPostsList = topPostPanel;
            //publicProfileInfo.RecentPostsList = recentPostPanel;
            publicProfileInfo.UserInfo.Endorsements = endorsements;

            return View(publicProfileInfo);
        }

        public ActionResult Private()
        {
            string currentUserId = User.Identity.GetUserId();
            var currentUser = this.data.Users.All()
                .FirstOrDefault(u => u.Id == currentUserId);

            Mapper.CreateMap<User, UserViewModel>();
            var userInfoForView = Mapper.Map<UserViewModel>(currentUser);

            var messagesReceived = this.data.Messages.All()
                .Where(m => m.ToUserId == currentUserId)
                .Where(m => m.IsRead == false)
                .GroupBy(m => m.FromUserId)
                .Select(g => new MessagesViewModel
                {
                    FromUserId = g.Key,
                    FromUserName = g.FirstOrDefault().FromUser.FirstName + " " + g.FirstOrDefault().FromUser.LastName,
                    Preview = g.FirstOrDefault().Content.Substring(0, 20) + "...",
                    //Messages = g.Select(i => )
                });

            var updateModel = new UpdatesViewModel();
            if (messagesReceived.Count() > 0)
            {
                updateModel.IsMessaged = true;
                updateModel.ActiveChats = messagesReceived.ToList();
            }

            var privateProfileInfo = new PrivateProfileViewModel();

            IList<NavigationItem> navItems = this.GetNavItems();
            var navList = new HorizontalNavbarViewModel();
            navList.Title = "Navigation";
            navList.ListItems = navItems;
            privateProfileInfo.UserInfo = userInfoForView;
            privateProfileInfo.UpdatesInfo = updateModel;
            privateProfileInfo.NavigationList = navList;

            return View(privateProfileInfo);
        }

        public ActionResult EndorseUser(string id)
        {
            var endorsee = this.data.Users.GetById(id);
            return Content(endorsee.FirstName);
        }

        private IList<NavigationItem> GetNavItems()
        {
            var currentUserId = User.Identity.GetUserId();
            return new List<NavigationItem>
            {
                new NavigationItem { 
                    Content = "Create Post",
                    Url = WebConstants.CreatePostPageRoute
                },
                new NavigationItem { 
                    Content = "Go to post's page",
                    Url = WebConstants.UserPostsPageRoute + currentUserId
                },
                new NavigationItem { 
                    Content = "Go to endorsements's page",
                    Url = WebConstants.UserEndorsementsPageRoute + currentUserId
                },
            };
        }

        public ActionResult Filter(string query)
        {
            if (currentUser == null)
            {
                return View("Error", "Something went wrong.");
            }

            //Taking the top
            var posts = postsData
                .Where(p => p.CreatorID == currentUser.Id)
                .Where(p => p.Field.Name == query)
                .OrderBy(p => p.Rank)
                .Take(WebConstants.ListPanelCount)
                .Select(i => new NavigationItem
                {
                    Content = i.Title,
                    Url = WebConstants.PostPageRoute + i.ID
                }).ToList();

            // TODO: Get Recent posts as well - bug with same id='posts' of containers

            return this.PartialView("~/Views/Shared/Partials/_ListItems.cshtml", posts);
        }

        public ActionResult Image(int id)
        {
            var image = this.data.Images.GetById(id);
            if (image == null)
            {
                throw new HttpException(404, "Image not found");
            }

            return File(image.Content, "image/" + image.FileExtension);
        }
    }
}