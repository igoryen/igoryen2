using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using igoryen2.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using igoryen2.ViewModels;
using System.Data.Entity.Validation;


namespace igoryen2.Controllers {
    public class CourseController : Controller {
        private DataContext db = new DataContext();
        private UserManager<ApplicationUser> manager;
        static CourseCreateForHttpGet courseToCreate = new CourseCreateForHttpGet();
        private Repo_Student rs = new Repo_Student();
        private Repo_Faculty rf = new Repo_Faculty();
        private Repo_Course rc = new Repo_Course();
        private VM_Error vme = new VM_Error();


        //v3
        // GET: /Course/
        public ActionResult Index() {
            var currentUser = manager.FindById(User.Identity.GetUserId());
            IEnumerable<Course> courses = new List<Course>();

            if (User.IsInRole("Student")) {
                courses = db.Courses.Include("Faculty").Where(c => c.Students.Any(s => s.Id == currentUser.Id));
            }
            if (User.IsInRole("Faculty")) {
                courses = db.Courses.Where(c => c.Faculty.Id == currentUser.Id);
            }
            if (User.IsInRole("Admin")) {
                courses = db.Courses.Include("Faculty");
            }
            return View(courses.ToList());
        }

        // v2
        // GET: /Course/Details/5
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Include("Faculty").Include("Students").SingleOrDefault(c => c.CourseId == id);
            if (course == null) {
                return HttpNotFound();
            }
            return View(course);
        }

        //v3
        // GET: /Course/Create
        public ActionResult Create() {
            var currentUser = manager.FindById(User.Identity.GetUserId());
            courseToCreate.SelectListOfStudent = rs.getSelectListOfStudent();
            courseToCreate.SelectListOfFaculty = rf.getSelectListOfFaculty();
            return View(courseToCreate);
        }

        //v3
        // POST: /Course/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseCreateForHttpPost newItem) {

            if (ModelState.IsValid && newItem.FacultyId != -1) {
                var builtCourse = rc.buildCourse(newItem);
                if (builtCourse == null) {
                    return View("Error", vme.GetErrorModel(null, ModelState));
                }
                else {
                    //courseToCreate.Clear();
                    return RedirectToAction("Details", new { id = builtCourse.CourseId });
                }
            }
            else{
                courseToCreate.CourseCode = newItem.CourseCode;
                courseToCreate.CourseId = newItem.CourseId;
                courseToCreate.CourseName = newItem.CourseName;
                courseToCreate.DateEnd = newItem.DateEnd;
                courseToCreate.DateStart = newItem.DateStart;
                courseToCreate.RoomNo = newItem.RoomNo;
                if (newItem.FacultyId == -1){
                    ModelState.AddModelError("SelectListOfFaculty", "Select a Faculty");
                }
                if (newItem.StudentId == null){
                    ModelState.AddModelError("SelectListOfStudent", "Select one or more Students");
                }
                return View(courseToCreate);
            }
        }



        // GET: /Course/Edit/5
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null) {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: /Course/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseId,CourseCode,CourseName,RoomNumber,TimeStart,TimeEnd")] Course course) {
            if (ModelState.IsValid) {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(course);
        }

        // GET: /Course/Delete/5
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null) {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: /Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // v1
        public CourseController() {
            db = new DataContext();
            manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }
    }
}
