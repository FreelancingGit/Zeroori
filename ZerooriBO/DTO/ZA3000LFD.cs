using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZerooriBO
{
    public class ZA3000LFD
    {
        ZA3000D _UserData = new ZA3000D();
        ZA3220D _Catagory = new ZA3220D();
        ComDisValD _Location = new ComDisValD();
        ComDisValD _SortBy = new ComDisValD();
        int _PageNo = 1;
        int? _DealMastID = null;

        public ZA3000D UserData { get => _UserData; set => _UserData = value; }
        public ZA3220D Category { get => _Catagory; set => _Catagory = value; }
        public ComDisValD Location { get => _Location; set => _Location = value; }
        public ComDisValD SortBy { get => _SortBy; set => _SortBy = value; }
        public int PageNo { get => _PageNo; set => _PageNo = value; }

        public int? DealMastID { get => _DealMastID; set => _DealMastID = value; }
    }
}
