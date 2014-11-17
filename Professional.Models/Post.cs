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
    public class Post: AuditInfo, IDeletableEntity
    {
        private ICollection<EndorsementOfPost> postEndorsements;
        public Post()
		{
            this.postEndorsements = new HashSet<EndorsementOfPost>();
			this.CreatedOn = DateTime.Now;
		}

        [Key]
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string CreatorID { get; set; }
        public virtual User Creator { get; set; }

        [Required]
        public int FieldID { get; set; }
        public virtual FieldOfExpertise Field { get; set; }

        public int Rank { get; set; }

        public virtual ICollection<EndorsementOfPost> PostEndorsementsents
        {
            get { return this.postEndorsements; }
            set { this.postEndorsements = value; }
        }

        [Index]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
