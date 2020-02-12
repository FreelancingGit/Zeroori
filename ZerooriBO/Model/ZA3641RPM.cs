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
    public class ZA3641RPM
    {


        public ZA3641RPD DoLoad(ZA3641RPD FilterData, String Mode)
        {
            ZA3641RPD JobHirDet = new ZA3641RPD();

            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                new XElement("as_mode", Mode),
                new XElement("ai_com_job_mast_id", FilterData.ComJobMast.CompanyJobMastId),
                new XElement("as_sessionid", FilterData.UserData.ZaBase.SessionId)
                ));

                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                DataSet ds = dbObj.SelectSP("ZA3641RP_sel", XString, PLABSM.DbProvider.MSSql);

                System.Data.DataTable UserDt = PLWM.Utils.GetDataTable(ds, 0);
                System.Data.DataTable EmpJobColDt = PLWM.Utils.GetDataTable(ds, 1);
               
                foreach (DataRow dr in EmpJobColDt.Rows)
                {
                    JobHirDet.ComJobMast = new ZA3640D()
                    {
                        CompanyJobMastId = PLWM.Utils.CnvToStr(dr["compny_job_mast_id"]),
                        CompanyName = PLWM.Utils.CnvToStr(dr["compny_name"]),
                        EmpType = PLWM.Utils.CnvToStr(dr["EmpTyp"]),
                        Title = PLWM.Utils.CnvToStr(dr["job_title"]),
                        Description = PLWM.Utils.CnvToStr(dr["Descr"]),
                        Neighbr = PLWM.Utils.CnvToStr(dr["neighbrhd"]),
                        MinWorkExp = PLWM.Utils.CnvToStr(dr["MinWorkExp"]),
                        MinEduLvl = PLWM.Utils.CnvToStr(dr["MinEduLvl"]),
                        ListedBy = PLWM.Utils.CnvToStr(dr["ListedBy"]),
                        Compnysize = PLWM.Utils.CnvToStr(dr["CompnySize"]),
                        CarierLvl = PLWM.Utils.CnvToStr(dr["CarierLvl"]),
                        CurrentLoc = PLWM.Utils.CnvToStr(dr["location"]),
                        CrtdDt = PLWM.Utils.CnvToStr(dr["crtd_dt"]),
                        Phone = PLWM.Utils.CnvToStr(dr["ph_num"]),
                        Email = PLWM.Utils.CnvToStr(dr["compny_websit"]),
                        Proimg = PLWM.Utils.CnvToStr(dr["proimg"]),
						imgPath = PLWM.Utils.CnvToStr(dr["img_fullPath"])
					};

                }
                
                if (UserDt.Rows.Count > 0)
                {
                    JobHirDet.UserData = new ZA3000D()
                    {
                        UsrMastID = PLWM.Utils.CnvToInt(UserDt.Rows[0]["usr_mast_id"]),
                        FistNam = PLWM.Utils.CnvToStr(UserDt.Rows[0]["FirstName"]),
                        ZaBase = new BaseD()
                        {
                            SessionId = PLWM.Utils.CnvToStr(UserDt.Rows[0]["SESSIONID"]),
                        },
                    };
                }
                //UserData
            }
            catch (Exception e)
            {
                JobHirDet.UserData.ZaBase.ErrorMsg = PLWM.Utils.CnvToSentenceCase(e.Message.ToLower().Replace("plerror", "").Replace("plerror", "").Trim());
            }
            return JobHirDet;
        }

    }
}
