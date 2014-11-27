using Professional.Data;
using Professional.Models;
using Professional.Web.Areas.UserArea.Models.InputModels;
using System;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Professional.Web.Areas.UserArea.Controllers
{
    public class EndorsementsController : UserController
    {
        public EndorsementsController(IApplicationData data)
            : base(data)
        {
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // POST: UserArea/Endorsements
        public ActionResult Create(UserEndorsementInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var newUserEndorsement = new EndorsementOfUser();
            newUserEndorsement.Value = model.Value;
            newUserEndorsement.Comment = model.Comment;
            newUserEndorsement.EndorsedUserID = model.EndorsedUserID;
            newUserEndorsement.EndorsingUserID = User.Identity.GetUserId();

            try
            {
                this.data.EndorsementsOfUsers.Add(newUserEndorsement);
                this.data.SaveChanges();
                return RedirectToAction("Index", "Home", new { Area = "" });
            }
            catch
            {
                // Implement better error handling
                return View("Error");
            }
        }
    }
}