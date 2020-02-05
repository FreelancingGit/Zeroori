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
    public class ZA3701M
    {


        public ZA3701LD DoLoad(ZA3701LD FilterData, String Mode)
        {
            ZA3701LD UsageD = new ZA3701LD();

            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                new XElement("as_mode", "LD"),
                new XElement("ai_motors_ad_mast_id", FilterData.MotorsAdMastId),
                new XElement("as_sessionid", FilterData.UserData.ZaBase.SessionId)));

                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                DataSet ds = dbObj.SelectSP("ZA3701_sel", XString, PLABSM.DbProvider.MSSql);

                System.Data.DataTable MotorDataDt = PLWM.Utils.GetDataTable(ds, 0);
                System.Data.DataTable FileNamesDt = PLWM.Utils.GetDataTable(ds, 1);
                System.Data.DataTable UserDt = PLWM.Utils.GetDataTable(ds, 2);

                UsageD.BodyType = PLWM.Utils.CnvToStr(MotorDataDt.Rows[0]["BodyType"]);
                UsageD.Brand = PLWM.Utils.CnvToStr(MotorDataDt.Rows[0]["Brand"]);
                UsageD.Years = PLWM.Utils.CnvToStr(MotorDataDt.Rows[0]["Years"]);
                UsageD.Kmters = PLWM.Utils.CnvToStr(MotorDataDt.Rows[0]["Kmters"]);
                UsageD.Colors = PLWM.Utils.CnvToStr(MotorDataDt.Rows[0]["Colors"]);
                UsageD.Doors = PLWM.Utils.CnvToStr(MotorDataDt.Rows[0]["Doors"]);
                UsageD.Warenty = PLWM.Utils.CnvToStr(MotorDataDt.Rows[0]["Warenty"]);
                UsageD.RegionalSpecs = PLWM.Utils.CnvToStr(MotorDataDt.Rows[0]["RegionalSpecs"]);
                UsageD.Transmisson = PLWM.Utils.CnvToStr(MotorDataDt.Rows[0]["Transmisson"]);
                UsageD.FuelType = PLWM.Utils.CnvToStr(MotorDataDt.Rows[0]["FuelType"]);
                UsageD.SellerType = PLWM.Utils.CnvToStr(MotorDataDt.Rows[0]["SellerType"]);
                UsageD.Cylinders = PLWM.Utils.CnvToStr(MotorDataDt.Rows[0]["Cylinders"]);
                UsageD.HoursePower = PLWM.Utils.CnvToStr(MotorDataDt.Rows[0]["HoursePower"]);
                UsageD.Condition = PLWM.Utils.CnvToStr(MotorDataDt.Rows[0]["condition"]);
                UsageD.UsrEmail = PLWM.Utils.CnvToStr(MotorDataDt.Rows[0]["usr_email"]);
                UsageD.UsrPhno = PLWM.Utils.CnvToStr(MotorDataDt.Rows[0]["usr_phno"]);
                UsageD.MotorDescription = PLWM.Utils.CnvToStr(MotorDataDt.Rows[0]["mot_Description"]);
                UsageD.Location = PLWM.Utils.CnvToStr(MotorDataDt.Rows[0]["Location"]);
                UsageD.CrtdDt = PLWM.Utils.CnvToStr(MotorDataDt.Rows[0]["crtd_dt"]);
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
