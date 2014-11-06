using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;

namespace Professional.Models
{
    public partial class User : IdentityUser
    {
        public User()
            : base()
        {
            this.occupations = new HashSet<Occupation>();
            this.fieldOfExpertise = new HashSet<FieldOfExpertise>();
            this.connections = new HashSet<User>();
            this.postsEndorsements = new HashSet<EndorsementOfPost>();
            this.usersEndorsements = new HashSet<EndorsementOfUser>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
