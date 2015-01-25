namespace Professional.Web.Areas.UserArea.Models.CreateItem
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;

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

        public string CreatorID { get; set; }

        [Display(Name = "Field of Expertise")]
        public string FieldName { get; set; }
    }
}