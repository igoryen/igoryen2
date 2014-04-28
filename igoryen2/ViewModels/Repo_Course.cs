﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace igoryen2.ViewModels {
    public class Repo_Course : RepositoryBase {

        private Repo_Faculty rf = new Repo_Faculty();
        private Repo_Student rs = new Repo_Student();

        public IEnumerable<CourseBase> getListOfCourseBase(string currentUserId) {
            var courses = dc.Courses.Where(course => course.Faculty.Id == currentUserId).ToList();
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
            var lcb = new List<CourseBase>();
            lcb.Add(new CourseBase {
                CourseCode = "Select a course code",
                CourseId = -1
            });
            foreach (var item in getListOfCourseBase(currentUserId)) {
                lcb.Add(item);
            }
            SelectList sl = new SelectList(lcb.ToList(), "CourseId", "CourseCode");
            return sl;
        }

        //v1
        public CourseFull getCourseFull(int? CourseId) {
            if (CourseId == null) return null;
            var course = dc.Courses.Include("Students").Include("Faculty").SingleOrDefault(c => c.CourseId == CourseId);
            if (course == null) return null;
            CourseFull cf = new CourseFull();
            cf.CourseCode = course.CourseCode;
            cf.CourseId = course.CourseId;
            cf.CourseName = course.CourseName;
            cf.DateEnd = course.DateEnd;
            cf.DateStart = course.DateStart;
            cf.Faculty = rf.getFacultyFull(course.Faculty.PersonId);
            cf.RoomNo = course.RoomNumber;
            List<StudentFull> lsf = new List<StudentFull>();
            foreach(var item in course.Students){
                lsf.Add(rs.getStudentFull(item.PersonId));
            }
            cf.Students = lsf;
            return cf;
        }

    }
}