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

using Model = Professional.Models.User;
using ViewModel = Professional.Web.Areas.Administration.Models.UserAdminModel;

namespace Professional.Web.Areas.Administration.Controllers
{
    public class UsersAdminController : KendoGridAdministrationController
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
                .ToDataSourceResult(request);

            return Json(users, JsonRequestBehavior.AllowGet);
        }

        protected override System.Collections.IEnumerable GetData()
        {
            return this.data.Users.All();
        }

        protected override T GetById<T>(object id)
        {
            return this.data.Users.GetById(id) as T;
        }

        //[HttpPost]
        //public ActionResult Create([DataSourceRequest]DataSourceRequest request, ViewModel model)
        //{
        //    var dbModel = base.Create<Model>(model);
        //    if (dbModel != null) model.Id = dbModel.Id;
        //    return this.GridOperation(model, request);
        //}

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            base.Update<Model, ViewModel>(model, model.Id);
            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                this.data.Users.Delete(model.Id);
                this.data.SaveChanges();
            }

            return this.GridOperation(model, request);
        }
    }
}