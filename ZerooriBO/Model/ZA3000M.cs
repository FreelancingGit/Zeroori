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
    public class ZA3000M
    {
        public ZA3000D DoSave(ZA3000D FilterData, String Mode)
        {
            ZA3000D SignUpV = new ZA3000D();

            if (FilterData.FistNam.Trim() == "")
            {
                SignUpV.ZaBase.ErrorMsg = "Please Enter First Name";
            }
            else if (!Regex.IsMatch(FilterData.FistNam.Trim(), @"^[a-zA-Z]+$"))
            {
                SignUpV.ZaBase.ErrorMsg = "Please Enter A Valid First Name";
            }
            else if (FilterData.LastNam.Trim() == "")
            {
                SignUpV.ZaBase.ErrorMsg = "Please Enter Last Name";
            }
            else if (!Regex.IsMatch(FilterData.LastNam.Trim(), @"^[a-zA-Z]+$"))
            {
                SignUpV.ZaBase.ErrorMsg = "Please Enter A Valid Last Name";
            }
            else if (!Regex.IsMatch(FilterData.Mob.Trim(), @"^[0-9]+$"))
            {
                SignUpV.ZaBase.ErrorMsg = "Please Enter A Valid Mobile Number";
            }
            else if (FilterData.Mob.Trim().Length < 8)
            {
                SignUpV.ZaBase.ErrorMsg = " Mobile Number Must Be Grater Than 8 Digits";
            }
            else if (FilterData.Passwd.Trim().Length < 8)
            {
                SignUpV.ZaBase.ErrorMsg = "Your password must be at least 8 characters."; ;
            }
            else
            {
                try
                {
                    XDocument doc = new XDocument(new XElement("Root",
                    new XElement("as_FistNam", FilterData.FistNam),
                    new XElement("as_LastNam", FilterData.LastNam),
                    new XElement("as_Email", FilterData.Email),
                    new XElement("as_Passwd", FilterData.Passwd),
                    new XElement("as_Mob", FilterData.Mob),
                    new XElement("as_mode", Mode),
                    new XElement("ai_usr_mast_id", FilterData.UsrMastID)
                 ));

                    String XString = doc.ToString();
                    PLABSM.DAL dbObj = new PLABSM.DAL();
                    dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                    DataSet ds = dbObj.SelectSP("ZA3000_IU", XString, PLABSM.DbProvider.MSSql);

                    System.Data.DataTable dtComn = PLWM.Utils.GetDataTable(ds, 0);
                    System.Data.DataTable dtOtpEmail = PLWM.Utils.GetDataTable(ds, 1);

                    if (dtComn.Rows.Count > 0)
                    {
                        System.Data.DataRow dr1 = dtComn.Rows[0];
                        SignUpV = new ZA3000D()
                        {
                            ZaBase = new BaseD()
                            {
                                SessionId = PLWM.Utils.CnvToStr(dr1["SessionId"]),
                                UserName = PLWM.Utils.CnvToStr(dr1["FirstName"]),
                                ErrorMsg = "",
                                ZaKey = Utils.GetKey(),
                                Fld = PLWM.Utils.CnvToStr(dr1["usr_fldr_nam"])
                            }
                        };
                    }

                    if (dtOtpEmail.Rows.Count > 0)
                    {
                        String Otp = PLWM.Utils.CnvToStr(dtOtpEmail.Rows[0]["OTP"]);
                        String PhoneNo = PLWM.Utils.CnvToStr(dtOtpEmail.Rows[0]["Mobno"]);
                        String Email = PLWM.Utils.CnvToStr(dtOtpEmail.Rows[0]["Email"]);

                        String Message = GetSmsEmailFormats.GetOtp(Otp);
                        Utils.GetSms(PhoneNo, Otp);

                        Message = GetSmsEmailFormats.GetEmail(Otp);
                        Utils.SendEmail(Message, Email, "Please find the otp in this email for activation", "Zeroori", SignUpV.ZaBase.UserName);
                    }
                }
                catch (Exception e)
                {
                    SignUpV.ZaBase.ErrorMsg = PLWM.Utils.CnvToSentenceCase(e.Message.ToLower().Replace("plerror", "").Replace("plerror", "").Trim());
                }
            }
            return SignUpV;
        }
        //DoMyAdds


        //public ZA3000D DoMyAdds(ZA3000D FilterData, String Mode)
        //{
        //    ZA3000D SignUpV = new ZA3000D();


        //    return SignUpV;
        //}
        public ZA3000D DoUpdatePwd(ZA3000D FilterData, String Mode)
        {
            ZA3000D SignUpV = new ZA3000D();

            if (FilterData.FistNam.Trim() == "")
            {
                SignUpV.ZaBase.ErrorMsg = "Please Enter First Name";
            }
            else if (!Regex.IsMatch(FilterData.FistNam.Trim(), @"^[a-zA-Z]+$"))
            {
                SignUpV.ZaBase.ErrorMsg = "Please Enter A Valid First Name";
            }
            else if (FilterData.LastNam.Trim() == "")
            {
                SignUpV.ZaBase.ErrorMsg = "Please Enter Last Name";
            }
            else if (!Regex.IsMatch(FilterData.LastNam.Trim(), @"^[a-zA-Z]+$"))
            {
                SignUpV.ZaBase.ErrorMsg = "Please Enter A Valid Last Name";
            }
            else if (!Regex.IsMatch(FilterData.Mob.Trim(), @"^[0-9]+$"))
            {
                SignUpV.ZaBase.ErrorMsg = "Please Enter A Valid Mobile Number";
            }
            else
            {
                try
                {
                    XDocument doc = new XDocument(new XElement("Root",
                    new XElement("as_FistNam", FilterData.FistNam),
                    new XElement("as_LastNam", FilterData.LastNam),
                    new XElement("as_Email", FilterData.Email),
                    new XElement("as_Mob", FilterData.Mob),
                    new XElement("as_mode", Mode),
                    new XElement("as_Passwd", FilterData.Passwd),
                    new XElement("as_OldPasswd", FilterData.OldPasswd),
                    new XElement("ai_usr_mast_id", FilterData.UsrMastID)
                 ));

                    String XString = doc.ToString();
                    PLABSM.DAL dbObj = new PLABSM.DAL();
                    dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                    DataSet ds = dbObj.SelectSP("ZA3000_IU", XString, PLABSM.DbProvider.MSSql);

                    System.Data.DataTable dtUser = PLWM.Utils.GetDataTable(ds, 0);



                    if (dtUser.Rows.Count > 0)
                    {
                        System.Data.DataRow dr1 = dtUser.Rows[0];

                        SignUpV = new ZA3000D()
                        {
                            FistNam = PLWM.Utils.CnvToStr(dr1["usr_FistNam"]),
                            LastNam = PLWM.Utils.CnvToStr(dr1["usr_LastNam"]),
                            Email = PLWM.Utils.CnvToStr(dr1["usr_email"]),
                            Mob = PLWM.Utils.CnvToStr(dr1["usr_phno"]),

                            ZaBase = new BaseD()
                            {
                                ErrorMsg = "",
                                ZaKey = Utils.GetKey(),
                            }
                        };
                    }


                }
                catch (Exception e)
                {
                    SignUpV.ZaBase.ErrorMsg = PLWM.Utils.CnvToSentenceCase(e.Message.ToLower().Replace("plerror", "").Replace("plerror", "").Trim());
                }
            }
            return SignUpV;
        }

        public ZA3000D DoUpdate(ZA3000D FilterData, String Mode)
        {
            ZA3000D SignUpV = new ZA3000D();

            if (FilterData.FistNam.Trim() == "")
            {
                SignUpV.ZaBase.ErrorMsg = "Please Enter First Name";
            }
            else if (!Regex.IsMatch(FilterData.FistNam.Trim(), @"^[a-zA-Z]+$"))
            {
                SignUpV.ZaBase.ErrorMsg = "Please Enter A Valid First Name";
            }
            else if (FilterData.LastNam.Trim() == "")
            {
                SignUpV.ZaBase.ErrorMsg = "Please Enter Last Name";
            }
            else if (!Regex.IsMatch(FilterData.LastNam.Trim(), @"^[a-zA-Z]+$"))
            {
                SignUpV.ZaBase.ErrorMsg = "Please Enter A Valid Last Name";
            }
            else if (!Regex.IsMatch(FilterData.Mob.Trim(), @"^[0-9]+$"))
            {
                SignUpV.ZaBase.ErrorMsg = "Please Enter A Valid Mobile Number";
            }
            else
            {
                try
                {
                    XDocument doc = new XDocument(new XElement("Root",
                    new XElement("as_FistNam", FilterData.FistNam),
                    new XElement("as_LastNam", FilterData.LastNam),
                    new XElement("as_Email", FilterData.Email),
                    new XElement("as_Mob", FilterData.Mob),
                    new XElement("as_mode", Mode),
                    new XElement("ai_usr_mast_id", FilterData.UsrMastID)
                 ));

                    String XString = doc.ToString();
                    PLABSM.DAL dbObj = new PLABSM.DAL();
                    dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                    DataSet ds = dbObj.SelectSP("ZA3000_IU", XString, PLABSM.DbProvider.MSSql);

                    System.Data.DataTable dtUser = PLWM.Utils.GetDataTable(ds, 0);



                    if (dtUser.Rows.Count > 0)
                    {
                        System.Data.DataRow dr1 = dtUser.Rows[0];

                        SignUpV = new ZA3000D()
                        {
                            FistNam = PLWM.Utils.CnvToStr(dr1["usr_FistNam"]),
                            LastNam = PLWM.Utils.CnvToStr(dr1["usr_LastNam"]),
                            Email = PLWM.Utils.CnvToStr(dr1["usr_email"]),
                            Mob = PLWM.Utils.CnvToStr(dr1["usr_phno"]),

                            ZaBase = new BaseD()
                            {
                                ErrorMsg = "",
                                ZaKey = Utils.GetKey(),
                            }
                        };
                    }

                   
                }
                catch (Exception e)
                {
                    SignUpV.ZaBase.ErrorMsg = PLWM.Utils.CnvToSentenceCase(e.Message.ToLower().Replace("plerror", "").Replace("plerror", "").Trim());
                }
            }
            return SignUpV;
        }

        public ZA3000D DoLoad(ZA3000D FilterData, String Mode)
        {
            ZA3000D SignUpV = new ZA3000D();
            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                    new XElement("as_otp", FilterData.Otp),
                    new XElement("as_Email", FilterData.Email),
                    new XElement("as_Passwd", FilterData.Passwd),
                    new XElement("ai_usr_mast_id", FilterData.UsrMastID),
                    new XElement("ai_SessionId", FilterData.ZaBase.SessionId),
                    new XElement("as_mode", Mode)
                ));

                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                DataSet ds = dbObj.SelectSP("ZA3000_sel", XString, PLABSM.DbProvider.MSSql);

                System.Data.DataTable dtComn = PLWM.Utils.GetDataTable(ds, 1);

                if (dtComn.Rows.Count > 0)
                {
                    System.Data.DataRow dr1 = dtComn.Rows[0];


                    SignUpV = new ZA3000D()
                    {
                       
                        UsrMastID = PLWM.Utils.CnvToNullableInt(dr1["usr_mast_id"]),

                        FistNam = PLWM.Utils.CnvToStr(dr1["usr_FistNam"]),
                        LastNam = PLWM.Utils.CnvToStr(dr1["usr_LastNam"]),
                        Email = PLWM.Utils.CnvToStr(dr1["usr_email"]),
                        Mob = PLWM.Utils.CnvToStr(dr1["usr_phno"]),
                        ZaBase = new BaseD()
                        {
                            SessionId = PLWM.Utils.CnvToStr(dr1["SessionId"]),
                            UserName = PLWM.Utils.CnvToStr(dr1["usr_FistNam"]),
                            ErrorMsg = "",
                            ZaKey = Utils.GetKey()
                        }
                    };
                }
            }
            catch (Exception e)
            {
                SignUpV.ZaBase.ErrorMsg = PLWM.Utils.CnvToSentenceCase(e.Message.ToLower().Replace("plerror", "").Replace("plerror", "").Trim());
            }
            return SignUpV;
        }
    }
}
