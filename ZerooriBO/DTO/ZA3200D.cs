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
    public class ZA3200D
    {
        int? _motorSpecDtlId = null;
        String _motorSpecValue = string.Empty;
        int? _motorSpecMastId = null;
        String _motorSpecImgPath = "";



        public string MotorSpecValue { get => _motorSpecValue; set => _motorSpecValue = value; }
        public int? MotorSpecMastId { get => _motorSpecMastId; set => _motorSpecMastId = value; }
        public int? MotorSpecDtlId { get => _motorSpecDtlId; set => _motorSpecDtlId = value; }
        public string MotorSpecImgPath { get => _motorSpecImgPath; set => _motorSpecImgPath = value; }
    }


    public class ZA3200DCol : System.Collections.ObjectModel.ObservableCollection<ZA3200D>
    {

    }


}
