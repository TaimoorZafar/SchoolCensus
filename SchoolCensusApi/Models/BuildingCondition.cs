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
    
    public partial class BuildingCondition
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BuildingCondition()
        {
            this.InfraStructures = new HashSet<InfraStructure>();
        }
    
        public int BuildingCondition_id { get; set; }
        public string BuildingCondition1 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InfraStructure> InfraStructures { get; set; }
    }
}