using Professional.Common;
using Professional.Web.Controllers;
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
        protected IList GetUserFields(string currentUserId)
        {
            var fields = this.data.Users.All()
            .FirstOrDefault(u => u.Id == currentUserId)
            .FieldsOfExpertise.ToList();

            return fields;
        }

        protected IList GetRecentPosts(string currentUserId)
        {
            var recentPost = this.data.Posts.All()
                .Where(p => p.CreatorID == currentUserId)
                .OrderBy(p => p.DateCreated)
                .Take(GlobalConstants.ListPanelCount)
                .ToList();

            return recentPost;
        }
        protected IList GetTopPosts(string currentUserId)
        {
            var recentPost = this.data.Posts.All()
                .Where(p => p.CreatorID == currentUserId)
                .OrderBy(p => p.Rank)
                .Take(GlobalConstants.ListPanelCount)
                .ToList();

            return recentPost;
        }

    }
}