using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZerooriBO
{
    public class ZA3720LFD
    {
        ZA3000D _UserData = new ZA3000D();
        ZA3220D _Catagory  = new ZA3220D();
        ComDisValD _Age = new ComDisValD();
        ComDisValD _SortBy = new ComDisValD();
        int _PageNo = 1;

        public ZA3000D UserData { get => _UserData; set => _UserData = value; }
        public ZA3220D Catagory { get => _Catagory; set => _Catagory = value; }
        public ComDisValD Age { get => _Age; set => _Age = value; }
        public ComDisValD SortBy { get => _SortBy; set => _SortBy = value; }
        public int PageNo { get => _PageNo; set => _PageNo = value; }
    }



}
