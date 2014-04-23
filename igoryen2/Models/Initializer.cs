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

            var UserMark = new ApplicationUser();
            string UserMarkPw = "123456";
            var UserMarkInfo = new MyUserInfo() { FirstName = "Mark", LastName = "Fernandes" };
            UserMark.UserName = "Mark";
            UserMark.HomeTown = "Markham";
            UserMark.MyUserInfo = UserMarkInfo;
            var UserMarkCreate = UserManager.Create(UserMark, UserMarkPw);
            if (UserMarkCreate.Succeeded) {
                var addUserMarkToRoleFacultyResult = UserManager.AddToRole(UserMark.Id, roleFaculty);
            }

            var UserIan = new ApplicationUser();
            string UserIanPw = "123456";
            var UserIanInfo = new MyUserInfo() { FirstName = "Ian", LastName = "Tipson" };
            UserIan.UserName = "Ian";
            UserIan.HomeTown = "Mississauga";
            UserIan.MyUserInfo = UserIanInfo;
            var UserIanCreate = UserManager.Create(UserIan, UserIanPw);
            if (UserIanCreate.Succeeded) {
                var addUserIanToRoleFacultyResult = UserManager.AddToRole(UserIan.Id, roleFaculty);
            }

            try {
                Student igor = new Student();
                igor.PersonId = 1;
                igor.FirstName = "Igor";
                igor.LastName = "Entaltsev";
                igor.Phone = "555-555-5555";
                igor.SenecaId = "011111111";
                igor.UserName = "Igor";
                igor.HomeTown = "Sochi";
                //................................
                string userIgorPw = "123456";
                var userIgorInfo = new MyUserInfo() { FirstName = "Igor", LastName = "Entaltsev" };
                igor.MyUserInfo = userIgorInfo;
                var UserIgorCreate = UserManager.Create(igor, userIgorPw);
                if (UserIgorCreate.Succeeded) {
                    var addUserIgorToRoleStudentResult = UserManager.AddToRole(igor.Id, roleStudent);
                }
                //................................
                dc.Students.Add(igor);


                Student bob = new Student();
                bob.PersonId = 2;
                bob.FirstName = "Bob";
                bob.LastName = "White";
                bob.Phone = "777-777-7777";
                bob.SenecaId = "022222222";
                bob.UserName = "Bob";
                bob.HomeTown = "Mishawaka";
                //................................
                string userBobPw = "123456";
                var userBobInfo = new MyUserInfo() { FirstName = "Bob", LastName = "White" };
                bob.MyUserInfo = userBobInfo;
                var UserBobCreate = UserManager.Create(bob, userBobPw);
                if (UserBobCreate.Succeeded) {
                    var addUserBobToRoleStudentResult = UserManager.AddToRole(bob.Id, roleStudent);
                }
                //................................
                dc.Students.Add(bob);


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