using Professional.Web.Areas.UserArea.Models.ListingViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Professional.Web.Areas.UserArea.Models
{
    public class PrivateProInfoViewModel
    {
        public ShortListingViewModel OccupationsListing { get; set; }
        public ShortListingViewModel FieldsListing { get; set; }
    }
}