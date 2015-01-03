namespace Professional.Web.Areas.UserArea.Models.AddInfo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public class UserInputModel
    {
        [UIHint("DateTime")]
        public DateTime? DateOfBirth { get; set; }

        [UIHint("BoolCheckbox")]
        public bool? IsMale { get; set; }

        [UIHint("TextArea")]
        public string PersonalHistory { get; set; }

        public bool ContinueToProfile { get; set; }

        public HttpPostedFileBase ProfileImage { get; set; }

        public IEnumerable<string> Occupations { get; set; }

        public IEnumerable<string> Fields { get; set; }
    }
}