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
    public class ZA3620LD
    {

        int? _ClasifdADMastID = null;
        ZA3000D _UserData = new ZA3000D();
        ZA3220DCol _CategoryCol = new ZA3220DCol();
        ZA3220DCol _SubCategoryCol = new ZA3220DCol();
        ZA3220DCol _AgeCol = new ZA3220DCol();
        ZA3220DCol _UsageCol = new ZA3220DCol();
        ZA3220DCol _ConditionCol = new ZA3220DCol();
        ZA3220DCol _WarrantyCol = new ZA3220DCol();
        ZA2000DCol _LocationCol = new ZA2000DCol();
        ZA3620SD _SelectedData = new ZA3620SD();
        ZA3621SD _SelectedDataDtl = new ZA3621SD();

        public ZA3000D UserData { get => _UserData; set => _UserData = value; }
        public int? ClasifdADMastID { get => _ClasifdADMastID; set => _ClasifdADMastID = value; }
        public ZA3220DCol CategoryCol { get => _CategoryCol; set => _CategoryCol = value; }
        public ZA3220DCol SubCategoryCol { get => _SubCategoryCol; set => _SubCategoryCol = value; }
        public ZA3220DCol AgeCol { get => _AgeCol; set => _AgeCol = value; }
        public ZA3220DCol UsageCol { get => _UsageCol; set => _UsageCol = value; }
        public ZA3220DCol ConditionCol { get => _ConditionCol; set => _ConditionCol = value; }
        public ZA3220DCol WarrantyCol { get => _WarrantyCol; set => _WarrantyCol = value; }
        public ZA3620SD SelectedData { get => _SelectedData; set => _SelectedData = value; }
        public ZA3621SD SelectedDataDtl { get => _SelectedDataDtl; set => _SelectedDataDtl = value; }
        public ZA2000DCol LocationCol { get => _LocationCol; set => _LocationCol = value; }
    }


   


}
