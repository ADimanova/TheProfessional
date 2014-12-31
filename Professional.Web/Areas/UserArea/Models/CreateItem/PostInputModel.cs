using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Professional.Web.Areas.UserArea.Models.InputModels
{
    public class PostInputModel
    {
        // TODO: Create tags
        [UIHint("TextInput")]
        public string Title { get; set; }

        [Display(Name = "Created on")]
        public DateTime DateCreated { get; set; }

        [AllowHtml]
        [UIHint("tinymce_classic")]
        public string Content { get; set; }
        public String CreatorID { get; set; }

        [Display(Name = "Field of Expertise")]
        public string FieldName { get; set; }
    }
}