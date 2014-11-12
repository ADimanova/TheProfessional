using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Kendo.Mvc.Extensions;
using Professional.Web.Models;
using Kendo.Mvc.UI;
using Professional.Data;
using Professional.Web.Areas.Administration.Models;
using Professional.Models;
using AutoMapper;

namespace Professional.Web.Areas.Administration.Controllers
{
    public class UsersAdminController : AdminController
    {
        public UsersAdminController(IApplicationData data)
            :base(data)
        {
        }
        // GET: Administration/UsersAdmin
        public ActionResult Index([DataSourceRequest]DataSourceRequest request)
        {
            return View();
        }

        public JsonResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var users = this.data.Users.All()
                .OrderByDescending(u => u.FirstName)
                .ThenByDescending(u => u.LastName)
                .Project().To<UserAdminModel>()
                .ToDataSourceResult(request); // allows paging, sorting, etc.

            return Json(users, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public ActionResult Create([DataSourceRequest]DataSourceRequest request, UserAdminModel model)
        //{
        //    if (model != null && ModelState.IsValid)
        //    {
        //        var dbModel = Mapper.Map<User>(model);
        //        this.data.Users.Update(dbModel);
        //        model.Id = dbModel.Id;
        //    }

        //    return Json(new[] { model }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        //}

        //protected ActionResult Update([DataSourceRequest]DataSourceRequest request, UserAdminModel model)
        //{
        //    if (model != null && ModelState.IsValid)
        //    {
        //        var dbModel = this.data.Users.All().FirstOrDefault(u => u.Id == model.Id);
        //        Mapper.Map<UserAdminModel, User>(model, dbModel);
        //        this.data.Users.Update(dbModel);
        //    }

        //    return Json(new[] { model }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, UserAdminModel model)
        //{
        //    if (model != null && ModelState.IsValid)
        //    {
        //        var dbModel = this.data.Users.All().FirstOrDefault(u => u.Id == model.Id);
        //        Mapper.Map<UserAdminModel, User>(model, dbModel);

        //        this.data.Users.Delete(dbModel);
        //        this.data.SaveChanges();
        //    }

        //    return Json(new[] { model }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        //}
    }
}