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
    public class ZA3601M
    {
        public ZA3601SD DoLoad(ZA3601SD FilterData, String Mode)
        {
            ZA3601SD SignUpV = new ZA3601SD();

            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                    new XElement("as_Email", FilterData.UserData.Email),
                    new XElement("as_Passwd", FilterData.UserData.Passwd),
                    new XElement("as_mode", Mode),
                    new XElement("as_sessionid", FilterData.UserData.ZaBase.SessionId),
                    new XElement("ai_motors_ad_mast_id", FilterData.AddMotorsADMastID)
                ));

                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                DataSet ds = dbObj.SelectSP("ZA3601_sel", XString, PLABSM.DbProvider.MSSql);

                System.Data.DataTable dtComn = PLWM.Utils.GetDataTable(ds, 0);
                System.Data.DataTable dtLocation = PLWM.Utils.GetDataTable(ds, 1);
                System.Data.DataTable dtSel = PLWM.Utils.GetDataTable(ds, 2);

                if (dtComn.Rows.Count > 0)
                {
                    System.Data.DataRow dr1 = dtComn.Rows[0];
                    SignUpV = new ZA3601SD()
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
                    SignUpV = new ZA3601SD()
                    {
                        UserData = new ZA3000D()
                        {
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

                foreach (DataRow dr in dtLocation.Rows)
                {
                    SignUpV.LocationCol.Add(new ZA2000D()
                    {
                        CityMastID = PLWM.Utils.CnvToStr(dr["city_mast_id"]),
                        PlaceName = PLWM.Utils.CnvToStr(dr["place_name"])
                    });
                }

                if (dtSel.Rows.Count > 0)
                {
                    DataRow dr = dtSel.Rows[0];
                    SignUpV.Price = PLWM.Utils.CnvToInt(dr["Price"]);

                    SignUpV.Location = SignUpV.LocationCol.FirstOrDefault(x => x.CityMastID ==
                        PLWM.Utils.CnvToStr(dtSel.Rows[0]["city_mast_id"]));

                }
            }
            catch (Exception e)
            {
                SignUpV.UserData.ZaBase.ErrorMsg = PLWM.Utils.CnvToSentenceCase(e.Message.ToLower().Replace("plerror", "").Replace("plerror", "").Trim());
            }

            return SignUpV;
        }


        public ZA3601SD DoRemove(ZA3601SD FilterData, String Mode)
        {
            ZA3601SD SignUpV = new ZA3601SD();

            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                    new XElement("as_MotorsADMastID", FilterData.AddMotorsADMastID),
                    new XElement("as_seq", FilterData.AdSeq),
                    new XElement("as_sessionid", FilterData.UserData.ZaBase.SessionId),
                    new XElement("as_Price", FilterData.Price),
                    new XElement("as_mode", Mode)

                ));

                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                DataSet ds = dbObj.SelectSP("ZA3601_IU", XString, PLABSM.DbProvider.MSSql);
                System.Data.DataTable dtComn = PLWM.Utils.GetDataTable(ds, 0);
                 
                if (dtComn.Rows.Count > 0)
                {
                    SignUpV.FileNames = new ComDisValDCol();
                    foreach (DataRow dr in dtComn.Rows)
                    {
                        SignUpV.FileNames.Add(new ComDisValD()
                        {
                            Descriptn = PLWM.Utils.CnvToStr(dr["motors_img_fldr_full_path"]) 
                        });
                    }
                }
                 
            }
            catch (Exception e)
            {
                SignUpV.UserData.ZaBase.ErrorMsg = PLWM.Utils.CnvToSentenceCase(e.Message.ToLower().Replace("plerror", "").Replace("plerror", "").Trim());
            }

            return SignUpV;
        }

        public ZA3601SD DoSave(ZA3601SD FilterData, String Mode)
        {
            ZA3601SD SignUpV = new ZA3601SD();

            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                    new XElement("as_MotorsADMastID", FilterData.AddMotorsADMastID),
                    new XElement("as_seq", FilterData.AdSeq),
                    new XElement("as_sessionid", FilterData.UserData.ZaBase.SessionId),
                    new XElement("as_Price", FilterData.Price),
                    new XElement("as_mode", Mode),
                    new XElement("as_Location", FilterData.Location.CityMastID)

                ));

                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                DataSet ds = dbObj.SelectSP("ZA3601_IU", XString, PLABSM.DbProvider.MSSql);

                System.Data.DataTable dtComn = PLWM.Utils.GetDataTable(ds, 0);
                System.Data.DataTable dtFiles = PLWM.Utils.GetDataTable(ds, 1);


                if (dtComn.Rows.Count > 0)
                {
                    System.Data.DataRow dr1 = dtComn.Rows[0];
               
                    SignUpV = new ZA3601SD()
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

                    SignUpV.FileNames = new ComDisValDCol();
                    foreach (DataRow dr in dtFiles.Rows)
                    {
                        SignUpV.FileNames.Add(new ComDisValD()
                        {
                            DisPlyMembr = PLWM.Utils.CnvToStr(dr["ImgFullPath"]),
                            ValMembr = PLWM.Utils.CnvToNullableInt(dr["seqnc"]),
                            Descriptn = PLWM.Utils.CnvToStr(dr["ImageUrl"]),
                        });
                    }
                }
                else
                {
                    SignUpV = new ZA3601SD()
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

                    SignUpV.FileNames = new ComDisValDCol();

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
