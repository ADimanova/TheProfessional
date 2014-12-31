using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Professional.Web.Models;
using Professional.Web.Areas.UserArea.Models.ListingViewModels;

namespace Professional.Web.Areas.UserArea.Models
{
    public class PrivateProfileViewModel
    {
        public UserViewModel UserInfo { get; set; }
        public UpdatesViewModel UpdatesInfo { get; set; }
        public HorizontalNavbarViewModel NavigationList { get; set; }
        public PrivateProInfoViewModel ProInfo { get; set; }
    }
}