using Professional.Web.Areas.UserArea.Models.InputModels;
using Professional.Web.Models;
using Professional.Web.Models.InputViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Professional.Web.Areas.UserArea.Models
{
    public class PublicProfileViewModel
    {
        public UserViewModel UserInfo { get; set; }
        public ChatViewModel ChatInfo { get; set; }
        public EndorsementInputModel EndorseFunctionality { get; set; }
        public NavigationItem BtnNavigatePosts { get; set; }
        public NavigationItem BtnNavigateEndorsements { get; set; }
        public ListPanelViewModel TopPostsList { get; set; }
        public ListPanelViewModel RecentPostsList { get; set; }
    }
}