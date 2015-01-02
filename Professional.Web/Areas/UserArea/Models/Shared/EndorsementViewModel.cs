using Professional.Web.Infrastructure.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Professional.Web.Areas.UserArea.Models.ListingViewModels
{
    public class EndorsementViewModel
    {
        public int ID { get; set; }
        public string Comment { get; set; }
        public int Value { get; set; }
        public string EndorsingUserID { get; set; }
        public string EndorsingUserName { get; set; }
    }
}