using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using igoryen2.Models;

namespace igoryen2.Controllers {
    public class CancellationController : Controller {
        private DataContext db = new DataContext();

        // GET: /Cancellation/
        public ActionResult Index() {
            return View(db.Cancellations.ToList());
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
            return View();
        }

        // POST: /Cancellation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CancellationId,Date,Message")] Cancellation cancellation) {
            if (ModelState.IsValid) {
                db.Cancellations.Add(cancellation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cancellation);
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
    }
}
