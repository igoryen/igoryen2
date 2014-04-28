using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace igoryen2.ViewModels {
    public class Repo_Faculty : RepositoryBase {

        // v2
        public SelectList getSelectListOfFaculty() {
            IEnumerable<FacultyBase> lfb = getListOfFacultyBase();
            SelectList sl = new SelectList(lfb, "Id", "Caption");
            return sl;
        }

        // v1
        public IEnumerable<FacultyBase> getListOfFacultyBase() {
            var faculties = dc.Faculties.OrderBy(faculty => faculty.PersonLastName);
            if (faculties == null) return null;
            List<FacultyBase> lsb = new List<FacultyBase>();
            foreach (var item in faculties) {
                FacultyBase sb = new FacultyBase();
                sb.FirstName = item.PersonFirstName;
                sb.LastName = item.PersonLastName;
                sb.SenecaId = item.SenecaId;
                lsb.Add(sb);
            }

            return lsb.ToList();
        }

        // v1
        public FacultyFull getFacultyFull(int? PersonId) {
            var dbFaculty = dc.Faculties.Include("Courses").FirstOrDefault(f => f.PersonId == PersonId);
            if (dbFaculty == null) return null;
            FacultyFull ff = new FacultyFull();
            List<CourseBase> lcb = new List<CourseBase>();
            foreach (var item in dbFaculty.Courses) {
                CourseBase cb = new CourseBase();
                cb.CourseCode = item.CourseCode;
                cb.CourseId = item.CourseId;
                cb.CourseName = item.CourseName;
                lcb.Add(cb);
            }
            ff.Courses = lcb;
            return ff;
        }
    }
}