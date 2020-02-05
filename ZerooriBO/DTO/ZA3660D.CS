using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZerooriBO
{
    public class ZA3660LD
    {
        ZA3660DCol _dirCol = new ZA3660DCol();
        ComDisValDCol _pageNoCol = new ComDisValDCol();
        ZA3000D _UserData = new ZA3000D();
        int _PageNo = 1;

        public ZA3660DCol DirCol { get => _dirCol; set => _dirCol = value; }
        public ComDisValDCol PageNoCol { get => _pageNoCol; set => _pageNoCol = value; }
        public ZA3000D UserData { get => _UserData; set => _UserData = value; }
        public int PageNo { get => _PageNo; set => _PageNo = value; }
    }


    public class ZA3660D
    {
        string _dir_mast_id = string.Empty;
        string _dir_grup_name = string.Empty;
        BaseD _ZaBase = new BaseD();
        
        public BaseD ZaBase { get => _ZaBase; set => _ZaBase = value; }
        public string Dir_mast_id { get => _dir_mast_id; set => _dir_mast_id = value; }
        public string Dir_grup_name { get => _dir_grup_name; set => _dir_grup_name = value; }
    }

    public class ZA3660DCol : System.Collections.ObjectModel.ObservableCollection<ZA3660D>
    {

    }




}
