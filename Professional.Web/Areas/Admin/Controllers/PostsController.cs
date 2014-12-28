using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Professional.Web.Areas.Admin.Models;
using Professional.Data;
using System.Net;
using AutoMapper;
using Professional.Web.Infrastructure.HtmlSanitise;

namespace Professional.Web.Areas.Admin.Controllers
{
    public class PostsController : AdminController
    {
        private readonly ISanitiser sanitizer;
        public PostsController(IApplicationData data, ISanitiser sanitizer)
            :base(data)
        {
            this.sanitizer = sanitizer;
        }

        public ActionResult PostsAdmin()
        {
            var posts = this.data.Posts.All()
                .OrderBy(p => p.CreatedOn)
               .Project().To<PostAdminModel>();

            var model = new PostsAdminModel();
            model.Posts = posts.ToList();
            model.SelectedPost = new PostAdminModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int? id, string title, string content)
        {
            if (ModelState.IsValid)
            {
                var post = this.data.Posts.GetById(id);
                if (post == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                post.Title = this.sanitizer.Sanitize(title);
                post.Content = this.sanitizer.Sanitize(content);

                try
                {
                    this.data.Posts.Update(post);
                    this.data.SaveChanges();
                }
                catch
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                return RedirectToAction("PostsAdmin", null, new { Area = "Admin" });
            }

            return View("Error", "Input is not valid");
        }

        public ActionResult Delete(int? id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    this.data.Posts.Delete(id);
                    this.data.SaveChanges();
                }
                catch
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                return RedirectToAction("PostsAdmin", null, new { Area = "Admin" });
            }

            return View("Error", "Input is not valid");
        }

        public ActionResult GetForEditing(int? id)
        {
            if (ModelState.IsValid)
            {
                var post = this.data.Posts.GetById(id);
                if (post == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var model = Mapper.Map<PostAdminModel>(post);

                return this.PartialView("~/Areas/Admin/Views/Shared/Partials/_AdminPost.cshtml", model);
            }

            return View("Error", "Input is not valid");
        }
    }
}