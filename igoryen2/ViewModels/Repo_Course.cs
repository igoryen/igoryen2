using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace igoryen2.ViewModels {
    public class Repo_Course : RepositoryBase {

        public SelectList getSelectListOfCourse(string currentUserId) {
            IEnumerable<CourseBase> cbs = getListOfCourseBaseAM(currentUserId);
            SelectList sl = new SelectList(cbs, "CourseId", "CourseCode");
            return sl;
        }

    }
}