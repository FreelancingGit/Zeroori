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
    public class ZA3720ILD
    {
        ZA3000D _UserData = new ZA3000D();
        ZA3220DCol _CatagoryCol = new ZA3220DCol();
        ComDisValDCol _AgeCol = new ComDisValDCol();
        ComDisValDCol _SortByCol = new ComDisValDCol();
        ComDisValDCol _pageNoCol = new ComDisValDCol();
        ZA3720BDCol _ClasifiedsDataCol = new ZA3720BDCol();

        public ZA3000D UserData { get => _UserData; set => _UserData = value; }
        public ZA3220DCol CatagoryCol { get => _CatagoryCol; set => _CatagoryCol = value; }
        public ComDisValDCol AgeCol { get => _AgeCol; set => _AgeCol = value; }
        public ComDisValDCol SortByCol { get => _SortByCol; set => _SortByCol = value; }
        public ZA3720BDCol ClasifiedsDataCol { get => _ClasifiedsDataCol; set => _ClasifiedsDataCol = value; }
        public ZA3720BDCol ClasifiedsFilterDataCol { get => _ClasifiedsDataCol; set => _ClasifiedsDataCol = value; }
        public ComDisValDCol PageNoCol { get => _pageNoCol; set => _pageNoCol = value; }
    }
}
