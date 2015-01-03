namespace Professional.Web.Areas.UserArea.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;

    using Professional.Data;
    using Professional.Models;
    using Professional.Web.Controllers;

    [Authorize]
    public abstract class UserController : BaseController
    {
        public UserController(IApplicationData data)
            : base(data)
        {
        }

        public User GetUser(string userId)
        {
            var user = this.data.Users.GetById(userId);

            return user;
        }

        public string GetLoggedUserId()
        {
            var userId = this.User.Identity.GetUserId();

            return userId;
        }

        public User GetLoggedUser()
        {
            var userId = this.User.Identity.GetUserId();
            var user = this.data.Users.GetById(userId);

            return user;
        }
    }
}