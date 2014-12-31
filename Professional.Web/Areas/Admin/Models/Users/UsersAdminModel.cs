using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Professional.Web.Areas.Admin.Models
{
    public class UsersAdminModel
    {
        public IEnumerable<UserAdminModel> Users;
        public UserDetailedAdminModel SelectedUser;
    }
}