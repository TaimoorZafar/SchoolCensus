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
    
    public partial class EmergencyContact
    {
        public int EmergencyContacts_id { get; set; }
        public string SurveyID { get; set; }
        public string Entity { get; set; }
        public string Section { get; set; }
        public string Name { get; set; }
        public string Profession { get; set; }
        public Nullable<decimal> ContactNo { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
    
        public virtual Entity Entity1 { get; set; }
        public virtual Section Section1 { get; set; }
        public virtual Survey Survey { get; set; }
    }
}
