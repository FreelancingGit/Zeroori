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
    public class ZA3610SD
    {
        int? _propADMastID = null;
        ZA3000D _UserData = new ZA3000D();
        ZA3210D _Bedroom = new ZA3210D();
        ZA3210D _BathRoom = new ZA3210D();
        ZA3210D _Size = new ZA3210D();
        ZA3210D _Furnished = new ZA3210D();
        ZA3210D _ApartmentFor = new ZA3210D();
        ZA3210D _RentIsPaid = new ZA3210D();
        ZA3210D _ListedBy = new ZA3210D();
        ZA3210D _Category = new ZA3210D();
        String _Description = "";
        String _Title = "";


        public ZA3000D UserData { get => _UserData; set => _UserData = value; }
        public string Description { get => _Description; set => _Description = value; }
        public int? PropADMastID { get => _propADMastID; set => _propADMastID = value; }
        public ZA3210D Bedroom { get => _Bedroom; set => _Bedroom = value; }
        public ZA3210D BathRoom { get => _BathRoom; set => _BathRoom = value; }
        public ZA3210D Size { get => _Size; set => _Size = value; }
        public ZA3210D Furnished { get => _Furnished; set => _Furnished = value; }
        public ZA3210D ApartmentFor { get => _ApartmentFor; set => _ApartmentFor = value; }
        public ZA3210D RentIsPaid { get => _RentIsPaid; set => _RentIsPaid = value; }
        public ZA3210D ListedBy { get => _ListedBy; set => _ListedBy = value; }
        public ZA3210D Category { get => _Category; set => _Category = value; }
        public string Title { get => _Title; set => _Title = value; }
    }
}
