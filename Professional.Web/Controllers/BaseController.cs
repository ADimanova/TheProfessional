using System;
using System.Linq;
using System.Web.Mvc;
using Professional.Data;
using System.Data.Entity;

namespace Professional.Web.Controllers
{
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