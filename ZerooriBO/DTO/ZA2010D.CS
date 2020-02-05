using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZerooriBO
{
    public class ZA2010LD
    {
        ZA2010DCol _silcol = new ZA2010DCol();
        ZA2010DCol _goldcol = new ZA2010DCol();
        ZA2010DCol _platcol = new ZA2010DCol();
        ZA3000D _UserData = new ZA3000D();
        ZA2010D _silverMast = new ZA2010D();
        ZA2010D _goldMast = new ZA2010D();
        ZA2010D _platinumMast = new ZA2010D();

        String _Otp = string.Empty;
        String _UserAccount = "";
        int _PageNo = 1;


        public ZA3000D UserData { get => _UserData; set => _UserData = value; }
        public ComDisValDCol PageNoCol { get => _pageNoCol; set => _pageNoCol = value; }
        public int PageNo { get => _PageNo; set => _PageNo = value; }
      
        public ZA2010D SilverMast { get => _silverMast; set => _silverMast = value; }
        public ZA2010D GoldMast { get => _goldMast; set => _goldMast = value; }
        public ZA2010D PlatinumMast { get => _platinumMast; set => _platinumMast = value; }

        public ZA2010DCol Silcol { get => _silcol; set => _silcol = value; }
        public ZA2010DCol Goldcol { get => _goldcol; set => _goldcol = value; }
        public ZA2010DCol Platcol { get => _platcol; set => _platcol = value; }
        public string Otp { get => _Otp; set => _Otp = value; }
        public string UserAccount { get => _UserAccount; set => _UserAccount = value; }

        ComDisValDCol _pageNoCol = new ComDisValDCol();
    }


    public class ZA2010D
    {
        int? plan_mast_id = null;
        string _plan_name = string.Empty;
        string _amount = string.Empty;
        string _curncy = string.Empty;
        string _duration = string.Empty;
        string _no_of_packs = string.Empty;
        BaseD _ZaBase = new BaseD();


        public int? PlanMastId { get => plan_mast_id; set => plan_mast_id = value; }
        public string PlaName { get => _plan_name; set => _plan_name = value; }
        public string Amount { get => _amount; set => _amount = value; }
        public string Curncy { get => _curncy; set => _curncy = value; }
        public string Duration { get => _duration; set => _duration = value; }
        public string NoOfPacks { get => _no_of_packs; set => _no_of_packs = value; }
        public BaseD ZaBase { get => _ZaBase; set => _ZaBase = value; }

    }

    public class ZA2010DCol : System.Collections.ObjectModel.ObservableCollection<ZA2010D>
    {

    }




}
