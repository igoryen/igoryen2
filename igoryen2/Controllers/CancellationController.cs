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

        // v2
        // GET: /Cancellation/
        public ActionResult Index() {
            var currentUser = manager.FindById(User.Identity.GetUserId());
            IEnumerable<Cancellation> cancellations = new List<Cancellation>();

            if (User.IsInRole("Student")) {
                cancellations = db.Cancellations.Where(c => c.Students.Any(s => s.Id == currentUser.Id));
            }
            if (User.IsInRole("Faculty")) {
                cancellations = db.Cancellations.Where(c => c.Creator.Id == currentUser.Id);
            }
            if (User.IsInRole("Admin")) {
                cancellations = db.Cancellations;
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

        // GET: /Cancellation/Create
        public ActionResult Create() {
            var currentuser = manager.FindById(User.Identity.GetUserId());
            cancellationToCreate.SelectListOfCourse = rc.getSelectListOfCourse(currentuser.Id);
            return View(cancellationToCreate);
        }

        // v6
        // POST: /Cancellation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CancellationCreateForHttpPost newItem) {
            var currentUser = manager.FindById(User.Identity.GetUserId());
            if (ModelState.IsValid && newItem.CourseId != -1) {
                var cancellation = rcc.buildCancellation(newItem);
                cancellation.Creator = currentUser;
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
                    ModelState.AddModelError("CourseSelectList", "Select a Course");
                //if (newItem.GenreId == null) ModelState.AddModelError("GenreSelectList", "Select One or More Genres");

                cancellationToCreate.Date = newItem.Date;
                cancellationToCreate.Message = newItem.Message;

                return View(cancellationToCreate);
            }
        }


        // GET: /Cancellation/Edit/5
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cancellation cancellation = db.Cancellations.Find(id);
            if (cancellation == null) {
                return HttpNotFound();
            }
            return View(cancellation);
        }

        // POST: /Cancellation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CancellationId,Date,Message")] Cancellation cancellation) {
            if (ModelState.IsValid) {
                db.Entry(cancellation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cancellation);
        }

        // GET: /Cancellation/Delete/5
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cancellation cancellation = db.Cancellations.Find(id);
            if (cancellation == null) {
                return HttpNotFound();
            }
            return View(cancellation);
        }

        // POST: /Cancellation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            Cancellation cancellation = db.Cancellations.Find(id);
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
