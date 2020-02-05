using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZerooriBO
{

    /// <summary>
    /// Classified Site  Collection  
    /// For Initial Loading 
    /// </summary>
    public class ZA3710ILD
    {
        ZA3000D _UserData = new ZA3000D();
        ZA3210DCol _CatagoryCol = new ZA3210DCol();
        ComDisValDCol _LocationCol = new ComDisValDCol();
        ComDisValDCol _SortByCol = new ComDisValDCol();
        ComDisValDCol _pageNoCol = new ComDisValDCol();
        ZA3710BDCol _PropDataCol = new ZA3710BDCol();

        public ZA3000D UserData { get => _UserData; set => _UserData = value; }
        public ComDisValDCol LocationCol { get => _LocationCol; set => _LocationCol = value; }
        public ComDisValDCol SortByCol { get => _SortByCol; set => _SortByCol = value; }
        public ComDisValDCol PageNoCol { get => _pageNoCol; set => _pageNoCol = value; }
        public ZA3710BDCol PropDataCol { get => _PropDataCol; set => _PropDataCol = value; }
        public ZA3210DCol CatagoryCol { get => _CatagoryCol; set => _CatagoryCol = value; }
    }
}
