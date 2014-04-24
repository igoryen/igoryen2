using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace igoryen2.Models {

    //v2
    public class Cancellation {
        public int CancellationId { get; set; }
        public Faculty Faculty { get; set; }
        public int CourseId { get; set; }
        public string Date { get; set; }
        public string Message { get; set; }
    }

    public class ComMethod {
        public int ComMethodId { get; set; }
        public string Handle { get; set; }
        public string CellNo { get; set; }
        public string Email { get; set; }
    }

    public class Course {
        public Course() {
            this.Faculty = new Faculty();
            this.Students = new List<Student>();
        }
        [Key]
        public int CourseId { get; set; }
        [Required]
        public string CourseCode { get; set; }
        [Required]
        public string CourseName { get; set; }
        public string RoomNumber { get; set; }
        public string TimeStart { get; set; }
        public string TimeEnd { get; set; }
        public Faculty Faculty { get; set; }
        public List<Student> Students { get; set; }
    }

    public class Faculty : Person {
        public Faculty() {
            this.Courses = new List<Course>();
            SenecaId = string.Empty;
        }

        public Faculty(string fname, string lname, string phone, string senId)
            : base(fname, lname, phone) {
            this.Courses = new List<Course>();
            SenecaId = senId;
        }

        [Required]
        [RegularExpression("^[0][0-9]{8}$", ErrorMessage = "0 followed by 8 digits")]
        public string SenecaId { get; set; }
        public List<Course> Courses { get; set; }
    }

    public class Person : ApplicationUser {

        public Person() {
            FirstName = LastName = Phone = string.Empty;
        }

        public Person(string f, string l, string p) {
            FirstName = f;
            LastName = l;
            Phone = p;
        }

        [Key]
        public int PersonId { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 3)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [RegularExpression("^[2-9]\\d{2}-\\d{3}-\\d{4}$", ErrorMessage = "nnn-nnn-nnnn")]
        public string Phone { get; set; }
    }

    public class Student : Person {
        public Student() {
            SenecaId = string.Empty;
            this.Courses = new List<Course>();
        }

        public Student(string f, string l, string p, string senId)
            : base(f, l, p) {
            SenecaId = senId;
        }

        [Required]
        [RegularExpression("^[0][0-9]{8}$", ErrorMessage = "0 followed by 8 digits")]
        public string SenecaId { get; set; }
        public List<Course> Courses { get; set; }
        //public List<ComMethod> ComMethods { get; set; }
    }


    public class MyUserInfo {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}