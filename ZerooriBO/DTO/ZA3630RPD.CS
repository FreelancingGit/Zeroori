using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZerooriBO
{
    public class ZA3630RPD
    {
        ZA3630DCol _empjobcol = new ZA3630DCol();
        ZA3000D _UserData = new ZA3000D();
        ZA3230D _indstry = new ZA3230D();
        ZA3230DCol _industry = new ZA3230DCol();
        ComDisValDCol _pageNoCol = new ComDisValDCol();
        ComDisValDCol _reportypCol = new ComDisValDCol();
        int _PageNo = 1;

        public ZA3630DCol EmpJobCol { get => _empjobcol; set => _empjobcol = value; }
        public ZA3000D UserData { get => _UserData; set => _UserData = value; }
        public ZA3230D IndstryCol { get => _indstry; set => _indstry = value; }
        public ZA3230DCol IndustryCol { get => _industry; set => _industry = value; }

        public ComDisValDCol PageNoCol { get => _pageNoCol; set => _pageNoCol = value; }
        public int PageNo { get => _PageNo; set => _PageNo = value; }
        public ComDisValDCol ReportypCol { get => _reportypCol; set => _reportypCol = value; }
    }


    public class ZA3630D
    {
        string _empjobmast_id= String.Empty;
        string _FirstName = String.Empty;
        string _LastName = String.Empty;
        string _Gender = String.Empty;
        string _Title = String.Empty;
        string _Description = String.Empty;
        string _Mobile = String.Empty;
        string _Email = String.Empty;
        string _CurrentCompany = String.Empty;
        string _CurrentPos = String.Empty;
        string _NoticePeriod = String.Empty;
        string _Nationality_id = String.Empty;
        string _CurrentLoc_id = String.Empty;
        string _VisaStatus_id = String.Empty;
        string _CarrierLevel_id = String.Empty;
        string _CurrentSalary_id = String.Empty;
        string _WorkExperiance_id = String.Empty;
        string _EducationalLevel_id = String.Empty;
        string _Commitment_id = String.Empty;
        string _usr_mast_id = String.Empty;
        string _PostID = String.Empty;
        string _stats = String.Empty;
        string _crtd_dt = string.Empty;
        string _place = string.Empty;
        String _proimg = string.Empty;
        ComDisValD _reportyp = new ComDisValD();


        public string EmpJobMastID { get => _empjobmast_id; set => _empjobmast_id = value; }
        public string FirstName { get => _FirstName; set => _FirstName = value; }
        public string LastName { get => _LastName; set => _LastName = value; }
        public string Gender { get => _Gender; set => _Gender = value; }
        public string Title { get => _Title; set => _Title = value; }
        public string Description { get => _Description; set => _Description = value; }
        public string Mobile { get => _Mobile; set => _Mobile = value; }
        public string Email { get => _Email; set => _Email = value; }
        public string CurrentCompany { get => _CurrentCompany; set => _CurrentCompany = value; }
        public string CurrentPos { get => _CurrentPos; set => _CurrentPos = value; }
        public string NoticePeriod { get => _NoticePeriod; set => _NoticePeriod = value; }
        public string Nationality { get => _Nationality_id; set => _Nationality_id = value; }
        public string CurrentLoc { get => _CurrentLoc_id; set => _CurrentLoc_id = value; }
        public string VisaStatus { get => _VisaStatus_id; set => _VisaStatus_id = value; }
        public string CarrierLevel { get => _CarrierLevel_id; set => _CarrierLevel_id = value; }
        public string CurrentSalary { get => _CurrentSalary_id; set => _CurrentSalary_id = value; }
        public string WorkExperiance { get => _WorkExperiance_id; set => _WorkExperiance_id = value; }
        public string EducationalLevel { get => _EducationalLevel_id; set => _EducationalLevel_id = value; }
        public string Commitment { get => _Commitment_id; set => _Commitment_id = value; }
        public string UsrMastId { get => _usr_mast_id; set => _usr_mast_id = value; }
        public string PostID { get => _PostID; set => _PostID = value; }
        public string Stats { get => _stats; set => _stats = value; }
        public string CrtdDt { get => _crtd_dt; set => _crtd_dt = value; }
        public string Place { get => _place; set => _place = value; }
        public string Proimg { get => _proimg; set => _proimg = value; }
        public ComDisValD Reportyp { get => _reportyp; set => _reportyp = value; }
    }

    public class ZA3630DCol : System.Collections.ObjectModel.ObservableCollection<ZA3630D>
    {

    }




}
