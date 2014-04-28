using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace igoryen2.ViewModels {

    //v2
    public class CourseBase {
        [Key]
        public int CourseId { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
    }

    //v1
    public class CourseFull : CourseBase {
        public string RoomNo { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DateStart { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DateEnd { get; set; }
        public FacultyFull Faculty { get; set; }
        public List<StudentFull> Students { get; set; }

        public CourseFull() {
            this.Faculty = new FacultyFull();
            this.Students = new List<StudentFull>();
        }
    }

    //v2
    public class CourseCreateForHttpGet {
        [Key]
        public int CourseId { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public string RoomNo { get; set; }
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DateStart { get; set; }
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DateEnd { get; set; }
        public SelectList SelectListOfFaculty { get; set; }
        public SelectList SelectListOfStudent { get; set; }

        public void Clear() {
            DateStart = (DateTime?)DateTime.Now;
            DateEnd = (DateTime?)DateTime.Now;
        }
    }

    //v3
    public class CourseCreateForHttpPost {
        [Key]
        public int CourseId { get; set; }
        [Required]
        public string CourseCode { get; set; }
        [Required]
        public string CourseName { get; set; }
        [Required]
        public string RoomNo { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DateStart { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DateEnd { get; set; }
        [Required(ErrorMessage = "Select a Faculty")]
        public int FacultyId { get; set; }
        [Required(ErrorMessage = "Select One or More Students")]
        public virtual ICollection<int> StudentIds { get; set; }

    }
}