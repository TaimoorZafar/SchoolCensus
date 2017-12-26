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
    
    public partial class InfraStructure
    {
        public int InfraStructures_id { get; set; }
        public string SurveyID { get; set; }
        public string Entity { get; set; }
        public string Section { get; set; }
        public string SchoolBuildingAvailable { get; set; }
        public string ConstructionType { get; set; }
        public string BuildingCondition { get; set; }
        public string RemarksOfBuildingCondition { get; set; }
        public string OwnedBy { get; set; }
        public string IsSchoolFunctionalAtSamePlaceWhereOfficiallyEstablished { get; set; }
        public string RazorWireOnWall { get; set; }
        public string SewerageSystem { get; set; }
        public string SecurityGuard { get; set; }
        public string MetalDetector { get; set; }
        public string MainGate { get; set; }
        public string PlayGroundAvailable { get; set; }
        public string HurdlesOnGate { get; set; }
        public string IsGuardProperlyArmed { get; set; }
        public string Cleanliness { get; set; }
        public string CCTVcameras { get; set; }
        public string Swings { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> NoofTrees { get; set; }
    
        public virtual BuildingCondition BuildingCondition1 { get; set; }
        public virtual ConstructionType ConstructionType1 { get; set; }
        public virtual Entity Entity1 { get; set; }
        public virtual OwnedBy OwnedBy1 { get; set; }
        public virtual Section Section1 { get; set; }
        public virtual Survey Survey { get; set; }
    }
}
