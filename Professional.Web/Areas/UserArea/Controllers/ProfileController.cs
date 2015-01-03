﻿namespace Professional.Web.Areas.UserArea.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Dynamic;
    using System.Linq.Expressions;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Microsoft.AspNet.Identity;

    using Professional.Data;
    using Professional.Models;
    using Professional.Web.Areas.UserArea.Models;
    using Professional.Web.Areas.UserArea.Models.DatabaseVeiwModels;
    using Professional.Web.Areas.UserArea.Models.ListingViewModels;
    using Professional.Web.Areas.UserArea.Models.Profile.Public;
    using Professional.Web.Helpers;
    using Professional.Web.Infrastructure.Services.Contracts;
    using Professional.Web.Models;
    using Professional.Web.Models.InputViewModels;

    public class ProfileController : UserController
    {
        private static User currentUser;
        private IProfileServices profileServices;

        public ProfileController(IApplicationData data, IProfileServices profileServices)
            : base(data)
        {
            this.profileServices = profileServices;
        }

        public ActionResult Public(string id)
        {
            currentUser = this.GetUser(id);

            if (currentUser == null)
            {
                return this.RedirectToAction("Index", "Home", new { Area = string.Empty });   
            }

            var userInfo = this.SetUserInfo(currentUser);

            var userFields = this.profileServices.GetUserFields(id)
                .Select(f => f.Name).ToList();

            var topPostPanel = new ListPanelViewModel();
            topPostPanel.UniqueIdentificator = "Top";
            topPostPanel.Title = "Top Posts";
            topPostPanel.Items = this.profileServices.GetTopPosts(id)
                .Select(p => new NavigationItem
                {
                    Content = p.Title,
                    Url = "#"
                }).ToList<NavigationItem>();
            topPostPanel.Fields = userFields;

            var recentPostPanel = new ListPanelViewModel();
            recentPostPanel.UniqueIdentificator = "Recent";
            recentPostPanel.Title = "Recent Posts";
            recentPostPanel.Items = this.profileServices.GetRecentPosts(id)
                .Select(p => new NavigationItem
                {
                    Content = p.Title,
                    Url = "#"
                }).ToList<NavigationItem>();
            recentPostPanel.Fields = userFields;

            var btnNavigatePosts = new NavigationItem();
            btnNavigatePosts.Content = "See post's page";
            btnNavigatePosts.Url = WebConstants.UserPostsPageRoute + id;

            var btnNavigateEndorsements = new NavigationItem();
            btnNavigateEndorsements.Content = "See endorsements's page";
            btnNavigateEndorsements.Url = WebConstants.UserEndorsementsPageRoute + id;

            var loggedUserId = User.Identity.GetUserId();
            var isEndorsed = this.profileServices.IsEndorsed(id, loggedUserId);

            var endorsements = this.profileServices.GetUserEndorsements(id)
                .Project().To<UserEndorsementViewModel>();

            var contactModel = new ContactViewModel();
            contactModel.FromUserId = id;
            contactModel.FromUserName = currentUser.FullName;
            contactModel.IsConnected = this.profileServices.IsConnected(id, loggedUserId);

            var publicProfileInfo = new PublicProfileViewModel();
            publicProfileInfo.UserInfo = userInfo;
            publicProfileInfo.ContactInfo = contactModel;
            if (!isEndorsed && loggedUserId != id)
            {
                var endorseInfo = new EndorsementInputModel();
                endorseInfo.EndorsedID = id;
                endorseInfo.EndorseAction = "EndorsementOfUser";
                publicProfileInfo.EndorseFunctionality = endorseInfo;
            }

            publicProfileInfo.BtnNavigatePosts = btnNavigatePosts;
            publicProfileInfo.BtnNavigateEndorsements = btnNavigateEndorsements;
            publicProfileInfo.TopPostsList = topPostPanel;
            publicProfileInfo.RecentPostsList = recentPostPanel;
            publicProfileInfo.UserInfo.Endorsements = endorsements.ToList();

            return this.View(publicProfileInfo);
        }

        public ActionResult Private()
        {
            string currentUserId = User.Identity.GetUserId();

            var currentUser = this.GetUser(currentUserId);
            var userInfo = this.SetUserInfo(currentUser);

            var occupationsListing = new ShortListingViewModel();
            occupationsListing.Title = "Occupations";
            occupationsListing.Type = "Occupation";
            occupationsListing.Items = userInfo.Occupations;

            var fieldsListing = new ShortListingViewModel();
            fieldsListing.Title = "Fields";
            fieldsListing.Type = "Field";
            fieldsListing.Items = userInfo.Fields;

            var proInfo = new PrivateProInfoViewModel();
            proInfo.OccupationsListing = occupationsListing;
            proInfo.FieldsListing = fieldsListing;

            var messagesReceived = this.profileServices.GetUserMessages(currentUserId)
                .GroupBy(m => m.FromUserId)
                .Select(g => new MessageViewModel
                {
                    FromUserId = g.Key,
                    FromUserName = g.FirstOrDefault().FromUser.FirstName + " " + g.FirstOrDefault().FromUser.LastName,
                    Preview = g.FirstOrDefault().Content.Substring(0, 20) + "...",
                    IsRead = g.FirstOrDefault().IsRead
                });

            var connectionRequests = this.profileServices.GetUserConnectionRequests(currentUserId)
                .Project().To<ConnectionViewModel>();

            var notifications = this.profileServices.GetUserNotifications(currentUserId)
                .Project().To<NotificationShortViewModel>();

            var updateModel = new UpdatesViewModel();
            if (messagesReceived.Any(m => !m.IsRead))
            {
                updateModel.HasNewMessages = true;
            }

            if (messagesReceived.Count() > 0)
            {
                updateModel.ActiveChats = messagesReceived.ToList();
            }

            if (connectionRequests.Count() > 0)
            {
                updateModel.HasNewConnection = true;
                updateModel.ConnectionRequests = connectionRequests.ToList();
            }

            if (notifications.Count() > 0)
            {
                updateModel.HasNewNotifications = true;
                updateModel.Notifications = notifications.ToList();
            }

            var privateProfileInfo = new PrivateProfileViewModel();

            IList<NavigationItem> navItems = this.GetNavItems(currentUser.Id);
            var navList = new HorizontalNavbarViewModel();
            navList.Title = "Navigation";
            navList.ListItems = navItems;
            privateProfileInfo.UserInfo = userInfo;
            privateProfileInfo.UpdatesInfo = updateModel;
            privateProfileInfo.NavigationList = navList;
            privateProfileInfo.ProInfo = proInfo;

            return this.View(privateProfileInfo);
        }

        public ActionResult DeleteOccupation(string query, string title)
        {
            string currentUserId = User.Identity.GetUserId();
            var currentUser = this.GetUser(currentUserId);

            var occupation = currentUser.Occupations.FirstOrDefault(f => f.Title == query);
            currentUser.Occupations.Remove(occupation);

            var editedResult = currentUser.Occupations.Select(o => o.Title);

            var listModel = new ShortListingViewModel();
            listModel.Title = title;
            listModel.Type = "Occupation";
            listModel.Items = editedResult.ToList();

            return this.PartialView("~/Areas/UserArea/Views/Shared/Partials/_ShortListPanel.cshtml", listModel);
        }
        public ActionResult DeleteField(string query, string title)
        {
            string currentUserId = User.Identity.GetUserId();
            var currentUser = this.GetUser(currentUserId);

            var field = currentUser.FieldsOfExpertise.FirstOrDefault(f => f.Name == query);
            currentUser.FieldsOfExpertise.Remove(field);

            this.data.SaveChanges();

            var editedResult = currentUser.FieldsOfExpertise.Select(o => o.Name);

            var listModel = new ShortListingViewModel();
            listModel.Title = title;
            listModel.Type = "Field";
            listModel.Items = editedResult.ToList();

            return this.PartialView("~/Areas/UserArea/Views/Shared/Partials/_ShortListPanel.cshtml", listModel);
        }

        public ActionResult Filter(string query, string condition)
        {
            var resultPosts = this.profileServices.GetFilteredPosts(query, condition)
                .Take(WebConstants.ListPanelCount)
                .Select(i => new NavigationItem
                {
                    Content = i.Title,
                    Url = WebConstants.PostPageRoute + i.ID
                }).ToList();

            return this.PartialView("~/Views/Shared/Partials/_ListItems.cshtml", resultPosts);
        }

        private UserViewModel SetUserInfo(User user)
        {
            var userInfo = Mapper.Map<UserViewModel>(user);
            userInfo.PersonalHistory = this.GetModifiedHistory(userInfo.PersonalHistory);
            userInfo.IsPrivate = false;
            userInfo.ProfileImageUrl = userInfo.ProfileImageId == null ?
                WebConstants.DefaultImage :
                WebConstants.GetImagePageRoute + userInfo.ProfileImageId;

            return userInfo;
        }

        private string GetModifiedHistory(string history)
        {
            if (history == null)
            {
                return WebConstants.DefaultHistory;
            }
            else
            {
                return history;
            }
        }

        private IList<NavigationItem> GetNavItems(string currentUserId)
        {
            return new List<NavigationItem>
            {
                new NavigationItem 
                { 
                    Content = "Create Post",
                    Url = WebConstants.CreatePostPageRoute
                },
                new NavigationItem 
                { 
                    Content = "Posts",
                    Url = WebConstants.UserPostsPageRoute + currentUserId
                },
                new NavigationItem 
                { 
                    Content = "Endorsements",
                    Url = WebConstants.UserEndorsementsPageRoute + currentUserId
                },
                new NavigationItem 
                { 
                    Content = "Connections",
                    Url = WebConstants.UserConnectionsPageRoute + currentUserId
                },
            };
        }
    }
}