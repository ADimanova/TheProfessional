using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using Professional.Data.Contracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace Professional.Models
{
    public partial class User : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public User()
            : base()
        {
            this.occupations = new HashSet<Occupation>();
            this.fieldOfExpertise = new HashSet<FieldOfExpertise>();
            this.connections = new HashSet<User>();
            this.postsEndorsements = new HashSet<EndorsementOfPost>();
            this.usersEndorsements = new HashSet<EndorsementOfUser>();

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
