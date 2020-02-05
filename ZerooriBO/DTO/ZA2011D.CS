using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZerooriBO
{
    public class ZA2011LD
    {
        ZA2011DCol _silvercol = new ZA2011DCol();
        ZA2011DCol _goldcol = new ZA2011DCol();
        ZA2011DCol _platinumcol = new ZA2011DCol();
        ZA3000D _UserData = new ZA3000D();

        public ZA2011DCol SilverCol { get => _silvercol; set => _silvercol = value; }
        public ZA2011DCol GoldCol { get => _goldcol; set => _goldcol = value; }
        public ZA2011DCol PlatinumCol { get => _platinumcol; set => _platinumcol = value; }
        public ZA3000D UserData { get => _UserData; set => _UserData = value; }
    }


    public class ZA2011D
    {
        int? plan_mast_id = null;
        string _plan_value = string.Empty;

        public int? PlanMastId { get => plan_mast_id; set => plan_mast_id = value; }
        public string PlanValue { get => _plan_value; set => _plan_value = value; }
    }

    public class ZA2011DCol : System.Collections.ObjectModel.ObservableCollection<ZA2011D>
    {

    }




}
