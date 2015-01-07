namespace Professional.Web.Areas.UserArea.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

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
    }
}