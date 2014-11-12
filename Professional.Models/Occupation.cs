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
    public class Occupation : AuditInfo, IDeletableEntity
    {
        private ICollection<User> holders;

        public Occupation()
        {
            this.holders = new HashSet<User>();
        }

        [Key]
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }

        public virtual ICollection<User> Holders
        {
            get { return this.holders; }
            set { this.holders = value; }
        }

        [Index]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
