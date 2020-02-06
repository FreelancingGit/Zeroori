using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZerooriBO
{
    public class ZA3710LFD
    {
        ZA3000D _UserData = new ZA3000D();
        ZA3210D _Catagory  = new ZA3210D();
        ComDisValD _Location = new ComDisValD();
        ComDisValD _SortBy = new ComDisValD();
        int _PageNo = 1;

        public ZA3000D UserData { get => _UserData; set => _UserData = value; }
        public ZA3210D Category { get => _Catagory; set => _Catagory = value; }
        public ComDisValD Location { get => _Location; set => _Location = value; }
        public ComDisValD SortBy { get => _SortBy; set => _SortBy = value; }
        public int PageNo { get => _PageNo; set => _PageNo = value; }
    }



}
