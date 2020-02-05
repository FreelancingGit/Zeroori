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
    public class ZA2990M
    {
        public ZA2990D DoLoad(ZA3000D FilterData, String Mode)
        {
            ZA2990D SignUpV = new ZA2990D();

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
                DataSet ds = dbObj.SelectSP("ZA2990_sel", XString, PLABSM.DbProvider.MSSql);
                System.Data.DataTable dtComn = PLWM.Utils.GetDataTable(ds, 0);

                if (dtComn.Rows.Count > 0)
                {
                    System.Data.DataRow dr1 = dtComn.Rows[0];
                    SignUpV = new ZA2990D()
                    {
                       
                        UserData = new ZA3000D()
                        {
                            FistNam = PLWM.Utils.CnvToStr(dr1["FirstName"]),
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
                    SignUpV = new ZA2990D()
                    {
                        UserData = new ZA3000D()
                        {
                            FistNam = "",
                            ZaBase = new BaseD()
                            {
                                SessionId = "",
                                UserName ="",
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
        public ZA2990D SendPwd(ZA3000D FilterData, String Mode)
        {
            ZA2990D SignUpV = new ZA2990D();

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
                DataSet ds = dbObj.SelectSP("ZA2990_IU", XString, PLABSM.DbProvider.MSSql);
                System.Data.DataTable dtComn = PLWM.Utils.GetDataTable(ds, 0);

                if (dtComn.Rows.Count > 0)
                {
                    String Password = PLWM.Utils.CnvToStr(dtComn.Rows[0]["usr_passwd"]);
                    String UsrFistNam = PLWM.Utils.CnvToStr(dtComn.Rows[0]["usr_FistNam"]);
                    String PhoneNo = PLWM.Utils.CnvToStr(dtComn.Rows[0]["usr_phno"]);


                    String Message = GetSmsEmailFormats.GetResetPwdEmail(Password);
                    Utils.SendEmail(Message, FilterData.Email, "Please find the new password", "Zeroori", UsrFistNam);

                    //String Message = GetSmsEmailFormats.GetOtp(Otp);
                    Utils.GetSms(PhoneNo, Password);



                    SignUpV = new ZA2990D()
                    {
                        UserData = new ZA3000D()
                        {
                            FistNam = "",
                            ZaBase = new BaseD()
                            {
                                SessionId = "",
                                UserName = UsrFistNam,
                                ErrorMsg = "",
                                ZaKey = Utils.GetKey()
                            }
                        }
                    };
                }
                else
                {
                    SignUpV = new ZA2990D()
                    {
                        UserData = new ZA3000D()
                        {
                            FistNam = "",
                            ZaBase = new BaseD()
                            {
                                SessionId = "",
                                UserName = "",
                                ErrorMsg = "Invalid email ID",
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

        public ZA2990D DoSave(ZA3000D FilterData, String Mode)
        {
            ZA2990D SignUpV = new ZA2990D();

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
                DataSet ds = dbObj.SelectSP("ZA2990_IU", XString, PLABSM.DbProvider.MSSql);
                System.Data.DataTable dtComn = PLWM.Utils.GetDataTable(ds, 0);


                if (dtComn.Rows.Count > 0)
                {
                    System.Data.DataRow dr1 = dtComn.Rows[0];
                    SignUpV = new ZA2990D()
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
                    SignUpV = new ZA2990D()
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
