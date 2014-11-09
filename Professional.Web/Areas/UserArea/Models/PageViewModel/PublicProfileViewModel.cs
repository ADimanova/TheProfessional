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
        public string BtnNavigatePosts { get; set; }
        public string BtnNavigateEndorsements { get; set; }
        public ListPanelViewModel TopPostsList { get; set; }
        public ListPanelViewModel RecentPostsList { get; set; }

    }
}