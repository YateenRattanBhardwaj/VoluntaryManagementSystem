using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VMSApp.ViewModels
{
    public class ActivityViewModel
    {
        [Required]
        [Display(Name="Activity Name")]
        public string Name { get; set; }

        public List<JobViewModel> Jobs { get; set; }
    }
}