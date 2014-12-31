using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Professional.Web.Areas.KendoUIAdmin.Models
{
    public class FieldInputModel
    {
        [Required]
        public string Name { get; set; }
    }
}