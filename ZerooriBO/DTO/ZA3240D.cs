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
    public class ZA3240D
    {
        int? _usrBusinesMastId = null;
        int? _usrPlanMastId = null;
        String _PlanName = string.Empty;
        String _usrBusinesName = string.Empty;
        String _usrBusinesUrl = string.Empty;
        String _categryMastId = string.Empty;
        String _titleLogoUrl = string.Empty;
        String _businesLogoUrl = string.Empty;
        String _FacebookUrl = string.Empty;
        String _InstagramUrl = string.Empty;
        String _TwitterUrl = string.Empty;
        String _PhoneNo = string.Empty;
        String _EmailId = string.Empty;
        String _businesUrl = string.Empty;
        String _geoLocation = string.Empty;
        String _DescriptionAboutus = string.Empty;
        ZA3000D _UserData = new ZA3000D();

        public int? UsrBusinesMastId { get => _usrBusinesMastId; set => _usrBusinesMastId = value; }
        public int? UsrPlanMastId { get => _usrPlanMastId; set => _usrPlanMastId = value; }
        public string UsrBusinesName { get => _usrBusinesName; set => _usrBusinesName = value; }
        public string UsrBusinesUrl { get => _usrBusinesUrl; set => _usrBusinesUrl = value; }
        public string CategryMastId { get => _categryMastId; set => _categryMastId = value; }
        public string TitleLogoUrl { get => _titleLogoUrl; set => _titleLogoUrl = value; }
        public string BusinesLogoUrl { get => _businesLogoUrl; set => _businesLogoUrl = value; }
        public string FacebookUrl { get => _FacebookUrl; set => _FacebookUrl = value; }
        public string InstagramUrl { get => _InstagramUrl; set => _InstagramUrl = value; }
        public string TwitterUrl { get => _TwitterUrl; set => _TwitterUrl = value; }
        public string PhoneNo { get => _PhoneNo; set => _PhoneNo = value; }
        public string EmailId { get => _EmailId; set => _EmailId = value; }
        public string BusinesUrl { get => BusinesUrl1; set => BusinesUrl1 = value; }
        public string BusinesUrl1 { get => _businesUrl; set => _businesUrl = value; }
        public string GeoLocation { get => _geoLocation; set => _geoLocation = value; }
        public string DescriptionAboutus { get => _DescriptionAboutus; set => _DescriptionAboutus = value; }
        public string PlanName { get => _PlanName; set => _PlanName = value; }
        public ZA3000D UserData { get => _UserData; set => _UserData = value; }
    }

    public class ZA3240DCol : System.Collections.ObjectModel.ObservableCollection<ZA3240D>
    {

    }

    public class ZA3240LD
    {
        ZA3240DCol _business = new ZA3240DCol();
        ZA3000D _UserData = new ZA3000D();

        public ZA3000D UserData { get => _UserData; set => _UserData = value; }
        public ZA3240DCol Business { get => _business; set => _business = value; }
    }

}