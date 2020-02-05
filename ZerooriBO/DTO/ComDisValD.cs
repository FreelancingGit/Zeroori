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
    public class ComDisValD
    {
        int? _ValMembr = null;
        String _DisPlyMembr = "";
        String _Descriptn = "";

        public int? ValMembr { get => _ValMembr; set => _ValMembr = value; }
        public string DisPlyMembr { get => _DisPlyMembr; set => _DisPlyMembr = value; }
        public string Descriptn { get => _Descriptn; set => _Descriptn = value; }
    }

    public class ComDisValDCol : System.Collections.ObjectModel.ObservableCollection<ComDisValD>
    {

    }

}
