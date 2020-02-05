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
    public class ZA2000D
    {
        String _CityMastID = string.Empty;
        String _PlaceName = string.Empty;

        public string CityMastID { get => _CityMastID; set => _CityMastID = value; }
        public string PlaceName { get => _PlaceName; set => _PlaceName = value; }
    }

    public class ZA2000DCol : System.Collections.ObjectModel.ObservableCollection<ZA2000D>
    {

    }

}
