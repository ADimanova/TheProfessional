using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Professional.Common;
using Professional.Models;
using Professional.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Professional.Web.Helpers;

namespace Professional.Web.Controllers
{
    public class HomeController : BaseController
    {
        private const int FieldsCount = 9;
        private const int PostCount = 3;
        private const int FeaturedCount = 3;
        public ActionResult Index()
        {
            var fields = this.data.FieldsOfExpertise.All()
                .OrderByDescending(f => f.Rank)
                .Take(FieldsCount)
                .Select(f => new NavigationItem
                {
                    Content = f.Name,
                    Url = "#"
                });

            var posts = this.data.Posts.All()
                .OrderByDescending(p => p.DateCreated)
                .Take(PostCount)
                .Project().To<PostSimpleViewModel>()
                .ToList<PostSimpleViewModel>();

            // TODO: Don't get administrators (the code is ready to be added)
            var featured = this.data.Users.All()
                .OrderByDescending(u => u.UsersEndorsements.Count)
                .Take(FeaturedCount)
                .Project().To<UserSimpleViewModel>()
                .ToList<UserSimpleViewModel>();

            var fieldsView = new HorizontalNavbarViewModel();
            fieldsView.Title = "Our top fields";
            fieldsView.ListItems = fields;

            var vielModel = new IndexViewModel();
            vielModel.FieldsListing = fieldsView;
            vielModel.Posts = posts;
            vielModel.Featured = featured;

            return View(vielModel);
        }

        public ActionResult HomeDesign()
        {
            return View();
        }

        public ActionResult About()
        {
            this.data.SaveChanges();

            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}