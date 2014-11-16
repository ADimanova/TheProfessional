using AutoMapper;
using AutoMapper.QueryableExtensions;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Professional.Data;
using Professional.Models;
using Professional.Web.Areas.Administration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Professional.Web.Areas.Administration.Controllers
{
    public class FieldsAdminController : AdminController
    {
        public FieldsAdminController(IApplicationData data)
            : base(data)
        {

        }
        // GET: Administration/FieldsAdmin
        public ActionResult Index()
        {
            var fields = this.GetFields();

            return View(fields);
        }

        public ActionResult Editing_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(this.GetFields().ToDataSourceResult(request));
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Editing_Create([DataSourceRequest] DataSourceRequest request, FieldViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var dbField = new FieldOfExpertise();
                dbField.Name = model.Name;
                dbField.Rank = model.Rank;

                try
                {
                    this.data.FieldsOfExpertise.Add(dbField);
                    this.data.SaveChanges();
                }
                catch
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }

            var afterAddingFilds = this.GetFields();
            return Json(afterAddingFilds.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult Editing_Update([DataSourceRequest] DataSourceRequest request, FieldViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var dbField = this.data.FieldsOfExpertise.GetById(model.ID);
                if (dbField == null)
	            {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
	            }

                Mapper.Map<FieldViewModel, FieldOfExpertise>(model, dbField);

                try
                {
                    this.data.FieldsOfExpertise.Update(dbField);
                    this.data.SaveChanges();
                }
                catch
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [HttpPost]
        public ActionResult Editing_Destroy([DataSourceRequest] DataSourceRequest request, FieldViewModel model)
        {
            if (model != null)
            {
                var dbField = this.data.FieldsOfExpertise.GetById(model.ID);

                if (dbField == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                try
                {
                    this.data.FieldsOfExpertise.Delete(dbField.ID);
                    this.data.SaveChanges();
                }
                catch
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }

        private IQueryable<FieldViewModel> GetFields()
        {
            var fields = this.data.FieldsOfExpertise.All()
            .Project().To<FieldViewModel>();

            return fields;
        }
    }
}