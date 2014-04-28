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

        // v7
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
            string userAdminPw = "123456";
            var userAdminInfo = new MyUserInfo() { FirstName = "Admin", LastName = "Admin" };
            UserAdmin.MyUserInfo = userAdminInfo;
            UserAdmin.UserName = "Admin";
            UserAdmin.HomeTown = "Ottawa";
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
                // create student igor (3)
                Student igor = new Student();
                string userIgorPw = "123456";
                igor.PersonFirstName = "Igor";
                igor.HomeTown = "Sochi";
                igor.PersonLastName = "Entaltsev";
                var userIgorInfo = new MyUserInfo() { FirstName = "Igor", LastName = "Entaltsev" };
                igor.MyUserInfo = userIgorInfo;
                igor.PersonId = 3;
                igor.PersonPhone = "555-555-5555";
                igor.SenecaId = "011111111";
                igor.UserName = "Igor";
                dc.Students.Add(igor);
                // Make "igor" a user and add him to role "student"
                var UserIgorCreate = UserManager.Create(igor, userIgorPw);
                if (UserIgorCreate.Succeeded) {
                    var addUserIgorToRoleStudentResult = UserManager.AddToRole(igor.Id, roleStudent);
                }
                // create student bob (2)
                Student bob = new Student();
                string userBobPw = "123456";
                bob.PersonFirstName = "Bob";
                bob.HomeTown = "Mishawaka";
                bob.PersonLastName = "White";
                var userBobInfo = new MyUserInfo() { FirstName = "Bob", LastName = "White" };
                bob.MyUserInfo = userBobInfo;
                bob.PersonId = 2;
                bob.PersonPhone = "777-777-7777";
                bob.SenecaId = "022222222";
                bob.UserName = "Bob";
                // Make "bob" a user and add him to role "student"
                dc.Students.Add(bob);
                var UserBobCreate = UserManager.Create(bob, userBobPw);
                if (UserBobCreate.Succeeded) {
                    var addUserBobToRoleStudentResult = UserManager.AddToRole(bob.Id, roleStudent);
                }
                // create student wei (4)
                Student wei = new Student();
                string userWeiPw = "123456";
                wei.PersonFirstName = "Wei";
                wei.HomeTown = "Welland";
                wei.PersonLastName = "Chen";
                var userWeiInfo = new MyUserInfo() { FirstName = "Wei", LastName = "Chen" };
                wei.MyUserInfo = userWeiInfo;
                wei.PersonId = 4;
                wei.PersonPhone = "444-444-4444";
                wei.SenecaId = "044444444";
                wei.UserName = "Wei";
                dc.Students.Add(wei);
                // Make "wei" a user and add him to role "student"
                var UserWeiCreate = UserManager.Create(wei, userWeiPw);
                if (UserWeiCreate.Succeeded) {
                    var addUserWeiToRoleStudentResult = UserManager.AddToRole(wei.Id, roleStudent);
                }

                // create student john (5)
                Student john = new Student();
                string userJohnPw = "123456";
                john.PersonFirstName = "John";
                john.HomeTown = "Belleville";
                john.PersonLastName = "Woo";
                var userJohnInfo = new MyUserInfo() { FirstName = "John", LastName = "Woo" };
                john.MyUserInfo = userJohnInfo;
                john.PersonId = 5;
                john.PersonPhone = "555-555-5555";
                john.SenecaId = "055555555";
                john.UserName = "John";
                dc.Students.Add(john);
                // Make "john" a user and add him to role "student"
                var UserJohnCreate = UserManager.Create(john, userJohnPw);
                if (UserJohnCreate.Succeeded) {
                    var addUserJohnToRoleStudentResult = UserManager.AddToRole(john.Id, roleStudent);
                }

                // create student jack (6)
                Student jack = new Student();
                string userJackPw = "123456";
                jack.PersonFirstName = "Jack";
                jack.HomeTown = "Hamilton";
                jack.PersonLastName = "Smith";
                var userJackInfo = new MyUserInfo() { FirstName = "Jack", LastName = "Smith" };
                jack.MyUserInfo = userJackInfo;
                jack.PersonId = 6;
                jack.PersonPhone = "666-666-6666";
                jack.SenecaId = "066666666";
                jack.UserName = "Jack";
                dc.Students.Add(jack);
                // Make "jack" a user and add him to role "student"
                var UserJackCreate = UserManager.Create(jack, userJackPw);
                if (UserJackCreate.Succeeded) {
                    var addUserJackToRoleStudentResult = UserManager.AddToRole(jack.Id, roleStudent);
                }

                // create student jill (7)
                Student jill = new Student();
                string userJillPw = "123456";
                jill.PersonFirstName = "Jill";
                jill.HomeTown = "Hamilton";
                jill.PersonLastName = "Smith";
                var userJillInfo = new MyUserInfo() { FirstName = "Jill", LastName = "Smith" };
                jill.MyUserInfo = userJillInfo;
                jill.PersonId = 7;
                jill.PersonPhone = "777-777-7777";
                jill.SenecaId = "077777777";
                jill.UserName = "Jill";
                
                dc.Students.Add(jill);
                // Make "jill" a user and add her to role "student"
                var UserJillCreate = UserManager.Create(jill, userJillPw);
                if (UserJillCreate.Succeeded) {
                    var addUserJillToRoleStudentResult = UserManager.AddToRole(jill.Id, roleStudent);
                }


                // 1 create faculty Mark (8)
                Faculty mark = new Faculty();
                mark.SenecaId = "034234678";
                mark.UserName = "Mark";
                mark.PersonFirstName = "Mark";
                mark.PersonLastName = "Fernandes";
                mark.PersonPhone = "555-567-6789";
                string UserMarkPw = "123456";
                mark.HomeTown = "Markham";
                var UserMarkInfo = new MyUserInfo() { FirstName = "Mark", LastName = "Fernandes" };
                mark.MyUserInfo = UserMarkInfo;
                mark.PersonId = 8;
                dc.Faculties.Add(mark);
                // Make "mark" a user and add him to role "faculty"
                var UserMarkCreate = UserManager.Create(mark, UserMarkPw);
                if (UserMarkCreate.Succeeded) {
                    var addUserMarkToRoleFacultyResult = UserManager.AddToRole(mark.Id, roleFaculty);
                }

                // 2 create faculty Ian (9)
                Faculty ian = new Faculty();
                string UserIanPw = "123456";
                ian.PersonFirstName = "Ian";
                ian.HomeTown = "Mississauga";
                ian.PersonLastName = "Tipson";
                var UserIanInfo = new MyUserInfo() { FirstName = "Ian", LastName = "Tipson" };
                ian.MyUserInfo = UserIanInfo;
                ian.PersonId = 9;
                ian.PersonPhone = "555-567-6790";
                ian.SenecaId = "034234679";
                ian.UserName = "Ian";
                dc.Faculties.Add(ian);
                // Make "ian" a user and add him to role "faculty"
                var UserIanCreate = UserManager.Create(ian, UserIanPw);
                if (UserIanCreate.Succeeded) {
                    var addUserIanToRoleFacultyResult = UserManager.AddToRole(ian.Id, roleFaculty);
                }

                // 3 create faculty Ron (10)
                Faculty ron = new Faculty();
                string UserRonPw = "123456";
                ron.PersonFirstName = "Ronald";
                ron.HomeTown = "Oshawa";
                ron.PersonLastName = "Ronaldson";
                var UserRonInfo = new MyUserInfo() { FirstName = "Ronald", LastName = "Ronaldson" };
                ron.MyUserInfo = UserRonInfo;
                ron.PersonId = 10;
                ron.PersonPhone = "555-567-6791";
                ron.SenecaId = "034234680";
                ron.UserName = "Ron";
                dc.Faculties.Add(ron);
                // Make "ron" a user and add him to role "faculty"
                var UserRonCreate = UserManager.Create(ron, UserRonPw);
                if (UserRonCreate.Succeeded) {
                    var addUserRonToRoleFacultyResult = UserManager.AddToRole(ron.Id, roleFaculty);
                }

                // 4 create faculty Bill (11)
                Faculty bill = new Faculty();
                string UserBillPw = "123456";
                bill.PersonFirstName = "Bill";
                bill.HomeTown = "Vaughn";
                bill.PersonLastName = "Johnson";
                var UserBillInfo = new MyUserInfo() { FirstName = "Bill", LastName = "Johnson" };
                bill.MyUserInfo = UserBillInfo;
                bill.PersonId = 11;
                bill.PersonPhone = "555-567-6791";
                bill.SenecaId = "034234682";
                bill.UserName = "Bill";
                dc.Faculties.Add(bill);
                // Make "bill" a user and add him to role "faculty"
                var UserBillCreate = UserManager.Create(bill, UserBillPw);
                if (UserBillCreate.Succeeded) {
                    var addUserBillToRoleFacultyResult = UserManager.AddToRole(bill.Id, roleFaculty);
                }

                // add courses to mark (8)
                // 1) Mark teaches IPC144
                Course ipc144 = new Course();
                ipc144.CourseCode = "IPC144";
                ipc144.CourseId = 1;
                ipc144.CourseName = "Introduction into programming";
                ipc144.Faculty = mark;
                ipc144.RoomNumber = "1111";
                ipc144.Students = new List<Student>();
                ipc144.Students.Add(igor);
                ipc144.DateEnd = new DateTime(2014, 3, 31);
                ipc144.DateStart = new DateTime(2014, 1, 10);
                dc.Courses.Add(ipc144);

                igor.Courses.Add(ipc144);
                mark.Courses.Add(ipc144);

                dc.SaveChanges();

                // 2) Mark teaches ULI101 (2)
                Course uli101 = new Course();
                uli101.CourseCode = "ULI101";
                uli101.CourseId = 2;
                uli101.CourseName = "OS - Unix";
                uli101.Faculty = mark;
                uli101.RoomNumber = "2222";
                uli101.Students = new List<Student>();
                uli101.Students.Add(bob);
                uli101.DateEnd = new DateTime(2014, 3, 31);
                uli101.DateStart = new DateTime(2014, 1, 10);
                dc.Courses.Add(uli101);

                bob.Courses.Add(uli101);
                mark.Courses.Add(uli101);

                dc.SaveChanges();

                // 3) Mark teaches IOS110 (3)
                Course ios110 = new Course();
                ios110.CourseCode = "IOS110";
                ios110.CourseId = 3;
                ios110.CourseName = "OS - Windows";
                ios110.Faculty = mark;
                ios110.RoomNumber = "3333";
                ios110.Students = new List<Student>();
                ios110.Students.Add(wei);
                ios110.DateEnd = new DateTime(2014, 3, 31);
                ios110.DateStart = new DateTime(2014, 1, 10);
                dc.Courses.Add(ios110);

                wei.Courses.Add(ios110);
                mark.Courses.Add(ios110);

                dc.SaveChanges();

                // 4) Mark teaches OOP244 (4)
                Course oop244 = new Course();
                oop244.CourseCode = "OOP244";
                oop244.CourseId = 4;
                oop244.CourseName = "OOP development using C++";
                oop244.Faculty = mark;
                oop244.RoomNumber = "4444";
                oop244.Students = new List<Student>();
                oop244.Students.Add(igor);
                oop244.Students.Add(john);
                oop244.DateEnd = new DateTime(2014, 3, 31);
                oop244.DateStart = new DateTime(2014, 1, 10);
                dc.Courses.Add(oop244);

                mark.Courses.Add(oop244);
                igor.Courses.Add(oop244);
                john.Courses.Add(oop244);

                dc.SaveChanges();

                // add courses to Ian
                // 1) Ian teaches INT222
                Course int222 = new Course();
                int222.CourseCode = "INT222";
                int222.CourseId = 5;
                int222.CourseName = "Web development - client";
                int222.Faculty = ian;
                int222.RoomNumber = "5555";
                int222.Students = new List<Student>();
                int222.Students.Add(bob);
                int222.Students.Add(jack);
                int222.DateEnd = new DateTime(2014, 3, 31);
                int222.DateStart = new DateTime(2014, 1, 10);
                dc.Courses.Add(int222);

                ian.Courses.Add(int222);
                bob.Courses.Add(int222);
                jack.Courses.Add(int222);

                dc.SaveChanges();


                // 2) Ian teaches IBC233 (6)
                Course ibc233 = new Course();
                ibc233.CourseCode = "IBC233";
                ibc233.CourseId = 6;
                ibc233.CourseName = "iSeries - Business Applications";
                ibc233.Faculty = ian;
                ibc233.RoomNumber = "6666";
                ibc233.Students = new List<Student>();
                ibc233.Students.Add(wei);
                ibc233.Students.Add(jill);
                ibc233.DateEnd = new DateTime(2014, 3, 31);
                ibc233.DateStart = new DateTime(2014, 1, 10);
                dc.Courses.Add(ibc233);

                ian.Courses.Add(ibc233);
                wei.Courses.Add(ibc233);
                jill.Courses.Add(ibc233);

                dc.SaveChanges();


                // 3) Ian teaches DBS201
                Course dbs201 = new Course();
                dbs201.CourseCode = "DBS201";
                dbs201.CourseId = 7;
                dbs201.CourseName = "Database principles";
                dbs201.Faculty = ian;
                dbs201.RoomNumber = "7777";
                dbs201.Students = new List<Student>();
                dbs201.Students.Add(igor);
                dbs201.Students.Add(john);
                dbs201.DateEnd = new DateTime(2014, 3, 31);
                dbs201.DateStart = new DateTime(2014, 1, 10);
                dc.Courses.Add(dbs201);

                ian.Courses.Add(dbs201);
                igor.Courses.Add(dbs201);
                john.Courses.Add(dbs201);

                dc.SaveChanges();


                // 4) Ian teaches OOP344
                Course oop344 = new Course();
                oop344.CourseCode = "OOP344";
                oop344.CourseId = 8;
                oop344.CourseName = "OOP development - C++";
                oop344.Faculty = ian;
                oop344.RoomNumber = "8888";
                oop344.Students = new List<Student>();
                oop344.Students.Add(bob);
                oop344.Students.Add(jack);
                oop344.DateEnd = new DateTime(2014, 3, 31);
                oop344.DateStart = new DateTime(2014, 1, 10);
                dc.Courses.Add(oop344);

                ian.Courses.Add(oop344);
                bob.Courses.Add(oop344);
                jack.Courses.Add(oop344);

                dc.SaveChanges();

                // add courses to Ron
                // 1) Ron teaches INT322
                Course int322 = new Course();
                int322.CourseCode = "INT322";
                int322.CourseId = 9;
                int322.CourseName = "Web development - Unix server";
                int322.Faculty = ron;
                int322.RoomNumber = "9999";
                int322.Students = new List<Student>();
                int322.Students.Add(wei);
                int322.Students.Add(jill);
                int322.DateEnd = new DateTime(2014, 3, 31);
                int322.DateStart = new DateTime(2014, 1, 10);
                dc.Courses.Add(int322);

                ron.Courses.Add(int322);
                wei.Courses.Add(int322);
                jill.Courses.Add(int322);

                dc.SaveChanges();


                // 2) Ron teaches DBS301
                Course dbs301 = new Course();
                dbs301.CourseCode = "DBS301";
                dbs301.CourseId = 10;
                dbs301.CourseName = "Web development - Unix server";
                dbs301.Faculty = ron;
                dbs301.RoomNumber = "1010";
                dbs301.Students = new List<Student>();
                dbs301.Students.Add(jill);
                dbs301.DateEnd = new DateTime(2014, 3, 31);
                dbs301.DateStart = new DateTime(2014, 1, 10);
                dc.Courses.Add(dbs301);

                ron.Courses.Add(dbs301);
                jill.Courses.Add(dbs301);

                dc.SaveChanges();


                // 3) Ron teaches JAC444
                Course jac444 = new Course();
                jac444.CourseCode = "JAC444";
                jac444.CourseId = 11;
                jac444.CourseName = "OOP develoment - Java";
                jac444.Faculty = ron;
                jac444.RoomNumber = "111";
                jac444.Students = new List<Student>();
                jac444.Students.Add(igor);
                jac444.Students.Add(john);
                jac444.Students.Add(jack);
                jac444.DateEnd = new DateTime(2014, 3, 31);
                jac444.DateStart = new DateTime(2014, 1, 10);
                dc.Courses.Add(jac444);

                ron.Courses.Add(jac444);
                igor.Courses.Add(jac444);
                john.Courses.Add(jac444);
                jack.Courses.Add(jac444);

                dc.SaveChanges();


                // 4) Ron teaches INT422
                Course int422 = new Course();
                int422.CourseCode = "INT422";
                int422.CourseId = 12;
                int422.CourseName = "Web development - Windows";
                int422.Faculty = ron;
                int422.RoomNumber = "1212";
                int422.Students = new List<Student>();
                int422.Students.Add(igor);
                int422.DateEnd = new DateTime(2014, 3, 31);
                int422.DateStart = new DateTime(2014, 1, 10);
                dc.Courses.Add(int422);

                ron.Courses.Add(int422);
                igor.Courses.Add(int422);

                dc.SaveChanges();

                // Add courses to Bill
                // 1) Bill teaches DCN455 (13)
                Course dcn455 = new Course();
                dcn455.CourseCode = "DCN455";
                dcn455.CourseId = 13;
                dcn455.CourseName = "Web development - Windows";
                dcn455.Faculty = bill;
                dcn455.RoomNumber = "1313";
                dcn455.Students = new List<Student>();
                dcn455.Students.Add(bob);
                dcn455.DateEnd = new DateTime(2014, 3, 31);
                dcn455.DateStart = new DateTime(2014, 1, 10);
                dc.Courses.Add(dcn455);

                bill.Courses.Add(dcn455);
                bob.Courses.Add(dcn455);

                dc.SaveChanges();


                // 2) Bill teaches BAC344
                Course bac344 = new Course();
                bac344.CourseCode = "BAC344";
                bac344.CourseId = 14;
                bac344.CourseName = "Business apps - Cobol";
                bac344.Faculty = bill;
                bac344.RoomNumber = "1414";
                bac344.Students = new List<Student>();
                bac344.Students.Add(bob);
                bac344.Students.Add(jack);
                bac344.DateEnd = new DateTime(2014, 3, 31);
                bac344.DateStart = new DateTime(2014, 1, 10);
                dc.Courses.Add(bac344);

                bill.Courses.Add(bac344);
                bob.Courses.Add(bac344);
                jack.Courses.Add(bac344);

                dc.SaveChanges();


                // 3) Bill teaches MAP524
                Course map524 = new Course();
                map524.CourseCode = "MAP524";
                map524.CourseId = 15;
                map524.CourseName = "Mobile apps - Android";
                map524.Faculty = bill;
                map524.RoomNumber = "1515";
                map524.Students = new List<Student>();
                map524.Students.Add(wei);
                map524.Students.Add(jill);
                map524.DateEnd = new DateTime(2014, 3, 31);
                map524.DateStart = new DateTime(2014, 1, 10);
                dc.Courses.Add(map524);

                bill.Courses.Add(map524);
                wei.Courses.Add(map524);
                jill.Courses.Add(map524);

                dc.SaveChanges();


                // 4) Bill teaches  WIN210
                Course win210 = new Course();
                win210.CourseCode = "WIN210";
                win210.CourseId = 16;
                win210.CourseName = "Windows administration";
                win210.Faculty = bill;
                win210.RoomNumber = "1616";
                win210.Students = new List<Student>();
                win210.Students.Add(john);
                win210.DateEnd = new DateTime(2014, 3, 31);
                win210.DateStart = new DateTime(2014, 1, 10);
                dc.Courses.Add(win210);

                bill.Courses.Add(win210);
                john.Courses.Add(win210);

                dc.SaveChanges();


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
                    }
                output2 = output1;
                throw;
            } // catch
        }
        protected override void Seed(DataContext dc) {
            InitializeIdentityForEF(dc);
            base.Seed(dc);
        }
    }
}