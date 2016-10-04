using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WorkFlowSpy.Models;

namespace WorkFlowSpy.Tools
{
    public class UsersAndRolesInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // role creation
            var role1 = new IdentityRole { Name = "admin" };
            var role2 = new IdentityRole { Name = "manager" };
            var role3 = new IdentityRole { Name = "taskholder" };

            // insert roles into database
            roleManager.Create(role1);
            roleManager.Create(role2);
            roleManager.Create(role3);

            // users creation
            var admin = new ApplicationUser { UserName = "Admin", FirstName = "Alex", LastName = "Alex" };
            string password = "admin1";
            var result = userManager.Create(admin, password);

            if (result.Succeeded)
            {
                // append role to user
                userManager.AddToRole(admin.Id, role1.Name);
                //userManager.AddToRole(admin.Id, role2.Name);
            }

            base.Seed(context);
        }
    }
}