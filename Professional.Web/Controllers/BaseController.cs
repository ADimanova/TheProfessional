namespace Professional.Web.Controllers
{
    using System.Data.Entity;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;

    using Professional.Data;
    using Professional.Models;

    public abstract class BaseController : Controller
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

        [NonAction]
        public User GetUser(string userId)
        {
            var user = this.data.Users.GetById(userId);

            return user;
        }

        [NonAction]
        public string GetLoggedUserId()
        {
            var userId = this.User.Identity.GetUserId();

            return userId;
        }

        [NonAction]
        public User GetLoggedUser()
        {
            var userId = this.User.Identity.GetUserId();
            var user = this.data.Users.GetById(userId);

            return user;
        }
    }
}