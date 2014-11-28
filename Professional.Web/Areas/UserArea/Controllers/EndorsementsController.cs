using Professional.Data;
using Professional.Models;
using Professional.Web.Areas.UserArea.Models.InputModels;
using System;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Professional.Web.Models.InputViewModels;

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
        public ActionResult Create(EndorsementInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool success = false;
            if (model.IsOfUser)
            {
                success = EndorseUser(model);
            }
            else
            {
                success = EndorsePost(model);
            }

            if (success)
            {
                return RedirectToAction("Index", "Home", new { Area = "" });
            }
            else
            {
                // Implement better error handling
                return View("Error");
            }
        }

        private bool EndorseUser(EndorsementInputModel model) 
        {
            var newUserEndorsement = new EndorsementOfUser();
            newUserEndorsement.Value = model.Value;
            newUserEndorsement.Comment = model.Comment;
            newUserEndorsement.EndorsedUserID = model.EndorsedID;
            newUserEndorsement.EndorsingUserID = User.Identity.GetUserId();

            try
            {
                this.data.EndorsementsOfUsers.Add(newUserEndorsement);
                this.data.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool EndorsePost(EndorsementInputModel model)
        {
            var newPostEndorsement = new EndorsementOfPost();
            newPostEndorsement.Value = model.Value;
            newPostEndorsement.Comment = model.Comment;
            newPostEndorsement.EndorsedPostID = model.EndorsedID;
            newPostEndorsement.EndorsingUserID = User.Identity.GetUserId();

            try
            {
                this.data.EndorsementsOfPosts.Add(newPostEndorsement);
                this.data.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}