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

    public class ZA3011LD
    {
        ZA3011DCol _dealcol = new ZA3011DCol();
        ZA3000D _UserData = new ZA3000D();
        ZA3010D _PackDealDet = new ZA3010D();
        ComDisValDCol _pageNoCol = new ComDisValDCol();

        public ZA3011DCol Dealcol { get => _dealcol; set => _dealcol = value; }
        public ZA3000D UserData { get => _UserData; set => _UserData = value; }
        public ZA3010D PackDealMast { get => _PackDealDet; set => _PackDealDet = value; }
        public ComDisValDCol PageNoCol { get => _pageNoCol; set => _pageNoCol = value; }
    }

    public class ZA3011D
    {
       
        int? _dealMastId = null;
        int? _packDealMastId = null;
        string _dealName = string.Empty;
        int? _price = null;
        string _descrptn = string.Empty;
        string _startDt = string.Empty;
        string _endDt = string.Empty;
        string _location = string.Empty;
        string _busName = string.Empty;
        ZA3000D _UserData = new ZA3000D();
        string _bannerImg = string.Empty;
		string _userfrdr = string.Empty;
		string _img1 = string.Empty;
		string _img2 = string.Empty;
		int _PageNo = 1;

        public ZA3000D UserData { get => _UserData; set => _UserData = value; }
        public int? DealMastId { get => _dealMastId; set => _dealMastId = value; }
        public int? PackDealMastId { get => _packDealMastId; set => _packDealMastId = value; }
        public string DealName { get => _dealName; set => _dealName = value; }
        public int? Price { get => _price; set => _price = value; }
        public string Descrptn { get => _descrptn; set => _descrptn = value; }
        public string StartDt { get => _startDt; set => _startDt = value; }
        public string EndDt { get => _endDt; set => _endDt = value; }
        public string Location { get => _location; set => _location = value; }
        public string BusName { get => _busName; set => _busName = value; }
        public int PageNo { get => _PageNo; set => _PageNo = value; }
        public string BannerImg { get => _bannerImg; set => _bannerImg = value; }
		public string Img1 { get => _img1; set => _img1 = value; }
		public string Img2 { get => _img2; set => _img2 = value; }
		public string usefldr { get => _userfrdr; set => _userfrdr = value; }
	}

    public class ZA3011DCol : System.Collections.ObjectModel.ObservableCollection<ZA3011D>
    {

    }
}
