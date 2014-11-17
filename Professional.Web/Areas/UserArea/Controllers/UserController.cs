using Professional.Common;
using Professional.Data;
using Professional.Models;
using Professional.Web.Controllers;
using Professional.Web.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Professional.Web.Areas.UserArea.Controllers
{
    [Authorize]
    public abstract class UserController : BaseController
    {
        public UserController(IApplicationData data)
            : base(data)
        {

        }
        protected IList GetUserFields(string currentUserId)
        {
            if (currentUserId == null)
            {
                //TODO:
                return new List<FieldOfExpertise>();
            }
            var fields = this.data.Users.All()
            .FirstOrDefault(u => u.Id == currentUserId)
            .FieldsOfExpertise.ToList();

            return fields;
        }

        protected IQueryable<Post> GetAllPosts()
        {
            var post = this.data.Posts.All()
                .OrderBy(p => p.Title);

            return post;
        }

        protected IQueryable<Post> GetAllPostsOfUser(string currentUserId)
        {
            var post = this.data.Posts.All()
                .Where(p => p.CreatorID == currentUserId)
                .OrderBy(p => p.Title);

            return post;
        }
        protected IQueryable<Post> GetRecentPosts(string currentUserId)
        {
            var recentPost = this.data.Posts.All()
                .Where(p => p.CreatorID == currentUserId)
                .OrderBy(p => p.DateCreated)
                .Take(WebConstants.ListPanelCount);

            return recentPost;
        }
        protected IQueryable<Post> GetTopPosts(string currentUserId)
        {
            var recentPost = this.data.Posts.All()
                .Where(p => p.CreatorID == currentUserId)
                .OrderBy(p => p.Rank)
                .Take(WebConstants.ListPanelCount);

            return recentPost;
        }

    }
}