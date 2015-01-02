namespace Professional.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class User
    {
        private ICollection<Occupation> occupations;
        private ICollection<FieldOfExpertise> fieldOfExpertise;
        private ICollection<Connection> connections;
        private ICollection<EndorsementOfPost> postsEndorsements;
        private ICollection<EndorsementOfUser> usersEndorsements;

        public int Rank { get; set; }

        public virtual ICollection<Occupation> Occupations
        {
            get { return this.occupations; }

            set { this.occupations = value; }
        }

        public virtual ICollection<FieldOfExpertise> FieldsOfExpertise
        {
            get { return this.fieldOfExpertise; }
            set { this.fieldOfExpertise = value; }
        }

        [ForeignKey("FirstUserId")] 
        public virtual ICollection<Connection> Connections
        {
            get { return this.connections; }
            set { this.connections = value; }
        }

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
