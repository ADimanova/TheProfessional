using Microsoft.AspNet.Identity;
using Professional.Data;
using Professional.Models;
using Professional.Web.Areas.UserArea.Models.InputModels;
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
        public CreateItemController(IApplicationData data)
            : base(data)
        {

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

                var newPost = new Post
                {
                    Title = model.Title,
                    DateCreated = DateTime.Now.ToUniversalTime(),
                    Content = model.Content,
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