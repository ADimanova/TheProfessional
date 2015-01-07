namespace Professional.Web.Areas.UserArea.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using Professional.Data;
    using Professional.Models;
    using Professional.Web.Helpers;
    using Professional.Web.Infrastructure.Services.Contracts;

    public class UpdatesController : UserController
    {
        private IUpdatesServices updatesServices;

        public UpdatesController(IApplicationData data, IUpdatesServices updatesServices)
            : base(data)
        {
            this.updatesServices = updatesServices;
        }

        public ActionResult Connect(string id)
        {
            var user = this.GetUser(id);
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var newConnection = new Connection();
            newConnection.FirstUserId = this.GetLoggedUserId();
            newConnection.SecondUserId = id;

            try
            {
                this.data.Connections.Add(newConnection);
                this.data.SaveChanges();
                return this.RedirectToAction("Public", "Profile", new { Area = WebConstants.UserArea, id = id });
            }
            catch
            {
                return this.View("Error");
            }
        }

        public ActionResult DeclineUser(string id)
        {
            var user = this.GetUser(id);
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var loggedUserId = this.GetLoggedUserId();
            var connection = this.updatesServices.GetUsersConnection(id, loggedUserId);
            try
            {
                this.data.Connections.Delete(connection.ID);
                this.data.SaveChanges();
                return this.RedirectToAction("Public", "Profile", new { Area = WebConstants.UserArea, id = id });
            }
            catch
            {
                return this.View("Error");
            }
        }

        public ActionResult AcceptConnection(int id)
        {
            var connection = this.data.Connections.GetById(id);
            var loggedUserId = this.GetLoggedUserId();
            var isAuthorised = this.updatesServices.IsAuthorised(id, loggedUserId);
            if (connection == null || !isAuthorised)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            connection.IsAccepted = true;

            try
            {
                this.data.Connections.Update(connection);
                this.data.SaveChanges();
                var message = "Connection accepted successfully.";
                return this.PartialView("~/Views/Shared/Partials/_SuccessAlert.cshtml", message);
            }
            catch
            {
                var message = "Something went wrong. Connection was not accepted.";
                return this.PartialView("~/Views/Shared/Partials/_FailAlert.cshtml", message);
            }
        }

        public ActionResult DeclineConnection(int id)
        {
            var connection = this.data.Connections.GetById(id);
            var loggedUserId = this.GetLoggedUserId();
            var isAuthorised = loggedUserId == connection.FirstUserId || loggedUserId == connection.SecondUserId;
            if (connection == null || !isAuthorised)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                this.data.Connections.Delete(connection);
                this.data.SaveChanges();
                return this.RedirectToAction("Private", "Profile", new { Area = "UserArea" });
            }
            catch
            {
                return this.View("Error");
            }
        }
    }
}