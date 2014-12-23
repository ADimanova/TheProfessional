using Professional.Common;
using Professional.Data;
using Professional.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Professional.Web.Areas.KendoUIAdmin.Controllers
{
    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public abstract class AdminController : BaseController
    {
        public AdminController(IApplicationData data)
            : base(data)
        {
        }
    }
}