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
    public class ZA3650LD
    {
        ZA3000D _UserData = new ZA3000D();
        int? _compny_mast_id = null;
        ZA3230DCol _indstryCol = new ZA3230DCol();
        ZA3230D _indstry = new ZA3230D();
        ZA3230DCol _compny_size = new ZA3230DCol();
        ZA3230DCol _emplymnt_typ = new ZA3230DCol();
        ZA3230DCol _monthly_salary = new ZA3230DCol();
        ZA3230DCol _eductn_lvl = new ZA3230DCol();
        ZA3230DCol _listed_by = new ZA3230DCol();
        ZA3230DCol _career_lvl = new ZA3230DCol();
        ZA3230DCol _exprnce = new ZA3230DCol();
        ZA3650SDCol _comCol = new ZA3650SDCol();
        int _pageNo = 1;
        String _PhotoPath = "";
        ComDisValDCol _pageNoCol = new ComDisValDCol();
        ZA3650SD _frelncMast = new ZA3650SD();
        ComDisValDCol _reportypCol = new ComDisValDCol();
       

        public ZA3000D UserData { get => _UserData; set => _UserData = value; }
        public ZA3230DCol IndstryCol { get => _indstryCol; set => _indstryCol = value; }
        public ZA3230D Indstry { get => _indstry; set => _indstry = value; }

        public ZA3230DCol CompnySizeCol { get => _compny_size; set => _compny_size = value; }
        public ZA3230DCol EmploymntTypeCol { get => _emplymnt_typ; set => _emplymnt_typ = value; }
        public ZA3230DCol MonthlySalaryCol { get => _monthly_salary; set => _monthly_salary = value; }
        public ZA3230DCol EductnLevlCol { get => _eductn_lvl; set => _eductn_lvl = value; }
        public ZA3230DCol ListedBycol { get => _listed_by; set => _listed_by = value; }
        public ZA3230DCol CareervLevelCol { get => _career_lvl; set => _career_lvl = value; }
        public string PhotoPath { get => _PhotoPath; set => _PhotoPath = value; }
        public ZA3230DCol ExprnceCol { get => _exprnce; set => _exprnce = value; }
        public int? CompnyMastId { get => _compny_mast_id; set => _compny_mast_id = value; }
        public ZA3650SDCol ComCol { get => _comCol; set => _comCol = value; }
        public int PageNo { get => _pageNo; set => _pageNo = value; }
        public ComDisValDCol PageNoCol { get => _pageNoCol; set => _pageNoCol = value; }
        public ZA3650SD FrelncMast { get => _frelncMast; set => _frelncMast = value; }
        public ComDisValDCol ReportypCol { get => _reportypCol; set => _reportypCol = value; }
    }
}
