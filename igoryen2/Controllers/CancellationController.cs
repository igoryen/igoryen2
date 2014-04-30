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
    public class CancellationController : Controller {
        private DataContext db = new DataContext();
        private UserManager<ApplicationUser> manager;
        private Repo_Course rc = new Repo_Course();
        static CancellationCreateForHttpGet cancellationToCreate = new CancellationCreateForHttpGet();
        private Repo_Cancellation rcc = new Repo_Cancellation();
        private VM_Error vme = new VM_Error();
        private CancellationEditForHttpGet cancellationToEdit = new CancellationEditForHttpGet();
        private Repo_Faculty rf = new Repo_Faculty();

        // v6
        // GET: /Cancellation/
        public ActionResult Index() {
            var currentUser = manager.FindById(User.Identity.GetUserId());
            IEnumerable<Cancellation> cancellations = new List<Cancellation>();

            if (User.IsInRole("Student")) {
                cancellations = db.Cancellations.Include("Creator").Where(c => c.Students.Any(s => s.UserId == currentUser.Id)).OrderBy(c => c.Date);
                int count = cancellations.Count();
            }
            if (User.IsInRole("Faculty")) {
                cancellations = db.Cancellations.Where(c => c.Creator.Id == currentUser.Id).OrderBy(c => c.Date);
            }
            if (User.IsInRole("Admin")) {
                cancellations = db.Cancellations.Include("Creator").OrderBy(c => c.Date);
            }
            return View(cancellations.ToList());
        }

        // GET: /Cancellation/Details/5
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cancellation cancellation = db.Cancellations.Find(id);
            if (cancellation == null) {
                return HttpNotFound();
            }
            return View(cancellation);
        }

        // v3
        // GET: /Cancellation/Create
        public ActionResult Create() {
            if (User.IsInRole("Faculty")) {
                var currentuser = manager.FindById(User.Identity.GetUserId());
                cancellationToCreate.SelectListOfCourse = rc.getSelectListOfCourse(currentuser.Id);
            }
            if (User.IsInRole("Admin")) {
                cancellationToCreate.SelectListOfCourse = rc.getSelectListOfCourse();
                cancellationToCreate.SelectListOfFaculty = rf.getSelectListOfFaculty();
            }
            if (cancellationToCreate.SelectListOfCourse == null) {
                var errors = new ViewModels.VM_Error();
                errors.ErrorMessages["ExceptionMessage"] = "This faculty has no courses";
                return View("Error", errors);
            }
            return View(cancellationToCreate);
        }

        // v7
        // POST: /Cancellation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CancellationCreateForHttpPost newItem) {
            var currentUser = manager.FindById(User.Identity.GetUserId());
            if (ModelState.IsValid && newItem.CourseId != -1) {
                var cancellation = rcc.buildCancellation(newItem);
                if (User.IsInRole("Faculty")) {
                    cancellation.Creator = currentUser;
                }
                if (User.IsInRole("Admin")) {
                    var faculty = db.Faculties.SingleOrDefault(f => f.PersonId == newItem.FacultyId);
                    cancellation.Creator = faculty;
                }
                db.Cancellations.Add(cancellation);
                try {
                    db.SaveChanges();
                }
                //---------------------------------------
                catch (DbEntityValidationException e) {
                    //----------------------------------------------------------
                    List<string> output1 = new List<string>();
                    List<string> output2 = new List<string>();
                    foreach (var eve in e.EntityValidationErrors) {
                        output1.Add("Entity of type " + eve.Entry.Entity.GetType().Name + " in state " + eve.Entry.State + " has the following validation errors:");
                        foreach (var ve in eve.ValidationErrors) {
                            output1.Add("- Property: " + ve.PropertyName + ", Error: " + ve.ErrorMessage);
                        } // foreach()

                        /*
                        Console.WriteLine("======================================");
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors) {
                          Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                              ve.PropertyName, ve.ErrorMessage);
                        }
                         */
                    } // foreach
                    output2 = output1;
                    throw;
                } // catch
                //---------------------------------------
                var createdCancellation = rcc.getCancellation(cancellation.CancellationId);
                if (createdCancellation == null) {
                    return View("Error", vme.GetErrorModel(null, ModelState));
                }
                else {
                    //cancellationToCreate.Clear();
                    return RedirectToAction("Details", new { id = createdCancellation.CancellationId });
                }
            }
            else {
                if (newItem.CourseId == -1)
                    ModelState.AddModelError("SelectListOfCourse", "Select a Course");
                //if (newItem.GenreId == null) ModelState.AddModelError("GenreSelectList", "Select One or More Genres");
                if (newItem.FacultyId == -1)
                    ModelState.AddModelError("SelectListOfFaculty", "Select a faculty");
                cancellationToCreate.Date = newItem.Date;
                cancellationToCreate.Message = newItem.Message;

                return View(cancellationToCreate);
            }
        }

        // v2
        // GET: /Cancellation/Edit/5
        public ActionResult Edit(int? id) {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var currentUser = manager.FindById(User.Identity.GetUserId());

            Cancellation cancellation = db.Cancellations.Find(id);
            if (cancellation == null) return HttpNotFound();

            cancellationToEdit.CancellationId = cancellation.CancellationId;
            cancellationToEdit.Creator = cancellation.Creator;
            cancellationToEdit.Date = cancellation.Date;
            cancellationToEdit.Message = cancellation.Message;

            var courses = db.Courses.Where(course => course.Faculty.Id == currentUser.Id).ToList();
            //SelectList sl = new SelectList(rc.getSelectListOfCourse(currentuser.Id));
            string selected = cancellation.CourseCode;
            cancellationToEdit.SelectListOfCourse = new SelectList(courses, "CourseId", "CourseCode", selected);
            return View(cancellationToEdit);
        }

        // v1
        // POST: /Cancellation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CancellationEditForHttpPost newItem) {
            var currentUser = manager.FindById(User.Identity.GetUserId());
            Cancellation cancellationOld = new Cancellation();
            cancellationOld = db.Cancellations.Include("Students").FirstOrDefault(cc => cc.CancellationId == newItem.CancellationId);
            Cancellation cancellationNew = new Cancellation();
            if (ModelState.IsValid) {
                //if(cancellationOld.CancellationId != newItem.CancellationId){
                    cancellationNew.CancellationId = newItem.CancellationId;
                //}
                Course course = new Course();
                course = db.Courses.AsNoTracking().Include("Students").FirstOrDefault(c => c.CourseId == newItem.CourseId);
                //if(cancellationOld.CourseCode != course.CourseCode) {
                    cancellationNew.CourseCode = course.CourseCode;
                //}
                //if(cancellationOld.CourseId != newItem.CourseId) {
                    cancellationNew.CourseId = newItem.CourseId;
                //}
                //if(cancellationOld.Creator != newItem.Creator) {
                    //cancellationNew.Creator = newItem.Creator;
                    cancellationNew.Creator = currentUser;
                //}
                //if(cancellationOld.Date != newItem.Date) {
                    cancellationNew.Date = newItem.Date;
                //}
                //if(cancellationOld.Message != newItem.Message) {
                    cancellationNew.Message = newItem.Message;
                //}                

                cancellationNew.Students = new List<StudentBase>();
                List<StudentBase> lsbNew = new List<StudentBase>();

                foreach (var studentNew in course.Students) {
                    StudentBase sbNew = new StudentBase();

                    //foreach(var studentOld in cancellationOld.Students){
                        //if (studentOld.UserId != studentNew.Id) {
                            sbNew.FirstName = studentNew.PersonFirstName;
                            sbNew.Id = studentNew.PersonId;
                            sbNew.LastName = studentNew.PersonLastName;
                            sbNew.SenecaId = studentNew.SenecaId;
                            sbNew.UserId = studentNew.Id;

                            lsbNew.Add(sbNew);
                        //}
                    //}
                }
                cancellationNew.Students = lsbNew;
                db.Detach(cancellationOld);
                db.Entry(cancellationNew).State = EntityState.Modified;
                db.SaveChanges();

                //------------------------------------
                var editedCancellation = rcc.getCancellation(cancellationNew.CancellationId);
                if (editedCancellation == null) {
                    return View("Error", vme.GetErrorModel(null, ModelState));
                }
                else {
                    //cancellationToCreate.Clear();
                    return RedirectToAction("Details", new { id = editedCancellation.CancellationId });
                }
                //------------------------------------
            }
            else {
                cancellationToEdit.Date = newItem.Date;
                cancellationToEdit.Message = newItem.Message;

                return View(cancellationToEdit);
            }
        }

        // v2
        // GET: /Cancellation/Delete/5
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cancellation cancellation = db.Cancellations.Include("Creator").Include("Students").SingleOrDefault(c => c.CancellationId == id);
            if (cancellation == null) {
                return HttpNotFound();
            }
            return View(cancellation);
        }

        // v2
        // POST: /Cancellation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            Cancellation cancellation = db.Cancellations.Include("Creator").Include("Students").SingleOrDefault(c => c.CancellationId == id);
            db.Cancellations.Remove(cancellation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public CancellationController() {
            db = new DataContext();
            manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }
    }
}
