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
    /// 
    public class ZA3010LD
    {
        ZA3010DCol _packCol = new ZA3010DCol();
        ZA3000D _UserData = new ZA3000D();
        ComDisValDCol _pageNoCol = new ComDisValDCol();
        ComDisValDCol _locationCol = new ComDisValDCol();
        ComDisValDCol _SortCol = new ComDisValDCol();
        ZA3220DCol _Catagory = new ZA3220DCol();

        public ZA3010DCol PackCol { get => _packCol; set => _packCol = value; }
        public ZA3000D UserData { get => _UserData; set => _UserData = value; }
        public ComDisValDCol LocationCol { get => _locationCol; set => _locationCol = value; }
        public ComDisValDCol SortByCol { get => _SortCol; set => _SortCol = value; }
        public ComDisValDCol PageNoCol { get => _pageNoCol; set => _pageNoCol = value; }
        public ZA3220DCol CategoryCol { get => _Catagory; set => _Catagory = value; }

    }
    public class ZA3010D
    {
        String _BusinessName = string.Empty;
        String _URL = string.Empty;
        String _Category = string.Empty;
        String _BannerImage = string.Empty;
        String _CompanyLogo = string.Empty;
        String _Facebook = string.Empty;
        String _Instagram = string.Empty;
        String _Twitter = String.Empty;
        String _PhoneNo = string.Empty;
        String _Email = string.Empty;
        String _Website = string.Empty;
        String _Location = string.Empty;
        String _Description = string.Empty;
        int? _DealMastID = null;
        int? _PackDealMastID = null;
        int? _planMastId = null;
        int? _packageMastId = null;
        ZA3000D _UserData = new ZA3000D();
        String _PhotoPath = string.Empty;
        int _PageNo = 1;

        public string URL { get => _URL; set => _URL = value; }
        public string CompanyLogo { get => _CompanyLogo; set => _CompanyLogo = value; }
        public string Facebook { get => _Facebook; set => _Facebook = value; }
        public string Instagram { get => _Instagram; set => _Instagram = value; }
        public string Twitter { get => _Twitter; set => _Twitter = value; }
        public string PhoneNo { get => _PhoneNo; set => _PhoneNo = value; }
        public string Email { get => _Email; set => _Email = value; }
        public string Website { get => _Website; set => _Website = value; }
        public string Location { get => _Location; set => _Location = value; }
        public string Description { get => _Description; set => _Description = value; }
        public string BusinessName { get => _BusinessName; set => _BusinessName = value; }
        public int? DealMastID { get => _DealMastID; set => _DealMastID = value; }
        public ZA3000D UserData { get => _UserData; set => _UserData = value; }
        public string Category { get => _Category; set => _Category = value; }
        public string BannerImage { get => _BannerImage; set => _BannerImage = value; }
        public int? PackDealMastID { get => _PackDealMastID; set => _PackDealMastID = value; }
        public int? PlanMastId { get => _planMastId; set => _planMastId = value; }
        public int? PackageMastId { get => _packageMastId; set => _packageMastId = value; }
        public string PhotoPath { get => _PhotoPath; set => _PhotoPath = value; }
        public int PageNo { get => _PageNo; set => _PageNo = value; }
    }

    public class ZA3010DCol : System.Collections.ObjectModel.ObservableCollection<ZA3010D>
    {

    }
}
