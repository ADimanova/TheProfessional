namespace Professional.Web.Infrastructure.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Professional.Data;
    using Professional.Models;
    using Professional.Web.Helpers;
    using Professional.Web.Infrastructure.Caching;
    using Professional.Web.Infrastructure.Services.Base;
    using Professional.Web.Infrastructure.Services.Contracts;

    public class HomeServices : BaseServices, IHomeServices
    {
        private const int FieldsCount = 9;
        private const int PostCount = 3;
        private const int FeaturedCount = 3;

        public HomeServices(IApplicationData data, ICacheService cache)
            : base(data, cache)
        {
        }

        public IQueryable<FieldOfExpertise> GetFields()
        {
            var fields = this.Cache.Get<IQueryable<FieldOfExpertise>>(
                "FieldsHome",
                () => this.Data.FieldsOfExpertise.All()
                .OrderByDescending(f => f.Rank)
                .Take(FieldsCount));

            return fields;
        }

        public IQueryable<Post> GetTopPosts()
        {
            var posts = this.Cache.Get<IQueryable<Post>>(
                "PostsHome", 
                () => this.Data.Posts.All()
                .OrderByDescending(p => p.CreatedOn)
                .Take(PostCount));

            var formatedPosts = this.FormatPosts(posts);

            return formatedPosts;
        }

        public IQueryable<User> GetFeatured()
        {
            var users = this.Cache.Get<IQueryable<User>>(
                "FeaturedHome",
                () => this.Data.Users.All()
                .Where(u => u.IsDeleted == false)
                .Where(u => u.FieldsOfExpertise.Count > 0)
                .OrderByDescending(u => u.UsersEndorsements.Count)
                .Take(FeaturedCount));

            return users;
        }

        private IQueryable<Post> FormatPosts(IQueryable<Post> rawPosts)
        {
            IList<Post> posts = rawPosts.ToList();
            for (int i = 0; i < posts.Count; i++)
            {
                var post = posts[i];
                var content = StringManipulations.StripHtml(post.Content);
                content = StringManipulations.GetSubstring(content, 0, WebConstants.TitleLength);
                posts[i].Content = content;
            }

            return posts.AsQueryable<Post>();
        }
    }
}