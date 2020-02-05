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
    public class ZA2010M
    {
        //DoLoadData

        public ZA2011LD DoLoadData(ZA2010D FilterData, String Mode)
        {
            ZA2011LD UsageD = new ZA2011LD();

            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                new XElement("as_mode", Mode),
                new XElement("as_sessionid", FilterData.ZaBase.SessionId)
                ));

                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                DataSet ds = dbObj.SelectSP("ZA2010_sel", XString, PLABSM.DbProvider.MSSql);

                System.Data.DataTable SilDt = PLWM.Utils.GetDataTable(ds, 0);
                System.Data.DataTable GoldDt = PLWM.Utils.GetDataTable(ds, 1);
                System.Data.DataTable PlatDt = PLWM.Utils.GetDataTable(ds, 2);
                

                foreach (DataRow dr in SilDt.Rows)
                {
                    UsageD.SilverCol.Add(new ZA2011D()
                    {
                        PlanMastId = PLWM.Utils.CnvToNullableInt(dr["plan_mast_id"]),
                        PlanValue = PLWM.Utils.CnvToStr(dr["plan_value"]),
                    });

                }

                foreach (DataRow dr in GoldDt.Rows)
                {
                    UsageD.GoldCol.Add(new ZA2011D()
                    {
                        PlanMastId = PLWM.Utils.CnvToNullableInt(dr["plan_mast_id"]),
                        PlanValue = PLWM.Utils.CnvToStr(dr["plan_value"]),
                    });

                }

                foreach (DataRow dr in PlatDt.Rows)
                {
                    UsageD.PlatinumCol.Add(new ZA2011D()
                    {
                        PlanMastId = PLWM.Utils.CnvToNullableInt(dr["plan_mast_id"]),
                        PlanValue = PLWM.Utils.CnvToStr(dr["plan_value"]),
                    });

                }
               
            }
            catch (Exception e)
            {
                UsageD.UserData.ZaBase.ErrorMsg = PLWM.Utils.CnvToSentenceCase(e.Message.ToLower().Replace("plerror", "").Replace("plerror", "").Trim());
            }
            return UsageD;
        }
        public ZA2010LD DoLoad(ZA2010D FilterData, String Mode)
        {
            ZA2010LD UsageD = new ZA2010LD();

            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                new XElement("as_mode", Mode),
                new XElement("as_sessionid", FilterData.ZaBase.SessionId) 
                ));

                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                DataSet ds = dbObj.SelectSP("ZA2010_sel", XString, PLABSM.DbProvider.MSSql);

                System.Data.DataTable SilDt = PLWM.Utils.GetDataTable(ds, 0);
                System.Data.DataTable GoldDt = PLWM.Utils.GetDataTable(ds, 1);
                System.Data.DataTable PlatDt = PLWM.Utils.GetDataTable(ds, 2);

                System.Data.DataTable UserDt = PLWM.Utils.GetDataTable(ds, 3);


                foreach (DataRow dr in SilDt.Rows)
                {
                    UsageD.SilverMast=new ZA2010D()
                    {
                       PlanMastId= PLWM.Utils.CnvToNullableInt(dr["plan_mast_id"]),
                       PlaName= PLWM.Utils.CnvToStr(dr["plan_name"]),
                       Amount= PLWM.Utils.CnvToStr(dr["amount"]),
                       Curncy= PLWM.Utils.CnvToStr(dr["curncy"]),
                       Duration= PLWM.Utils.CnvToStr(dr["duration"]),
                    };

                }

                foreach (DataRow dr in GoldDt.Rows)
                {
                    UsageD.GoldMast = new ZA2010D()
                    {
                        PlanMastId = PLWM.Utils.CnvToNullableInt(dr["plan_mast_id"]),
                        PlaName = PLWM.Utils.CnvToStr(dr["plan_name"]),
                        Amount = PLWM.Utils.CnvToStr(dr["amount"]),
                        Curncy = PLWM.Utils.CnvToStr(dr["curncy"]),
                        Duration = PLWM.Utils.CnvToStr(dr["duration"]),
                    };

                }

                foreach (DataRow dr in PlatDt.Rows)
                {
                    UsageD.PlatinumMast = new ZA2010D()
                    {
                        PlanMastId = PLWM.Utils.CnvToNullableInt(dr["plan_mast_id"]),
                        PlaName = PLWM.Utils.CnvToStr(dr["plan_name"]),
                        Amount = PLWM.Utils.CnvToStr(dr["amount"]),
                        Curncy = PLWM.Utils.CnvToStr(dr["curncy"]),
                        Duration = PLWM.Utils.CnvToStr(dr["duration"]),
                    };

                }
                UsageD.UserData = new ZA3000D();
                foreach (DataRow Dr in UserDt.Rows)
                {
                    UsageD.UserData=new ZA3000D()
                    {
                        UsrMastID = PLWM.Utils.CnvToInt(UserDt.Rows[0]["usr_mast_id"]),
                        FistNam = PLWM.Utils.CnvToStr(UserDt.Rows[0]["FirstName"]),
                        ZaBase = new BaseD()
                        {
                            SessionId = PLWM.Utils.CnvToStr(UserDt.Rows[0]["SESSIONID"]),
                        },
                    };
                }

            }
            catch (Exception e)
            {
                UsageD.UserData.ZaBase.ErrorMsg = PLWM.Utils.CnvToSentenceCase(e.Message.ToLower().Replace("plerror", "").Replace("plerror", "").Trim());
            }
            return UsageD;
        }

        public ZA2010LD DoSubscribe(ZA2010D FilterData, String Mode)
        {
            ZA2010LD UsageD = new ZA2010LD();

            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                new XElement("as_mode", Mode),
                new XElement("as_sessionid", FilterData.ZaBase.SessionId),
                new XElement("as_EmailID", FilterData.ZaBase.UserName),
                new XElement("ai_Otp", FilterData.ZaBase.PKID),
                new XElement("as_Pkg", FilterData.ZaBase.Fld)
                ));

                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                DataSet ds = dbObj.SelectSP("ZA2010_IU", XString, PLABSM.DbProvider.MSSql);

                System.Data.DataTable SilDt = PLWM.Utils.GetDataTable(ds, 0);

                if (SilDt.Rows.Count > 0)
                {
                    String Otp = PLWM.Utils.CnvToStr(SilDt.Rows[0]["OTP"]);
                    String UserName = PLWM.Utils.CnvToStr(SilDt.Rows[0]["UserEmail"]);

                    String Message = GetSmsEmailFormats.GetSubscriptionEmail(Otp, UserName);
                    Utils.SendEmail(Message, "active@zeroori.com", "Please find the otp in this email for activation of Subscription", "Zeroori", "Zeroori Active");

                    UsageD.Otp = Otp;
                    UsageD.UserAccount = Otp;
                }
                else
                {
                    UsageD.UserData = new ZA3000D()
                    {
                        ZaBase = new BaseD()
                        {
                            ErrorMsg = "Invalid Operation"
                        },
                    };
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
