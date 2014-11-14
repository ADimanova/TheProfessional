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
    public class AllUsersController : UserController
    {
        private readonly IQueryable<User> usersData;
        public AllUsersController(IApplicationData data)
            : base(data)
        {
            this.usersData = this.data.Users.All()
                .OrderByDescending(u => u.LastName)
                .ThenByDescending(u => u.FirstName);
        }

        // GET: UserArea/AllUsers
        public ActionResult Index(string id = null, int page = 1, int perPage = WebConstants.PostsPerPage)
        {
            var pagesCount = (int)Math.Ceiling(this.usersData.Count() / (decimal)perPage);

            var firstLetters = this.usersData
                .Select(f => f.LastName.Substring(0, 1));

            IQueryable<User> users = new List<User>().AsQueryable();
            if (id != null)
            {
                users = this.usersData
                .Where(p => p.LastName.StartsWith(id))
                .Skip(perPage * (page - 1))
                .Take(perPage);
            }
            else
            {
                users = this.usersData
                .Skip(perPage * (page - 1))
                .Take(perPage);
            }

            //var allUser = this.data.Users.All()
            //    .OrderBy(u => u.LastName)
            //    .ThenBy(u => u.FirstName);

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

            var viewModel = new ListCollectionViewModel();
            viewModel.Url = WebConstants.UsersPageRoute;
            viewModel.FieldsNames = firstLetters.ToList();
            viewModel.Title = "Professionals";
            viewModel.GetBy = "first letter";
            viewModel.Fields = groupedByFirstLetter.ToList();

            return View(viewModel);
        }
    }
}