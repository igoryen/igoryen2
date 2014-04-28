using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace igoryen2.ViewModels {

    public class Repo_Student : RepositoryBase {
        private Repo_Faculty rf = new Repo_Faculty();


        // v2
        public SelectList getSelectListOfStudent() {
            IEnumerable<StudentBase> lsb = getListOfStudentBase();
            SelectList sl = new SelectList(lsb, "Id", "Caption");
            return sl;
        }

        // v1
        public IEnumerable<StudentBase> getListOfStudentBase() {
            var students = dc.Students.OrderBy(student => student.PersonLastName);
            if (students == null) return null;
            List<StudentBase> lsb = new List<StudentBase>();
            foreach (var item in students) {
                StudentBase sb = new StudentBase();
                sb.FirstName = item.PersonFirstName;
                sb.LastName = item.PersonLastName;
                sb.SenecaId = item.SenecaId;
                sb.UserId = item.Id;
                lsb.Add(sb);
            }

            return lsb.ToList();
        }

        //v1
        public StudentFull getStudentFull(int? StudentId) {
            var dbStudent = dc.Students.Include("Courses").FirstOrDefault(s => s.PersonId == StudentId);
            if (dbStudent == null) return null;
            StudentFull sf = new StudentFull();
            sf.Courses = new List<CourseFull>();
            List<CourseFull> lcf = new List<CourseFull>();
            foreach (var item in dbStudent.Courses) {
                CourseFull cf = new CourseFull();
                cf.CourseCode = item.CourseCode;
                cf.CourseId = item.CourseId;
                cf.CourseName = item.CourseName;
                cf.DateEnd = item.DateEnd;
                cf.DateStart = item.DateStart;
                var faculty = dc.Courses.Include("Faculty").FirstOrDefault(c => c.CourseId == item.CourseId).Faculty;
                cf.Faculty = rf.getFacultyFull(faculty.Id);
                cf.RoomNo = item.RoomNumber;
            }
            sf.Courses = lcf;
            sf.FirstName = dbStudent.PersonFirstName;
            sf.Id = dbStudent.PersonId;
            sf.LastName = dbStudent.PersonLastName;
            sf.SenecaId = dbStudent.SenecaId;
            sf.UserId = dbStudent.UserName;
            return sf;
        }
    }
}