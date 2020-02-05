using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZerooriBO
{
    public class ZA3011LDD
    {
        ZA3011D _dealmast = new ZA3011D();
        ZA3000D _UserData = new ZA3000D();
        ZA3010D _deal = new ZA3010D();

       
        public ZA3000D UserData { get => _UserData; set => _UserData = value; }
        public ZA3011D DealMast { get => _dealmast; set => _dealmast = value; }
        public ZA3010D DealM { get => _deal; set => _deal = value; }
    }


    
}
