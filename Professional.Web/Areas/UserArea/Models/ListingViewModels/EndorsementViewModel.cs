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
        public string Content { get; set; }
        public string AuthorID { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
    }
}