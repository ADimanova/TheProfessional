using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Professional.Web.Areas.UserArea.Controllers
{
    public class ProfileController : Controller
    {
        // GET: UserArea/Profile
        public ActionResult Index()
        {
            return View();
        }
    }
}