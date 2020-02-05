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
    public class ZA3600SD
    {
        int? _motorsADMastID = null;
        ZA3000D _UserData = new ZA3000D();
        ZA3200D _Year = new ZA3200D();
        ZA3200D _Colour = new ZA3200D();
        ZA3200D _Doors = new ZA3200D();
        ZA3200D _Warranty = new ZA3200D();
        ZA3200D _RegionalSpecs = new ZA3200D();
        ZA3200D _Transmisson = new ZA3200D();
        ZA3200D _BodyType = new ZA3200D();
        ZA3200D _FuelType = new ZA3200D();
        ZA3200D _Cylinders = new ZA3200D();
        ZA3200D _SellerType = new ZA3200D();
        ZA3200D _Condition = new ZA3200D();
        ZA3200D _Extras = new ZA3200D();
        ZA3200D _TechinalFeatures = new ZA3200D();
        ZA3200D _HoursePower = new ZA3200D();
        ZA3200D _Brand = new ZA3200D();
        String _Title = "";
        String _Description = "";
        String _KiloMetrs = "";


        public ZA3000D UserData { get => _UserData; set => _UserData = value; }
        public ZA3200D Year { get => _Year; set => _Year = value; }
        public ZA3200D Colour { get => _Colour; set => _Colour = value; }
        public ZA3200D Doors { get => _Doors; set => _Doors = value; }
        public ZA3200D Warranty { get => _Warranty; set => _Warranty = value; }
        public ZA3200D RegionalSpecs { get => _RegionalSpecs; set => _RegionalSpecs = value; }
        public ZA3200D Transmisson { get => _Transmisson; set => _Transmisson = value; }
        public ZA3200D BodyType { get => _BodyType; set => _BodyType = value; }
        public ZA3200D FuelType { get => _FuelType; set => _FuelType = value; }
        public ZA3200D Cylinders { get => _Cylinders; set => _Cylinders = value; }
        public ZA3200D SellerType { get => _SellerType; set => _SellerType = value; }
        public ZA3200D Extras { get => _Extras; set => _Extras = value; }
        public ZA3200D TechinalFeatures { get => _TechinalFeatures; set => _TechinalFeatures = value; }
        public ZA3200D HoursePower { get => _HoursePower; set => _HoursePower = value; }
        public ZA3200D Brand { get => _Brand; set => _Brand = value; }
        public string Title { get => _Title; set => _Title = value; }
        public string Description { get => _Description; set => _Description = value; }
        public int? MotorsADMastID { get => _motorsADMastID; set => _motorsADMastID = value; }
        public ZA3200D Condition { get => _Condition; set => _Condition = value; }
        public string KiloMetrs { get => _KiloMetrs; set => _KiloMetrs = value; }
    }
}
