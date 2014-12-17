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
        public bool IsMessaged { get; set; }
        public bool IsNotified { get; set; }
        public bool IsUpdated { get; set; }
        public IEnumerable<MessagesViewModel> ActiveChats { get; set; }
    }
}