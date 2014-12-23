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

namespace Professional.Web.Areas.Admin.Controllers
{
    public class UsersController : AdminController
    {
        private readonly ISanitiser sanitizer;
        public UsersController(IApplicationData data, ISanitiser sanitizer)
            :base(data)
        {
            this.sanitizer = sanitizer;
        }

        public ActionResult UsersAdmin()
        {
            var users = this.data.Users.All()
                .Where(u => !u.IsDeleted)
                .OrderBy(p => p.LastName)
                .Project().To<UserAdminModel>();

            var model = new UsersAdminModel();
            model.Users = users.ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, string username, string email, string firstName, string lastName)
        {
            if (ModelState.IsValid)
            {
                var post = this.data.Users.GetById(id);
                if (post == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                post.UserName = this.sanitizer.Sanitize(username);
                post.Email = this.sanitizer.Sanitize(email);
                post.FirstName = this.sanitizer.Sanitize(firstName);
                post.LastName = this.sanitizer.Sanitize(lastName);

                try
                {
                    this.data.Users.Update(post);
                    this.data.SaveChanges();
                }
                catch
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                return RedirectToAction("UsersAdmin", null, new { Area = "Admin" });
            }

            return View("Error");
        }


        public ActionResult Delete(string id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    this.data.Users.Delete(id);
                    this.data.SaveChanges();
                }
                catch
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                return RedirectToAction("UsersAdmin", null, new { Area = "Admin" });
            }

            return View("Error");
        }

        public ActionResult GetForEditing(string id)
        {
            if (ModelState.IsValid)
            {
                var user = this.data.Users.GetById(id);
                if (user == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var model = Mapper.Map<UserDetailedAdminModel>(user);

                return this.PartialView("~/Areas/Admin/Views/Shared/Partials/_AdminUser.cshtml", model);
            }

            return View("Error");
        }
    }
}