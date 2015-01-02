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

    public class AddInfoController : UserController
    {
        public AddInfoController(IApplicationData data)
            : base(data)
        {
        }

        public ActionResult OnRegistration()
        {
            this.GetInfoNavigation(WebConstants.AddPersonalInfoPageRoute);

            return View();
        }

        [HttpGet]
        public ActionResult Personal()
        {
            this.GetInfoNavigation(WebConstants.AddProfessionalInfoPageRoute);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Personal(UserInputModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var user = this.data.Users.GetById(userId);

                user.PersonalHistory = model.PersonalHistory;
                if (user.IsMale != null)
                {
                    user.IsMale = model.IsMale;                    
                }

                user.DateOfBirth = model.DateOfBirth;

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
                    return RedirectToAction("Professional", "AddInfo", new { Area = WebConstants.UserArea });
                }
                catch
                {
                    // Implement better error handling
                    return View("Error");
                }
            }

            // Something failed, redisplay form
            return View(model);
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
            var model =  new UserInputModel();
            model.Occupations = new List<string>();
            model.Fields = new List<string>();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Professional(UserInputModel model)
        {
            var userId = User.Identity.GetUserId();
            var user = this.data.Users.GetById(userId);

            if (model.Occupations != null)
            {
                var occupations = model.Occupations.ToList<string>();
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
                var fields = model.Fields.ToList<string>();
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
                // Implement better error handling
                return View("Error");
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