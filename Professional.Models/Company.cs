using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Professional.Models
{
    public class Company
    {
        private ICollection<EndorsementOfPost> postsEndorsements;
        private ICollection<EndorsementOfUser> usersEndorsements;
        public Company()
		{
            this.postsEndorsements = new HashSet<EndorsementOfPost>();
            this.postsEndorsements = new HashSet<EndorsementOfPost>();
		}
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<EndorsementOfPost> PostsEndorsementsents
        {
            get { return this.postsEndorsements; }
            set { this.postsEndorsements = value; }
        }

        public virtual ICollection<EndorsementOfUser> UsersEndorsements
        {
            get { return this.usersEndorsements; }
            set { this.usersEndorsements = value; }
        }
    }
}
