using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace igoryen2.ViewModels {

    // v2
    public class CancellationCreateForHttpGet {
        [Key]
        public int CancellationId { get; set; }
        public SelectList SelectListOfCourse { get; set; }
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> Date { get; set; }
        public string Message { get; set; }
        public void Clear() {
            Date = (DateTime?)DateTime.Now;
            Message = string.Empty;
        }
    }

    // v2
    public class CancellationCreateForHttpPost {
        [Key]
        public int CancellationId { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> Date { get; set; }
        [Required(ErrorMessage = "Select a course")]
        public int CourseId { get; set; }
    }

}