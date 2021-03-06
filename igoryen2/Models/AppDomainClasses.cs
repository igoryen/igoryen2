﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using igoryen2.ViewModels;


namespace igoryen2.Models {

    //v5
    public class Cancellation {
        public int CancellationId { get; set; }
        public ApplicationUser Creator { get; set; }
        public int CourseId { get; set; }
        public string CourseCode { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> Date { get; set; }
        public string Message { get; set; }
        public List<StudentBase> Students { get; set; }
    }

    public class ComMethod {
        public int ComMethodId { get; set; }
        public string Handle { get; set; }
        public string CellNo { get; set; }
        public string Email { get; set; }
    }

    //v3
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
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DateStart { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DateEnd { get; set; }
        public Faculty Faculty { get; set; }
        public List<Student> Students { get; set; }
    }

    // v2
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
        public string Caption { get { return PersonFirstName + " " + PersonLastName + " (" + SenecaId + ")"; } }

    }

    // v2
    public class Person : ApplicationUser {

        public Person() {
            PersonFirstName = PersonLastName = PersonPhone = string.Empty;
        }

        public Person(string f, string l, string p) {
            PersonFirstName = f;
            PersonLastName = l;
            PersonPhone = p;
        }

        [Key]
        public int PersonId { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 3)]
        [Display(Name = "First Name")]
        public string PersonFirstName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        [Display(Name = "Last Name")]
        public string PersonLastName { get; set; }
        [Required]
        [RegularExpression("^[2-9]\\d{2}-\\d{3}-\\d{4}$", ErrorMessage = "nnn-nnn-nnnn")]
        public string PersonPhone { get; set; }
    }

    // v2
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
        public string Caption { get { return PersonFirstName + " " + PersonLastName + " (" + SenecaId + ")"; } }

    }


    public class MyUserInfo {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}