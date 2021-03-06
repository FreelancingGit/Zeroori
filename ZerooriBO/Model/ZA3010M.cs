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
    public class ZA3010M
    {
        public ZA3010D DoSave(ZA3010D FilterData, String Mode, long FileLength)
        {
            ZA3010D SaveDataV = new ZA3010D();

            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                    //new XElement("as_mode", Mode),
                    new XElement("as_sessionid", FilterData.UserData.ZaBase.SessionId),
                    new XElement("ai_deal_mast_id", FilterData.DealMastID),

                    new XElement("as_busines_Name", FilterData.BusinessName),
                    new XElement("as_busines_Url", FilterData.URL),
                    new XElement("ai_catgry_id", FilterData.Category),
                    new XElement("as_banner_img_url", FilterData.BannerImage),
                    new XElement("as_logo_img_url", FilterData.CompanyLogo),

                    new XElement("as_fb_url", FilterData.Facebook),
                    new XElement("as_Instagram_url", FilterData.Instagram),
                    new XElement("as_Twitter_url", FilterData.Twitter),
                    new XElement("as_Phone_No", FilterData.PhoneNo),
                    new XElement("as_Email", FilterData.Email),

                    new XElement("as_Website", FilterData.Website),
                    new XElement("as_geo_Location", FilterData.Location),
                    new XElement("as_Description", FilterData.Description),
                    new XElement("as_PhotoLength", FileLength)
                ));


                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                DataSet ds = dbObj.SelectSP("ZA3010_IU", XString, PLABSM.DbProvider.MSSql);
                

                System.Data.DataTable dtComn = PLWM.Utils.GetDataTable(ds, 0);
                
                if (dtComn.Rows.Count>0)
                {
                    SaveDataV.BusinessName = PLWM.Utils.CnvToStr(dtComn.Rows[0]["busines_Name"]);
                    SaveDataV.URL = PLWM.Utils.CnvToStr(dtComn.Rows[0]["busines_Url"]);
                    SaveDataV.Category = PLWM.Utils.CnvToStr(dtComn.Rows[0]["catgry_id"]);

                    SaveDataV.BannerImage = PLWM.Utils.CnvToStr(dtComn.Rows[0]["banner_img_url"]);
                    SaveDataV.CompanyLogo = PLWM.Utils.CnvToStr(dtComn.Rows[0]["logo_img_url"]);

                    SaveDataV.Facebook = PLWM.Utils.CnvToStr(dtComn.Rows[0]["fb_url"]);
                    SaveDataV.Instagram = PLWM.Utils.CnvToStr(dtComn.Rows[0]["Instagram_url"]);
                    SaveDataV.Twitter = PLWM.Utils.CnvToStr(dtComn.Rows[0]["Twitter_url"]);
                    SaveDataV.PhoneNo = PLWM.Utils.CnvToStr(dtComn.Rows[0]["Phone_No"]);
                    SaveDataV.Email = PLWM.Utils.CnvToStr(dtComn.Rows[0]["Email"]);
                    SaveDataV.Website = PLWM.Utils.CnvToStr(dtComn.Rows[0]["Website"]);
                    SaveDataV.Location = PLWM.Utils.CnvToStr(dtComn.Rows[0]["geo_Location"]);
                    SaveDataV.Description = PLWM.Utils.CnvToStr(dtComn.Rows[0]["Description"]);
                }
                
                
            }
            catch (Exception e)
            {
                SaveDataV.UserData.ZaBase.ErrorMsg = PLWM.Utils.CnvToSentenceCase(e.Message.ToLower().Replace("plerror", "").Replace("plerror", "").Trim());
            }

            return SaveDataV;
        }

        public ZA3010D DoLoad(ZA3010D FilterData, String Mode)
        {
            ZA3010D SaveDataV = new ZA3010D();

            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                    //new XElement("as_mode", Mode),
                    new XElement("as_sessionid", FilterData.UserData.ZaBase.SessionId),
                    new XElement("ai_deal_mast_id", FilterData.DealMastID),
                    new XElement("ai_pageno", ""),
                    new XElement("as_mode", Mode)
                ));


                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                DataSet ds = dbObj.SelectSP("ZA3010_SEL", XString, PLABSM.DbProvider.MSSql);



                System.Data.DataTable dtComn = PLWM.Utils.GetDataTable(ds, 0);
                System.Data.DataTable dtUser = PLWM.Utils.GetDataTable(ds, 1);

                if (dtComn.Rows.Count > 0)
                {
                    SaveDataV.BusinessName = PLWM.Utils.CnvToStr(dtComn.Rows[0]["busines_Name"]);
                    SaveDataV.URL = PLWM.Utils.CnvToStr(dtComn.Rows[0]["busines_Url"]);
                    SaveDataV.Category = PLWM.Utils.CnvToStr(dtComn.Rows[0]["catgry_id"]);
                    SaveDataV.BannerImage = PLWM.Utils.CnvToStr(dtComn.Rows[0]["banner_img_url"]);
                    SaveDataV.CompanyLogo = PLWM.Utils.CnvToStr(dtComn.Rows[0]["logo_img_url"]);
                    SaveDataV.Facebook = PLWM.Utils.CnvToStr(dtComn.Rows[0]["fb_url"]);
                    SaveDataV.Instagram = PLWM.Utils.CnvToStr(dtComn.Rows[0]["Instagram_url"]);
                    SaveDataV.Twitter = PLWM.Utils.CnvToStr(dtComn.Rows[0]["Twitter_url"]);
                    SaveDataV.PhoneNo = PLWM.Utils.CnvToStr(dtComn.Rows[0]["Phone_No"]);
                    SaveDataV.Email = PLWM.Utils.CnvToStr(dtComn.Rows[0]["Email"]);
                    SaveDataV.Website = PLWM.Utils.CnvToStr(dtComn.Rows[0]["Website"]);
                    SaveDataV.Location = PLWM.Utils.CnvToStr(dtComn.Rows[0]["geo_Location"]);
                    SaveDataV.Description = PLWM.Utils.CnvToStr(dtComn.Rows[0]["Description"]);
                }
                if (dtUser.Rows.Count > 0)
                {
                    SaveDataV.UserData = new ZA3000D()
                    {
                        FistNam = PLWM.Utils.CnvToStr(dtUser.Rows[0]["usr_FistNam"]),
                        ZaBase = new BaseD()
                        {
                            SessionId = PLWM.Utils.CnvToStr(dtUser.Rows[0]["sessionid"]),
                        }
                    };
                   
                }

            }
            catch (Exception e)
            {
                SaveDataV.UserData.ZaBase.ErrorMsg = PLWM.Utils.CnvToSentenceCase(e.Message.ToLower().Replace("plerror", "").Replace("plerror", "").Trim());
            }

            return SaveDataV;
        }

        //DoLoadPackage
        public ZA3010LD DoLoadPackage(ZA3000D FilterData, String Mode)
        {
            ZA3010LD UsageData= new ZA3010LD();

            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                    new XElement("ai_deal_mast_id", ""),
                    new XElement("as_sessionid", FilterData.ZaBase.SessionId),
                    new XElement("ai_pageno", ""),
                    new XElement("as_mode", Mode)
                ));


                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                DataSet ds = dbObj.SelectSP("ZA3010_SEL", XString, PLABSM.DbProvider.MSSql);
                
                System.Data.DataTable dtComn = PLWM.Utils.GetDataTable(ds, 0);
                System.Data.DataTable dtUser = PLWM.Utils.GetDataTable(ds, 1);
                System.Data.DataTable PageNoDt = PLWM.Utils.GetDataTable(ds, 2);

                if (dtComn.Rows.Count > 0)
                {
                    UsageData.PackCol.Add(new ZA3010D()
                    {
                        PackDealMastID = PLWM.Utils.CnvToInt(dtComn.Rows[0]["pack_deal_mast_id"]),
                        BusinessName = PLWM.Utils.CnvToStr(dtComn.Rows[0]["busines_Name"]),
                        Location = PLWM.Utils.CnvToStr(dtComn.Rows[0]["geo_Location"]),
                        Email = PLWM.Utils.CnvToStr(dtComn.Rows[0]["Email"]),
                        CompanyLogo= PLWM.Utils.CnvToStr(dtComn.Rows[0]["logo_img_url"]),
                    });
                }
                if (dtUser.Rows.Count > 0)
                {
                    UsageData.UserData = new ZA3000D()
                    {
                        FistNam = PLWM.Utils.CnvToStr(dtUser.Rows[0]["usr_FistNam"]),
                        ZaBase = new BaseD()
                        {
                            SessionId = PLWM.Utils.CnvToStr(dtUser.Rows[0]["sessionid"]),
                        }
                    };

                }

                UsageData.PageNoCol = new ComDisValDCol();
                foreach (DataRow Dr in PageNoDt.Rows)
                {
                    UsageData.PageNoCol.Add(new ComDisValD()
                    {
                        ValMembr = PLWM.Utils.CnvToInt(Dr["TotalPages"]),
                        DisPlyMembr = PLWM.Utils.CnvToStr(Dr["Page_No"]),
                    });
                }
            }
            catch (Exception e)
            {
                UsageData.UserData.ZaBase.ErrorMsg = PLWM.Utils.CnvToSentenceCase(e.Message.ToLower().Replace("plerror", "").Replace("plerror", "").Trim());
            }

            return UsageData;
        }
    }
}
