using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZerooriBO
{
    public class ZA3720LD
    {
        ZA3000D _UserData = new ZA3000D();
        ZA3720BDCol _ClasifiedsData = new ZA3720BDCol();

        public ZA3000D UserData { get => _UserData; set => _UserData = value; }
        public ZA3720BDCol ClasifiedsData { get => _ClasifiedsData; set => _ClasifiedsData = value; }
    }


    
}
