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
    public class ZA3240M
    {
        public ZA3240LD DoLoad(ZA3240D FilterData, String Mode)
        {
            ZA3240LD SignUpV = new ZA3240LD();

            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                    new XElement("as_mode", Mode),
                    new XElement("as_sessionid", FilterData.UserData.ZaBase.SessionId),
                    new XElement("ai_deal_mast_id", FilterData.UsrBusinesMastId)
                ));

                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                DataSet ds = dbObj.SelectSP("ZA3240_sel", XString, PLABSM.DbProvider.MSSql);

                if (Mode == "LM")
                {
                    System.Data.DataTable dtComn = PLWM.Utils.GetDataTable(ds, 0);
                    System.Data.DataTable dtPlanNames = PLWM.Utils.GetDataTable(ds, 1);

                    if (dtComn.Rows.Count > 0)
                    {
                        DataRow dr1 = dtComn.Rows[0];
                        SignUpV.UserData = new ZA3000D()
                        {
                            FistNam = PLWM.Utils.CnvToStr(dr1["FirstName"]),
                            ZaBase = new BaseD()
                            {
                                SessionId = PLWM.Utils.CnvToStr(dr1["SessionId"]),
                                UserName = PLWM.Utils.CnvToStr(dr1["FirstName"]),
                                ErrorMsg = "",
                                ZaKey = Utils.GetKey()
                            }
                        };
                    }
                    if (dtPlanNames.Rows.Count > 0)
                    {
                        foreach (DataRow drPlans in dtPlanNames.Rows)
                        {
                            SignUpV.Business.Add(new ZA3240D()
                            {
                                PlanName = PLWM.Utils.CnvToStr(drPlans["plan_name"]),
                                UsrBusinesMastId = PLWM.Utils.CnvToNullableInt(drPlans["pack_deal_mast_id"]),
                                UsrBusinesName = PLWM.Utils.CnvToStr(drPlans["busines_Name"]),
								Category = PLWM.Utils.CnvToStr(drPlans["Category"])
							});
                        }
                    }

                }
                //else if (Mode == "SE")
                //{
                //    System.Data.DataTable dtComn = PLWM.Utils.GetDataTable(ds, 0);
                //    System.Data.DataTable dtPlanNames = PLWM.Utils.GetDataTable(ds, 1);

                //    if (dtComn.Rows.Count > 0)
                //    {
                //        System.Data.DataRow dr1 = dtComn.Rows[0];
                //        SignUpV = new ZA3240D()
                //        {

                //            UserData = new ZA3000D()
                //            {
                //                FistNam = PLWM.Utils.CnvToStr(dr1["FirstName"]),
                //                ZaBase = new BaseD()
                //                {
                //                    SessionId = PLWM.Utils.CnvToStr(dr1["SessionId"]),
                //                    UserName = PLWM.Utils.CnvToStr(dr1["FirstName"]),
                //                    ErrorMsg = "",
                //                    ZaKey = Utils.GetKey()
                //                }
                //            }
                //        };
                //    }



                //    if (dtPlanNames.Rows.Count > 0)
                //    {
                //        DataRow drPlans = dtPlanNames.Rows[0];

                //        SignUpV.PlanName = PLWM.Utils.CnvToStr(drPlans["plan_name"]);
                //        SignUpV.UsrBusinesMastId = PLWM.Utils.CnvToNullableInt(drPlans["usr_busines_mast_id"]);
                //        SignUpV.UsrBusinesName = PLWM.Utils.CnvToStr(drPlans["usr_busines_Name"]);
                //        SignUpV.UsrBusinesUrl = PLWM.Utils.CnvToStr(drPlans["usr_busines_url"]);
                //        SignUpV.CategryMastId = PLWM.Utils.CnvToStr(drPlans["categry_mast_id"]);
                //        SignUpV.TitleLogoUrl = PLWM.Utils.CnvToStr(drPlans["title_logo_url"]);
                //        SignUpV.BusinesLogoUrl = PLWM.Utils.CnvToStr(drPlans["busines_logo_url"]);
                //        SignUpV.FacebookUrl = PLWM.Utils.CnvToStr(drPlans["Facebook_url"]);
                //        SignUpV.InstagramUrl = PLWM.Utils.CnvToStr(drPlans["Instagram_url"]);
                //        SignUpV.TwitterUrl = PLWM.Utils.CnvToStr(drPlans["Twitter_url"]);
                //        SignUpV.PhoneNo = PLWM.Utils.CnvToStr(drPlans["Phone_No"]);
                //        SignUpV.EmailId = PLWM.Utils.CnvToStr(drPlans["Email_id"]);
                //        SignUpV.BusinesUrl = PLWM.Utils.CnvToStr(drPlans["busines_url"]);
                //        SignUpV.GeoLocation = PLWM.Utils.CnvToStr(drPlans["geo_Location"]);
                //        SignUpV.DescriptionAboutus = PLWM.Utils.CnvToStr(drPlans["Description_aboutus"]);
                //    }
                //}
            }
            catch (Exception e)
            {
                SignUpV.UserData.ZaBase.ErrorMsg = PLWM.Utils.CnvToSentenceCase(e.Message.ToLower().Replace("plerror", "").Replace("plerror", "").Trim());
            }

            return SignUpV;
        }
       
        public ZA3240D DoSave(ZA3000D FilterData, String Mode)
        {
            ZA3240D SignUpV = new ZA3240D();

            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                    new XElement("as_Email", FilterData.Email),
                    new XElement("as_Passwd", FilterData.Passwd),
                    new XElement("as_mode", Mode),
                    new XElement("as_sessionid", FilterData.ZaBase.SessionId),
                    new XElement("ai_usr_mast_id", "")

                ));


                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                DataSet ds = dbObj.SelectSP("ZA3240_IU", XString, PLABSM.DbProvider.MSSql);
                System.Data.DataTable dtComn = PLWM.Utils.GetDataTable(ds, 0);


                if (dtComn.Rows.Count > 0)
                {
                    System.Data.DataRow dr1 = dtComn.Rows[0];
                    SignUpV = new ZA3240D()
                    {
                        UserData = new ZA3000D()
                        {
                            ZaBase = new BaseD()
                            {
                                SessionId = PLWM.Utils.CnvToStr(dr1["SessionId"]),
                                UserName = PLWM.Utils.CnvToStr(dr1["FirstName"]),
                                ErrorMsg = "",
                                ZaKey = Utils.GetKey()
                            }
                        }
                    };
                }
                else
                {
                    SignUpV = new ZA3240D()
                    {
                        UserData = new ZA3000D()
                        {
                            ZaBase = new BaseD()
                            {
                                SessionId = "",
                                UserName = "",
                                ErrorMsg = "",
                                ZaKey = Utils.GetKey()
                            }
                        }
                    };
                }
            }
            catch (Exception e)
            {
                SignUpV.UserData.ZaBase.ErrorMsg = PLWM.Utils.CnvToSentenceCase(e.Message.ToLower().Replace("plerror", "").Replace("plerror", "").Trim());
            }

            return SignUpV;
        }
    }
}
