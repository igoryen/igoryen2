using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using igoryen2.Models;


namespace igoryen2.ViewModels {

    // v3
    public class StudentBase {
        [Key]
        public int Id { get; set; }
        public string SenecaId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserId { get; set; }
        public string Caption { get { return FirstName + " " + LastName + " (" + SenecaId + ")"; } }
    }

    // v2
    public class StudentFull : StudentBase {
        public List<CourseBase> Courses { get; set; }
    }
}