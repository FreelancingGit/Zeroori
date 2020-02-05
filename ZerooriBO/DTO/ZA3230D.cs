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
    public class ZA3230D
    {
        int? _EmpJobDtlId = null;
        String _EmpJobValue = string.Empty;
        int? _EmpJobMastId = null;

        public int? EmpJobDtlId { get => _EmpJobDtlId; set => _EmpJobDtlId = value; }
        public string EmpJobValue { get => _EmpJobValue; set => _EmpJobValue = value; }
        public int? EmpJobMastId { get => _EmpJobMastId; set => _EmpJobMastId = value; }
    }


    public class ZA3230DCol : System.Collections.ObjectModel.ObservableCollection<ZA3230D>
    {

    }


}
