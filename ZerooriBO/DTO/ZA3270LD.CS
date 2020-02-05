using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZerooriBO
{
    public class ZA3270LD
    {
        ZA3270DCol _mallcol = new ZA3270DCol();
        ComDisValDCol _FileNames = new ComDisValDCol();
        ZA3000D _UserData = new ZA3000D();
        int _PageNo = 1;

        public ZA3270DCol Mallcol { get => _mallcol; set => _mallcol = value; }
        public ComDisValDCol FileNames { get => _FileNames; set => _FileNames = value; }
        public ZA3000D UserData { get => _UserData; set => _UserData = value; }
        public ComDisValDCol PageNoCol { get => _pageNoCol; set => _pageNoCol = value; }
        public int PageNo { get => _PageNo; set => _PageNo = value; }

        ComDisValDCol _pageNoCol = new ComDisValDCol();
    }


    public class ZA3270D
    {
        String _MallAdMastId = String.Empty;
        String _mallName = String.Empty;
        String _mallLocation = String.Empty;
        String _mallStartTiming = string.Empty;
        String _mallEndTiming = String.Empty;
        String _mallPhone = String.Empty;
        String _mallEmaild = String.Empty;
        String _mallUrl = String.Empty;
        String _mallDecrp = String.Empty;
        String _mallMastImgPath = String.Empty;
        BaseD _ZaBase = new BaseD();

      

        public string MallAdMastId { get => _MallAdMastId; set => _MallAdMastId = value; }
        public string MallStartTiming { get => _mallStartTiming; set => _mallStartTiming = value; }
        public string MallEndTiming { get => _mallEndTiming; set => _mallEndTiming = value; }
        public string MallPhone { get => _mallPhone; set => _mallPhone = value; }
        public string MallEmaild { get => _mallEmaild; set => _mallEmaild = value; }
        public string MallUrl { get => _mallUrl; set => _mallUrl = value; }
        public string MallDecrp { get => _mallDecrp; set => _mallDecrp = value; }
       

        public string MallName { get => _mallName; set => _mallName = value; }
        public string MallLocation { get => _mallLocation; set => _mallLocation = value; }
        public BaseD ZaBase { get => _ZaBase; set => _ZaBase = value; }
        public string MallMastImgPath { get => _mallMastImgPath; set => _mallMastImgPath = value; }
    }

    public class ZA3270DCol : System.Collections.ObjectModel.ObservableCollection<ZA3270D>
    {

    }




}
