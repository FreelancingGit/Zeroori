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
    /// 

        public class JobsmyaddsLD
        {
        JobsmyaddsDCol _jobMastCol = new JobsmyaddsDCol();
        ZA3000D _UserData = new ZA3000D();

        public JobsmyaddsDCol JobMastCol { get => _jobMastCol; set => _jobMastCol = value; }
        public ZA3000D UserData { get => _UserData; set => _UserData = value; }
    }

        public class JobsmyaddsD
        {
        //ZA3000D _UserData = new ZA3000D();
        int? _empJobMastId = null;
        int? _compnyJobMastId = null;
        int? _frelncEmpJobMastId = null;
        int? _frelncCompJobMastId = null;
        string _title = string.Empty;
        string _descrptn = string.Empty;
        string _proimg = string.Empty;
        string _jobType = string.Empty;

        //public ZA3000D UserData { get => _UserData; set => _UserData = value; }
        public int? EmpJobMastId { get => _empJobMastId; set => _empJobMastId = value; }
        public string JobType { get => _jobType; set => _jobType = value; }
        public int? FrelncEmpJobMastId { get => _frelncEmpJobMastId; set => _frelncEmpJobMastId = value; }
        public int? FrelncCompJobMastId { get => _frelncCompJobMastId; set => _frelncCompJobMastId = value; }
        public string Title { get => _title; set => _title = value; }
        public string Descrptn { get => _descrptn; set => _descrptn = value; }
        public string Proimg { get => _proimg; set => _proimg = value; }
        }

    public class JobsmyaddsDCol : System.Collections.ObjectModel.ObservableCollection<JobsmyaddsD>
    {

    }
}
