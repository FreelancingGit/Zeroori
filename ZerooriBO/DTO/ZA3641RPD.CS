using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZerooriBO
{
    public class ZA3641RPD
    {
        ZA3640D _comJobMast = new ZA3640D();
        ComDisValDCol _FileNames = new ComDisValDCol();
        ZA3000D _UserData = new ZA3000D();

        public ZA3640D ComJobMast { get => _comJobMast; set => _comJobMast = value; }
        public ComDisValDCol FileNames { get => _FileNames; set => _FileNames = value; }
        public ZA3000D UserData { get => _UserData; set => _UserData = value; }
    }
}
