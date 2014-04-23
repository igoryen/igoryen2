using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace igoryen2.ViewModels {
    public class Repo_Course : RepositoryBase {

        public IEnumerable<CourseBase> getListOfCourseBaseAM(string currentUserId) {
            //var courses = dc.Courses.Where(course => course.User.Id == currentUserId).ToList();
            if (courses == null) return null;
            return Mapper.Map<IEnumerable<CourseBase>>(courses);
        }

        public SelectList getSelectListOfCourse(string currentUserId) {
            IEnumerable<CourseBase> cbs = getListOfCourseBaseAM(currentUserId);
            SelectList sl = new SelectList(cbs, "CourseId", "CourseCode");
            return sl;
        }

    }
}