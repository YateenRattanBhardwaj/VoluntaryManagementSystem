using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace VMSApp.ViewModels
{
    public class UserTypeViewModel
    {
        [Required]
        [Display(Name="Register As")]
        public int UserType { get; set; }

        public List<SelectListItem> UserTypes { get; set; }
    }
}