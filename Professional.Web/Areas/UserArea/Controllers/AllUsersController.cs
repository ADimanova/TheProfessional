using Professional.Web.Areas.UserArea.Models;
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
        // GET: UserArea/AllUsers
        public ActionResult Index()
        {
            var allUser = this.data.Users.All()
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName);

            var groupedByFirstLetter = allUser.GroupBy(s => s.LastName.Substring(0, 1))

                .Select(g => new ItemsByFieldViewModel
                {
                    Name = g.Key.ToString(),
                    Items = g.Select(i => new NavigationItem
                    {
                        Content = i.LastName,
                        Url = "#"
                    }).ToList()
                });

            var viewModel = new ListCollectionViewModel();
            viewModel.Title = "Professionals";
            viewModel.GetBy = "first letter";
            viewModel.Fields = groupedByFirstLetter.ToList();

            return View(viewModel);
        }
    }
}