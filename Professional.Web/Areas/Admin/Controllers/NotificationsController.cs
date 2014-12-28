using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Professional.Web.Areas.Admin.Models;
using Professional.Data;
using System.Net;
using AutoMapper;
using Professional.Web.Infrastructure.HtmlSanitise;
using Professional.Models;
using Microsoft.AspNet.Identity;

namespace Professional.Web.Areas.Admin.Controllers
{
    public class NotificationsController : AdminController
    {
        private readonly ISanitiser sanitizer;
        public NotificationsController(IApplicationData data, ISanitiser sanitizer)
            :base(data)
        {
            this.sanitizer = sanitizer;
        }

        public ActionResult NotificationsAdmin()
        {
            var notifications = this.data.Notifications.All()
                .OrderBy(p => p.CreatedOn)
               .Project().To<NotificationAdminModel>();

            var model = new NotificationsAdminModel();
            model.Notifications = notifications.ToList();
            model.SelectedNotification = new NotificationAdminModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NotificationAdminModel model)
        {
            if (!ModelState.IsValid)
	        {
                return View("Error");
	        }

            var notification = new Notification();
            notification.Title = this.sanitizer.Sanitize(model.Title);
            notification.Content = this.sanitizer.Sanitize(model.Content);
            notification.CreatorID = this.User.Identity.GetUserId();

            try
            {
                this.data.Notifications.Add(notification);
                this.data.SaveChanges();
                return RedirectToAction("NotificationsAdmin", null, new { Area = "Admin" });
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int? id, string title, string content)
        {
            if (ModelState.IsValid)
            {
                var notification = this.data.Notifications.GetById(id);
                if (notification == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                notification.Title = this.sanitizer.Sanitize(title);
                notification.Content = this.sanitizer.Sanitize(content);

                try
                {
                    this.data.Notifications.Update(notification);
                    this.data.SaveChanges();
                }
                catch
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                return RedirectToAction("NotificationsAdmin", null, new { Area = "Admin" });
            }

            return View("Error");
        }

        public ActionResult Delete(int? id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    this.data.Notifications.Delete(id);
                    this.data.SaveChanges();
                }
                catch
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                return RedirectToAction("NotificationsAdmin", null, new { Area = "Admin" });
            }

            return View("Error", "Input is not valid");
        }

        public ActionResult GetForEditing(int? id)
        {
            if (ModelState.IsValid)
            {
                var notification = this.data.Notifications.GetById(id);
                if (notification == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var model = Mapper.Map<NotificationAdminModel>(notification);

                return this.PartialView("~/Areas/Admin/Views/Shared/Partials/_AdminNotification.cshtml", model);
            }

            return View("Error", "Input is not valid");
        }
    }
}