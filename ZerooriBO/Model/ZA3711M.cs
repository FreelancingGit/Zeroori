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
    public class ZA3711M
    {


        public ZA3711LD DoLoad(ZA3711LD FilterData, String Mode)
        {
            ZA3711LD UsageD = new ZA3711LD();

            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                new XElement("as_mode", "LD"),
                new XElement("ai_prop_ad_mast_id", FilterData.PropAdMastId),
                new XElement("as_sessionid", FilterData.UserData.ZaBase.SessionId)
                ));

                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                DataSet ds = dbObj.SelectSP("ZA3711_sel", XString, PLABSM.DbProvider.MSSql);

                System.Data.DataTable MotorDataDt = PLWM.Utils.GetDataTable(ds, 0);
                System.Data.DataTable FileNamesDt = PLWM.Utils.GetDataTable(ds, 1);
                System.Data.DataTable UserDt = PLWM.Utils.GetDataTable(ds, 2);

                UsageD.PropTitle = PLWM.Utils.CnvToStr(MotorDataDt.Rows[0]["prop_title"]);
                UsageD.CrtdDt = PLWM.Utils.CnvToStr(MotorDataDt.Rows[0]["crtd_dt"]);
                UsageD.BedRoom = PLWM.Utils.CnvToStr(MotorDataDt.Rows[0]["bedRoom"]);
                UsageD.BathRoom = PLWM.Utils.CnvToStr(MotorDataDt.Rows[0]["bathRoom"]);
                UsageD.Size = PLWM.Utils.CnvToStr(MotorDataDt.Rows[0]["size"]);
                UsageD.IsFurnished = PLWM.Utils.CnvToStr(MotorDataDt.Rows[0]["isFurnished"]);
                UsageD.Appartment = PLWM.Utils.CnvToStr(MotorDataDt.Rows[0]["appartment"]);
                UsageD.RentIsPaid = PLWM.Utils.CnvToStr(MotorDataDt.Rows[0]["rentIsPaid"]);
                UsageD.ListedBy = PLWM.Utils.CnvToStr(MotorDataDt.Rows[0]["listedBy"]);
                UsageD.Category = PLWM.Utils.CnvToStr(MotorDataDt.Rows[0]["category"]);
                UsageD.City= PLWM.Utils.CnvToStr(MotorDataDt.Rows[0]["city"]);
                UsageD.UsrEmail = PLWM.Utils.CnvToStr(MotorDataDt.Rows[0]["usr_email"]);
                UsageD.UsrPhno = PLWM.Utils.CnvToStr(MotorDataDt.Rows[0]["usr_phno"]);
                UsageD.PropDescription = PLWM.Utils.CnvToStr(MotorDataDt.Rows[0]["prop_Description"]);
                UsageD.PlaceName= PLWM.Utils.CnvToStr(MotorDataDt.Rows[0]["Location"]);
                UsageD.Price = PLWM.Utils.CnvToStr(MotorDataDt.Rows[0]["Price"]);
                
                UsageD.FileNames = new ComDisValDCol();
                foreach (DataRow Dr in FileNamesDt.Rows)
                {
                    UsageD.FileNames.Add(new ComDisValD
                    {
                        DisPlyMembr = PLWM.Utils.CnvToStr(Dr["full_path"]),
                    });
                }


                if (UserDt.Rows.Count > 0)
                {
                    UsageD.UserData = new ZA3000D()
                    {
                        FistNam = PLWM.Utils.CnvToStr(UserDt.Rows[0]["FirstName"]),
                        ZaBase = new BaseD()
                        {
                            ZaKey = Utils.GetKey(),
                            SessionId = PLWM.Utils.CnvToStr(UserDt.Rows[0]["SESSIONID"]),
                            ErrorMsg = "",
                        }
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
