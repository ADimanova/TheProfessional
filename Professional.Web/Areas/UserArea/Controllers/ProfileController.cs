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
            topPostPanel.UniqueIdentificator = "Top";
            topPostPanel.Title = "Top Posts";
            topPostPanel.Items = this.GetTopPosts(id)
                .Select(p => new NavigationItem
                {
                    Content = p.Title,
                    Url = "#"
                }).ToList<NavigationItem>();
            topPostPanel.Fields = userFields;

            var recentPostPanel = new ListPanelViewModel();
            recentPostPanel.UniqueIdentificator = "Recent";
            recentPostPanel.Title = "Recent Posts";
            recentPostPanel.Items = this.GetRecentPosts(id)
                .Select(p => new NavigationItem
                {
                    Content = p.Title,
                    Url = "#"
                }).ToList<NavigationItem>();
            recentPostPanel.Fields = userFields;

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
            publicProfileInfo.RecentPostsList = recentPostPanel;
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

            var occupationsListing = new ShortListingViewModel();
            occupationsListing.Title = "Occupations";
            occupationsListing.Type = "Occupation";
            occupationsListing.Items = userInfoForView.Occupations;

            var fieldsListing = new ShortListingViewModel();
            fieldsListing.Title = "Fields";
            fieldsListing.Type = "Field";
            fieldsListing.Items = userInfoForView.Fields;

            var proInfo = new PrivateProInfoViewModel();
            proInfo.OccupationsListing = occupationsListing;
            proInfo.FieldsListing = fieldsListing;

            var messagesReceived = this.data.Messages.All()
                .Where(m => m.ToUserId == currentUserId)
                .Where(m => m.IsRead == false)
                .GroupBy(m => m.FromUserId)
                .Select(g => new MessagesViewModel
                {
                    FromUserId = g.Key,
                    FromUserName = g.FirstOrDefault().FromUser.FirstName + " " + g.FirstOrDefault().FromUser.LastName,
                    Preview = g.FirstOrDefault().Content.Substring(0, 20) + "...",
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
            privateProfileInfo.ProInfo = proInfo;

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

        public ActionResult Filter(string query, string condition)
        {
            if (currentUser == null)
            {
                return View("Error", "Something went wrong.");
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
                return View("Error", "The condition specified is incorrect");
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

        public ActionResult Image(int id)
        {
            var image = this.data.Images.GetById(id);
            if (image == null)
            {
                throw new HttpException(404, "Image not found");
            }

            return File(image.Content, "image/" + image.FileExtension);
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
                return View("Error", "The type of the item is incorrect");
            }
        }
    }
}