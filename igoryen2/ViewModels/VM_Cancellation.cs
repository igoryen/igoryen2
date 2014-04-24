using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace igoryen2.ViewModels {

    public class CancellationCreateForHttpGet {
        [Key]
        public int CancellationId { get; set; }
        public SelectList SelectListOfCourse { get; set; }
        [Display(Name = "Date :)")]
        public string Date { get; set; }
        public string Message { get; set; }
        public void Clear() {
            Date = string.Empty;
            Message = string.Empty;
        }
    }

    public class CancellationCreateForHttpPost {
        [Key]
        public int CancellationId { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public string Date { get; set; }
        [Required(ErrorMessage = "Select a course")]
        public int CourseId { get; set; }
    }

}