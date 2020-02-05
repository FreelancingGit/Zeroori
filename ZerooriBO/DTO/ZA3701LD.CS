using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZerooriBO
{
    public class ZA3701LD
    {

        
        String _MotorsAdMastId = String.Empty;
        String _CrtdDt = string.Empty;
        String _MotTitle = String.Empty;
        String _Years = String.Empty;
        String _Kmters = String.Empty;
        String _Colors = String.Empty;
        String _Doors = String.Empty;
        String _Warenty = String.Empty;
        String _RegionalSpecs = String.Empty;
        String _Transmisson = String.Empty;
        String _BodyType = String.Empty;
        String _Brand = String.Empty;
        String _FuelType = String.Empty;
        String _SellerType = String.Empty;
        String _Cylinders = String.Empty;
        String _HoursePower = String.Empty;
        String _Condition = String.Empty;
        String _UsrEmail = String.Empty;
        String _UsrPhno = String.Empty;
        String _MotorDescription = String.Empty;
        String _Location = String.Empty;
        String _Price = String.Empty;
        ComDisValDCol _FileNames = new ComDisValDCol();
        ZA3000D _UserData = new ZA3000D();

        public string MotTitle { get => _MotTitle; set => _MotTitle = value; }
        public string Years { get => _Years; set => _Years = value; }
        public string Kmters { get => _Kmters; set => _Kmters = value; }
        public string Colors { get => _Colors; set => _Colors = value; }
        public string Doors { get => _Doors; set => _Doors = value; }
        public string Warenty { get => _Warenty; set => _Warenty = value; }
        public string RegionalSpecs { get => _RegionalSpecs; set => _RegionalSpecs = value; }
        public string Transmisson { get => _Transmisson; set => _Transmisson = value; }
        public string BodyType { get => _BodyType; set => _BodyType = value; }
        public string Brand { get => _Brand; set => _Brand = value; }
        public string FuelType { get => _FuelType; set => _FuelType = value; }
        public string SellerType { get => _SellerType; set => _SellerType = value; }
        public string Cylinders { get => _Cylinders; set => _Cylinders = value; }
        public string HoursePower { get => _HoursePower; set => _HoursePower = value; }
        public string Condition { get => _Condition; set => _Condition = value; }
        public string UsrEmail { get => _UsrEmail; set => _UsrEmail = value; }
        public string UsrPhno { get => _UsrPhno; set => _UsrPhno = value; }
        public string MotorDescription { get => _MotorDescription; set => _MotorDescription = value; }
        public string Location { get => _Location; set => _Location = value; }
        public string Price { get => _Price; set => _Price = value; }
        public ComDisValDCol FileNames { get => _FileNames; set => _FileNames = value; }
        public ZA3000D UserData { get => _UserData; set => _UserData = value; }
        public string MotorsAdMastId { get => _MotorsAdMastId; set => _MotorsAdMastId = value; }
        public string CrtdDt { get => _CrtdDt; set => _CrtdDt = value; }
    }
}
