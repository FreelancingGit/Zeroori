using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZerooriBO
{
    public class ZA3661LD
    {
        ZA3661DCol _dirCol = new ZA3661DCol();
        ComDisValDCol _pageNoCol = new ComDisValDCol();
        ZA3000D _UserData = new ZA3000D();

        public ZA3661DCol DirCol { get => _dirCol; set => _dirCol = value; }
        public ComDisValDCol PageNoCol { get => _pageNoCol; set => _pageNoCol = value; }
        public ZA3000D UserData { get => _UserData; set => _UserData = value; }
    }


    public class ZA3661DD
    {
        ZA3661D _dirMast = new ZA3661D();
        ZA3000D _UserData = new ZA3000D();

        public ZA3661D DirMast { get => _dirMast; set => _dirMast = value; }
        public ZA3000D UserData { get => _UserData; set => _UserData = value; }
    }

    public class ZA3661D
    {
        int? _dir_dtl_id = null;
        string _sub_grup_name = string.Empty;
        string _comp_name = string.Empty;
        string _sub_name = string.Empty;
        string _addrs = string.Empty;
        string _phone_1 = string.Empty;
        string _phone_2 = string.Empty;
        string _Mobile = string.Empty;
        string _fax = string.Empty;
        string _email = string.Empty;
        string _web = string.Empty;
        string _prod_orgin = string.Empty;
        string _email_2 = string.Empty;
        string _F15 = string.Empty;
        string _F16 = string.Empty;
        string _F17 = string.Empty;
        string _F18 = string.Empty;
        string _F19 = string.Empty;
        string _F20 = string.Empty;
        string _F21 = string.Empty;
        string _F22 = string.Empty;
        string _F23 = string.Empty;
        string _F24 = string.Empty;
        string _F25 = string.Empty;
        string _F26 = string.Empty;
        string _F27 = string.Empty;
        string _F28 = string.Empty;
        string _F29 = string.Empty;
        string _F30 = string.Empty;
        string _F31 = string.Empty;
        string _F32 = string.Empty;
        string _F33 = string.Empty;
        string _F34 = string.Empty;
        string _F35 = string.Empty;
        string _F36 = string.Empty;
        string _grup_id = string.Empty;
        BaseD _ZaBase = new BaseD();
        ZA3000D _UserData = new ZA3000D();
        int _PageNo = 1;

        public BaseD ZaBase { get => _ZaBase; set => _ZaBase = value; }
        public int? DirDtlId { get => _dir_dtl_id; set => _dir_dtl_id = value; }
        public string CompName { get => _comp_name; set => _comp_name = value; }
        public string Addrs { get => _addrs; set => _addrs = value; }
        public string Phone_1 { get => _phone_1; set => _phone_1 = value; }
        public string Email { get => _email; set => _email = value; }
        public string SubGrupName { get => _sub_grup_name; set => _sub_grup_name = value; }
        public string SubName { get => _sub_name; set => _sub_name = value; }
        public string Phone_2 { get => _phone_2; set => _phone_2 = value; }
        public string Mobile { get => _Mobile; set => _Mobile = value; }
        public string Fax { get => _fax; set => _fax = value; }
        public string Web { get => _web; set => _web = value; }
        public string ProdOrgin { get => _prod_orgin; set => _prod_orgin = value; }
        public string Email_2 { get => _email_2; set => _email_2 = value; }
        public string F15 { get => _F15; set => _F15 = value; }
        public string F16 { get => _F16; set => _F16 = value; }
        public string F17 { get => _F17; set => _F17 = value; }
        public string F18 { get => _F18; set => _F18 = value; }
        public string F19 { get => _F19; set => _F19 = value; }
        public string F20 { get => _F20; set => _F20 = value; }
        public string F21 { get => F211; set => F211 = value; }
        public string F22 { get => _F22; set => _F22 = value; }
        public string F23 { get => _F23; set => _F23 = value; }
        public string F24 { get => _F24; set => _F24 = value; }
        public string F25 { get => _F25; set => _F25 = value; }
        public string F211 { get => _F21; set => _F21 = value; }
        public string F26 { get => _F26; set => _F26 = value; }
        public string F27 { get => _F27; set => _F27 = value; }
        public string F28 { get => _F28; set => _F28 = value; }
        public string F29 { get => _F29; set => _F29 = value; }
        public string F30 { get => _F30; set => _F30 = value; }
        public string F31 { get => _F31; set => _F31 = value; }
        public string F32 { get => _F32; set => _F32 = value; }
        public string F33 { get => _F33; set => _F33 = value; }
        public string F34 { get => _F34; set => _F34 = value; }
        public string F35 { get => _F35; set => _F35 = value; }
        public string F36 { get => _F36; set => _F36 = value; }
        public string GrupId { get => _grup_id; set => _grup_id = value; }
        public ZA3000D UserData { get => _UserData; set => _UserData = value; }
        public int PageNo { get => _PageNo; set => _PageNo = value; }
    }

    public class ZA3661DCol : System.Collections.ObjectModel.ObservableCollection<ZA3661D>
    {

    }

}
