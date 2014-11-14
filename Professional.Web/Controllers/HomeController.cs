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

namespace Professional.Web.Controllers
{
    public class HomeController : BaseController
    {
        private ISanitiser sanitiser;
        public HomeController(IApplicationData data, ISanitiser sanitiser)
            : base(data)
        {
            this.sanitiser = sanitiser;
        }

        private const int FieldsCount = 9;
        private const int PostCount = 3;
        private const int FeaturedCount = 3;
        public ActionResult Index()
        {
            IQueryable<NavigationItem> fields;
            IQueryable<PostSimpleViewModel> posts;
            IQueryable<UserSimpleViewModel> featured;

            if (this.HttpContext.Cache["Fields"] == null
                || this.HttpContext.Cache["Posts"] == null
                || this.HttpContext.Cache["Featured"] == null)
	        {
                fields = this.data.FieldsOfExpertise.All()
                .OrderByDescending(f => f.Rank)
                .Take(FieldsCount)
                .Select(f => new NavigationItem
                {
                    Content = f.Name,
                    Url = "#"
                });

                var rawPosts = this.data.Posts.All()
                    .OrderByDescending(p => p.DateCreated)
                    .Take(PostCount)
                    .Project().To<PostSimpleViewModel>();

                posts = this.FormatItems(rawPosts);

                // TODO: Don't get administrators (the code is ready to be added)
                featured = this.data.Users.All()
                    .Where(u => u.FieldsOfExpertise.Count > 0)
                    .OrderByDescending(u => u.UsersEndorsements.Count)
                    .Take(FeaturedCount)
                    .Project().To<UserSimpleViewModel>();

                AddToCache<NavigationItem>("Fields", fields);
                AddToCache<PostSimpleViewModel>("Posts", posts);
                AddToCache<UserSimpleViewModel>("Featured", featured);
	        }

            fields = this.HttpContext.Cache["Fields"] as IQueryable<NavigationItem>;
            posts = this.HttpContext.Cache["Posts"] as IQueryable<PostSimpleViewModel>;
            featured = this.HttpContext.Cache["Featured"] as IQueryable<UserSimpleViewModel>;

            var fieldsView = new HorizontalNavbarViewModel();
            fieldsView.Title = "Our top fields";
            fieldsView.ListItems = fields.ToList<NavigationItem>();

            var vielModel = new IndexViewModel();
            vielModel.FieldsListing = fieldsView;
            vielModel.Posts = posts.ToList<PostSimpleViewModel>();
            vielModel.Featured = featured.ToList<UserSimpleViewModel>();

            return View(vielModel);
        }
 
        private void AddToCache<T>(string key, IQueryable<T> items)
        {
            this.HttpContext.Cache.Add(key, items, null, DateTime.Now.AddMinutes(1),
                TimeSpan.Zero, CacheItemPriority.Default, null);
        }

        public ActionResult Error()
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

        private IQueryable<PostSimpleViewModel> FormatItems(IQueryable<PostSimpleViewModel> rawPosts)
        {
            IList<PostSimpleViewModel> posts = rawPosts.ToList();
            for (int i = 0; i < posts.Count; i++)
            {
                var post = posts[i];

                // TODO: Refactor
                // TODO: To upper case of first letter
                var title = StringManipulations.GetSubstring(post.Title, 0, WebConstants.TitleLength);

                var content = StringManipulations.StripHtml(post.Content);
                content = StringManipulations.GetSubstring(content, 0, WebConstants.TitleLength);

                posts[i].Title = title;
                posts[i].Content = content;
            }

            return posts.AsQueryable<PostSimpleViewModel>();
        }
    }
}