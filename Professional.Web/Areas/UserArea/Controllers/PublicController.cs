using AutoMapper;
using Professional.Data;
using Professional.Models;
using Professional.Web.Areas.UserArea.Models;
using Professional.Web.Helpers;
using Professional.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Professional.Web.Areas.UserArea.Controllers
{
    public class PublicController : UserController
    {
        //private readonly IQuearyable<BlogPost> blogPostsData;
        private readonly IQueryable<Post> postsData;
        //private readonly IRepository<Page> pagesData;

        public PublicController(IApplicationData data)
            : base(data)
        {
            this.postsData = this.data.Posts.All().OrderBy(p => p.Title);
        }

        // GET: UserArea/Public/Profile/{id}
        public ActionResult Profile(string id)
        {
            var currentUser = this.data.Users.All()
                .FirstOrDefault(u => u.Id == id);

            Mapper.CreateMap<User, UserViewModel>();
            var userInfoForView = Mapper.Map<UserViewModel>(currentUser);

            var userFields = (List<FieldOfExpertise>)this.GetUserFields(id);

            var topPostPanel = new ListPanelViewModel();
            topPostPanel.Items = (List<Post>)this.GetTopPosts(id);
            topPostPanel.Fields = userFields;

            var recentPostPanel = new ListPanelViewModel();
            recentPostPanel.Items = (List<Post>)this.GetRecentPosts(id);
            recentPostPanel.Fields = userFields;

            var publicProfileInfo = new PublicProfileViewModel();
            publicProfileInfo.UserInfo = userInfoForView;
            publicProfileInfo.BtnNavigatePosts = "See post's page";
            publicProfileInfo.BtnNavigateEndorsements = "See endorsements's page";
            publicProfileInfo.TopPostsList = topPostPanel;
            publicProfileInfo.RecentPostsList = recentPostPanel;

            return View(publicProfileInfo);
        }

        // GET: UserArea/Public/Posts/{id} - the user id here 
        public ActionResult Posts(string id, int page = 1, int perPage = WebConstants.PostsPerPage)
        {
            var pagesCount = (int)Math.Ceiling(this.postsData.Count() / (decimal)perPage);

            IQueryable<Post> posts = new List<Post>().AsQueryable();
            if (id != null)
            {
                posts = this.GetAllPostsOfUser(id);                
            }
            else
            {
                posts = this.GetAllPosts(); 
            }

            var postRaw = posts.OrderBy(p => p.Field.Name)
                .Skip(perPage * (page - 1))
                .Take(perPage);

            var grouped = postRaw.GroupBy(p => p.Field.Name)
                .Select(p => new ItemsByFieldViewModel
                {
                    Name = p.FirstOrDefault().Field.Name,
                    Items = p.Select(i => new NavigationItem
                    {
                        Content = i.Title,
                        Url = "#"
                    }).ToList()
                });

            var viewModel = new ListCollectionViewModel();
            viewModel.Title = "Posts";
            viewModel.GetBy = "first letter";
            viewModel.Fields = grouped.ToList();
            viewModel.CurrentPage = page;
            viewModel.PagesCount = pagesCount;

            return View(viewModel);
        }
    }
}