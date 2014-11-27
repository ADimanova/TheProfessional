using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Professional.Data;
using Professional.Models;
using AutoMapper;
using Professional.Web.Models.DatabaseViewModels;
using Professional.Web.Areas.UserArea.Models;
using Professional.Web.Models;
using Professional.Web.Helpers;
using Professional.Web.Areas.UserArea.Models.InputModels;

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

            var recentPostPanel = new ListPanelViewModel();
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
            btnNavigateEndorsements.Url = "#";

            var userID = User.Identity.GetUserId();
            var isEndorsed = this.data.EndorsementsOfUsers.All()
                .Where(e => e.EndorsingUserID == userID)
                .Any(e => e.EndorsedUserID == id);

            var publicProfileInfo = new PublicProfileViewModel();
            publicProfileInfo.UserInfo = userInfoForView;
            if (!isEndorsed && userID != id)
            {
                var endorseInfo = new UserEndorsementInputModel();
                endorseInfo.EndorsedUserID = id;
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

            return View(publicProfileInfo);
        }

        public ActionResult Private()
        {
            string currentUserId = User.Identity.GetUserId();
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

        public ActionResult EndorseUser(string id)
        {
            var endorsee = this.data.Users.GetById(id);
            return Content(endorsee.FirstName);
        }

        private IList<NavigationItem> GetNavItems()
        {
            return new List<NavigationItem>
            {
                new NavigationItem { 
                    Content = "Create Post",
                    Url = WebConstants.CreatePostPageRoute
                },
                new NavigationItem { 
                    Content = "Go to post's page",
                    Url = WebConstants.UserPostsPageRoute + User.Identity.GetUserId()
                },
                new NavigationItem { 
                    Content = "Go to endorsements's page",
                    Url = "#"
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