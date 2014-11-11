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
            //TODO: handle if post does not exist

            var post = this.data.Posts.All()
                .First(p => p.ID == id);

            Mapper.CreateMap<Post, PostViewModel>();
            var postInfoForView = Mapper.Map<PostViewModel>(post);

            return View(postInfoForView);
        }
    }
}