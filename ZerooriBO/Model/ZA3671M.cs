using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace ZerooriBO
{
    /// <summary>
    /// Sign Up Page
    /// User Creation
    /// </summary>
    public class ZA3671M
    {


        public ZA3631RPD DoLoad(ZA3631RPD FilterData, String Mode)
        {
            ZA3631RPD JobWantDet = new ZA3631RPD();

            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                new XElement("as_mode", Mode),
                new XElement("ai_emp_job_mast_id", FilterData.EmpJobMast.EmpJobMastID),
                new XElement("as_sessionid", FilterData.UserData.ZaBase.SessionId)
                ));

                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                DataSet ds = dbObj.SelectSP("ZA3671_sel", XString, PLABSM.DbProvider.MSSql);

                System.Data.DataTable UserDt = PLWM.Utils.GetDataTable(ds, 0);
                System.Data.DataTable EmpJobColDt = PLWM.Utils.GetDataTable(ds, 1);
                foreach (DataRow dr in EmpJobColDt.Rows)
                {
                    JobWantDet.EmpJobMast = new ZA3630D()
                    {
                        EmpJobMastID = PLWM.Utils.CnvToStr(dr["frelnc_emp_job_mast_id"]),
                        FirstName = PLWM.Utils.CnvToStr(dr["FirstName"]),
                        Gender = PLWM.Utils.CnvToStr(dr["Gender"]),
                        Title = PLWM.Utils.CnvToStr(dr["Title"]),
                        Description = PLWM.Utils.CnvToStr(dr["descr"]),
                        Mobile = PLWM.Utils.CnvToStr(dr["Mobile"]),
                        Email = PLWM.Utils.CnvToStr(dr["Email"]),
                        CurrentCompany = PLWM.Utils.CnvToStr(dr["CurrentCompany"]),
                        CurrentPos = PLWM.Utils.CnvToStr(dr["CurrentPos"]),

                        NoticePeriod = PLWM.Utils.CnvToStr(dr["NoticePeriod"]),
                        Nationality = PLWM.Utils.CnvToStr(dr["Nationality"]),
                        CurrentLoc = PLWM.Utils.CnvToStr(dr["CurLoc"]),
                        VisaStatus = PLWM.Utils.CnvToStr(dr["VisaStatus"]),
                        CarrierLevel = PLWM.Utils.CnvToStr(dr["CarrierLevl"]),
                        CurrentSalary = PLWM.Utils.CnvToStr(dr["CurrentSal"]),
                        WorkExperiance = PLWM.Utils.CnvToStr(dr["WorkExp"]),
                        EducationalLevel = PLWM.Utils.CnvToStr(dr["EduLevel"]),

                        Commitment = PLWM.Utils.CnvToStr(dr["Commitment"]),
                        CrtdDt = PLWM.Utils.CnvToStr(dr["crtd_dt"]),
                        Proimg = PLWM.Utils.CnvToStr(dr["proimg"]),
                    };
                }
            }
            catch (Exception e)
            {
                JobWantDet.UserData.ZaBase.ErrorMsg = PLWM.Utils.CnvToSentenceCase(e.Message.ToLower().Replace("plerror", "").Replace("plerror", "").Trim());
            }
            return JobWantDet;
        }

    }
}
