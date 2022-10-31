using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VMSApp.ViewModels
{
    public class JobViewModel
    {
        public int Id;

        [Required]
        [Display(Name="Job Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Job Description")]
        public string Description { get; set; }

        [Required]
        public string Country { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string City { get; set; }

        public List<SelectListItem> Countries { get; set; }
        public List<SelectListItem> States { get; set; }
        public List<SelectListItem> Cities { get; set; }

        public List<ShiftViewModel> Shifts { get; set; }

        public ActivityViewModel Activity { get; set; }
    }
}