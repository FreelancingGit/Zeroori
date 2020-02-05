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
    public class ZA3220D
    {
        int? _ClasifdSpecDtlId = null;
        String _ClasifdSpecValue = string.Empty;
        int? _ClasifdSpecMastId = null;

        public int? ClasifdSpecDtlId { get => _ClasifdSpecDtlId; set => _ClasifdSpecDtlId = value; }
        public string ClasifdSpecValue { get => _ClasifdSpecValue; set => _ClasifdSpecValue = value; }
        public int? ClasifdSpecMastId { get => _ClasifdSpecMastId; set => _ClasifdSpecMastId = value; }
    }


    public class ZA3220DCol : System.Collections.ObjectModel.ObservableCollection<ZA3220D>
    {

    }


}
