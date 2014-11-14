using Professional.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Professional.Web.Areas.UserArea.Models
{
    public class PublicProfileViewModel
    {
        public UserViewModel UserInfo { get; set; }
        public NavigationItem BtnNavigatePosts { get; set; }
        public NavigationItem BtnNavigateEndorsements { get; set; }
        public ListPanelViewModel TopPostsList { get; set; }
        public ListPanelViewModel RecentPostsList { get; set; }

    }
}