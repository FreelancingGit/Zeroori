using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZerooriBO
{
    public class ZA3640RPD
    {
        ZA3640DCol _compnyjobcol = new ZA3640DCol();
        ZA3000D _UserData = new ZA3000D();
        ZA3230D _indstry = new ZA3230D();
        ComDisValDCol _reportypCol = new ComDisValDCol();
        ComDisValDCol _pageNoCol = new ComDisValDCol();

        public ZA3640DCol CompnyJobCol { get => _compnyjobcol; set => _compnyjobcol = value; }
        public ZA3000D UserData { get => _UserData; set => _UserData = value; }
        public ZA3230D IndstryCol { get => _indstry; set => _indstry = value; }
        public ComDisValDCol ReportypCol { get => _reportypCol; set => _reportypCol = value; }
        public ComDisValDCol PageNoCol { get => _pageNoCol; set => _pageNoCol = value; }
    }


    public class ZA3640D
    {
        string _Company_job_mast_id= string.Empty;
        string _CompanyName = string.Empty;
        string _EmpType = string.Empty;
        string _neighbr = string.Empty;
        string _Title = string.Empty;
        string _Description = string.Empty;
        string _MinWorkExp = string.Empty;
        string _MinEduLvl = string.Empty;
        string _ListedBy = string.Empty;
        string _Compnysize = string.Empty;
        string _CarierLvl = string.Empty;
        string _CurrentLoc_id = string.Empty;
        string _stats = string.Empty;
        string _crtd_dt = string.Empty;
        string _Usr_mast_id = string.Empty;
        string _phone = string.Empty;
        string _email = string.Empty;
        String _proimg = string.Empty;
		string _imgPath = string.Empty;
		ComDisValD _reportyp = new ComDisValD();
        ZA3230D _indstry = new ZA3230D();
        ZA3000D _UserData = new ZA3000D();
        int _PageNo = 1;

        public string CompanyJobMastId { get => _Company_job_mast_id; set => _Company_job_mast_id = value; }
        public string CompanyName { get => _CompanyName; set => _CompanyName = value; }
        public string EmpType { get => _EmpType; set => _EmpType = value; }
        public string Neighbr { get => _neighbr; set => _neighbr = value; }
        public string Title { get => _Title; set => _Title = value; }
        public string Description { get => _Description; set => _Description = value; }
        public string MinWorkExp { get => _MinWorkExp; set => _MinWorkExp = value; }
        public string MinEduLvl { get => _MinEduLvl; set => _MinEduLvl = value; }
        public string ListedBy { get => _ListedBy; set => _ListedBy = value; }
        public string Compnysize { get => _Compnysize; set => _Compnysize = value; }
        public string CarierLvl { get => _CarierLvl; set => _CarierLvl = value; }
        public string CurrentLoc { get => _CurrentLoc_id; set => _CurrentLoc_id = value; }
        public string Stats { get => _stats; set => _stats = value; }
        public string CrtdDt { get => _crtd_dt; set => _crtd_dt = value; }
        public string UsrMastId { get => _Usr_mast_id; set => _Usr_mast_id = value; }
        public string Phone { get => _phone; set => _phone = value; }
        public string Email { get => _email; set => _email = value; }
        public string Proimg { get => _proimg; set => _proimg = value; }
		public ComDisValD Reportyp { get => _reportyp; set => _reportyp = value; }
        public ZA3000D UserData { get => _UserData; set => _UserData = value; }
        public ZA3230D IndstryCol { get => _indstry; set => _indstry = value; }
		public string imgPath { get => _imgPath; set => _imgPath = value; }

        public int PageNo { get => _PageNo; set => _PageNo = value; }
    }

    public class ZA3640DCol : System.Collections.ObjectModel.ObservableCollection<ZA3640D>
    {

    }




}
