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
    
    public partial class InfraClass
    {
        public int InfraClasses_id { get; set; }
        public string SurveyID { get; set; }
        public string Entity { get; set; }
        public string Section { get; set; }
        public string RoomType { get; set; }
        public Nullable<int> RoomNo { get; set; }
        public string HandoverOrConstructed { get; set; }
        public string Useable { get; set; }
        public Nullable<int> Fans { get; set; }
        public string Windows { get; set; }
        public string Doors { get; set; }
        public string ProperVentilation { get; set; }
        public Nullable<int> WritingBoards { get; set; }
        public Nullable<int> BulbOrLight { get; set; }
        public Nullable<int> FurnitureAvailableForNoOfStudents { get; set; }
        public string Cleanliness { get; set; }
        public string RoomName { get; set; }
        public Nullable<int> Chairs { get; set; }
        public Nullable<int> Tabes { get; set; }
        public Nullable<int> Bench { get; set; }
        public Nullable<int> Desk { get; set; }
        public Nullable<int> Mats { get; set; }
        public Nullable<int> Computers { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
    
        public virtual Entity Entity1 { get; set; }
        public virtual HandoverAfterward HandoverAfterward { get; set; }
        public virtual RoomNo RoomNo1 { get; set; }
        public virtual RoomType RoomType1 { get; set; }
        public virtual Section Section1 { get; set; }
        public virtual Survey Survey { get; set; }
    }
}
