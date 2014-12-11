using AutoMapper.QueryableExtensions;
using Professional.Data;
using Professional.Web.Helpers;
using Professional.Web.Infrastructure.Caching;
using Professional.Web.Infrastructure.Services.Base;
using Professional.Web.Infrastructure.Services.Contracts;
using Professional.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Professional.Web.Infrastructure.Services
{
    public class HomeServices : BaseServices, IHomeServices
    {
        public HomeServices(IApplicationData data, ICacheService cache)
            : base(data, cache)
        {
        }

        private const int FieldsCount = 9;
        private const int PostCount = 3;
        private const int FeaturedCount = 3;

        public IQueryable<NavigationItem> GetFields()
        {
            var fields = this.Cache.Get<IQueryable<NavigationItem>>("FieldsHome",
                () => this.Data.FieldsOfExpertise.All()
                .Where(u => u.IsDeleted == false)
                .OrderByDescending(f => f.Rank)
                .Take(FieldsCount)
                .Select(f => new NavigationItem
                {
                    Content = f.Name,
                    Url = WebConstants.FieldInfoPageRoute + f.Name
                }));

            return fields;
        }

        public IQueryable<PostSimpleViewModel> GetTopPosts()
        {
            var posts = this.Cache.Get<IQueryable<PostSimpleViewModel>>("PostsHome", 
                () => this.Data.Posts.All()
                .Where(u => u.IsDeleted == false)
                .OrderByDescending(p => p.DateCreated)
                .Take(PostCount)
                .Project().To<PostSimpleViewModel>());

            var formatedPosts = this.FormatPosts(posts);

            return formatedPosts;
        }

        public IQueryable<UserSimpleViewModel> GetFeatured()
        {
            var users = this.Cache.Get<IQueryable<UserSimpleViewModel>>("FeaturedHome",
                () => this.Data.Users.All()
                .Where(u => u.IsDeleted == false)
                .Where(u => u.FieldsOfExpertise.Count > 0)
                .OrderByDescending(u => u.UsersEndorsements.Count)
                .Take(FeaturedCount)
                .Project().To<UserSimpleViewModel>());

            return users;
        }


        private IQueryable<PostSimpleViewModel> FormatPosts(IQueryable<PostSimpleViewModel> rawPosts)
        {
            IList<PostSimpleViewModel> posts = rawPosts.ToList();
            for (int i = 0; i < posts.Count; i++)
            {
                var post = posts[i];

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