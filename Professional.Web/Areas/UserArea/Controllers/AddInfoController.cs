namespace Professional.Web.Areas.UserArea.Controllers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;

    using Professional.Data;
    using Professional.Models;
    using Professional.Web.Areas.UserArea.Models.InputModels;
    using Professional.Web.Helpers;
    using System.Linq.Expressions;
    using Antlr.Runtime.Misc;

    public class AddInfoController : UserController
    {
        public AddInfoController(IApplicationData data)
            : base(data)
        {
        }

        public ActionResult OnRegistration()
        {
            this.GetInfoNavigation(WebConstants.AddPersonalInfoPageRoute);

            return this.View();
        }

        [HttpGet]
        public ActionResult Personal()
        {
            this.GetInfoNavigation(WebConstants.AddProfessionalInfoPageRoute);

            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Personal(UserInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = User.Identity.GetUserId();
            var user = this.data.Users.GetById(userId);

            if (model.PersonalHistory != null)
            {
                user.PersonalHistory = model.PersonalHistory;
            }

            if (model.IsMale != null)
            {
                user.IsMale = model.IsMale;                    
            }

            if (model.DateOfBirth != null)
            {
                user.DateOfBirth = model.DateOfBirth;
            }

            if (model.ProfileImage != null)
            {
                using (var memory = new MemoryStream())
                {
                    model.ProfileImage.InputStream.CopyTo(memory);
                    var content = memory.GetBuffer();

                    user.ProfileImage = new Image
                    {
                        Content = content,
                        FileExtension = model.ProfileImage.FileName.Split(new[] { '.' }).Last()
                    };
                }
            }

            try
            {
                this.data.Users.Update(user);
                this.data.SaveChanges();
                return this.RedirectToAction("Professional", "AddInfo", new { Area = WebConstants.UserArea });
            }
            catch
            {
                return this.View(model);
            }
        }

        [HttpGet]
        public ActionResult Professional()
        {      
            var occupations = this.data.Occupations.All()
                .Select(o => new
                {
                    Text = o.Title,
                    Value = o.Title
                });

            var fields = this.data.FieldsOfExpertise.All()
                .Select(o => new
                {
                    Text = o.Name,
                    Value = o.Name
                });

            ViewBag.Occupations = occupations.ToList();
            ViewBag.Fields = fields.ToList();

            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Professional(UserInputModel model)
        {
            var userId = User.Identity.GetUserId();
            var user = this.data.Users.GetById(userId);

            //TODO: Further refactor
            if (model.Occupations != null)
            {
                var occupations = model.Occupations.ToList();
                Occupation foundOccupation;
                for (int i = 0; i < occupations.Count; i++)
                {
                    var occupation = occupations[i];
                    foundOccupation = this.data.Occupations.All().FirstOrDefault(o => o.Title == occupation);
                    if (foundOccupation != null)
                    {
                        var found = user.Occupations.FirstOrDefault(o => o.Title == foundOccupation.Title);
                        if (found == null)
                        {
                            user.Occupations.Add(foundOccupation);
                        }
                    }
                }
            }

            if (model.Fields != null)
            {
                var fields = model.Fields.ToList();
                FieldOfExpertise foundField;
                for (int i = 0; i < fields.Count; i++)
                {
                    var field = fields[i];
                    foundField = this.data.FieldsOfExpertise.All().FirstOrDefault(o => o.Name == field);
                    if (foundField != null)
                    {
                        var found = user.FieldsOfExpertise.FirstOrDefault(o => o.Name == foundField.Name);
                        if (found == null)
                        {
                            user.FieldsOfExpertise.Add(foundField);
                        }
                    }
                }
            }
            
            try
            {
                this.data.SaveChanges();
                return this.RedirectToAction("Private", "Profile", new { Area = WebConstants.UserArea });
            }
            catch
            {
                return this.View(model);
            }
        }

        private void GetInfoNavigation(string nextPath)
        {
            var userId = User.Identity.GetUserId();
            var profilePath = WebConstants.PrivateProfilePageRoute + userId;
            ViewBag.Profile = profilePath;
            ViewBag.AddInfo = nextPath;
        }
    }
}