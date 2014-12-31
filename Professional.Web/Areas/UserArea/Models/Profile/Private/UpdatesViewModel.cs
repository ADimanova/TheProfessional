using Professional.Web.Areas.UserArea.Models.DatabaseVeiwModels;
using Professional.Web.Areas.UserArea.Models.ListingViewModels;
using Professional.Web.Models.DatabaseViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Professional.Web.Areas.UserArea.Models
{
    public class UpdatesViewModel
    {
        public bool HasNewMessages { get; set; }
        public bool HasNewConnection { get; set; }
        public bool HasNewNotifications { get; set; }
        public IEnumerable<MessageViewModel> ActiveChats { get; set; }
        public IEnumerable<ConnectionViewModel> ConnectionRequests { get; set; }
        public IEnumerable<NotificationShortViewModel> Notifications { get; set; }
    }
}