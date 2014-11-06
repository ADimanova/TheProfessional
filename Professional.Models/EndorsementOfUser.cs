using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Professional.Models
{
    public class EndorsementOfUser
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Range(0, 10)]
        public int Value { get; set; }
        public string Comment { get; set; }

        [Required]
        public int EndorsedUserID { get; set; }

        public virtual User EndorsedUser { get; set; }

        [Required]
        public int EndorsingUserID { get; set; }

        public virtual User EndorsingUser { get; set; }
    }
}
