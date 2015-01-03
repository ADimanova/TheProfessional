namespace Professional.Web.Models
{
    using System.Collections.Generic;

    using Microsoft.AspNet.Identity;

    using Professional.Web.Models.Shared;
    using Professional.Web.Models.Home;

    public class IndexViewModel
    {
        public bool HasPassword { get; set; }

        public IList<UserLoginInfo> Logins { get; set; }

        public string PhoneNumber { get; set; }

        public bool TwoFactor { get; set; }

        public bool BrowserRemembered { get; set; }

        public IEnumerable<UserSimpleViewModel> Featured { get; set; }

        public IEnumerable<PostSimpleViewModel> Posts { get; set; }

        public HorizontalNavbarViewModel FieldsListing { get; set; }
    }
}