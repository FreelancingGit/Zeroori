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
    public class ZA3210D
    {
        int? _PropSpecDtlId = null;
        String _PropSpecValue = string.Empty;
        int? _PropSpecMastId = null;

        public int? PropSpecDtlId { get => _PropSpecDtlId; set => _PropSpecDtlId = value; }
        public string PropSpecValue { get => _PropSpecValue; set => _PropSpecValue = value; }
        public int? PropSpecMastId { get => _PropSpecMastId; set => _PropSpecMastId = value; }
    }


    public class ZA3210DCol : System.Collections.ObjectModel.ObservableCollection<ZA3210D>
    {

    }


}
