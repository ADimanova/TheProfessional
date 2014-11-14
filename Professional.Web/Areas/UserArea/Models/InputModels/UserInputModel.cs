using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Professional.Web.Areas.UserArea.Models.InputModels
{
    public class UserInputModel
    {

        [UIHint("DateTime")]
        public DateTime DateOfBirth { get; set; }

        [UIHint("BoolCheckbox")]
        public bool? IsMale { get; set; }

        [UIHint("TextArea")]
        public string PersonalHistory { get; set; }
    }
}