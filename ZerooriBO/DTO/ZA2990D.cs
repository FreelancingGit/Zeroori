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
    public class ZA2990D
    {
        ZA3000D _UserData = new ZA3000D();
        String _FistNam = string.Empty;
        String _LastNam = string.Empty;
        String _Email = string.Empty;
        String _Mob = string.Empty;

        public ZA3000D UserData { get => _UserData; set => _UserData = value; }
        public string FistNam { get => _FistNam; set => _FistNam = value; }
        public string LastNam { get => _LastNam; set => _LastNam = value; }
        public string Email { get => _Email; set => _Email = value; }
        public string Mob { get => _Mob; set => _Mob = value; }
    }
}
