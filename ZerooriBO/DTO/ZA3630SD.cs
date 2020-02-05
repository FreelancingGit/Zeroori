using System;
using System.Linq;
using System.Text;

namespace ZerooriBO
{
    /// <summary>
    /// Sign Up Page
    /// User Creation
    /// </summary>
    public class ZA3630SD
    {
        ZA3000D _UserData = new ZA3000D();
        int? _EmpJobMastID = null;
        String _FirstName = "";
        String _LastName = "";
        String _Gender = "";
        String _Title = "";
        String _Description = "";
        String _Mobile = "";
        String _Email = "";
        String _Passwd = "";
        String _CurrentPos = "";
        String _NoticePeriod = "";
        String _CurrentCompany = "";
        String _PhotoPath = "";
        String _CvPath = "";
        
        ZA3230D _Nationality = new ZA3230D();
        ZA3230D _Industry = new ZA3230D();
        ZA3230D _CurrentLoc = new ZA3230D();
        ZA3230D _VisaStatus = new ZA3230D();
        ZA3230D _CarrierLevel = new ZA3230D();
        ZA3230D _CurrentSalary = new ZA3230D();
        ZA3230D _WorkExperiance = new ZA3230D();
        ZA3230D _EducationalLevel = new ZA3230D();
        ZA3230D _Commitment = new ZA3230D();


        public ZA3000D UserData { get => _UserData; set => _UserData = value; }
        public string FirstName { get => _FirstName; set => _FirstName = value; }
        public string LastName { get => _LastName; set => _LastName = value; }
        public string Gender { get => _Gender; set => _Gender = value; }
        public string Title { get => _Title; set => _Title = value; }
        public string Description { get => _Description; set => _Description = value; }
        public string Mobile { get => _Mobile; set => _Mobile = value; }
        public string Email { get => _Email; set => _Email = value; }
        public string CurrentPos { get => _CurrentPos; set => _CurrentPos = value; }
        public string NoticePeriod { get => _NoticePeriod; set => _NoticePeriod = value; }
        public ZA3230D Nationality { get => _Nationality; set => _Nationality = value; }
        public ZA3230D CurrentLoc { get => _CurrentLoc; set => _CurrentLoc = value; }
        public ZA3230D VisaStatus { get => _VisaStatus; set => _VisaStatus = value; }
        public ZA3230D CarrierLevel { get => _CarrierLevel; set => _CarrierLevel = value; }
        public ZA3230D CurrentSalary { get => _CurrentSalary; set => _CurrentSalary = value; }
        public ZA3230D WorkExperiance { get => _WorkExperiance; set => _WorkExperiance = value; }
        public ZA3230D EducationalLevel { get => _EducationalLevel; set => _EducationalLevel = value; }
        public ZA3230D Commitment { get => _Commitment; set => _Commitment = value; }
        public int? EmpJobMastID { get => _EmpJobMastID; set => _EmpJobMastID = value; }
        public string CurrentCompany { get => _CurrentCompany; set => _CurrentCompany = value; }
        public string Passwd { get => _Passwd; set => _Passwd = value; }
        public string PhotoPath { get => _PhotoPath; set => _PhotoPath = value; }
        public string CvPath { get => _CvPath; set => _CvPath = value; }
        public ZA3230D Industry { get => _Industry; set => _Industry = value; }
       
    }
    public class ZA3630SDCol : System.Collections.ObjectModel.ObservableCollection<ZA3630D>
    {

    }
}
