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
    
    public partial class Section1
    {
        public int Sections_id { get; set; }
        public string EmisCode { get; set; }
        public string DateOfVisit { get; set; }
        public string Entity { get; set; }
        public string Section { get; set; }
        public string Class { get; set; }
        public Nullable<int> NoOfSections { get; set; }
    
        public virtual Class Class1 { get; set; }
        public virtual Date Date { get; set; }
        public virtual Entity Entity1 { get; set; }
        public virtual Section Section2 { get; set; }
    }
}
