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
    
    public partial class EMI
    {
        public int Emis_id { get; set; }
        public string EmisCode { get; set; }
        public string NameOfSchool { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
    
        public virtual EMI EMIS1 { get; set; }
        public virtual EMI EMI1 { get; set; }
    }
}