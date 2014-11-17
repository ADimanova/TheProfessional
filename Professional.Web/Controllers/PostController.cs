using AutoMapper;
using Professional.Data;
using Professional.Models;
using Professional.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Professional.Web.Controllers
{
    public class PostController : BaseController
    {
        public PostController(IApplicationData data)
            : base(data)
        {

        }

        // GET: Post/Info
        public ActionResult Info(int id)
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