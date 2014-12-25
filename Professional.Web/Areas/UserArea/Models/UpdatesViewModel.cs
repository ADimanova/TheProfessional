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
        public bool IsMessaged { get; set; }
        public bool HasNewConnection { get; set; }
        public bool IsUpdated { get; set; }
        public IEnumerable<MessagesViewModel> ActiveChats { get; set; }
        public IEnumerable<ConnectionViewModel> ConnectionRequests { get; set; }
    }
}