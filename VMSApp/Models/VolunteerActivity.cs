//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VMSApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class VolunteerActivity
    {
        public VolunteerActivity()
        {
            this.VolunteerActivityJobs = new HashSet<VolunteerActivityJob>();
        }
    
        public int ActivityId { get; set; }
        public string Name { get; set; }
        public Nullable<int> OrgId { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> StartDateTime { get; set; }
        public Nullable<System.DateTime> EndDateTime { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
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
    
        public virtual ICollection<VolunteerActivityJob> VolunteerActivityJobs { get; set; }
        public virtual VolunteerOrganization VolunteerOrganization { get; set; }
    }
}
