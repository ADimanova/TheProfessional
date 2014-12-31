using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Professional.Web.Areas.Admin.Models
{
    public class NotificationsAdminModel
    {
        public IEnumerable<NotificationAdminModel> Notifications;
        public NotificationAdminModel SelectedNotification;
    }
}