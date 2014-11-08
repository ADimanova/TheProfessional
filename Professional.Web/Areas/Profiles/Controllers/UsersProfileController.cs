using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Professional.Web.Areas.Profiles.Controllers
{
    public class UsersProfileController : Controller
    {
        // GET: Profiles/UsersProfile
        public ActionResult UserPublicView()
        {
            return View();
        }
    }
}