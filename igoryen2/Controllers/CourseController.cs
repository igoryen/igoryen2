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

namespace igoryen2.Controllers {
    public class CourseController : Controller {
        private DataContext db = new DataContext();
        private UserManager<ApplicationUser> manager;
        static CourseCreateForHttpGet courseToCreate = new CourseCreateForHttpGet();


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

        // GET: /Course/Details/5
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null) {
                return HttpNotFound();
            }
            return View(course);
        }

        //v2
        // GET: /Course/Create
        public ActionResult Create() {
            var currentUser = manager.FindById(User.Identity.GetUserId());
            courseToCreate.SelectListOfStudent = rs.getSelectListOfStudent(currentUser.Id);
            courseToCreate.SelectListOfFaculty = rf.getSelectListOfFaculty(currentUser.Id);
            return View(courseToCreate);
        }

        // POST: /Course/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseId,CourseCode,CourseName,RoomNumber,TimeStart,TimeEnd")] Course course) {
            if (ModelState.IsValid) {
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(course);
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
