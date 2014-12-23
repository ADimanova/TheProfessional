using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Professional.Web.Areas.KendoUIAdmin.Models
{
    public abstract class AdministrationViewModel
    {
        [Display(Name = "Added on")]
        [HiddenInput(DisplayValue = false)]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Changed on")]
        [HiddenInput(DisplayValue = false)]
        public DateTime? ModifiedOn { get; set; }
    }
}