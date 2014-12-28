using Professional.Data;
using Professional.Web.Infrastructure.HtmlSanitise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Professional.Web.Areas.Admin.Models;
using System.Net;
using AutoMapper;
using Professional.Models;

namespace Professional.Web.Areas.Admin.Controllers
{
    public class FieldsController : AdminController
    {
        private readonly ISanitiser sanitizer;
        public FieldsController(IApplicationData data, ISanitiser sanitizer)
            :base(data)
        {
            this.sanitizer = sanitizer;
        }

        public ActionResult FieldsAdmin()
        {
            var fields = this.data.FieldsOfExpertise.All()
                .OrderBy(p => p.Name)
                .Project().To<FieldAdminModel>();

            var model = new FieldsAdminModel();
            model.Fields = fields.ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FieldAdminModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }

            var field = new FieldOfExpertise();
            field.Name = this.sanitizer.Sanitize(model.Name);
            field.Rank = model.Rank;

            try
            {
                this.data.FieldsOfExpertise.Add(field);
                this.data.SaveChanges();
                return RedirectToAction("FieldsAdmin", null, new { Area = "Admin" });
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string name, int? rank)
        {
            if (ModelState.IsValid)
            {
                var field = this.data.FieldsOfExpertise.GetById((int)id);
                if (field == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                field.Name = this.sanitizer.Sanitize(name);
                field.Rank = (int)rank;

                try
                {
                    this.data.FieldsOfExpertise.Update(field);
                    this.data.SaveChanges();
                }
                catch
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                return RedirectToAction("FieldsAdmin", null, new { Area = "Admin" });
            }

            return View("Error", "Input is not valid");
        }


        public ActionResult Delete(int? id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    this.data.FieldsOfExpertise.Delete(id);
                    this.data.SaveChanges();
                }
                catch
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                return RedirectToAction("FieldsAdmin", null, new { Area = "Admin" });
            }

            return View("Error", "Input is not valid");
        }

        public ActionResult GetForEditing(int? id)
        {
            if (ModelState.IsValid)
            {
                var field = this.data.FieldsOfExpertise.GetById(id);
                if (field == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var model = Mapper.Map<FieldAdminModel>(field);

                return this.PartialView("~/Areas/Admin/Views/Shared/Partials/_AdminField.cshtml", model);
            }

            return View("Error", "Input is not valid");
        }
    }
}