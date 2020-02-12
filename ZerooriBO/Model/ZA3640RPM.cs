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
    public class ZA3640RPM
    {
        public ZA3640LD DoInit(ZA3640D FilterData, String Mode)
        {
            ZA3640LD UsageD = new ZA3640LD();

            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                new XElement("as_mode", Mode),
                new XElement("ai_pageno", FilterData.PageNo),
                new XElement("as_sessionid", FilterData.UserData.ZaBase.SessionId),
                new XElement("as_jobfilter", FilterData.IndstryCol.EmpJobDtlId)
                ));

                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                DataSet ds = dbObj.SelectSP("ZA3640RP_sel", XString, PLABSM.DbProvider.MSSql);

                System.Data.DataTable UserDt = PLWM.Utils.GetDataTable(ds, 0);
                System.Data.DataTable CompnycolDt = PLWM.Utils.GetDataTable(ds, 1);
                System.Data.DataTable PageNoDt = PLWM.Utils.GetDataTable(ds, 2);
                System.Data.DataTable filterDt = PLWM.Utils.GetDataTable(ds, 3);

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

                foreach (DataRow dr in CompnycolDt.Rows)
                {
                    UsageD.CompnyJobCol.Add(new ZA3640D()
                    {
                        CompanyJobMastId = PLWM.Utils.CnvToStr(dr["compny_job_mast_id"]),
                        CompanyName = PLWM.Utils.CnvToStr(dr["compny_name"]),
                        Title = PLWM.Utils.CnvToStr(dr["job_title"]),
                        CrtdDt = PLWM.Utils.CnvToStr(dr["Crtd_dt"]),
                        CurrentLoc = PLWM.Utils.CnvToStr(dr["location"]),
                        Description = PLWM.Utils.CnvToStr(dr["Descr"]),
						imgPath = PLWM.Utils.CnvToStr(dr["img_fullPath"])
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

                UsageD.IndustryCol.Add(new ZA3230D()
                {
                    EmpJobDtlId = null,
                    EmpJobValue = "All",
                });
                foreach (DataRow dr in filterDt.Rows)
                {
                    UsageD.IndustryCol.Add(new ZA3230D()
                    {
                        EmpJobDtlId = PLWM.Utils.CnvToNullableInt(dr["Emp_Job_Dtl_Id"]),
                        EmpJobValue = PLWM.Utils.CnvToStr(dr["Emp_Job_Value"]),
                    });
                }

            }
            catch (Exception e)
            {
                UsageD.UserData.ZaBase.ErrorMsg = PLWM.Utils.CnvToSentenceCase(e.Message.ToLower().Replace("plerror", "").Replace("plerror", "").Trim());
            }
            return UsageD;
        }

        public ZA3640RPD DoLoad(ZA3640D FilterData, String Mode)
        {
            ZA3640RPD UsageD = new ZA3640RPD();

            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                new XElement("as_mode", Mode),
                new XElement("ai_pageno", FilterData.PageNo),
                new XElement("as_sessionid", FilterData.UserData.ZaBase.SessionId),
                new XElement("as_jobfilter", FilterData.IndstryCol.EmpJobDtlId)
                ));
                
                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                DataSet ds = dbObj.SelectSP("ZA3640RP_sel", XString, PLABSM.DbProvider.MSSql);

                System.Data.DataTable UserDt = PLWM.Utils.GetDataTable(ds, 0);
                System.Data.DataTable CompnycolDt = PLWM.Utils.GetDataTable(ds, 1);
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

                foreach (DataRow dr in CompnycolDt.Rows)
                {
                    UsageD.CompnyJobCol.Add(new ZA3640D()
                    {
                        CompanyJobMastId = PLWM.Utils.CnvToStr(dr["compny_job_mast_id"]),
                        CompanyName = PLWM.Utils.CnvToStr(dr["compny_name"]),
                        Title = PLWM.Utils.CnvToStr(dr["job_title"]),
                        CrtdDt = PLWM.Utils.CnvToStr(dr["Crtd_dt"]),
                        CurrentLoc = PLWM.Utils.CnvToStr(dr["location"]),
                        Description = PLWM.Utils.CnvToStr(dr["Descr"]),
						imgPath = PLWM.Utils.CnvToStr(dr["img_fullPath"])
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
