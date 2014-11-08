using Microsoft.AspNet.Identity;
using Professional.Models;
using Professional.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Professional.Web.Controllers
{
    [Authorize]
    public class PostViewsDesignController : BaseController
    {
        public ActionResult UserEndorsements()
        {
            return View();
        }
        public ActionResult CompanyEndorsements()
        {
            return View();
        }

        public ActionResult UserPosts()
        {
            return View();
        }

        // GET CreatePost
        [HttpGet]
        public ActionResult CreatePostView()
        {
            return View();
        }

        // Post CreatePost
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePostView(PostViewModel model)
        {
            if (ModelState.IsValid)
            {
                var creator = User.Identity.GetUserId();

                var newPost = new Post
                {
                    Title = model.Title,
                    DateCreated = DateTime.Now.ToUniversalTime(),
                    Content = model.Content,
                    CreatorID = creator
                };

                try
                {
                    this.data.Posts.Add(newPost);
                    this.data.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                catch
                {
                    // Implement better error handling
                    return View("Error");
                }                
            }

            // Something failed, redisplay form
            return View(model);
        }
    }
}