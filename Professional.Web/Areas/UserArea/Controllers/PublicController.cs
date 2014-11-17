using AutoMapper;
using Professional.Common;
using Professional.Data;
using Professional.Models;
using Professional.Web.Areas.UserArea.Models;
using Professional.Web.Helpers;
using Professional.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Professional.Web.Areas.UserArea.Controllers
{
    public class PublicController : UserController
    {
        private readonly IQueryable<Post> postsData;
        private static User currentUser;

        public PublicController(IApplicationData data)
            : base(data)
        {
            this.postsData = this.data.Posts.All().OrderBy(p => p.Title);
        }

        // GET: UserArea/Public/Profile/{id}
        public ActionResult Profile(string id)
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
            btnNavigatePosts.Url = WebConstants.PostsPageRoute + currentUser.Id;

            var btnNavigateEndorsements = new NavigationItem();
            btnNavigateEndorsements.Content = "See endorsements's page";
            btnNavigateEndorsements.Url = "#";

            var publicProfileInfo = new PublicProfileViewModel();
            publicProfileInfo.UserInfo = userInfoForView;
            publicProfileInfo.BtnNavigatePosts = btnNavigatePosts;
            publicProfileInfo.BtnNavigateEndorsements = btnNavigateEndorsements;
            publicProfileInfo.TopPostsList = topPostPanel;
            publicProfileInfo.RecentPostsList = recentPostPanel;

            return View(publicProfileInfo);
        }

        // GET: UserArea/Public/Posts/{id} - the user id here 
        public ActionResult Posts(string id, int page = 1, int perPage = WebConstants.PostsPerPage,
            string fieldName = null)
        {
            //var currentUrl = Request.Url.AbsolutePath;
            var pagesCount = (int)Math.Ceiling(this.postsData.Count() / (decimal)perPage);

            var fieldsNames = this.data.FieldsOfExpertise
                .All()
                .Select(f => f.Name);

            IQueryable<Post> posts = new List<Post>().AsQueryable();
            if (id != null)
            {
                posts = this.GetAllPostsOfUser(id);                
            }
            else
            {
                posts = this.GetAllPosts(); 
            }

            IQueryable<Post> postRaw = new List<Post>().AsQueryable();
            if (fieldName != null)
            {
                postRaw = posts.OrderBy(p => p.Field.Name)
                .Where(p => p.Field.Name == fieldName)
                .Skip(perPage * (page - 1))
                .Take(perPage);
            }
            else
            {
                postRaw = posts.OrderBy(p => p.Field.Name)
                .Skip(perPage * (page - 1))
                .Take(perPage);
            }

            var grouped = postRaw.GroupBy(p => p.Field.Name)
                .Select(p => new ItemsByFieldViewModel
                {
                    Name = p.FirstOrDefault().Field.Name,
                    Items = p.Select(i => new NavigationItem
                    {
                        Content = i.Title,
                        Url = WebConstants.PostPageRoute + i.ID
                    }).ToList()
                });

            var viewModel = new ListCollectionViewModel();
            viewModel.Url = WebConstants.PostsPageRoute;
            viewModel.FieldsNames = fieldsNames.ToList();
            viewModel.Title = "Posts";
            viewModel.GetBy = "field";
            viewModel.Fields = grouped.ToList();
            viewModel.CurrentPage = page;
            viewModel.PagesCount = pagesCount;

            return View(viewModel);
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