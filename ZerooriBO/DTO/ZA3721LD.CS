using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZerooriBO
{
    public class ZA3721LD
    {
        String _ClasifdAdMastId = String.Empty;
        String _clasifd_title = String.Empty;
        String _crtd_dt = String.Empty;
        String _Category = String.Empty;
        String _SubCategory = String.Empty;
        String _Age  = String.Empty;
        String _Usage = String.Empty;
        String _Condition = String.Empty;
        String _Warranty = String.Empty;
        String _cityMast = String.Empty;
        String _UsrEmail = String.Empty;
        String _UsrPhno = String.Empty;
        String _clasifdDescription = String.Empty;
        String _placeName = String.Empty;
        String _Price= String.Empty;

        
        ComDisValDCol _FileNames = new ComDisValDCol();
        ZA3000D _UserData = new ZA3000D();

        public string ClasifdAdMastId { get => _ClasifdAdMastId; set => _ClasifdAdMastId = value; }
        public string ClasifdTitle { get => _clasifd_title; set => _clasifd_title = value; }
        public string CrtdDt { get => _crtd_dt; set => _crtd_dt = value; }
        public string Category { get => _Category; set => _Category = value; }
        public string SubCategory { get => _SubCategory; set => _SubCategory = value; }
        public string Age { get => _Age; set => _Age = value; }
        public string Usage { get => _Usage; set => _Usage = value; }
        public string Condition { get => _Condition; set => _Condition = value; }
        public string Warranty { get => _Warranty; set => _Warranty = value; }
        public string CityMast { get => _cityMast; set => _cityMast = value; }
        public string UsrEmail { get => _UsrEmail; set => _UsrEmail = value; }
        public string UsrPhno { get => _UsrPhno; set => _UsrPhno = value; }
        public string ClasifdDescription { get => _clasifdDescription; set => _clasifdDescription = value; }
        public string PlaceName { get => _placeName; set => _placeName = value; }
        public string Price { get => _Price; set => _Price = value; }
        public ComDisValDCol FileNames { get => _FileNames; set => _FileNames = value; }
        public ZA3000D UserData { get => _UserData; set => _UserData = value; }
    }
}
