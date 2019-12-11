//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SEA_Application.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TeacherPenaltyRecord
    {
        public int Id { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public string PenaltyType { get; set; }
        public string Description { get; set; }
        public Nullable<double> Amount { get; set; }
        public string Months { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> Time { get; set; }
        public Nullable<int> SessionID { get; set; }
    
        public virtual AspNetEmployee AspNetEmployee { get; set; }
        public virtual AspNetSession AspNetSession { get; set; }
    }
}