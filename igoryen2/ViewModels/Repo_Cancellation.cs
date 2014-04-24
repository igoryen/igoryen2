using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using igoryen2.Models;


namespace igoryen2.ViewModels {
    public class Repo_Cancellation {
        private DataContext db = new DataContext();


        // v2
        public Cancellation buildCancellation(CancellationCreateForHttpPost newItem) {
            Cancellation cancellation = new Cancellation();
            cancellation.CancellationId = newItem.CancellationId;
            cancellation.Course = db.Courses.Find(newItem.CourseId);
            cancellation.Date = newItem.Date;
            cancellation.Message = newItem.Message;

            return cancellation;
        }

        // v1
        public Cancellation getCancellation(int? CancellationId) {
            if (CancellationId == null) return null;
            var cancellation = db.Cancellations.SingleOrDefault(c => c.CancellationId == CancellationId);
            if (cancellation == null) return null;

            return cancellation;
        }

    }
}