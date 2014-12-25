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
    public class Connection : AuditInfo, IDeletableEntity
    {
        [Key]
        public int ID { get; set; }
        public string FirstUserId { get; set; }

        public virtual User FirstUser { get; set; }

        public string SecondUserId { get; set; }

        public virtual User SecondUser { get; set; }

        public bool IsAccepted { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
