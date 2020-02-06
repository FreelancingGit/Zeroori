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
    public class ZA3720M
    {
        public ZA3720ILD DoInit(ZA3000D FilterData, String Mode)
        {
            ZA3720ILD UsageD = new ZA3720ILD();

            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                new XElement("as_mode", Mode),
                new XElement("ai_pageno", ""),
                new XElement("as_sessionid", FilterData.ZaBase.SessionId),
                new XElement("as_Option", ""),
                new XElement("as_location", ""),
                new XElement("as_sortby", "")
                ));

                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                DataSet ds = dbObj.SelectSP("ZA3720_sel", XString, PLABSM.DbProvider.MSSql);

                
                System.Data.DataTable ClasifiedDt = PLWM.Utils.GetDataTable(ds, 0);
                System.Data.DataTable PageNoDt = PLWM.Utils.GetDataTable(ds, 1);

                System.Data.DataTable UserDataDt = PLWM.Utils.GetDataTable(ds, 2);

                System.Data.DataTable CatagoryDt = PLWM.Utils.GetDataTable(ds, 3);
                System.Data.DataTable LocationDt = PLWM.Utils.GetDataTable(ds, 4);
                System.Data.DataTable SortByDt = PLWM.Utils.GetDataTable(ds, 5);

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

                UsageD.AgeCol = new ComDisValDCol();
                UsageD.AgeCol.Add(new ComDisValD() { DisPlyMembr = "Age", ValMembr = -1 });
                foreach (DataRow Dr in LocationDt.Rows)
                {
                    UsageD.AgeCol.Add(new ComDisValD()
                    {
                      DisPlyMembr = PLWM.Utils.CnvToStr(Dr["clasifd_value"]),
                      ValMembr = PLWM.Utils.CnvToInt(Dr["clasifd_dtl_id"]),
                    });
                }


                UsageD.SortByCol = new ComDisValDCol();
                UsageD.SortByCol.Add(new ComDisValD() { DisPlyMembr = "Sorted By", ValMembr = -1 });
                foreach (DataRow Dr in SortByDt.Rows)
                {
                    UsageD.SortByCol.Add(new ComDisValD()
                    {
                        DisPlyMembr = PLWM.Utils.CnvToStr(Dr["SortMode"]),
                        ValMembr = PLWM.Utils.CnvToInt(Dr["SortValue"]),
                    });
                }

                UsageD.CatagoryCol = new ZA3220DCol();
                foreach (DataRow Dr in CatagoryDt.Rows)
                {
                    UsageD.CatagoryCol.Add(new ZA3220D()
                    {
                        ClasifdSpecDtlId = PLWM.Utils.CnvToInt(Dr["clasifd_dtl_id"]),
                        ClasifdSpecValue = PLWM.Utils.CnvToStr(Dr["clasifd_value"]),
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



        public ZA3720ILD DoLoad(ZA3720LFD FilterData, String Mode)
        {
            ZA3720ILD UsageD = new ZA3720ILD();

            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                new XElement("as_mode", "LD"),
                new XElement("ai_pageno", FilterData.PageNo),
                new XElement("as_sessionid",""),
                new XElement("as_Option", FilterData.Category.ClasifdSpecDtlId),
                new XElement("as_age", FilterData.Age.ValMembr),
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

    }
}
