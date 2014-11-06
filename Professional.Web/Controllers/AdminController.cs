using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Professional.Web.Controllers
{
    public class AdminController : BaseController
    {
        public ActionResult AdminPanel()
        {
            return View();
        }
    }
}