using Professional.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Professional.Web.Controllers
{
    public class ProfilesDesignController : BaseController
    {
        public ProfilesDesignController(IApplicationData data)
            : base(data)
        {

        }

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