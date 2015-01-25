namespace Professional.Web.Areas.UserArea.Controllers
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper;

    using Professional.Data;
    using Professional.Models;
    using Professional.Web.Areas.UserArea.Models.CreateItem;
    using Professional.Web.Helpers;
    using Professional.Web.Infrastructure.HtmlSanitise;
    using Professional.Web.Infrastructure.Filters;

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

            return this.View();
        }

        // POST: UserArea/CreateItem/Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Post(PostInputModel model)
        {
            if (ModelState.IsValid)
            {
                var creator = this.GetLoggedUserId();
                var fieldId = this.data.FieldsOfExpertise.All()
                    .FirstOrDefault(f => f.Name == model.FieldName).ID;

                var sanitisedContent = this.sanitizer.Sanitize(model.Content);
                var editedTitle = StringManipulations.UppercaseFirst(model.Title);

                var newPost = new Post
                {
                    Title = editedTitle,
                    Content = sanitisedContent,
                    CreatorID = creator,
                    FieldID = fieldId
                };

                try
                {
                    this.data.Posts.Add(newPost);
                    this.data.SaveChanges();
                    return this.RedirectToAction("Index", "Home", new { Area = string.Empty });
                }
                catch
                {
                    return this.View("Error");
                }
            }
            else
            {
                ViewBag.Fields = this.data.FieldsOfExpertise
                    .All().Select(f => f.Name);
            }

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EndorsementOfPost(PostEndorsementInputModel model)
        {
            try
            {
                this.Endorse<EndorsementOfPost, PostEndorsementInputModel>(model);
                return this.RedirectToAction("Index", "Home", new { Area = string.Empty });
            }
            catch
            {
                return this.RedirectToAction("Info", "Post", new { Area = WebConstants.UserArea, id = model.EndorsedID });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EndorsementOfUser(UserEndorsementInputModel model)
        {
            try
            {
                this.Endorse<EndorsementOfUser, UserEndorsementInputModel>(model);
                return this.RedirectToAction("Index", "Home", new { Area = string.Empty });
            }
            catch
            {
                return this.RedirectToAction("Info", "Post", new { Area = WebConstants.UserArea, id = model.EndorsedID });
            }
        }

        private void Endorse<T, V>(V model)
            where T : class
            where V : EndorsementInputModel
        {
            if (!ModelState.IsValid)
            {
                throw new ArgumentException("The model is not valid");
            }

            model.EndorsingUserID = this.GetLoggedUserId();
            var newEndorsement = Mapper.Map<V, T>(model);
            this.ManipulateEntity(newEndorsement, EntityState.Added);
        }
    }
}