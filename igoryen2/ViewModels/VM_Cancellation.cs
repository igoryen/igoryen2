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
        public SelectList CourseSelectList { get; set; }
        [Display(Name = "Date :)")]
        public string Date { get; set; }
        public string Message { get; set; }
        public void Clear() {
            Date = string.Empty;
            Message = string.Empty;
        }
    }

}