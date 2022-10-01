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
    
    public partial class VolunteerShift
    {
        public VolunteerShift()
        {
            this.ShiftWorkers = new HashSet<ShiftWorker>();
        }
    
        public int ShiftId { get; set; }
        public string ShitName { get; set; }
        public string Description { get; set; }
        public System.DateTime StartDateTime { get; set; }
        public System.DateTime StopDateTime { get; set; }
        public Nullable<int> JobId { get; set; }
        public int RequiredVolunteers { get; set; }
    
        public virtual ICollection<ShiftWorker> ShiftWorkers { get; set; }
        public virtual VolunteerActivityJob VolunteerActivityJob { get; set; }
    }
}
