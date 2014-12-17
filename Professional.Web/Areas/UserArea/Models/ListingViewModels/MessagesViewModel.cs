using Professional.Web.Models.DatabaseViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Professional.Web.Areas.UserArea.Models.ListingViewModels
{
    public class MessagesViewModel
    {
        public string FromUserId { get; set; }
        public string FromUserName { get; set; }
        public string Preview { get; set; }
        //public IEnumerable<string> Messages { get; set; }
    }
}