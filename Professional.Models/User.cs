namespace Professional.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using Professional.Data.Contracts;

    public partial class User : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public User()
            : base()
        {
            this.occupations = new HashSet<Occupation>();
            this.fieldOfExpertise = new HashSet<FieldOfExpertise>();
            this.connections = new HashSet<Connection>();
            this.posts = new HashSet<Post>();
            this.usersEndorsements = new HashSet<EndorsementOfUser>();
            this.messages = new HashSet<Message>();

            // This will prevent UserManager.CreateAsync from throwing an exception
            this.DateOfBirth = DateTime.Now;
            this.CreatedOn = DateTime.Now;
        }

        public DateTime CreatedOn { get; set; }

        public bool PreserveCreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            return userIdentity;
        }
    }
}
