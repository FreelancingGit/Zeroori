using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZerooriBO
{
    public class ZA3711LD
    {
    
        String _PropAdMastId = String.Empty;
        String _PropTitle = String.Empty;
        String _CrtdDt = string.Empty;
        String _bedRoom = String.Empty;
        String _bathRoom = String.Empty;
        String _Size = String.Empty;
        String _IsFurnished = String.Empty;
        String _Appartment = String.Empty;
        String _RentIsPaid = String.Empty;
        String _ListedBy = String.Empty;
        String _Category = String.Empty;
        String _City = String.Empty;
        String _UserMast = String.Empty;
        String _UsrEmail = String.Empty;
        String _UsrPhno = String.Empty;
        String _PropDescription = String.Empty;
        String _placeName = String.Empty;
        
        String _Location = String.Empty;
        String _Price = String.Empty;
        ComDisValDCol _FileNames = new ComDisValDCol();
        ZA3000D _UserData = new ZA3000D();

        public string PropAdMastId { get => _PropAdMastId; set => _PropAdMastId = value; }
        public string PropTitle { get => _PropTitle; set => _PropTitle = value; }
        public string CrtdDt { get => _CrtdDt; set => _CrtdDt = value; }
        public string BedRoom { get => _bedRoom; set => _bedRoom = value; }
        public string BathRoom { get => _bathRoom; set => _bathRoom = value; }
        public string Size { get => _Size; set => _Size = value; }
        public string IsFurnished { get => _IsFurnished; set => _IsFurnished = value; }
        public string Appartment { get => _Appartment; set => _Appartment = value; }
        public string RentIsPaid { get => _RentIsPaid; set => _RentIsPaid = value; }
        public string ListedBy { get => _ListedBy; set => _ListedBy = value; }
        public string Category { get => _Category; set => _Category = value; }
        public string City { get => _City; set => _City = value; }
        public string UserMast { get => _UserMast; set => _UserMast = value; }
        public string UsrEmail { get => _UsrEmail; set => _UsrEmail = value; }
        public string UsrPhno { get => _UsrPhno; set => _UsrPhno = value; }
        public string PropDescription { get => _PropDescription; set => _PropDescription = value; }
        public string PlaceName { get => _placeName; set => _placeName = value; }
        public string Location { get => _Location; set => _Location = value; }
        public string Price { get => _Price; set => _Price = value; }
        public ComDisValDCol FileNames { get => _FileNames; set => _FileNames = value; }
        public ZA3000D UserData { get => _UserData; set => _UserData = value; }
    }
}
