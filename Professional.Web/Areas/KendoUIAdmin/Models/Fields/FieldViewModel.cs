using Professional.Models;
using Professional.Web.Infrastructure.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Professional.Web.Areas.KendoUIAdmin.Models
{
    public class FieldViewModel : AdministrationViewModel, IMapFrom<FieldOfExpertise>
    {
        [HiddenInput]
        public int ID { get; set; }
        public string Name { get; set; }

        public int Rank { get; set; }
    }
}