﻿namespace Professional.Web.Infrastructure.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Professional.Data;
    using Professional.Models;
    using Professional.Web.Infrastructure.Caching;
    using Professional.Web.Infrastructure.Services.Base;
    using Professional.Web.Infrastructure.Services.Contracts;

    public class ListingServices : BaseServices, IListingServices
    {
        private string all = "All";

        private IQueryable<string> letters = new List<string>
            { 
                "A", "B", "C", "D", "E", "F", "G", "H", "I",
                "J", "K", "L", "M", "N", "O", "P", "Q", "R",
                "S", "T", "U", "V", "W", "X", "Y", "Z" 
            }
            .AsQueryable<string>();

        public ListingServices(IApplicationData data, ICacheService cache)
            : base(data, cache)
        {
        }

        public IQueryable<User> GetUsers(string filter, string user)
        {
            var users = this.Cache.Get<IQueryable<User>>(
                "UsersListing",
                () => this.Data.Users.All().Where(u => !u.IsDeleted));

            if (filter != null && filter != this.all)
            {
                filter = filter.ToLower();
                users = users.Where(u => u.LastName.Substring(0, 1).ToLower() == filter);
            }

            if (user != null)
            {
                users = users.Where(u => u.Connections
                    .Any(c => (c.FirstUserId == user || c.SecondUserId == user) && c.IsAccepted));
            }

            return users;
        }

        public IQueryable<Post> GetPosts(string filter, string user)
        {
            var posts = this.Cache.Get<IQueryable<Post>>(
                "PostsListing",
                () => this.Data.Posts.All());

            if (filter != null && filter != this.all)
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
            var endorsements = this.Cache.Get<IQueryable<EndorsementOfUser>>(
                "PostsListing",
                () => this.Data.EndorsementsOfUsers.All());

            if (userID == null)
            {
                throw new ArgumentNullException("You must specify the user by ID");
            }

            endorsements = endorsements.Where(p => p.EndorsedUserID == userID);

            return endorsements;
        }

        public IQueryable<string> GetLetters()
        {
            var letters = this.letters;

            return letters;
        }

        public IQueryable<string> GetFeilds()
        {
            var fields = this.Cache.Get<IQueryable<string>>(
                "FieldsListing",
                () => this.Data.FieldsOfExpertise.All().Select(f => f.Name));

            return fields;
        }
    }
}