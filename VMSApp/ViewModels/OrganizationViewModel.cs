using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace VMSApp.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$")]
        public string Phone { get; set; }


        public string Description { get; set; }
        public string Website { get; set; }
        [Required]

        public string Country { get; set; }
        [Required]

        public string State { get; set; }
        [Required]

        public string City { get; set; }
        [Required]

        public string PinCode { get; set; }
        [Required]

        public string Address { get; set; }
        [Required]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Please Enter a Valid Email Address")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Password must have: \nMinimum 8 characters in length.\n At least one uppercase English letter.\n At least one lowercase English letter.\n At least one digit.\n At least one special character")]
        public string Password { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Password must have: \nMinimum 8 characters in length.\n At least one uppercase English letter.\n At least one lowercase English letter.\n At least one digit.\n At least one special character")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password does not match")]
        public string ConfirmPassword { get; set; }


        public List<SelectListItem> Countries { get; set; }
        public List<SelectListItem> States { get; set; }
        public List<SelectListItem> Cities { get; set; }
    }
}