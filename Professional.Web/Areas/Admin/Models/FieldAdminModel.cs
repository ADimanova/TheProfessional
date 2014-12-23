using Professional.Models;
using Professional.Web.Infrastructure.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Professional.Web.Areas.Admin.Models
{
    public class FieldAdminModel : AdministrationViewModel, IMapFrom<FieldOfExpertise>
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Rank { get; set; }
    }
}