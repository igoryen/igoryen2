using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace igoryen2.ViewModels {

    // v2
    public class FacultyBase {
        [Key]
        public int Id { get; set; }
        public string SenecaId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Caption { get { return FirstName + " " + LastName; } }

    }
}