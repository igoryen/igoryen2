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

            string roleAdmin = "Admin";
            if (!RoleManager.RoleExists(roleAdmin)) {
                var role1CreateResult = RoleManager.Create(new IdentityRole(roleAdmin));
            }

            string roleStudent = "Student";
            if (!RoleManager.RoleExists(roleStudent)) {
                var role2CreateResult = RoleManager.Create(new IdentityRole(roleStudent));
            }

            string roleFaculty = "Faculty";
            if (!RoleManager.RoleExists(roleFaculty)) {
                var role3CreateResult = RoleManager.Create(new IdentityRole(roleFaculty));
            }

            var UserAdmin = new ApplicationUser();
            //string userName1 = "Igor";
            string userAdminPw = "123456";
            var userAdminInfo = new MyUserInfo() { FirstName = "Alan", LastName = "Admin" };
            UserAdmin.UserName = "Alan";
            UserAdmin.HomeTown = "Ottawa";
            UserAdmin.MyUserInfo = userAdminInfo;
            var UserIgorCreateResult = UserManager.Create(UserAdmin, userAdminPw);
            if (UserIgorCreateResult.Succeeded) {
                var addUserIgorToRole1Result = UserManager.AddToRole(UserAdmin.Id, roleAdmin);
            }

            var UserBob = new ApplicationUser();
            string userBobPw = "123456";
            var userBobInfo = new MyUserInfo() { FirstName = "Bob", LastName = "White" };
            UserBob.UserName = "Bob";
            UserBob.HomeTown = "Sochi";
            UserBob.MyUserInfo = userBobInfo;
            var UserBobCreate = UserManager.Create(UserBob, userBobPw);
            if (UserBobCreate.Succeeded) {
                var addUserBobToRole2Result = UserManager.AddToRole(UserBob.Id, roleStudent);
            }

            var UserIgor = new ApplicationUser();
            string userIgorPw = "123456";
            var userIgorInfo = new MyUserInfo() { FirstName = "Igor", LastName = "Entaltsev" };
            UserIgor.UserName = "Igor";
            UserIgor.HomeTown = "Sochi";
            UserIgor.MyUserInfo = userIgorInfo;
            var UserMaryCreate = UserManager.Create(UserIgor, userIgorPw);
            if (UserMaryCreate.Succeeded) {
                var addUserMaryToRole2Result = UserManager.AddToRole(UserIgor.Id, roleStudent);
            }
        }
    }
}