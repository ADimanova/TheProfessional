using Microsoft.AspNet.Identity;
using Professional.Data;
using Professional.Models;
using Professional.Web.Areas.UserArea.Models.InputModels;
using Professional.Web.Helpers;
using Professional.Web.Infrastructure.HtmlSanitise;
using Professional.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Professional.Web.Areas.UserArea.Controllers
{
    public class CreateItemController : UserController
    {
        private readonly ISanitiser sanitizer;
        public CreateItemController(IApplicationData data, ISanitiser sanitizer)
            : base(data)
        {
            this.sanitizer = sanitizer;
        }

        // GET: UserArea/CreateItem/Post
        [HttpGet]
        public ActionResult Post()
        {
            ViewBag.Fields = this.data.FieldsOfExpertise
                .All().Select(f => f.Name);
            return View();
        }

        // POST: UserArea/CreateItem/Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Post(PostInputModel model)
        {
            if (ModelState.IsValid)
            {
                var creator = User.Identity.GetUserId();
                var fieldId = this.data.FieldsOfExpertise.All()
                    .FirstOrDefault(f => f.Name == model.FieldName).ID;

                var sanitisedContent = sanitizer.Sanitize(model.Content);
                var editedTitle = StringManipulations.UppercaseFirst(model.Title);

                var newPost = new Post
                {
                    Title = editedTitle,
                    DateCreated = DateTime.Now.ToUniversalTime(),
                    Content = sanitisedContent,
                    CreatorID = creator,
                    FieldID = fieldId
                };

                try
                {
                    this.data.Posts.Add(newPost);
                    this.data.SaveChanges();
                    return RedirectToAction("Index", "Home", new { Area = "" });
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