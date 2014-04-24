using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using igoryen2.Models;


namespace igoryen2.ViewModels {

    // v1
    public class StudentBase {
        [Key]
        public string SenecaId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Id { get; set; }
    }

    // v1
    public class StudentNormal : StudentBase {
        public List<CourseBase> Courses { get; set; }
    }
}