﻿using System;
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
    public class JobsmyaddsM
    {
        public JobsmyaddsLD DoInit(ZA3000D FilterData, String Mode)
        {
            JobsmyaddsLD UsageD = new JobsmyaddsLD();

            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                new XElement("as_mode", Mode),
                new XElement("as_sessionid", FilterData.ZaBase.SessionId)
                ));

                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                DataSet ds = dbObj.SelectSP("ZAJobList_Sel", XString, PLABSM.DbProvider.MSSql);

                
                System.Data.DataTable ClasifiedDt = PLWM.Utils.GetDataTable(ds, 0);
                System.Data.DataTable UserDataDt = PLWM.Utils.GetDataTable(ds, 1);
                
                if (UserDataDt.Rows.Count > 0)
                {
                    UsageD.UserData = new ZA3000D()
                    {
                        FistNam = PLWM.Utils.CnvToStr(UserDataDt.Rows[0]["FirstName"]),
                        ZaBase = new BaseD()
                        {
                            ZaKey = Utils.GetKey(),
                            SessionId = PLWM.Utils.CnvToStr(UserDataDt.Rows[0]["SESSIONID"]),
                            ErrorMsg = "",
                        }
                    };
                }
                else
                {
                    UsageD.UserData = new ZA3000D()
                    {
                        FistNam = "",
                        ZaBase = new BaseD()
                        {
                            ZaKey = Utils.GetKey(),
                            SessionId = "",
                            ErrorMsg = "",
                        }
                    };
                }

                UsageD.JobMastCol = new JobsmyaddsDCol();
                foreach (DataRow Dr in ClasifiedDt.Rows)
                {
                    UsageD.JobMastCol.Add(new JobsmyaddsD
                    {
                        EmpJobMastId= PLWM.Utils.CnvToInt(Dr["emp_job_mast_id"]),
                        JobType = PLWM.Utils.CnvToStr(Dr["jobtype"]),
                        Title = PLWM.Utils.CnvToStr(Dr["title"]),
                        Descrptn = PLWM.Utils.CnvToStr(Dr["descrptn"]),
                        Proimg = PLWM.Utils.CnvToStr(Dr["proimg"]),
                       
                    });
                }
            }
            catch (Exception e)
            {
                UsageD.UserData.ZaBase.ErrorMsg = PLWM.Utils.CnvToSentenceCase(e.Message.ToLower().Replace("plerror", "").Replace("plerror", "").Trim());
            }
            return UsageD;
        }

        public ZA3720ILD DoLoad(ZA3720LFD FilterData, String Mode)
        {
            ZA3720ILD UsageD = new ZA3720ILD();

            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                new XElement("as_mode", "LD"),
                new XElement("ai_pageno", FilterData.PageNo),
                new XElement("as_sessionid", ""),
                new XElement("as_Option", FilterData.Category.ClasifdSpecDtlId),
                new XElement("as_location", FilterData.Age.ValMembr),
                new XElement("as_sortby", FilterData.SortBy.ValMembr)
                ));

                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                DataSet ds = dbObj.SelectSP("ZA3720_sel", XString, PLABSM.DbProvider.MSSql);

                System.Data.DataTable ClasifiedDt = PLWM.Utils.GetDataTable(ds, 0);
                System.Data.DataTable PageNoDt = PLWM.Utils.GetDataTable(ds, 1);

                UsageD.UserData = new ZA3000D()
                {
                    ZaBase = new BaseD()
                    {
                        ZaKey = Utils.GetKey(),
                        ErrorMsg = ""
                    }
                };


                UsageD.ClasifiedsDataCol = new ZA3720BDCol();
                foreach (DataRow Dr in ClasifiedDt.Rows)
                {
                    UsageD.ClasifiedsDataCol.Add(new ZA3720BD
                    {
                        Title = PLWM.Utils.CnvToStr(Dr["clasifd_title"]),
                        Age = PLWM.Utils.CnvToStr(Dr["Age"]),
                        Condition = PLWM.Utils.CnvToStr(Dr["Condition"]),
                        Email = PLWM.Utils.CnvToStr(Dr["usr_email"]),
                        Location = PLWM.Utils.CnvToStr(Dr["Location"]),
                        PhNo = PLWM.Utils.CnvToStr(Dr["usr_phno"]),
                        PostDate = PLWM.Utils.CnvToStr(Dr["crtd_dt"]),
                        ProductImage = PLWM.Utils.CnvToStr(Dr["full_path"]),
                        Rate = PLWM.Utils.CnvToStr(Dr["Price"]),
                        Usage = PLWM.Utils.CnvToStr(Dr["Usage"]),
                        Warranty = PLWM.Utils.CnvToStr(Dr["warrenty"]),
                        ClasifdAdMastId = PLWM.Utils.CnvToStr(Dr["clasifd_ad_mast_id"]),
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



            }
            catch (Exception e)
            {
                UsageD.UserData.ZaBase.ErrorMsg = PLWM.Utils.CnvToSentenceCase(e.Message.ToLower().Replace("plerror", "").Replace("plerror", "").Trim());
            }
            return UsageD;
        }
        //DeleteProduct
       
        public JobsmyaddsLD DeleteJob(JobsmyaddsD FilterData)
        {
            JobsmyaddsLD UsageD = new JobsmyaddsLD();

            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                new XElement("ai_job_mast_id", FilterData.EmpJobMastId)
                ));

                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                if (FilterData.JobType == "JW")
                {
                    dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                    DataSet ds = dbObj.SelectSP("ZA3630_Del", XString, PLABSM.DbProvider.MSSql);
                }
                else if (FilterData.JobType == "JH")
                {
                    dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                    DataSet ds = dbObj.SelectSP("ZA3650_Del", XString, PLABSM.DbProvider.MSSql);
                }
                else if (FilterData.JobType == "FW")
                {
                    dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                    DataSet ds = dbObj.SelectSP("ZA3670_Del", XString, PLABSM.DbProvider.MSSql);
                }
                else if (FilterData.JobType == "FH")
                {
                    dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                    DataSet ds = dbObj.SelectSP("ZA3680_Del", XString, PLABSM.DbProvider.MSSql);
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
