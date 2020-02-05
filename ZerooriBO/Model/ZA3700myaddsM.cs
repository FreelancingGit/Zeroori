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
    public class ZA3700myaddsM
    {
        public ZA3700ILD DoInit(ZA3000D FilterData, String Mode)
        {
            ZA3700ILD UsageD = new ZA3700ILD();

            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                new XElement("as_mode", Mode),
                new XElement("as_sessionid", FilterData.ZaBase.SessionId)
                ));

                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                DataSet ds = dbObj.SelectSP("ZA3700AD_sel", XString, PLABSM.DbProvider.MSSql);

                
                System.Data.DataTable MotorDt = PLWM.Utils.GetDataTable(ds, 0);
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

                UsageD.MotorDataCol = new ZA3700BDCol();
                foreach (DataRow Dr in MotorDt.Rows)
                {
                    UsageD.MotorDataCol.Add(new ZA3700BD
                    {
                        Title = PLWM.Utils.CnvToStr(Dr["mot_Title"]),
                        ProductImage = PLWM.Utils.CnvToStr(Dr["full_path"]),
                        Rate = PLWM.Utils.CnvToStr(Dr["Price"]),
                        MotorsAdMastId = PLWM.Utils.CnvToStr(Dr["motors_ad_mast_id"])
                    });
                }


            }
            catch (Exception e)
            {
                UsageD.UserData.ZaBase.ErrorMsg = PLWM.Utils.CnvToSentenceCase(e.Message.ToLower().Replace("plerror", "").Replace("plerror", "").Trim());
            }
            return UsageD;
        }



        public ZA3700ILD DoLoad(ZA3700LFD FilterData, String Mode)
        {
            ZA3700ILD UsageD = new ZA3700ILD();

            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                new XElement("as_mode", "LD"),
                new XElement("as_sessionid","")
                ));

                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                DataSet ds = dbObj.SelectSP("ZA3700AD_sel", XString, PLABSM.DbProvider.MSSql);

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


                UsageD.MotorDataCol = new ZA3700BDCol();
                foreach (DataRow Dr in ClasifiedDt.Rows)
                {
                    UsageD.MotorDataCol.Add(new ZA3700BD
                    {
                        Title = PLWM.Utils.CnvToStr(Dr["mot_Title"]),
                        Years = PLWM.Utils.CnvToStr(Dr["Years"]),
                        Kmters = PLWM.Utils.CnvToStr(Dr["Kmters"]),
                        Email = PLWM.Utils.CnvToStr(Dr["usr_email"]),
                        Location = PLWM.Utils.CnvToStr(Dr["Location"]),
                        PhNo = PLWM.Utils.CnvToStr(Dr["usr_phno"]),
                        PostDate = PLWM.Utils.CnvToStr(Dr["crtd_dt"]),
                        ProductImage = PLWM.Utils.CnvToStr(Dr["full_path"]),
                        Rate = PLWM.Utils.CnvToStr(Dr["Price"]),
                        Colors = PLWM.Utils.CnvToStr(Dr["Colors"]),
                        Doors = PLWM.Utils.CnvToStr(Dr["Doors"]),
                        MotorsAdMastId = PLWM.Utils.CnvToStr(Dr["motors_ad_mast_id"])
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
        
        public ZA3700ILD DoDelete(ZA3600LD FilterData)
        {
            ZA3700ILD UsageD = new ZA3700ILD();

            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                new XElement("ai_motors_ad_mast_id", FilterData.MotorsADMastID)
                ));

                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                DataSet ds = dbObj.SelectSP("ZA3700AD_Del", XString, PLABSM.DbProvider.MSSql);
            }
            catch (Exception e)
            {
                UsageD.UserData.ZaBase.ErrorMsg = PLWM.Utils.CnvToSentenceCase(e.Message.ToLower().Replace("plerror", "").Replace("plerror", "").Trim());
            }
            return UsageD;
        }
    }
}
