using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace VMSApp.ViewModels
{
    public class OrganizationViewModel :
UserViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$")]
        public string Phone { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }

        public List<ActivityViewModel> Activities { get; set; }

        public List<JobViewModel> Jobs { get; set; }
        public List<ShiftViewModel> Shifts { get; set; }
       
    }
}