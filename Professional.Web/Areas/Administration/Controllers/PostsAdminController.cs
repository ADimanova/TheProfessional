using AutoMapper.QueryableExtensions;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Professional.Data;
using Professional.Models;
using Professional.Web.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model = Professional.Models.Post;
using ViewModel = Professional.Web.Areas.Administration.Models.PostAdminModel;

namespace Professional.Web.Areas.Administration.Controllers
{
    public class PostsAdminController : KendoGridAdministrationController
    {
        public PostsAdminController(IApplicationData data)
            :base(data)
        {
        }

        // GET: Administration/PostsAdmin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var posts = this.GetData();

            return Json(posts.ToDataSourceResult(request).Data, JsonRequestBehavior.AllowGet);
        }

        protected override IEnumerable GetData()
        {
            return this.data.Posts.All()
                .Project().To<ViewModel>();
        }

        protected override T GetById<T>(object id)
        {
            return this.data.Posts.GetById(id) as T;
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            base.Update<Model, ViewModel>(model, model.ID);
            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                this.data.Posts.Delete(model.ID);
                this.data.SaveChanges();
            }

            return this.GridOperation(model, request);
        }
    }
}