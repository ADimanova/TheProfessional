using Professional.Models;
using Professional.Web.Infrastructure.Mappings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Professional.Web.Models;

namespace Professional.Web.Areas.UserArea.Models.InputModels
{
    public class UserEndorsementInputModel : IMapFrom<EndorsementOfUser>
    {
        [Required]
        [Range(1, 10)]
        public int Value { get; set; }
        public string Comment { get; set; }
        public string EndorsedUserID { get; set; }
    }
}