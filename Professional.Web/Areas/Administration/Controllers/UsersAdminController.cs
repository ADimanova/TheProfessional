using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Kendo.Mvc.Extensions;
using Professional.Web.Models;
using Kendo.Mvc.UI;

namespace Professional.Web.Areas.Administration.Controllers
{
    public class UsersAdminController : AdminController
    {
        // GET: Administration/UsersAdmin
        public ActionResult Index([DataSourceRequest]DataSourceRequest request)
        {
            return View();
        }

        public JsonResult Read_Users([DataSourceRequest]DataSourceRequest request)
        {
            var users = this.data.Users.All()
                .OrderByDescending(u => u.FirstName)
                .ThenByDescending(u => u.LastName)
                .Project().To<UserSimpleViewModel>()
                .ToDataSourceResult(request); // allows paging, sorting, etc.

            return Json(users, JsonRequestBehavior.AllowGet);
        }
    }
}