namespace Professional.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Professional.Data.Contracts;

    public class Company : AuditInfo, IDeletableEntity
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

        [Index]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

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
