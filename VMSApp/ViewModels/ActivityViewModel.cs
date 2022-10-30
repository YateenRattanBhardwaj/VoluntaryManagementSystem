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


        public string Description { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
       
        public string Address { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Pincode { get; set; }
        public string ContactNo { get; set; }
        public string ContactPersonName { get; set; }
        public string ContactPersonAddress { get; set; }
        public string ContactPersonCountry { get; set; }
        public string ContactPersonState { get; set; }
        public string ContactPersonCity { get; set; }
        public string ContactPersonPinCode { get; set; }
    
        public List<JobViewModel> Jobs { get; set; }
    }
}