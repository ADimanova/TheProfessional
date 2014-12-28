using Professional.Models;
using Professional.Web.Infrastructure.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Professional.Web.Areas.Admin.Models
{
    public class NotificationAdminModel : AdministrationViewModel, IMapFrom<Notification>
    {
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }

        public string Title { get; set; }

        [AllowHtml]
        public string Content { get; set; }
    }
}