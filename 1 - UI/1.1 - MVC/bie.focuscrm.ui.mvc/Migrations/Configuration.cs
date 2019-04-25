using bie.focuscrm.application;
using bie.focuscrm.ui.mvc.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace bie.focuscrm.ui.mvc.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(bie.focuscrm.ui.mvc.Models.ApplicationDbContext context)
        {

            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole { Name = "Cliente" });
            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole { Name = "Setor" });
            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole { Name = "Focus" });
            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole { Name = "Administrador" });
            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole { Name = "Superadmin" });





            if (!(context.Users.Any(u => u.UserName == "superadmin@biexpert.com.br")))
            {
                using (UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(context))
                {
                    using (UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(userStore))
                    {
                        var userToInsert = new ApplicationUser
                        {
                            UserName = "superadmin@biexpert.com.br",
                            Email = "superadmin@biexpert.com.br",
                            PhoneNumber = "5521985000625",
                            EmailConfirmed = true,
                            PhoneNumberConfirmed = true
                        };

                        userManager.Create(userToInsert, "Pyz52rog");

                        userManager.AddToRoles(userToInsert.Id, "Administrador");
                        userManager.AddToRoles(userToInsert.Id, "Superadmin");

                    }
                }
            }






            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
