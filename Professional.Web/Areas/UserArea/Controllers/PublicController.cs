using AutoMapper;
using Professional.Models;
using Professional.Web.Areas.UserArea.Models;
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

        // GET: UserArea/Public/Posts/{id}
        public ActionResult Posts(string id)
        {
            var posts = this.GetAllPosts(id);
            var grouped = posts.GroupBy(p => p.FieldID)
                .Select(p => new PostsByFieldViewModel
                {
                    Name = p.FirstOrDefault().Field.Name,
                    Posts = p.ToList()
                });

            var viewModel = new ListCollectionViewModel();
            viewModel.Title = "Posts";
            viewModel.Fields = grouped.ToList();

            return View(viewModel);
        }
    }
}