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
    public class ZA3680M
    {
        public ZA3650LD DoLoad(ZA3650SD FilterData, String Mode)
        {
            ZA3650LD UsageD = new ZA3650LD();

            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                new XElement("as_sessionid", FilterData.UserData.ZaBase.SessionId),
                new XElement("ai_frelnc_comp_job_mast_id", FilterData.UserData.ZaBase.PKID),
                new XElement("ai_pageno", ""),
                new XElement("as_mode", Mode)
                ));


                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                DataSet ds = dbObj.SelectSP("ZA3680_sel", XString, PLABSM.DbProvider.MSSql);

                System.Data.DataTable IndstryTypDt = PLWM.Utils.GetDataTable(ds, 0);
                System.Data.DataTable CompnySizeDt = PLWM.Utils.GetDataTable(ds, 1);
                System.Data.DataTable EmplymntTypeDt = PLWM.Utils.GetDataTable(ds, 2);
                System.Data.DataTable MonthlySlryDt = PLWM.Utils.GetDataTable(ds, 3);
                System.Data.DataTable ExprnceDt = PLWM.Utils.GetDataTable(ds, 4);
                System.Data.DataTable EductnLvlDt = PLWM.Utils.GetDataTable(ds, 5);
                System.Data.DataTable ListedByDt = PLWM.Utils.GetDataTable(ds, 6);
                System.Data.DataTable CareerLvlDt = PLWM.Utils.GetDataTable(ds, 7);
                System.Data.DataTable dtUser = PLWM.Utils.GetDataTable(ds, 8);
                System.Data.DataTable dtSel = PLWM.Utils.GetDataTable(ds, 9);


                DataRow drUser = null;
                if (dtUser.Rows.Count > 0)
                {
                    drUser = dtUser.Rows[0];
                }

                UsageD.IndstryCol = new ZA3230DCol();
                UsageD.IndstryCol.Add(new ZA3230D() {  EmpJobDtlId = -1,  EmpJobValue = "Jobs" });
                foreach (DataRow dr in IndstryTypDt.Rows)
                {
                    UsageD.IndstryCol.Add(new ZA3230D()
                    {
                        EmpJobDtlId = PLWM.Utils.CnvToNullableInt(dr["Emp_Job_Dtl_Id"]),
                        EmpJobValue = PLWM.Utils.CnvToStr(dr["Emp_Job_Value"]),
                    });
                }



                UsageD.CompnySizeCol = new ZA3230DCol();
                UsageD.CompnySizeCol.Add(new ZA3230D() { EmpJobDtlId = -1, EmpJobValue = "Company Size" });
                foreach (DataRow dr in CompnySizeDt.Rows)
                {
                    UsageD.CompnySizeCol.Add(new ZA3230D()
                    {
                        EmpJobDtlId = PLWM.Utils.CnvToNullableInt(dr["Emp_Job_Dtl_Id"]),
                        EmpJobValue = PLWM.Utils.CnvToStr(dr["Emp_Job_Value"]),
                    });
                }

                UsageD.EmploymntTypeCol = new ZA3230DCol();
                UsageD.EmploymntTypeCol.Add(new ZA3230D() { EmpJobDtlId = -1, EmpJobValue = "Commitment" });
                foreach (DataRow dr in EmplymntTypeDt.Rows)
                {
                    UsageD.EmploymntTypeCol.Add(new ZA3230D()
                    {
                        EmpJobDtlId = PLWM.Utils.CnvToNullableInt(dr["Emp_Job_Dtl_Id"]),
                        EmpJobValue = PLWM.Utils.CnvToStr(dr["Emp_Job_Value"]),
                    });
                }


                UsageD.MonthlySalaryCol = new ZA3230DCol();
                UsageD.MonthlySalaryCol.Add(new ZA3230D() { EmpJobDtlId = -1, EmpJobValue = "Current Salary" });
                foreach (DataRow dr in MonthlySlryDt.Rows)
                {
                    UsageD.MonthlySalaryCol.Add(new ZA3230D()
                    {
                        EmpJobDtlId = PLWM.Utils.CnvToNullableInt(dr["Emp_Job_Dtl_Id"]),
                        EmpJobValue = PLWM.Utils.CnvToStr(dr["Emp_Job_Value"]),
                    });
                }

                UsageD.ExprnceCol = new ZA3230DCol();
                UsageD.ExprnceCol.Add(new ZA3230D() { EmpJobDtlId = -1, EmpJobValue = "Work Experience" });
                foreach (DataRow dr in ExprnceDt.Rows)
                {
                    UsageD.ExprnceCol.Add(new ZA3230D()
                    {
                        EmpJobDtlId = PLWM.Utils.CnvToNullableInt(dr["Emp_Job_Dtl_Id"]),
                        EmpJobValue = PLWM.Utils.CnvToStr(dr["Emp_Job_Value"]),
                    });
                }


                UsageD.EductnLevlCol = new ZA3230DCol();
                UsageD.EductnLevlCol.Add(new ZA3230D() { EmpJobDtlId = -1, EmpJobValue = "Education Level" });
                foreach (DataRow dr in EductnLvlDt.Rows)
                {
                    UsageD.EductnLevlCol.Add(new ZA3230D()
                    {
                        EmpJobDtlId = PLWM.Utils.CnvToNullableInt(dr["Emp_Job_Dtl_Id"]),
                        EmpJobValue = PLWM.Utils.CnvToStr(dr["Emp_Job_Value"]),
                    });
                }

                UsageD.ListedBycol = new ZA3230DCol();
                UsageD.ListedBycol.Add(new ZA3230D() { EmpJobDtlId = -1, EmpJobValue = "Listed By" });
                foreach (DataRow dr in ListedByDt.Rows)
                {
                    UsageD.ListedBycol.Add(new ZA3230D()
                    {
                        EmpJobDtlId = PLWM.Utils.CnvToNullableInt(dr["Emp_Job_Dtl_Id"]),
                        EmpJobValue = PLWM.Utils.CnvToStr(dr["Emp_Job_Value"]),
                    });
                }

                UsageD.CareervLevelCol  = new ZA3230DCol();
                UsageD.CareervLevelCol.Add(new ZA3230D() { EmpJobDtlId = -1, EmpJobValue = "Career Level" });
                foreach (DataRow dr in CareerLvlDt.Rows)
                {
                    UsageD.CareervLevelCol.Add(new ZA3230D()
                    {
                        EmpJobDtlId = PLWM.Utils.CnvToNullableInt(dr["Emp_Job_Dtl_Id"]),
                        EmpJobValue = PLWM.Utils.CnvToStr(dr["Emp_Job_Value"]),
                    });
                }

                if (drUser != null)
                {
                    UsageD.UserData = new ZA3000D()
                    {
                        ZaBase = new BaseD()
                        {
                            SessionId = PLWM.Utils.CnvToStr(drUser["SessionId"]),
                            UserName = PLWM.Utils.CnvToStr(drUser["FirstName"]),
                            ErrorMsg = "",
                            ZaKey = Utils.GetKey()
                        }
                    };
                }

                foreach (DataRow dr in dtSel.Rows)
                {
                    UsageD.FrelncMast = new ZA3650SD()
                    {
                        CompnyJobMastId = PLWM.Utils.CnvToInt(dr["frelnc_comp_job_mast_id"]),
                        CompnyName = PLWM.Utils.CnvToStr(dr["comp_name"]),
                        TradeLicns = PLWM.Utils.CnvToStr(dr["trade_licns"]),
                        ContctName = PLWM.Utils.CnvToStr(dr["contct_name"]),
                        Phone = PLWM.Utils.CnvToStr(dr["ph_num"]),
                        CompnyWebsit = PLWM.Utils.CnvToStr(dr["compny_websit"]),
                        Addrs = PLWM.Utils.CnvToStr(dr["addrs"]),
                        DescrpnStepOne = PLWM.Utils.CnvToStr(dr["descrpn_step_one"]),
                        Indstry = UsageD.IndstryCol.FirstOrDefault(x => x.EmpJobDtlId ==
                                      PLWM.Utils.CnvToInt(dtSel.Rows[0]["indstry"])),
                        CompnySize = UsageD.CompnySizeCol.FirstOrDefault(x => x.EmpJobDtlId ==
                                      PLWM.Utils.CnvToInt(dtSel.Rows[0]["compny_size"])),

                        //StepTwo
                        JobTitle = PLWM.Utils.CnvToStr(dr["job_title"]),
                        Neighbrhd = PLWM.Utils.CnvToStr(dr["neighbrhd"]),
                      //  Location = PLWM.Utils.CnvToStr(dr["location"]),
                        DescrptnStepTwo = PLWM.Utils.CnvToStr(dr["descrptn_step_two"]),

                        EmplymntTyp = UsageD.EmploymntTypeCol.FirstOrDefault(x => x.EmpJobDtlId ==
                                      PLWM.Utils.CnvToInt(dtSel.Rows[0]["emplymnt_typ"])),

                        MonthlySalary = UsageD.MonthlySalaryCol.FirstOrDefault(x => x.EmpJobDtlId ==
                                      PLWM.Utils.CnvToInt(dtSel.Rows[0]["monthly_salary"])),

                        Exprnce = UsageD.ExprnceCol.FirstOrDefault(x => x.EmpJobDtlId ==
                                      PLWM.Utils.CnvToInt(dtSel.Rows[0]["exprnce"])),

                        EductnLvl = UsageD.EductnLevlCol.FirstOrDefault(x => x.EmpJobDtlId ==
                                      PLWM.Utils.CnvToInt(dtSel.Rows[0]["eductn_lvl"])),

                        ListedBy = UsageD.ListedBycol.FirstOrDefault(x => x.EmpJobDtlId ==
                                      PLWM.Utils.CnvToInt(dtSel.Rows[0]["listed_by"])),

                        CareerLvl = UsageD.CareervLevelCol.FirstOrDefault(x => x.EmpJobDtlId ==
                                      PLWM.Utils.CnvToInt(dtSel.Rows[0]["career_lvl"])),

                    };
                }
            }
            catch (Exception e)
            {
                UsageD.UserData.ZaBase.ErrorMsg = PLWM.Utils.CnvToSentenceCase(e.Message.ToLower().Replace("plerror", "").Replace("plerror", "").Trim());
            }

            return UsageD;
        }

        public ZA3650LD DoLoadFrelncList(ZA3650LD FilterData, String Mode)
        {
            ZA3650LD UsageD = new ZA3650LD();

            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                new XElement("as_mode", Mode),
                new XElement("as_sessionid", FilterData.UserData.ZaBase.SessionId),
                new XElement("ai_pageno", FilterData.PageNo),
                new XElement("ai_frelnc_comp_job_mast_id", "")
                ));

                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                DataSet ds = dbObj.SelectSP("ZA3680_sel", XString, PLABSM.DbProvider.MSSql);

                System.Data.DataTable UserDt = PLWM.Utils.GetDataTable(ds, 0);
                System.Data.DataTable FrelncColDt = PLWM.Utils.GetDataTable(ds, 1);
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

                foreach (DataRow dr in FrelncColDt.Rows)
                {
                    UsageD.ComCol.Add(new ZA3650SD()
                    {
                        CompnyJobMastId = PLWM.Utils.CnvToInt(dr["frelnc_comp_job_mast_id"]),
                        CompnyName = PLWM.Utils.CnvToStr(dr["comp_name"]),
                        JobTitle = PLWM.Utils.CnvToStr(dr["Title"]),
                        CrtdDt = PLWM.Utils.CnvToStr(dr["crtd_dt"]),
                       // Location = PLWM.Utils.CnvToStr(dr["location"]),
                        DescrpnStepOne = PLWM.Utils.CnvToStr(dr["descrpn"]),
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

        public ZA3641RPD DoLoadFrelncListDetails(ZA3641RPD FilterData, String Mode)
        {
            ZA3641RPD FrelncDet = new ZA3641RPD();

            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                new XElement("as_mode", Mode),
                new XElement("as_sessionid", FilterData.UserData.ZaBase.SessionId),
                new XElement("ai_com_job_mast_id", FilterData.ComJobMast.CompanyJobMastId)
                ));

                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                DataSet ds = dbObj.SelectSP("ZA3681_sel", XString, PLABSM.DbProvider.MSSql);

                System.Data.DataTable FrelncColDt = PLWM.Utils.GetDataTable(ds, 1);
                
                foreach (DataRow dr in FrelncColDt.Rows)
                {
                    FrelncDet.ComJobMast = new ZA3640D()
                    {
                        CompanyJobMastId = PLWM.Utils.CnvToStr(dr["frelnc_comp_job_mast_id"]),
                        CompanyName = PLWM.Utils.CnvToStr(dr["comp_name"]),
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
                    };

                }
               
            }
            catch (Exception e)
            {
                FrelncDet.UserData.ZaBase.ErrorMsg = PLWM.Utils.CnvToSentenceCase(e.Message.ToLower().Replace("plerror", "").Replace("plerror", "").Trim());
            }
            return FrelncDet;
        }

        public ZA3650SD DoSave(ZA3650SD SaveData, String Mode, long FileLength)
        {
            ZA3650SD UsageD = new ZA3650SD();

            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                new XElement("as_mode", Mode),
                new XElement("as_sessionid", SaveData.UserData.ZaBase.SessionId),
                new XElement("ai_compny_job_mast_id", SaveData.CompnyJobMastId),
                new XElement("as_company_name", SaveData.CompnyName),
                new XElement("as_trade_lic", SaveData.TradeLicns),
                new XElement("as_contact_name", SaveData.ContctName),
                new XElement("ai_industry", SaveData.Indstry.EmpJobDtlId),
                new XElement("as_phone", SaveData.Phone),
                new XElement("ai_company_size", SaveData.CompnySize.EmpJobDtlId),
                new XElement("as_compny_website", SaveData.CompnyWebsit),
                new XElement("as_adrs", SaveData.Addrs),
                new XElement("as_dscrptn_step_one", SaveData.DescrpnStepOne),

                new XElement("as_job_title", SaveData.JobTitle),
                new XElement("ai_empl_type", SaveData.EmplymntTyp.EmpJobDtlId),
                new XElement("ai_monthly_salry", SaveData.MonthlySalary.EmpJobDtlId),
                new XElement("as_neighbrhd", SaveData.Neighbrhd),
                new XElement("ai_exprnce", SaveData.Exprnce.EmpJobDtlId),
                new XElement("ai_eductn_lvl", SaveData.EductnLvl.EmpJobDtlId),
                new XElement("ai_listed_by", SaveData.ListedBy.EmpJobDtlId),
                 new XElement("ai_career_lvl", SaveData.CareerLvl.EmpJobDtlId),
                new XElement("as_location", SaveData.Location),
                new XElement("as_dscrptn_step_two", SaveData.DescrptnStepTwo), 
                new XElement("as_PhotoLength", FileLength)

                ));


                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                DataSet ds = dbObj.SelectSP("ZA3680_IU", XString, PLABSM.DbProvider.MSSql);
                DataTable dt = PLWM.Utils.GetDataTable(ds, 0);
                DataTable dt_id = PLWM.Utils.GetDataTable(ds, 1);

                if (dt.Rows.Count > 0)
                {
                    UsageD.PhotoPath = PLWM.Utils.CnvToStr(dt.Rows[0]["UsrFldrName"]);
                }
                if (dt_id.Rows.Count > 0)
                {
                    UsageD.CompnyJobMastId = PLWM.Utils.CnvToInt(dt_id.Rows[0]["compny_job_mast_id"]);
                }

            }
            catch (Exception e)
            {
                UsageD.UserData.ZaBase.ErrorMsg = PLWM.Utils.CnvToSentenceCase(e.Message.ToLower().Replace("plerror", "").Replace("plerror", "").Trim());
            }

            return UsageD;
        }
    }
}
