using System;
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
using Professional.Web.Areas.UserArea.Models.DatabaseVeiwModels;
using Professional.Web.Infrastructure.Services.Contracts;

namespace Professional.Web.Areas.UserArea.Controllers
{
    public class ProfileController : UserController
    {
        private static User currentUser;
        private readonly IQueryable<Post> postsData;
        private IProfileServices profileServices;
        public ProfileController(IApplicationData data, IProfileServices profileServices)
            : base(data)
        {
            this.postsData = profileServices.GetAllPosts();
            this.profileServices = profileServices;
        }

        // GET: UserArea/Profile
        public ActionResult Public(string id)
        {
            currentUser = this.GetUser(id);

            if (currentUser == null)
            {
                return this.RedirectToAction("Index", "Home", new { Area = "" });   
            }

            var userInfo = Mapper.Map<UserViewModel>(currentUser);

            var userFields = profileServices.GetUserFields(id)
                .Select(f => f.Name).ToList();

            var topPostPanel = new ListPanelViewModel();
            topPostPanel.UniqueIdentificator = "Top";
            topPostPanel.Title = "Top Posts";
            topPostPanel.Items = profileServices.GetTopPosts(id)
                .Select(p => new NavigationItem
                {
                    Content = p.Title,
                    Url = "#"
                }).ToList<NavigationItem>();
            topPostPanel.Fields = userFields;

            var recentPostPanel = new ListPanelViewModel();
            recentPostPanel.UniqueIdentificator = "Recent";
            recentPostPanel.Title = "Recent Posts";
            recentPostPanel.Items = profileServices.GetRecentPosts(id)
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
            var isEndorsed = profileServices.IsEndorsed(id, loggedUserId);

            var endorsements = profileServices.GetUserEndorsements(id)
                .Select(e => new EndorsementViewModel
                {
                    ID = e.ID,
                    Content = e.Comment,
                    AuthorFirstName = e.EndorsingUser.FirstName,
                    AuthorLastName = e.EndorsingUser.LastName
                });

            var contactModel = new ContactViewModel();
            contactModel.FromUserId = id;
            contactModel.FromUserName = currentUser.FullName;
            contactModel.IsConnected = profileServices.IsConnected(id, loggedUserId);

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
            publicProfileInfo.UserInfo.Endorsements = endorsements;

            return View(publicProfileInfo);
        }

        public ActionResult Private()
        {
            string currentUserId = User.Identity.GetUserId();
            var currentUser = this.GetUser(currentUserId);

            if (currentUser == null)
            {
                return this.RedirectToAction("Index", "Home", new { Area = "" });   
            }

            var userInfo = Mapper.Map<UserViewModel>(currentUser);

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

            var messagesReceived = profileServices.GetUserMessages(currentUserId)
                .GroupBy(m => m.FromUserId)
                .Select(g => new MessageViewModel
                {
                    FromUserId = g.Key,
                    FromUserName = g.FirstOrDefault().FromUser.FirstName + " " + g.FirstOrDefault().FromUser.LastName,
                    Preview = g.FirstOrDefault().Content.Substring(0, 20) + "...",
                    IsRead = g.FirstOrDefault().IsRead
                });

            var connectionRequests = profileServices.GetUserConnectionRequests(currentUserId)
                .Project().To<ConnectionViewModel>();

            var notifications = profileServices.GetUserNotifications(currentUserId)
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

            return View(privateProfileInfo);
        }

        public ActionResult EndorseUser(string id)
        {
            var endorsee = this.data.Users.GetById(id);
            return Content(endorsee.FirstName);
        }

        private IList<NavigationItem> GetNavItems(string currentUserId)
        {
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

        public ActionResult Filter(string query, string condition)
        {
            if (currentUser == null)
            {
                return View("Error");
            }

            var posts = postsData
                .Where(p => p.CreatorID == currentUser.Id);

            if (query != "All")
	        {
                posts = posts
                .Where(p => p.Field.Name == query);
	        }

            if (condition == "Recent")
            {
                posts = posts
                .OrderBy(p => p.CreatedOn);
            }
            else if (condition == "Top")
            {
                posts = posts
                .OrderBy(p => p.Rank);
            }
            else
            {
                return View("Error");
            }

            var resultPosts = posts
                .Take(WebConstants.ListPanelCount)
                .Select(i => new NavigationItem
                {
                    Content = i.Title,
                    Url = WebConstants.PostPageRoute + i.ID
                }).ToList();

            return this.PartialView("~/Views/Shared/Partials/_ListItems.cshtml", resultPosts);
        }

        public ActionResult Delete(string query, string type, string title)
        {
            string currentUserId = User.Identity.GetUserId();
            var currentUser = this.data.Users.All()
                .FirstOrDefault(u => u.Id == currentUserId);

            if (type == "Occupation")
            {
                var occupation = this.data.Occupations.All()
                    .FirstOrDefault(f => f.Title == query);
                currentUser.Occupations.Remove(occupation);
                this.data.SaveChanges();

                var editedOccupations = currentUser.Occupations;

                var model = new ShortListingViewModel();
                model.Title = title;
                model.Type = type;
                model.Items = editedOccupations.Select(o => o.Title);

                return this.PartialView("~/Areas/UserArea/Views/Shared/Partials/_ShortListPanel.cshtml", model);
            }
            else if (type == "Field")
            {
                var field = this.data.FieldsOfExpertise.All()
                    .FirstOrDefault(f => f.Name == query);
                currentUser.FieldsOfExpertise.Remove(field);
                this.data.SaveChanges();

                var editedFields = currentUser.FieldsOfExpertise;

                var model = new ShortListingViewModel();
                model.Title = title;
                model.Type = type;
                model.Items = editedFields.Select(o => o.Name);

                return this.PartialView("~/Areas/UserArea/Views/Shared/Partials/_ShortListPanel.cshtml", model);
            }
            else
            {
                return View("Error");
            }
        }
    }
}