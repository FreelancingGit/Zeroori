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
    public class ZA3620SD
    {
        int? _ClasifdADMastID = null;
        ZA3000D _UserData = new ZA3000D();
        ZA3220D _Category = new ZA3220D();
        ZA3220D _SubCategory = new ZA3220D();
        ZA3220D _Age = new ZA3220D();
        ZA3220D _Usage = new ZA3220D();
        ZA3220D _Condition = new ZA3220D();
        ZA3220D _Warranty = new ZA3220D();
        String _Description = "";
        String _Title = "";


        public ZA3000D UserData { get => _UserData; set => _UserData = value; }
        public string Description { get => _Description; set => _Description = value; }
        public int? ClasifdADMastID { get => _ClasifdADMastID; set => _ClasifdADMastID = value; }
        public string Title { get => _Title; set => _Title = value; }

        public ZA3220D Category { get => _Category; set => _Category = value; }
        public ZA3220D SubCategory { get => _SubCategory; set => _SubCategory = value; }
        public ZA3220D Age { get => _Age; set => _Age = value; }
        public ZA3220D Usage { get => _Usage; set => _Usage = value; }
        public ZA3220D Condition { get => _Condition; set => _Condition = value; }
        public ZA3220D Warranty { get => _Warranty; set => _Warranty = value; }


    }

   
}
