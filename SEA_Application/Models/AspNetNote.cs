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
    using System.ComponentModel;

    public partial class AspNetNote
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AspNetNote()
        {
            this.AspNetNotesOrders = new HashSet<AspNetNotesOrder>();
        }
    
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Nullable<int> SubjectID { get; set; }
        [DisplayName("Course Type")]
        public string CourseType { get; set; }
        public Nullable<int> Price { get; set; }
        [DisplayName("Creation Date")]

        public Nullable<System.DateTime> CreationDate { get; set; }
        public virtual AspNetSubject AspNetSubject { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AspNetNotesOrder> AspNetNotesOrders { get; set; }
    }
}