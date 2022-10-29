using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VMSApp.ViewModels
{
    public class ShiftViewModel
    {
        [Required]
        [Display(Name="Shift Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name="Shift Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name="Shift Start Date and Time")]
        public DateTime StartDateTime { get; set; }


        [Required]
        [Display(Name = "Shift End Date and Time")]
        public DateTime StopDateTime { get; set; }


        [Required]
        [Display(Name = "Number of Volunteers Required for This Job")]
        public int RequiredVolunteers { get; set; }

        
    }
}