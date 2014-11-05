using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Professional.Models;
using Professional.Data.Migrations;

namespace Professional.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext()
            : base("TheProfessionalConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual IDbSet<Article> Articles { get; set; }
        public virtual IDbSet<ArticleEndorsement> ArticleEndorsements { get; set; }
        public virtual IDbSet<UserEndorsement> UserEndorsements { get; set; }
    }
}
