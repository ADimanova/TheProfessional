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
using Professional.Web.Infrastructure.HtmlSanitise;
using System.Web.Caching;
using Professional.Data;
using Professional.Web.Helpers;
using Professional.Web.Infrastructure.Services;
using Professional.Web.Infrastructure.Services.Contracts;

namespace Professional.Web.Controllers
{
    public class HomeController : BaseController
    {
        private const string FieldsNavBarTitle = "Our top fields";
        private const int FieldsCount = 9;
        private const int PostCount = 3;
        private const int FeaturedCount = 3;

        private IHomeServices homeServices;
        public HomeController(IApplicationData data, IHomeServices homeServices)
            : base(data)
        {
            this.homeServices = homeServices;
        }

        public ActionResult Index()
        {
            var fields = this.homeServices.GetFields().ToList<NavigationItem>();
            var posts = this.homeServices.GetTopPosts().ToList<PostSimpleViewModel>();
            var featured = this.homeServices.GetFeatured().ToList<UserSimpleViewModel>();

            var fieldsView = new HorizontalNavbarViewModel();
            fieldsView.Title = FieldsNavBarTitle;
            fieldsView.ListItems = fields;

            var vielModel = new IndexViewModel();

            vielModel.FieldsListing = fieldsView;
            vielModel.Posts = posts.ToList<PostSimpleViewModel>();
            vielModel.Featured = featured.ToList<UserSimpleViewModel>();

            return View(vielModel);
        }
 
        public ActionResult Error()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}