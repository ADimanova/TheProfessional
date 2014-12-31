using Professional.Models;
using Professional.Web.Infrastructure.Mappings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Professional.Web.Models.InputViewModels
{
    public class EndorsementInputModel
    {
        [Required]
        [Range(1, 10)]
        public int Value { get; set; }
        public string Comment { get; set; }
        public string EndorsedID { get; set; }
        public string EndorsingUserID { get; set; }
        public string EndorseAction { get; set; }

        public EndorsementOfUser ToEndorsementOfUser()
        {
            return new EndorsementOfUser
            {
                Value = this.Value,
                Comment = this.Comment,
                EndorsedUserID = this.EndorsedID,
                EndorsingUserID = this.EndorsingUserID,
            };
        }

        public EndorsementOfPost ToEndorsementOfPost()
        {
            return new EndorsementOfPost
            {
                Value = this.Value,
                Comment = this.Comment,
                EndorsedPostID = this.EndorsedID,
                EndorsingUserID = this.EndorsingUserID,
            };
        }

    }
}