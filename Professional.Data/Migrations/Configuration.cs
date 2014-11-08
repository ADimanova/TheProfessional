namespace Professional.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Professional.Common;
    using Professional.Models;
    using Microsoft.AspNet.Identity;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            this.SeedRoles(context);
            this.SeedInitialAdmin(context);
        }

        private void SeedRoles(ApplicationDbContext context)
        {
            if (context.Roles.Any())
            {
                return;
            }

            context.Roles.AddOrUpdate(new IdentityRole(GlobalConstants.AdministratorRoleName));
            context.Roles.AddOrUpdate(new IdentityRole(GlobalConstants.UserRoleName));
            context.SaveChanges();
        }

        private void SeedInitialAdmin(ApplicationDbContext context)
        {
            string username = "admin@admin.admin";
            string password = "admin@admin.admin";
            string firstName = "Admin";
            string lastName = "Admin";

            if (context.Users.Any(u => u.UserName == username))
            {
                return;
            }

            var admin = new User()
            {
                UserName = username,
                Email = username,
                FirstName = firstName,
                LastName = lastName,
            };

            var userManager = new UserManager<User>(new UserStore<User>(context));

            if (!userManager.Users.Any(u => u.UserName == username))
            {
                userManager.Create(admin, password);
                userManager.AddToRole(admin.Id, GlobalConstants.AdministratorRoleName);
            }
        }
    }
}
