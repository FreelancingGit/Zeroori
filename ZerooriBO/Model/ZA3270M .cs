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
    public class ZA3270M
    {


        public ZA3270LD DoLoad(ZA3270LD FilterData, String Mode)
        {
            ZA3270LD UsageD = new ZA3270LD();

            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                new XElement("as_mode", "LD"),
                 new XElement("ai_pageno", FilterData.PageNo),
                new XElement("as_sessionid", FilterData.UserData.ZaBase.SessionId),
                new XElement("as_Option", ""),
                new XElement("as_location", ""),
                new XElement("as_sortby", "")
                ));

                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                DataSet ds = dbObj.SelectSP("ZA3270_sel", XString, PLABSM.DbProvider.MSSql);


                System.Data.DataTable UserDataDt = PLWM.Utils.GetDataTable(ds, 0);
                System.Data.DataTable MallcolDt = PLWM.Utils.GetDataTable(ds, 1);
                System.Data.DataTable PageNoDt = PLWM.Utils.GetDataTable(ds, 2);


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

                foreach (DataRow dr in MallcolDt.Rows)
                {
                    UsageD.Mallcol.Add(new ZA3270D()
                    {
                        MallAdMastId= PLWM.Utils.CnvToStr(dr["mall_mast_id"]),
                        MallName = PLWM.Utils.CnvToStr(dr["mall_name"]),
                        MallLocation = PLWM.Utils.CnvToStr(dr["mall_location"]),
                        MallStartTiming = PLWM.Utils.CnvToStr(dr["mall_start_timing"]),
                        MallEndTiming = PLWM.Utils.CnvToStr(dr["mall_end_timing"]),
                        MallPhone = PLWM.Utils.CnvToStr(dr["Phone"]),
                        MallEmaild = PLWM.Utils.CnvToStr(dr["email"]),
                        MallUrl = PLWM.Utils.CnvToStr(dr["mall_url"]),
                        MallDecrp = PLWM.Utils.CnvToStr(dr["mall_decrp"]),
                        MallMastImgPath = PLWM.Utils.CnvToStr(dr["mall_mast_img_path"]),
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
