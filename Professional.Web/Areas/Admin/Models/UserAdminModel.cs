using Professional.Models;
using Professional.Web.Infrastructure.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Professional.Web.Areas.Admin.Models
{
    public class UserAdminModel : AdministrationViewModel, IMapFrom<User>
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAdmin { get; set; }
    }
}