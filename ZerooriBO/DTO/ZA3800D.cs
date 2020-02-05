using System;
using System.Linq;
using System.Text;

namespace ZerooriBO
{
    /// <summary>
    /// Sign Up Page
    /// User Creation
    /// </summary>
    public class ZA3800D
    {
        ZA3000D _UserData = new ZA3000D();
        int? _jobCountJW = null;
        int? _jobCountJH = null;
        int? _jobCountFW = null;
        int? _jobCountFH = null;
        int? _motorsCount = null;
        int? _clasiifiedsCount = null;
        int? _propertiesCount = null;


        public ZA3000D UserData { get => _UserData; set => _UserData = value; }
        public int? MotorsCount { get => _motorsCount; set => _motorsCount = value; }
        public int? ClasiifiedsCount { get => _clasiifiedsCount; set => _clasiifiedsCount = value; }
        public int? PropertiesCount { get => _propertiesCount; set => _propertiesCount = value; }
        public int? JobCountJW { get => _jobCountJW; set => _jobCountJW = value; }
        public int? JobCountJH { get => _jobCountJH; set => _jobCountJH = value; }
        public int? JobCountFW { get => _jobCountFW; set => _jobCountFW = value; }
        public int? JobCountFH { get => _jobCountFH; set => _jobCountFH = value; }
    }
}
