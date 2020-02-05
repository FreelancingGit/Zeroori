using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZerooriBO
{
    public class ZA3710BD
    {
        
        String _Title = "";
        String _PostDate = "";
        String _Rate = "";
        String _ProductImage = "";
        String _Location = "";
        String _BedRoom = "";
        String _BathRoom = "";
        String _Area = "";
        String _ISFurnised = "";
        String _PhNo = "";
        String _Email = "";
        String _PropAdMastId = "";

        public string Title { get => _Title; set => _Title = value; }
        public string PostDate { get => _PostDate; set => _PostDate = value; }
        public string Rate { get => _Rate; set => _Rate = value; }
        public string ProductImage { get => _ProductImage; set => _ProductImage = value; }
        public string Location { get => _Location; set => _Location = value; }
        public string PhNo { get => _PhNo; set => _PhNo = value; }
        public string Email { get => _Email; set => _Email = value; }
        public string BedRoom { get => _BedRoom; set => _BedRoom = value; }
        public string BathRoom { get => _BathRoom; set => _BathRoom = value; }
        public string Area { get => _Area; set => _Area = value; }
        public string ISFurnised { get => _ISFurnised; set => _ISFurnised = value; }
        public string PropAdMastId { get => _PropAdMastId; set => _PropAdMastId = value; }
    }


    public class ZA3710BDCol : System.Collections.ObjectModel.ObservableCollection<ZA3710BD>
    {

    }
}
