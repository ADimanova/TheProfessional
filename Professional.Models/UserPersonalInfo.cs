namespace Professional.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class User
    {
        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        public string FirstName { get; set; }

        [MinLength(2)]
        [MaxLength(40)]
        public string MiddleName { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public bool? IsMale { get; set; }

        [Column(TypeName = "ntext")]
        [MaxLength]
        public string PersonalHistory { get; set; }

        public int? ProfileImageId { get; set; }

        public virtual Image ProfileImage { get; set; }
    }
}
