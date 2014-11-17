using AutoMapper;
using Professional.Data;
using Professional.Models;
using Professional.Web.Models.DatabaseViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Professional.Web.Controllers
{
    public class FieldController : BaseController
    {
        public FieldController(IApplicationData data)
            : base(data)
        {

        }

        // GET: Field/Info
        public ActionResult Info(string id)
        {
            var field = this.data.FieldsOfExpertise.All()
                .FirstOrDefault(f => f.Name == id);

            if (field == null)
            {
                return this.HttpNotFound("This field does not exist");
            }

            Mapper.CreateMap<FieldOfExpertise, FieldViewModel>();
            var fieldInfoForView = Mapper.Map<FieldViewModel>(field);

            return View(fieldInfoForView);
        }
    }
}