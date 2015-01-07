namespace Professional.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper;

    using Professional.Data;
    using Professional.Models;
    using Professional.Web.Models.Field;
    using Professional.Web.Infrastructure.HtmlSanitise;
    using Professional.Web.Helpers;
    using Professional.Web.Models.Shared;
    using System.Collections.Generic;
    using System.Collections;

    public class FieldController : BaseController
    {
        private readonly ISanitiser sanitizer;
        public FieldController(IApplicationData data, ISanitiser sanitizer)
            : base(data)
        {
            this.sanitizer = sanitizer;
        }

        // GET: Field/Info
        public ActionResult Info(string id)
        {
            var field = this.data.FieldsOfExpertise.All()
                .FirstOrDefault(f => f.Name == id);

            if (field == null)
            {
                return this.HttpNotFound("This field does not exist");
            }

            var featured = this.data.EndorsementsOfPosts.All()
                .Where(e => e.EndorsedPost.FieldID == field.ID)
                .GroupBy(e => e.EndorsedPost)
                .Select(g => new
                {
                    Value = g.Average(i => i.Value),
                    HolderId = g.Key.Creator.Id,
                    Holder = g.Key.Creator.LastName + ", " + g.Key.Creator.FirstName,
                })
                .OrderByDescending(i => i.Value)
                .Take(10)
                .Select(p => new NavigationItem
                {
                    Url = WebConstants.PublicProfilePageRoute + p.HolderId,
                    Content = p.Holder
                });

            var fieldInfoForView = Mapper.Map<FieldViewModel>(field);
            if (fieldInfoForView.Name == null)
            {
                return this.View("Error");
            }

            fieldInfoForView.Name = this.sanitizer.Sanitize(fieldInfoForView.Name);
            fieldInfoForView.FieldInfo = fieldInfoForView.FieldInfo != null ?
                this.sanitizer.Sanitize(fieldInfoForView.FieldInfo) :
                WebConstants.DefaultFieldInfo;
            fieldInfoForView.Featured = new List<NavigationItem>();
            fieldInfoForView.Featured = featured.ToList();

            return this.View(fieldInfoForView);
        }

        public ActionResult FieldsListing()
        {
            var fields = this.data.FieldsOfExpertise.All()
                .OrderByDescending(f => f.Name)
                .Select(f => f.Name).ToList();

            return this.View(fields);
        }
    }
}