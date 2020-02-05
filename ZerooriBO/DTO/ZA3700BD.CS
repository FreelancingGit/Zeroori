using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZerooriBO
{
    public class ZA3700BD
    {
        String _Title = "";
        String _PostDate = "";
        String _Rate = "";
        String _ProductImage = "";
        String _Location = "";
        String _Years = "";
        String _Kmters = "";
        String _Doors = "";
        String _Colors  = "";
        String _PhNo = "";
        String _Email = "";
        String _MotorsAdMastId = "";

        public string Title { get => _Title; set => _Title = value; }
        public string PostDate { get => _PostDate; set => _PostDate = value; }
        public string Rate { get => _Rate; set => _Rate = value; }
        public string ProductImage { get => _ProductImage; set => _ProductImage = value; }
        public string Location { get => _Location; set => _Location = value; }
        public string Years { get => _Years; set => _Years = value; }
        public string Kmters { get => _Kmters; set => _Kmters = value; }
        public string Doors { get => _Doors; set => _Doors = value; }
        public string Colors { get => _Colors; set => _Colors = value; }
        public string PhNo { get => _PhNo; set => _PhNo = value; }
        public string Email { get => _Email; set => _Email = value; }
        public string MotorsAdMastId { get => _MotorsAdMastId; set => _MotorsAdMastId = value; }
    }


    public class ZA3700BDCol : System.Collections.ObjectModel.ObservableCollection<ZA3700BD>
    {

    }
}
