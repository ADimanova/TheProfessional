using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Professional.Web.Areas.UserArea.Models
{
    public class ContactViewModel
    {
        public string FromUserId { get; set; }
        public string FromUserName { get; set; }

        public bool IsConnected { get; set; }
        public bool IsAccepted { get; set; }
    }
}