namespace Professional.Web.Areas.UserArea.Models.Profile.Private
{
    using System.Collections.Generic;

    public class UpdatesViewModel
    {
        public bool HasNewMessages { get; set; }

        public bool HasNewConnection { get; set; }

        public bool HasNewNotifications { get; set; }

        public AddMassagesViewModel ActiveChats { get; set; }

        public IEnumerable<ConnectionViewModel> ConnectionRequests { get; set; }

        public IEnumerable<NotificationShortViewModel> Notifications { get; set; }
    }
}