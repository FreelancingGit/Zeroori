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
    public class ZA3611SD
    {
        ZA3000D _UserData = new ZA3000D();
        int? _AddPropADMastID = null;
        String _AdSeq = "";
        String _PHNo = "";
        Double? _Price = null;
        ComDisValDCol _FileNames = new ComDisValDCol();
        ZA2000DCol _LocationCol = new ZA2000DCol();
        ZA2000D _Location = new ZA2000D();

        public ZA3000D UserData { get => _UserData; set => _UserData = value; }
        public int? AddPropADMastID { get => _AddPropADMastID; set => _AddPropADMastID = value; }
        public string AdSeq { get => _AdSeq; set => _AdSeq = value; }
        public string PHNo { get => _PHNo; set => _PHNo = value; }
        public double? Price { get => _Price; set => _Price = value; }
        public ComDisValDCol FileNames { get => _FileNames; set => _FileNames = value; }

        public ZA2000DCol LocationCol { get => _LocationCol; set => _LocationCol = value; }
        public ZA2000D Location { get => _Location; set => _Location = value; }
    }
}
