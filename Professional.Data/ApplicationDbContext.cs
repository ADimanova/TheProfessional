﻿using System;
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

        public virtual IDbSet<Post> Posts { get; set; }
        public virtual IDbSet<PostEndorsement> PostEndorsements { get; set; }
        public virtual IDbSet<UserEndorsement> UserEndorsements { get; set; }
    }
}
