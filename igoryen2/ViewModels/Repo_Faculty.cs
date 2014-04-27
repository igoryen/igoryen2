using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace igoryen2.ViewModels {
    public class Repo_Faculty : RepositoryBase {

        // v1
        public SelectList getSelectListOfFaculty() {
            IEnumerable<FacultyBase> lfb = getListOfFacultyBase();
            SelectList sl = new SelectList(lfb, "Id", "FirstName", "LastName");
            return sl;
        }

        // v1
        public IEnumerable<FacultyBase> getListOfFacultyBase() {
            var faculties = dc.Faculties.OrderBy(faculty => faculty.PersonLastName);
            if (faculties == null) return null;
            List<FacultyBase> lsb = new List<FacultyBase>();
            foreach (var item in faculties) {
                FacultyBase sb = new FacultyBase();
                sb.FirstName = item.PersonFirstName;
                sb.LastName = item.PersonLastName;
                sb.SenecaId = item.SenecaId;
                lsb.Add(sb);
            }

            return lsb.ToList();
        }
    }
}