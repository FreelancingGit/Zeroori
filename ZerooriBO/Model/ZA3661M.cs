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
    public class ZA3661M
    {


        public ZA3661LD DoLoad(ZA3661D FilterData, String Mode)
        {
            ZA3661LD UsageD = new ZA3661LD();

            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                new XElement("as_mode", Mode),
                new XElement("ai_pageno", FilterData.PageNo),
                 new XElement("ai_dir_dtl_id",""),
                new XElement("as_sessionid", FilterData.UserData.ZaBase.SessionId)
                ));

                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                DataSet ds = dbObj.SelectSP("ZA3661_sel", XString, PLABSM.DbProvider.MSSql);

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
                    UsageD.DirCol.Add(new ZA3661D()
                    {
                        DirDtlId= PLWM.Utils.CnvToInt(dr["dir_dtl_id"]),
                        CompName = PLWM.Utils.CnvToStr(dr["comp_name"]),
                        Addrs = PLWM.Utils.CnvToStr(dr["addrs"]),
                        Phone_1 = PLWM.Utils.CnvToStr(dr["phone_1"]),
                        Mobile = PLWM.Utils.CnvToStr(dr["Mobile"]),
                        Email = PLWM.Utils.CnvToStr(dr["email"]),
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

        public ZA3661DD DoLoadDirDetail(ZA3661DD FilterData, String Mode)
        {
            ZA3661DD DirDet = new ZA3661DD();

            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                new XElement("as_mode", Mode),
                new XElement("ai_pageno", ""),
                new XElement("ai_dir_dtl_id", FilterData.DirMast.DirDtlId),
                new XElement("as_sessionid", FilterData.UserData.ZaBase.SessionId)
                ));

                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                DataSet ds = dbObj.SelectSP("ZA3661_sel", XString, PLABSM.DbProvider.MSSql);

                System.Data.DataTable DirDt = PLWM.Utils.GetDataTable(ds, 0);
               
                foreach (DataRow dr in DirDt.Rows)
                {
                    DirDet.DirMast=new ZA3661D()
                    {
                        DirDtlId = PLWM.Utils.CnvToInt(dr["dir_dtl_id"]),
                        CompName = PLWM.Utils.CnvToStr(dr["comp_name"]),
                        Addrs = PLWM.Utils.CnvToStr(dr["addrs"]),
                        Phone_1 = PLWM.Utils.CnvToStr(dr["phone_1"]),
                        Mobile = PLWM.Utils.CnvToStr(dr["Mobile"]),
                        Email = PLWM.Utils.CnvToStr(dr["email"]),
                        Fax= PLWM.Utils.CnvToStr(dr["fax"]),
                        Web= PLWM.Utils.CnvToStr(dr["web"]),
                    };

                }

                
            }
            catch (Exception e)
            {
                DirDet.UserData.ZaBase.ErrorMsg = PLWM.Utils.CnvToSentenceCase(e.Message.ToLower().Replace("plerror", "").Replace("plerror", "").Trim());
            }
            return DirDet;
        }
    }
}
