using Professional.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Professional.Data;
using System.Net;
using Professional.Web.Helpers;

namespace Professional.Web.Areas.UserArea.Controllers
{
    public class UpdatesController : UserController
    {
        public UpdatesController(IApplicationData data)
            : base(data)
        {
        }
        // GET: UserArea/Updates
        public ActionResult Connect(string id)
        {
            var user = this.data.Users.GetById(id);
            if (user == null)
	        {
		        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
	        }

            var newConnection = new Connection();
            newConnection.FirstUserId = this.User.Identity.GetUserId();
            newConnection.SecondUserId = id;

            try
            {
                this.data.Connections.Add(newConnection);
                this.data.SaveChanges();
                return RedirectToAction("Public", "Profile", new { Area = WebConstants.UserArea, id = id });
            }
            catch
            {
                return View("Error");
            }
        }

        public ActionResult DeclineUser(string id)
        {
            var user = this.data.Users.GetById(id);
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var loggedUserId = this.User.Identity.GetUserId();
            var connection = this.data.Connections.All()
                .FirstOrDefault(c => ((c.FirstUserId == id || c.SecondUserId == id) &&
                (c.FirstUserId == loggedUserId || c.SecondUserId == loggedUserId) &&
                id != loggedUserId));

            try
            {
                this.data.Connections.Delete(connection);
                this.data.SaveChanges();
                return RedirectToAction("Public", "Profile", new { Area = WebConstants.UserArea, id = id });
            }
            catch
            {
                return View("Error");
            }
        }

        public ActionResult AcceptConnection(int id)
        {
            var connection = this.data.Connections.GetById(id);
            var loggedUserId = this.User.Identity.GetUserId();
            var isAuthorised = loggedUserId == connection.FirstUserId || loggedUserId == connection.SecondUserId;
            if (connection == null || !isAuthorised)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            connection.IsAccepted = true;

            try
            {
                this.data.SaveChanges();
                return RedirectToAction("Private", "Profile", new { Area = "UserArea" });
            }
            catch
            {
                return View("Error");
            }
        }

        public ActionResult DeclineConnection(int id)
        {
            var connection = this.data.Connections.GetById(id);
            var loggedUserId = this.User.Identity.GetUserId();
            var isAuthorised = loggedUserId == connection.FirstUserId || loggedUserId == connection.SecondUserId;
            if (connection == null || !isAuthorised)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                this.data.Connections.Delete(connection);
                this.data.SaveChanges();
                return RedirectToAction("Private", "Profile", new { Area = "UserArea" });
            }
            catch
            {
                return View("Error");
            }
        }
    }
}