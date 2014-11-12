using Professional.Data;
using Professional.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Professional.Web.Areas.UserArea.Controllers
{
    public class SearchController : UserController
    {
        public SearchController(IApplicationData data)
            : base(data)
        {

        }

        // GET: UserArea/Search
        public ActionResult Index(string searchString)
        {
            var movies = new List<NavigationItem> 
            { new NavigationItem {
                Content = "First here",
                Url = "#"
                },
                new NavigationItem {
                Content = "Second here",
                Url = "#"
                }
            };

            List<NavigationItem> result = new List<NavigationItem>();

            if (!String.IsNullOrEmpty(searchString))
            {
                result = movies.Where(s => s.Content.ToLower().Contains(searchString.ToLower())).ToList<NavigationItem>();
            }

            return View(result);
        }
    }
}