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
    public class ZA3630RPM
    {
        public ZA3630RPD DoLoad(ZA3630RPD FilterData, String Mode)
        {
            ZA3630RPD UsageD = new ZA3630RPD();

            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                new XElement("as_mode", Mode),
                new XElement("as_sessionid", FilterData.UserData.ZaBase.SessionId),
                new XElement("ai_pageno", FilterData.PageNo)
                ));

                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                DataSet ds = dbObj.SelectSP("ZA3630RP_sel", XString, PLABSM.DbProvider.MSSql);

                System.Data.DataTable UserDt = PLWM.Utils.GetDataTable(ds, 0);
                System.Data.DataTable MallcolDt = PLWM.Utils.GetDataTable(ds, 1);
                System.Data.DataTable PageNoDt = PLWM.Utils.GetDataTable(ds, 2);

                if (UserDt.Rows.Count > 0)
                {
                    UsageD.UserData = new ZA3000D()
                    {
                        UsrMastID = PLWM.Utils.CnvToInt(UserDt.Rows[0]["usr_mast_id"]),
                        FistNam = PLWM.Utils.CnvToStr(UserDt.Rows[0]["FirstName"]),
                        ZaBase = new BaseD()
                        {
                            SessionId = PLWM.Utils.CnvToStr(UserDt.Rows[0]["SESSIONID"]),
                        },
                    };
                }

                foreach (DataRow dr in MallcolDt.Rows)
                {
                    UsageD.EmpJobCol.Add(new ZA3630D()
                    {
                        EmpJobMastID= PLWM.Utils.CnvToStr(dr["emp_job_mast_id"]),
                        FirstName= PLWM.Utils.CnvToStr(dr["FirstName"]),
                        Title = PLWM.Utils.CnvToStr(dr["Title"]),
                        CrtdDt = PLWM.Utils.CnvToStr(dr["crtd_dt"]),
                        WorkExperiance= PLWM.Utils.CnvToStr(dr["WorkExp"]),
                        CurrentLoc = PLWM.Utils.CnvToStr(dr["CurLoc"]),
                        EducationalLevel= PLWM.Utils.CnvToStr(dr["EduLevel"]),
                        Proimg= PLWM.Utils.CnvToStr(dr["proimg"])
                    });

                }
                UsageD.PageNoCol = new ComDisValDCol();
                foreach (DataRow Dr in PageNoDt.Rows)
                {
                    UsageD.PageNoCol.Add(new ComDisValD()
                    {
                        ValMembr = PLWM.Utils.CnvToInt(Dr["TotalPages"]),
                        DisPlyMembr = PLWM.Utils.CnvToStr(Dr["Page_No"]),
                    });
                }

                UsageD.ReportypCol.Add(new ComDisValD()
                {
                    DisPlyMembr = "Hiring",
                    ValMembr = 0
                });

                UsageD.ReportypCol.Add(new ComDisValD()
                {
                    DisPlyMembr = "Jobs Wanted",
                    ValMembr = 1
                });

            }
            catch (Exception e)
            {
                UsageD.UserData.ZaBase.ErrorMsg = PLWM.Utils.CnvToSentenceCase(e.Message.ToLower().Replace("plerror", "").Replace("plerror", "").Trim());
            }
            return UsageD;
        }
       
        
    }
}
