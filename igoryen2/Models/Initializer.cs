using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.Validation;


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
                var roleStudentCreateResult = RoleManager.Create(new IdentityRole(roleStudent));
            }

            string roleFaculty = "Faculty";
            if (!RoleManager.RoleExists(roleFaculty)) {
                var roleFacultyCreateResult = RoleManager.Create(new IdentityRole(roleFaculty));
            }

            var UserAdmin = new ApplicationUser();
            //string userName1 = "Igor";
            string userAdminPw = "123456";
            var userAdminInfo = new MyUserInfo() { FirstName = "Alan", LastName = "Admin" };
            UserAdmin.UserName = "Alan";
            UserAdmin.HomeTown = "Ottawa";
            UserAdmin.MyUserInfo = userAdminInfo;
            var UserAdminCreateResult = UserManager.Create(UserAdmin, userAdminPw);
            if (UserAdminCreateResult.Succeeded) {
                var addUserAdminToRoleAdminResult = UserManager.AddToRole(UserAdmin.Id, roleAdmin);
            }

            //var UserBob = new ApplicationUser();
            //string userBobPw = "123456";
            //var userBobInfo = new MyUserInfo() { FirstName = "Bob", LastName = "White" };
            //UserBob.UserName = "Bob";
            //UserBob.HomeTown = "Sochi";
            //UserBob.MyUserInfo = userBobInfo;
            //var UserBobCreate = UserManager.Create(UserBob, userBobPw);
            //if (UserBobCreate.Succeeded) {
            //    var addUserBobToRoleStudentResult = UserManager.AddToRole(UserBob.Id, roleStudent);
            //}

            //var UserIgor = new ApplicationUser();
            //string userIgorPw = "123456";
            //var userIgorInfo = new MyUserInfo() { FirstName = "Igor", LastName = "Entaltsev" };
            //UserIgor.UserName = "Igor";
            //UserIgor.HomeTown = "Sochi";
            //UserIgor.MyUserInfo = userIgorInfo;
            //var UserIgorCreate = UserManager.Create(UserIgor, userIgorPw);
            //if (UserIgorCreate.Succeeded) {
            //    var addUserIgorToRoleStudentResult = UserManager.AddToRole(UserIgor.Id, roleStudent);
            //}

            //var UserMark = new ApplicationUser();
            //string UserMarkPw = "123456";
            //var UserMarkInfo = new MyUserInfo() { FirstName = "Mark", LastName = "Fernandes" };
            //UserMark.UserName = "Mark";
            //UserMark.HomeTown = "Markham";
            //UserMark.MyUserInfo = UserMarkInfo;
            //var UserMarkCreate = UserManager.Create(UserMark, UserMarkPw);
            //if (UserMarkCreate.Succeeded) {
            //    var addUserMarkToRoleFacultyResult = UserManager.AddToRole(UserMark.Id, roleFaculty);
            //}

            //var UserIan = new ApplicationUser();
            //string UserIanPw = "123456";
            //var UserIanInfo = new MyUserInfo() { FirstName = "Ian", LastName = "Tipson" };
            //UserIan.UserName = "Ian";
            //UserIan.HomeTown = "Mississauga";
            //UserIan.MyUserInfo = UserIanInfo;
            //var UserIanCreate = UserManager.Create(UserIan, UserIanPw);
            //if (UserIanCreate.Succeeded) {
            //    var addUserIanToRoleFacultyResult = UserManager.AddToRole(UserIan.Id, roleFaculty);
            //}

            try {
                // create student igor
                Student igor = new Student();
                igor.FirstName = "Igor";
                igor.HomeTown = "Sochi";
                igor.LastName = "Entaltsev";
                var userIgorInfo = new MyUserInfo() { FirstName = "Igor", LastName = "Entaltsev" };
                igor.MyUserInfo = userIgorInfo;
                igor.PersonId = 1;
                igor.Phone = "555-555-5555";
                igor.SenecaId = "011111111";
                igor.UserName = "Igor";
                // add igor to role "Student"
                string userIgorPw = "123456";
                var UserIgorCreate = UserManager.Create(igor, userIgorPw);
                if (UserIgorCreate.Succeeded) {
                    var addUserIgorToRoleStudentResult = UserManager.AddToRole(igor.Id, roleStudent);
                }
                dc.Students.Add(igor);

                // create student bob
                Student bob = new Student();
                bob.FirstName = "Bob";
                bob.HomeTown = "Mishawaka";
                bob.LastName = "White";
                var userBobInfo = new MyUserInfo() { FirstName = "Bob", LastName = "White" };
                bob.MyUserInfo = userBobInfo;
                bob.PersonId = 2;
                bob.Phone = "777-777-7777";
                bob.SenecaId = "022222222";
                bob.UserName = "Bob";
                // add bob to role "Student"
                string userBobPw = "123456";
                var UserBobCreate = UserManager.Create(bob, userBobPw);
                if (UserBobCreate.Succeeded) {
                    var addUserBobToRoleStudentResult = UserManager.AddToRole(bob.Id, roleStudent);
                }
                dc.Students.Add(bob);

                // create faculty Mark
                Faculty mark = new Faculty();
                mark.FirstName = "Mark";
                mark.HomeTown = "Markham";
                mark.LastName = "Fernandes";
                var UserMarkInfo = new MyUserInfo() { FirstName = "Mark", LastName = "Fernandes" };
                mark.MyUserInfo = UserMarkInfo;
                mark.PersonId = 10;
                mark.Phone = "555-567-6789";
                mark.SenecaId = "034234678";
                mark.UserName = "Mark";
                // add "mark" to role "faculty"
                string UserMarkPw = "123456";
                var UserMarkCreate = UserManager.Create(mark, UserMarkPw);
                if (UserMarkCreate.Succeeded) {
                    var addUserMarkToRoleFacultyResult = UserManager.AddToRole(mark.Id, roleFaculty);
                }
                dc.Faculties.Add(mark);

                // create faculty Ian
                Faculty ian = new Faculty();
                ian.FirstName = "Ian";
                ian.HomeTown = "Mississauga";
                ian.LastName = "Tipson";
                var UserIanInfo = new MyUserInfo() { FirstName = "Ian", LastName = "Tipson" };
                ian.MyUserInfo = UserIanInfo;
                ian.PersonId = 11;
                ian.Phone = "555-567-6790";
                ian.SenecaId = "034234679";
                ian.UserName = "Ian";
                // add ian to role Faculty
                string UserIanPw = "123456";
                var UserIanCreate = UserManager.Create(ian, UserIanPw);
                if (UserIanCreate.Succeeded) {
                    var addUserIanToRoleFacultyResult = UserManager.AddToRole(ian.Id, roleFaculty);
                }
                dc.Faculties.Add(ian);

            }
            catch (DbEntityValidationException e) {
                //----------------------------------------------------------
                List<string> output1 = new List<string>();
                List<string> output2 = new List<string>();
                foreach (var eve in e.EntityValidationErrors) {
                    output1.Add("Entity of type " + eve.Entry.Entity.GetType().Name + " in state " + eve.Entry.State + " has the following validation errors:");
                        foreach (var ve in eve.ValidationErrors) {
                            output1.Add("- Property: " + ve.PropertyName + ", Error: " + ve.ErrorMessage);
                        }

                        Console.WriteLine("======================================");
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors) {
                          Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                              ve.PropertyName, ve.ErrorMessage);
                        }
                        
                    }
                output2 = output1;
                throw;
            } // catch
        }
    }
}