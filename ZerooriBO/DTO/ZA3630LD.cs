using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZerooriBO
{
    /// <summary>
    /// Sign Up Page
    /// User Creation
    /// </summary>
    public class ZA3630LD
    {
        ZA3000D _UserData = new ZA3000D();
        int? _EmpJobMastID = null;
        ComDisValDCol _pageNoCol = new ComDisValDCol();
        ComDisValDCol _reportypCol = new ComDisValDCol();
        int _PageNo = 1;

        ZA3230DCol _NationalityCol = new ZA3230DCol();
        ZA3230DCol _IndustryCol = new ZA3230DCol();
        ZA3630DCol _empjobcol = new ZA3630DCol();
        ZA3230DCol _CurrentLocCol = new ZA3230DCol();
        ZA3230DCol _VisaStatusCol = new ZA3230DCol();
        ZA3230DCol _CarrierLevelCol = new ZA3230DCol();
        ZA3230DCol _CurrentSalaryCol = new ZA3230DCol();
        ZA3230DCol _WorkExperianceCol = new ZA3230DCol();
        ZA3230DCol _EducationalLevelCol = new ZA3230DCol();
        ZA3230DCol _CommitmentCol = new ZA3230DCol();
        ZA3630SD _jobMast = new ZA3630SD();

        public ZA3630DCol EmpJobCol { get => _empjobcol; set => _empjobcol = value; }
        public ZA3000D UserData { get => _UserData; set => _UserData = value; }
        public ZA3230DCol NationalityCol { get => _NationalityCol; set => _NationalityCol = value; }
        public ZA3230DCol CurrentLocCol { get => _CurrentLocCol; set => _CurrentLocCol = value; }
        public ZA3230DCol VisaStatusCol { get => _VisaStatusCol; set => _VisaStatusCol = value; }
        public ZA3230DCol CarrierLevelCol { get => _CarrierLevelCol; set => _CarrierLevelCol = value; }
        public ZA3230DCol CurrentSalaryCol { get => _CurrentSalaryCol; set => _CurrentSalaryCol = value; }
        public ZA3230DCol WorkExperianceCol { get => _WorkExperianceCol; set => _WorkExperianceCol = value; }
        public ZA3230DCol EducationalLevelCol { get => _EducationalLevelCol; set => _EducationalLevelCol = value; }
        public ZA3230DCol CommitmentCol { get => _CommitmentCol; set => _CommitmentCol = value; }
        public int? EmpJobMastID { get => _EmpJobMastID; set => _EmpJobMastID = value; }
        public ZA3230DCol IndustryCol { get => _IndustryCol; set => _IndustryCol = value; }
        public ZA3630SD JobMast { get => _jobMast; set => _jobMast = value; }
        public ComDisValDCol PageNoCol { get => _pageNoCol; set => _pageNoCol = value; }
        public int PageNo { get => _PageNo; set => _PageNo = value; }
        public ComDisValDCol ReportypCol { get => _reportypCol; set => _reportypCol = value; }
    }
}
