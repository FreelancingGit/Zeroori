using System;
using System.Linq;
using System.Text;

namespace ZerooriBO
{
    /// <summary>
    /// Sign Up Page
    /// User Creation
    /// </summary>
    public class ZA3650SD
    {
        ZA3000D _UserData = new ZA3000D();
        int compny_job_mast_id = -1;
        string _compny_name = string.Empty;
        string _trade_licns = string.Empty;
        string _contct_name = string.Empty;
        string _ph_num = string.Empty;
        string _compny_websit = string.Empty;
        string _addrs = string.Empty;
        string _descrpn_step_one = string.Empty;
        string _compny_logo_img = string.Empty;
        string _job_title = string.Empty;
        string _neighbrhd = string.Empty;
        string _descrptn_step_two = string.Empty;
        String _PhotoPath = "";
        string _crtdDt = string.Empty;
		string _filename = string.Empty;
		string _imgName = string.Empty;
        ComDisValD _reportyp = new ComDisValD();

        ZA3230D _indstry = new ZA3230D();
        ZA3230D _compny_size = new ZA3230D();
        ZA3230D _emplymnt_typ = new ZA3230D();
        ZA3230D _monthly_salary = new ZA3230D();
        ZA3230D _eductn_lvl = new ZA3230D();
        ZA3230D _listed_by = new ZA3230D();
        ZA3230D _career_lvl = new ZA3230D();
        ZA3230D _exprnce = new ZA3230D();
		ComDisValD _location = new ComDisValD();


       
        public int CompnyJobMastId { get => compny_job_mast_id; set => compny_job_mast_id = value; }
        public string CompnyName { get => _compny_name; set => _compny_name = value; }
        public string TradeLicns { get => _trade_licns; set => _trade_licns = value; }
        public string ContctName { get => _contct_name; set => _contct_name = value; }
        public string Phone { get => _ph_num; set => _ph_num = value; }
        public string CompnyWebsit { get => _compny_websit; set => _compny_websit = value; }
        public string Addrs { get => _addrs; set => _addrs = value; }
        public string DescrpnStepOne { get => _descrpn_step_one; set => _descrpn_step_one = value; }
        public string CompnyLogoImg { get => _compny_logo_img; set => _compny_logo_img = value; }
        public string JobTitle { get => _job_title; set => _job_title = value; }
        public string Neighbrhd { get => _neighbrhd; set => _neighbrhd = value; }
        public ComDisValD Location { get => _location; set => _location = value; }
        public string DescrptnStepTwo { get => _descrptn_step_two; set => _descrptn_step_two = value; }
        public string PhotoPath { get => _PhotoPath; set => _PhotoPath = value; }
        public ZA3000D UserData { get => _UserData; set => _UserData = value; }
        public ZA3230D Indstry { get => _indstry; set => _indstry = value; }
        public ZA3230D CompnySize { get => _compny_size; set => _compny_size = value; }
        public ZA3230D EmplymntTyp { get => _emplymnt_typ; set => _emplymnt_typ = value; }
        public ZA3230D MonthlySalary { get => _monthly_salary; set => _monthly_salary = value; }
        public ZA3230D EductnLvl { get => _eductn_lvl; set => _eductn_lvl = value; }
        public ZA3230D ListedBy { get => _listed_by; set => _listed_by = value; }
        public ZA3230D CareerLvl { get => _career_lvl; set => _career_lvl = value; }
        public ZA3230D Exprnce { get => _exprnce; set => _exprnce = value; }
        public string CrtdDt { get => _crtdDt; set => _crtdDt = value; }
        public ComDisValD Reportyp { get => _reportyp; set => _reportyp = value; }
		public string filename { get => _filename; set => _filename = value; }
		public string imgName { get => _imgName; set => _imgName = value; }
	}

    public class ZA3650SDCol : System.Collections.ObjectModel.ObservableCollection<ZA3650SD>
    {

    }
}
