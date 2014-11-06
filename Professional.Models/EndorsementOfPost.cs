using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Professional.Models
{
    public class EndorsementOfPost
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Range(0, 10)]
        public int Value { get; set; }
        public string Comment { get; set; }

        [Required]
        public int EndorsedPostID { get; set; }

        public virtual Post EndorsedPost { get; set; }

        [Required]
        public int EndorsingUserID { get; set; }

        public virtual User EndorsingUser { get; set; }
    }
}
