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
    
    public partial class Worker
    {
        public Worker()
        {
            this.ShiftWorkers = new HashSet<ShiftWorker>();
        }
    
        public int WorkerId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> UserId { get; set; }
    
        public virtual ICollection<ShiftWorker> ShiftWorkers { get; set; }
        public virtual Status Status1 { get; set; }
        public virtual User User { get; set; }
    }
}
