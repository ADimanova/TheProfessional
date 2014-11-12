using AutoMapper;
using Professional.Models;
using Professional.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Professional.Web.Controllers
{
    public class PostDisplayController : BaseController
    {
        // GET: PostDisplay/Post
        public ActionResult Post(int id)
        {
            var post = this.data.Posts.All()
                .Where(p => p.ID == id)
                .FirstOrDefault();

            if (post == null)
            {
                return this.HttpNotFound("This post does not exist");
            }

            Mapper.CreateMap<Post, PostViewModel>();
            var postInfoForView = Mapper.Map<PostViewModel>(post);

            return View(postInfoForView);
        }
    }
}