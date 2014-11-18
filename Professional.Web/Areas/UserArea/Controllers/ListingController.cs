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
    public class ListingController : UserController
    {
        int itemsPerPage = WebConstants.PostsPerPage;
        public ListingController(IApplicationData data)
            : base(data)
        {
        }

        // GET: UserArea/Listing
        public ActionResult Users(string id, int? page)
        {
            int pageNumber = page.GetValueOrDefault(1);

            var users = this.GetUsers(id)
                .OrderBy(f => f.UserName)
                .Skip((pageNumber - 1) * itemsPerPage)
                .Take(itemsPerPage);

            var firstLetters = users
                .Select(f => f.LastName.Substring(0, 1));

            var groupedByFirstLetter = users.GroupBy(s => s.LastName.Substring(0, 1))
                .Select(g => new ItemsByFieldViewModel
                {
                    Name = g.Key.ToString(),
                    Items = g.Select(i => new NavigationItem
                    {
                        Content = i.LastName,
                        Url = WebConstants.PublicProfilePageRoute + i.Id
                    }).ToList()
                });

            var pageCount = Math.Ceiling((double)this.GetUsers(null).Count() / itemsPerPage);
            this.SetPaging(pageNumber, pageCount);
            ViewBag.Url = WebConstants.UsersPageRoute;

            var viewModel = new ListCollectionViewModel();
            viewModel.FieldsNames = firstLetters.ToList();
            viewModel.Title = "Professionals";
            viewModel.GetBy = "first letter";
            viewModel.Fields = groupedByFirstLetter.ToList();

            return View(viewModel);
        }

        public ActionResult Posts(string id, int? page)
        {
            int pageNumber = page.GetValueOrDefault(1);

            var posts = this.GetPosts(id)
                .OrderBy(f => f.Field.Name)
                .Skip((pageNumber - 1) * itemsPerPage)
                .Take(itemsPerPage);

            var fieldsNames = this.data.FieldsOfExpertise
                .All()
                .Select(f => f.Name);

            var groupedByField = posts.GroupBy(p => p.Field.Name)
                .Select(p => new ItemsByFieldViewModel
                {
                    Name = p.FirstOrDefault().Field.Name,
                    Items = p.Select(i => new NavigationItem
                    {
                        Content = i.Title,
                        Url = WebConstants.PostPageRoute + i.ID
                    }).ToList()
                });

            var pageCount = Math.Ceiling((double)this.GetPosts(null).Count() / itemsPerPage);
            this.SetPaging(pageNumber, pageCount);
            ViewBag.Url = WebConstants.PostsPageRoute;

            var viewModel = new ListCollectionViewModel();
            viewModel.FieldsNames = fieldsNames.ToList();
            viewModel.Title = "Posts";
            viewModel.GetBy = "field";
            viewModel.Fields = groupedByField.ToList();

            return View(viewModel);
        }

        private void SetPaging(int pageNumber, double pageCount)
        {
            ViewBag.Pages = pageCount;

            ViewBag.CurrentPage = pageNumber;

            if (ViewBag.CurrentPage >= pageCount)
            {
                ViewBag.NextPage = 1;
            }
            else
            {
                ViewBag.NextPage = ViewBag.CurrentPage + 1;
            }

            if (ViewBag.CurrentPage <= 1)
            {
                ViewBag.PreviousPage = pageCount;
            }
            else
            {
                ViewBag.PreviousPage = ViewBag.CurrentPage - 1;
            }
        }

        [OutputCache(Duration = 60, VaryByParam = "none")]
        private IQueryable<User> GetUsers(string filter)
        {
            var users = this.data.Users.All();
            if (filter != null)
            {
                filter = filter.ToLower();
                users = users.Where(u => u.UserName.Substring(0, 1) == filter);
            }

            return users;
        }

        [OutputCache(Duration = 60, VaryByParam = "none")]
        private IQueryable<Post> GetPosts(string filter)
        {
            var posts = this.data.Posts.All();
            if (filter != null)
            {
                filter = filter.ToLower();
                posts = posts.Where(u => u.Field.Name.ToLower() == filter);
            }
            return posts;
        }
    }
}