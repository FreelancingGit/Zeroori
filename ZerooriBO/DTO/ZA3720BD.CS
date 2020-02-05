using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZerooriBO
{
    public class ZA3720BD
    {
        
        String _Title = "";
        String _PostDate = "";
        String _Rate = "";
        String _ProductImage = "";
        String _Location = "";
        String _Age = "";
        String _Usage = "";
        String _Condition = "";
        String _Warranty = "";
        String _PhNo = "";
        String _Email = "";
        String _ClasifdAdMastId = "";

        public string Title { get => _Title; set => _Title = value; }
        public string PostDate { get => _PostDate; set => _PostDate = value; }
        public string Rate { get => _Rate; set => _Rate = value; }
        public string ProductImage { get => _ProductImage; set => _ProductImage = value; }
        public string Location { get => _Location; set => _Location = value; }
        public string Age { get => _Age; set => _Age = value; }
        public string Usage { get => _Usage; set => _Usage = value; }
        public string Condition { get => _Condition; set => _Condition = value; }
        public string Warranty { get => _Warranty; set => _Warranty = value; }
        public string PhNo { get => _PhNo; set => _PhNo = value; }
        public string Email { get => _Email; set => _Email = value; }
        public string ClasifdAdMastId { get => _ClasifdAdMastId; set => _ClasifdAdMastId = value; }
    }


    public class ZA3720BDCol : System.Collections.ObjectModel.ObservableCollection<ZA3720BD>
    {

    }
}
