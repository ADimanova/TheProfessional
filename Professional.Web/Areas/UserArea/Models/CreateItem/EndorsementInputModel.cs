namespace Professional.Web.Areas.UserArea.Models.CreateItem
{
    using System.ComponentModel.DataAnnotations;

    public class EndorsementInputModel
    {
        [Required]
        [Range(1, 10)]
        public int Value { get; set; }

        public string Comment { get; set; }

        public string EndorsedID { get; set; }

        public string EndorsingUserID { get; set; }

        public string EndorseAction { get; set; }
    }
}