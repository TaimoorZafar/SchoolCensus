//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SchoolCensusApi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class HardArea
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HardArea()
        {
            this.SchoolAccesses = new HashSet<SchoolAccess>();
        }
    
        public int HardAreas_id { get; set; }
        public string IsSchoolLocatedInHardAreas { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchoolAccess> SchoolAccesses { get; set; }
    }
}
