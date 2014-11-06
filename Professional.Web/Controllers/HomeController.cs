using Professional.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Professional.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HomeDesign()
        {
            return View();
        }

        public ActionResult About()
        {
            this.data.FieldsOfExpertise.Add(
            new FieldOfExpertise
            {
                Name = "Fishing"
            });

            this.data.SaveChanges();

            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}