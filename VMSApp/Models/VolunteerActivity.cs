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
    
        public virtual ICollection<VolunteerActivityJob> VolunteerActivityJobs { get; set; }
        public virtual VolunteerOrganization VolunteerOrganization { get; set; }
    }
}