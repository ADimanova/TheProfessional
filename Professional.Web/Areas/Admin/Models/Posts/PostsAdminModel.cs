using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Professional.Web.Areas.Admin.Models
{
    public class PostsAdminModel
    {
        public IEnumerable<PostAdminModel> Posts;
        public PostAdminModel SelectedPost;
    }
}