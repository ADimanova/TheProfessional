using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Professional.Web.Controllers
{
    public class PostViewsDesignController : Controller
    {
        public ActionResult UserEndorsements()
        {
            return View();
        }
        public ActionResult CompanyEndorsements()
        {
            return View();
        }

        public ActionResult UserPosts()
        {
            return View();
        }
    }
}