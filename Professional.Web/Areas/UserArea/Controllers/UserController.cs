using Professional.Common;
using Professional.Data;
using Professional.Models;
using Professional.Web.Controllers;
using Professional.Web.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Professional.Web.Areas.UserArea.Controllers
{
    [Authorize]
    public abstract class UserController : BaseController
    {
        public UserController(IApplicationData data)
            : base(data)
        {
        }

        public User GetUser(string currentUserId)
        {
            var user = this.data.Users.All()
                 .FirstOrDefault(u => u.Id == currentUserId);

            return user;
        }
    }
}