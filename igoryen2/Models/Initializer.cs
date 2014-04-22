using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace igoryen2.Models {
    public class Initializer : DropCreateDatabaseAlways<DataContext> {

        private void InitializeIdentityForEF(DataContext dc) {

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(dc));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(dc));

            string roleName1 = "Admin";
            if (!RoleManager.RoleExists(roleName1)) {
                var role1CreateResult = RoleManager.Create(new IdentityRole(roleName1));
            }

            string roleName2 = "Student";
            if (!RoleManager.RoleExists(roleName2)) {
                var role2CreateResult = RoleManager.Create(new IdentityRole(roleName2));
            }

            string roleName3 = "Faculty";
            if (!RoleManager.RoleExists(roleName3)) {
                var role3CreateResult = RoleManager.Create(new IdentityRole(roleName3));
            }
        }
    }
}