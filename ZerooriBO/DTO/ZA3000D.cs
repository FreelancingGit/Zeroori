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
    public class ZA3000D
    {
        String _FistNam = string.Empty;
        String _LastNam = string.Empty;
        String _Email = string.Empty;
        String _Mob = string.Empty;
        String _OldPasswd = string.Empty;
        String _passwd = string.Empty;
        String _Cpasswd = string.Empty;
        BaseD _ZaBase = new BaseD();
        int? _usrMastID = null;
        String _Otp = string.Empty;
        bool _AcceptAgrement = false;

        public string FistNam { get => _FistNam; set => _FistNam = value; }
        public string LastNam { get => _LastNam; set => _LastNam = value; }
        public string Email { get => _Email; set => _Email = value; }
        public string Mob { get => _Mob; set => _Mob = value; }
        public string Passwd { get => _passwd; set => _passwd = value; }
        public string Cpasswd { get => _Cpasswd; set => _Cpasswd = value; }
        public BaseD ZaBase { get => _ZaBase; set => _ZaBase = value; }
        public int? UsrMastID { get => _usrMastID; set => _usrMastID = value; }
        public string Otp { get => _Otp; set => _Otp = value; }
        public bool AcceptAgrement { get => _AcceptAgrement; set => _AcceptAgrement = value; }
        public string OldPasswd { get => _OldPasswd; set => _OldPasswd = value; }
    }
}
