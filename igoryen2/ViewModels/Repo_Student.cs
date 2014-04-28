using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace igoryen2.ViewModels {

    public class Repo_Student : RepositoryBase {

        // v2
        public SelectList getSelectListOfStudent() {
            IEnumerable<StudentBase> lsb = getListOfStudentBase();
            SelectList sl = new SelectList(lsb, "Id", "Caption");
            return sl;
        }

        // v1
        public IEnumerable<StudentBase> getListOfStudentBase() {
            var students = dc.Students.OrderBy(student => student.PersonLastName);
            if (students == null) return null;
            List<StudentBase> lsb = new List<StudentBase>();
            foreach (var item in students) {
                StudentBase sb = new StudentBase();
                sb.FirstName = item.PersonFirstName;
                sb.LastName = item.PersonLastName;
                sb.SenecaId = item.SenecaId;
                sb.UserId = item.Id;
                lsb.Add(sb);
            }

            return lsb.ToList();
        }
    }
}