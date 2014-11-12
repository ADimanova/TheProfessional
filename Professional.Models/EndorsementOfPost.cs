using Professional.Data.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Professional.Models
{
    public class EndorsementOfPost : AuditInfo, IDeletableEntity
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Range(0, 10)]
        public int Value { get; set; }
        public string Comment { get; set; }

        [Required]
        public string EndorsedPostID { get; set; }

        public virtual Post EndorsedPost { get; set; }

        [Required]
        public string EndorsingUserID { get; set; }

        public virtual User EndorsingUser { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
