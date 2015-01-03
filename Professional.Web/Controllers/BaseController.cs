namespace Professional.Web.Controllers
{
    using System.Data.Entity;
    using System.Web.Mvc;

    using Professional.Data;

    public class BaseController : Controller
    {
        protected IApplicationData data;

        public BaseController(IApplicationData data)
        {
            this.data = data;
        }

        [NonAction]
        protected void ManipulateEntity(object dbModel, EntityState state)
        {
            var entry = this.data.Context.Entry(dbModel);
            entry.State = state;
            this.data.SaveChanges();
        }
    }
}