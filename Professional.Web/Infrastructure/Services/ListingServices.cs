using Professional.Web.Infrastructure.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Professional.Web.Infrastructure.Services.Contracts;
using System.Web.Mvc;
using Professional.Models;
using Professional.Data;
using Professional.Web.Infrastructure.Caching;

namespace Professional.Web.Infrastructure.Services
{
    public class ListingServices : BaseServices, IListingServices
    {
        public ListingServices(IApplicationData data, ICacheService cache)
            : base(data, cache)
        {
        }

        public IQueryable<User> GetUsers(string filter)
        {
            var users = this.Cache.Get<IQueryable<User>>("UsersListing",
                () => this.Data.Users.All());

            if (filter != null)
            {
                filter = filter.ToLower();
                users = users.Where(u => u.UserName.Substring(0, 1) == filter);
            }

            return users;
        }

        //TODO: Check if cache is affected by filtering
        public IQueryable<Post> GetPosts(string filter, string user)
        {
            var posts = this.Cache.Get<IQueryable<Post>>("PostsListing",
                () => this.Data.Posts.All());

            if (filter != null)
            {
                filter = filter.ToLower();
                posts = posts.Where(p => p.Field.Name.ToLower() == filter);
            }

            if (user != null)
            {
                posts = posts.Where(p => p.CreatorID == user);
            }

            return posts;
        }

        public IQueryable<EndorsementOfUser> GetEndorsements(string userID)
        {
            var endorsements = this.Cache.Get<IQueryable<EndorsementOfUser>>("PostsListing",
                () => this.Data.EndorsementsOfUsers.All());

            if (userID == null)
            {
                throw new ArgumentNullException("You must specify the user by ID");
            }

            endorsements = endorsements.Where(p => p.EndorsedUserID == userID);

            return endorsements;
        }
    }
}