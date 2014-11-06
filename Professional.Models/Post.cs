using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Professional.Models
{
    public class Post
    {
        private ICollection<EndorsementOfPost> postEndorsements;
        public Post()
		{
            this.postEndorsements = new HashSet<EndorsementOfPost>();
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
        public int CreatorID { get; set; }
        public User Creator { get; set; }

        public virtual ICollection<EndorsementOfPost> PostEndorsementsents
        {
            get { return this.postEndorsements; }
            set { this.postEndorsements = value; }
        }
    }
}
