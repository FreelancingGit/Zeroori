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
    public class ZA3640LD
    {
        ZA3000D _UserData = new ZA3000D();
        int? _EmpJobMastID = null;
        ComDisValDCol _pageNoCol = new ComDisValDCol();
        ComDisValDCol _reportypCol = new ComDisValDCol();
        int _PageNo = 1;

        ZA3640DCol _compnyjobcol = new ZA3640DCol();
        ZA3230DCol _IndustryCol = new ZA3230DCol();

        public ZA3640DCol CompnyJobCol { get => _compnyjobcol; set => _compnyjobcol = value; }
        public ZA3000D UserData { get => _UserData; set => _UserData = value; }
        public int? EmpJobMastID { get => _EmpJobMastID; set => _EmpJobMastID = value; }
        public ZA3230DCol IndustryCol { get => _IndustryCol; set => _IndustryCol = value; }
        public ComDisValDCol PageNoCol { get => _pageNoCol; set => _pageNoCol = value; }
        public int PageNo { get => _PageNo; set => _PageNo = value; }
        public ComDisValDCol ReportypCol { get => _reportypCol; set => _reportypCol = value; }
    }
}
