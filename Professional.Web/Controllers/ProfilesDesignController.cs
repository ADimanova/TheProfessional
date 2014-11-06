using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Professional.Web.Controllers
{
    public class ProfilesDesignController : Controller
    {
        public ActionResult UserProfile()
        {
            return View();
        }

        public ActionResult CompanyProfile()
        {
            return View();
        }

        public ActionResult PublicUserProfile()
        {
            return View();
        }

        public ActionResult PublicCompanyProfile()
        {
            return View();
        }
    }
}