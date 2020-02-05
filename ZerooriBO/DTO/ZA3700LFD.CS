using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZerooriBO
{
    public class ZA3700LFD
    {
        ZA3000D _UserData = new ZA3000D();
        ZA3200D _Catagory  = new ZA3200D();
        ComDisValD _Location = new ComDisValD();
        ComDisValD _SortBy = new ComDisValD();
		ZA3200D _BodyType = new ZA3200D();
		ZA3200D _Model = new ZA3200D();
		ZA3200D _Color = new ZA3200D();
		ZA3200D _FuelType = new ZA3200D();

		int _PageNo = 1;



        public ZA3000D UserData { get => _UserData; set => _UserData = value; }
        public ZA3200D Catagory { get => _Catagory; set => _Catagory = value; }
        public ComDisValD Location { get => _Location; set => _Location = value; }
        public ComDisValD SortBy { get => _SortBy; set => _SortBy = value; }
        public int PageNo { get => _PageNo; set => _PageNo = value; }
		public ZA3200D FuelType { get => _FuelType; set => _FuelType = value; }
		public ZA3200D BodyType { get => _BodyType; set => _BodyType = value; }
		public ZA3200D Color { get => _Color; set => _Color = value; }
		public ZA3200D Model { get => _Model; set => _Model = value; }
		


	}
}
