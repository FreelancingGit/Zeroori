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
    public class ZA3600LD
    {
        int? _motorsADMastID = null;
        ZA3000D _UserData = new ZA3000D();
        ZA3200DCol _YearCol = new ZA3200DCol();
        ZA3200DCol _ColourCol = new ZA3200DCol();
        ZA3200DCol _DoorsCol = new ZA3200DCol();
        ZA3200DCol _WarrantyCol = new ZA3200DCol();
        ZA3200DCol _RegionalSpecsCol = new ZA3200DCol();
        ZA3200DCol _TransmissonCol = new ZA3200DCol();
        ZA3200DCol _BodyTypeCol = new ZA3200DCol();
        ZA3200DCol _FuelTypeCol = new ZA3200DCol();
        ZA3200DCol _CylindersCol = new ZA3200DCol();
        ZA3200DCol _SellerTypeCol = new ZA3200DCol();
        ZA3200DCol _ExtrasCol = new ZA3200DCol();
        ZA3200DCol _TechinalFeaturesCol = new ZA3200DCol();
        ZA3200DCol _HoursePowerCol = new ZA3200DCol();
        ZA3200DCol _BrandCol = new ZA3200DCol();
        ZA3200DCol _ConditionCol = new ZA3200DCol();
        ZA3600SD _SelectedData = new ZA3600SD();

        public ZA3000D UserData { get => _UserData; set => _UserData = value; }
        public ZA3200DCol YearCol { get => _YearCol; set => _YearCol = value; }
        public ZA3200DCol ColourCol { get => _ColourCol; set => _ColourCol = value; }
        public ZA3200DCol DoorsCol { get => _DoorsCol; set => _DoorsCol = value; }
        public ZA3200DCol WarrantyCol { get => _WarrantyCol; set => _WarrantyCol = value; }
        public ZA3200DCol RegionalSpecsCol { get => _RegionalSpecsCol; set => _RegionalSpecsCol = value; }
        public ZA3200DCol TransmissonCol { get => _TransmissonCol; set => _TransmissonCol = value; }
        public ZA3200DCol BodyTypeCol { get => _BodyTypeCol; set => _BodyTypeCol = value; }
        public ZA3200DCol FuelTypeCol { get => _FuelTypeCol; set => _FuelTypeCol = value; }
        public ZA3200DCol CylindersCol { get => _CylindersCol; set => _CylindersCol = value; }
        public ZA3200DCol SellerTypeCol { get => _SellerTypeCol; set => _SellerTypeCol = value; }
        public ZA3200DCol ExtrasCol { get => _ExtrasCol; set => _ExtrasCol = value; }
        public ZA3200DCol TechinalFeaturesCol { get => _TechinalFeaturesCol; set => _TechinalFeaturesCol = value; }
        public ZA3200DCol HoursePowerCol { get => _HoursePowerCol; set => _HoursePowerCol = value; }
        public ZA3200DCol BrandCol { get => _BrandCol; set => _BrandCol = value; }
        public int? MotorsADMastID { get => _motorsADMastID; set => _motorsADMastID = value; }
        public ZA3200DCol ConditionCol { get => _ConditionCol; set => _ConditionCol = value; }
        public ZA3600SD SelectedData { get => _SelectedData; set => _SelectedData = value; }
    }


   


}
