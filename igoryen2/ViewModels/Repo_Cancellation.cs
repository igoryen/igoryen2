using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using igoryen2.Models;


namespace igoryen2.ViewModels {
    public class Repo_Cancellation {
        private DataContext db = new DataContext();


        // v6
        public Cancellation buildCancellation(CancellationCreateForHttpPost newItem) {

            Cancellation cancellation = new Cancellation();
            cancellation.CancellationId = newItem.CancellationId;
            Course course = new Course();
            course = db.Courses.AsNoTracking().Include("Students").FirstOrDefault(c => c.CourseId == newItem.CourseId);
            cancellation.CourseId = course.CourseId;
            cancellation.Date = newItem.Date;
            cancellation.Message = newItem.Message;
            cancellation.Students = new List<StudentBase>();

            List<StudentBase> lsb = new List<StudentBase>();
            foreach (var student in course.Students) {
                StudentBase sb = new StudentBase();
                sb.UserId = student.Id;
                sb.FirstName = student.PersonFirstName;
                sb.LastName = student.PersonLastName;
                sb.SenecaId = student.SenecaId;
                lsb.Add(sb);
            }
            cancellation.Students = lsb;

            return cancellation;
        }

        // v3
        public Cancellation getCancellation(int? CancellationId) {
            if (CancellationId == null) return null;
            var cancellation = db.Cancellations.Include("Students").SingleOrDefault(c => c.CancellationId == CancellationId);
            if (cancellation == null) return null;

            return cancellation;
        }

    }
}