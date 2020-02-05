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
    public class ZA3700ILD
    {
        ZA3000D _UserData = new ZA3000D();
        ComDisValDCol _LocationCol = new ComDisValDCol();
        ComDisValDCol _SortByCol = new ComDisValDCol();
        ComDisValDCol _pageNoCol = new ComDisValDCol();
        ZA3700BDCol _MotorDataCol = new ZA3700BDCol();

        ZA3200DCol _BodyTypeCol = new ZA3200DCol();
        ZA3200DCol _FuelTypeCol = new ZA3200DCol(); 
        ZA3200DCol _ModelCol = new ZA3200DCol();
        ZA3200DCol _BrandCol = new ZA3200DCol();
        ZA3200DCol _ColorCol = new ZA3200DCol();


        public ZA3000D UserData { get => _UserData; set => _UserData = value; }
        public ComDisValDCol LocationCol { get => _LocationCol; set => _LocationCol = value; }
        public ComDisValDCol SortByCol { get => _SortByCol; set => _SortByCol = value; }
        public ComDisValDCol PageNoCol { get => _pageNoCol; set => _pageNoCol = value; }
        public ZA3700BDCol MotorDataCol { get => _MotorDataCol; set => _MotorDataCol = value; }

        public ZA3200DCol BodyTypeCol { get => _BodyTypeCol; set => _BodyTypeCol = value; }
        public ZA3200DCol FuelTypeCol { get => _FuelTypeCol; set => _FuelTypeCol = value; }
        public ZA3200DCol ModelCol { get => _ModelCol; set => _ModelCol = value; }
        public ZA3200DCol BrandCol { get => _BrandCol; set => _BrandCol = value; }
        public ZA3200DCol ColorCol { get => _ColorCol; set => _ColorCol = value; }
    }
}
