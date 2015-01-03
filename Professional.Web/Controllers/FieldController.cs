namespace Professional.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper;

    using Professional.Data;
    using Professional.Models;
    using Professional.Web.Models.Field;

    public class FieldController : BaseController
    {
        public FieldController(IApplicationData data)
            : base(data)
        {
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

            Mapper.CreateMap<FieldOfExpertise, FieldViewModel>();
            var fieldInfoForView = Mapper.Map<FieldViewModel>(field);

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