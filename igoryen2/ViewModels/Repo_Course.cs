using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace igoryen2.ViewModels {
    public class Repo_Course : RepositoryBase {

        public IEnumerable<CourseBase> getListOfCourseBase(string currentUserId) {
            var courses = dc.Courses.Where(course => course.Students.Any(st => st.Id == currentUserId)).ToList();
            if (courses == null) return null;
            List<CourseBase> lcb = new List<CourseBase>();
            foreach (var item in courses) {
                CourseBase cb = new CourseBase();
                cb.CourseCode = item.CourseCode;
                cb.CourseId = item.CourseId;
                cb.CourseName = item.CourseName;
                lcb.Add(cb);
            }

            return lcb.ToList();
        }

        public SelectList getSelectListOfCourse(string currentUserId) {
            IEnumerable<CourseBase> cbs = getListOfCourseBase(currentUserId);
            SelectList sl = new SelectList(cbs, "CourseId", "CourseCode");
            return sl;
        }

    }
}