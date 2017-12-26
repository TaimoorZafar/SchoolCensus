using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolCensusApi.Models
{
    public class GeneralModel
    {
        public EMI Emi { get; set; }
        public Comment Comment{ get; set; }
        public SpecialFacility Specialfacility { get; set; }
        public List<Picture> Img { get; set; }
    }
    public class SurveyorsModel
    {
        public Surveyor Surveyor { get; set; }
        public GeneralModel Generalmodel { get; set; }
    }
    public class SchoolAddressesModel
    {
        public SchoolAddress Schooladdress { get; set; }
        public GeneralModel Generalmodel { get; set; }
    }
    public class LicenceeModel
    {
        public Licensee Licensee { get; set; }
        public GeneralModel Generalmodel { get; set; }
    }
    public class SchoolAccessModel
    {
        public SchoolAccess Schoolaccess { get; set; }
        public GeneralModel Generalmodel { get; set; }
    }
    public class SchoolStatusModel
    {
        public SchoolStatus Schoolstatus { get; set; }
        public GeneralModel Generalmodel { get; set; }
    }
    public class SchoolSpecsModel
    {
        public SchoolSpec Schoolspec { get; set; }
        public GeneralModel Generalmodel { get; set; }
    }
    public class InfraSpecsModel
    {
        public InfraSpec Infraspec { get; set; }
        public GeneralModel Generalmodel { get; set; }
    }
    public class InfraStructureModel
    {
        public InfraStructure Infrastructure { get; set; }
        public GeneralModel Generalmodel { get; set; }
    }

    public class ReconciliationModel
    {
        public Reconciliation Reconciliation { get; set; }
        public GeneralModel Generalmodel { get; set; }
    }

    public class ElectricityModel
    {
        public Electricity Electricity { get; set; }
        public GeneralModel Generalmodel { get; set; }
    }
    public class WallsModel
    {
        public Wall Wall { get; set; }
        public GeneralModel Generalmodel { get; set; }
    }
    public class LabsModel
    {
        public Lab Lab { get; set; }
        public GeneralModel Generalmodel { get; set; }
    }
    public class WaterModel
    {
        public Water Water { get; set; }
        public GeneralModel Generalmodel { get; set; }
    }
    public class ConcileModel
    {
        public Council Council { get; set; }
        public GeneralModel Generalmodel { get; set; }
    }
    public class RecordModel
    {
        public Record Record{ get; set; }
        public GeneralModel Generalmodel { get; set; }
    }
    public class GroundSurroundingModel
    {
        public GroundSurrounding Groundsurrounding { get; set; }
        public GeneralModel Generalmodel { get; set; }
    }
    public class StudentCleanlinessModel
    {
        public StudentCleanliness Studentcleanliness { get; set; }
        public GeneralModel Generalmodel { get; set; }
    }
    public class TeacherCleanlinessModel
    {
        public TeachersCleanliness Teacherscleanliness { get; set; }
        public GeneralModel Generalmodel { get; set; }
    }


}