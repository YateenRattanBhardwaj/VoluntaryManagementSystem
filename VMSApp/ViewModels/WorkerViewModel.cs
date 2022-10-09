using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VMSApp.ViewModels
{
    public class AdminNWorkerViewModel :
UserViewModel
    {
        [Required]

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]

        public string LastName { get; set; }
    }
}