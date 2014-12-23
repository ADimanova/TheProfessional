using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Professional.Web.Models;
using Professional.Data;
using Professional.Web.Areas.KendoUIAdmin.Models;
using Professional.Models;
using AutoMapper;

using Model = Professional.Models.User;
using ViewModel = Professional.Web.Areas.KendoUIAdmin.Models.UserAdminModel;
using Professional.Web.Helpers;
using Professional.Common;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace Professional.Web.Areas.KendoUIAdmin.Controllers
{
    public class UsersAdminController : KendoGridAdministrationController
    {
        private UserManager<User> userManager;

        public UsersAdminController(IApplicationData data)
            :base(data)
        {
            this.userManager = new UserManager<User>(new UserStore<User>(this.data.Context.DbContext));
        }

        // GET: Administration/UsersAdmin
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var users = this.GetData();

            return Json(users.ToDataSourceResult(request).Data, JsonRequestBehavior.AllowGet);
        }

        protected override System.Collections.IEnumerable GetData()
        {
            return this.data.Users.All()
                .Project().To<UserAdminModel>();
        }

        protected override T GetById<T>(object id)
        {
            return this.data.Users.GetById(id) as T;
        }

        [HttpGet]
        public ActionResult MakeAdmin(string id)
        {
            // Won't add user twice to role
            try
            {
                this.userManager.AddToRole(id, GlobalConstants.AdministratorRoleName);
                this.data.SaveChanges();

                this.TempData["success"] = "true";
            }
            catch
            {
                throw new HttpException(500, "Something went really wrong. Sorry...");
            }

            return new RedirectResult(WebConstants.AdminUsersPageRoute);
        }

        [HttpGet]
        public ActionResult RemoveAdmin(string id)
        {
            try
            {
                this.userManager.RemoveFromRole(id, GlobalConstants.AdministratorRoleName);
                this.data.SaveChanges();

                this.TempData["success"] = "false";
            }
            catch
            {
                throw new HttpException(500, "Something went really wrong. Sorry...");
            }

            return new RedirectResult(WebConstants.AdminUsersPageRoute);
        }

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
            //var roles = this.data.Users.All().FirstOrDefault().Roles
            return this.GridOperation(model, request);
        }
    }
}