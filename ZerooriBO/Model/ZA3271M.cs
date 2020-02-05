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
    public class ZA3271M
    {


        public ZA3271LD DoLoad(ZA3271LD FilterData, String Mode)
        {
            ZA3271LD MallDet = new ZA3271LD();

            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                new XElement("as_mode", "LD"),
                new XElement("ai_mallMastId", FilterData.MallMast.MallAdMastId),
                new XElement("as_sessionid", FilterData.UserData.ZaBase.SessionId)
                ));

                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                DataSet ds = dbObj.SelectSP("ZA3271_sel", XString, PLABSM.DbProvider.MSSql);

                System.Data.DataTable UserDt = PLWM.Utils.GetDataTable(ds, 0);
                System.Data.DataTable MallcolDt = PLWM.Utils.GetDataTable(ds, 1);
                System.Data.DataTable FileNamesDt = PLWM.Utils.GetDataTable(ds, 2);
               

                foreach (DataRow dr in MallcolDt.Rows)
                {
                    MallDet.MallMast=new ZA3270D()
                    {
                        MallAdMastId= PLWM.Utils.CnvToStr(dr["mall_mast_id"]),
                        MallName = PLWM.Utils.CnvToStr(dr["mall_name"]),
                        MallLocation = PLWM.Utils.CnvToStr(dr["mall_location"]),
                        MallStartTiming = PLWM.Utils.CnvToStr(dr["mall_start_timing"]),
                        MallEndTiming = PLWM.Utils.CnvToStr(dr["mall_end_timing"]),
                        MallPhone = PLWM.Utils.CnvToStr(dr["Phone"]),
                        MallEmaild = PLWM.Utils.CnvToStr(dr["email"]),
                        MallUrl = PLWM.Utils.CnvToStr(dr["mall_url"]),
                        MallDecrp = PLWM.Utils.CnvToStr(dr["mall_decrp"])
                    };

                }

                MallDet.FileNames = new ComDisValDCol();
                foreach (DataRow Dr in FileNamesDt.Rows)
                {
                    MallDet.FileNames.Add(new ComDisValD
                    {
                        DisPlyMembr = PLWM.Utils.CnvToStr(Dr["mall_mast_img_path"]),
                    });
                }

                if (UserDt.Rows.Count > 0)
                {
                    MallDet.UserData = new ZA3000D()
                    {
                        UsrMastID = PLWM.Utils.CnvToInt(UserDt.Rows[0]["usr_mast_id"]),
                        FistNam = PLWM.Utils.CnvToStr(UserDt.Rows[0]["FirstName"]),
                        ZaBase = new BaseD()
                        {
                            SessionId = PLWM.Utils.CnvToStr(UserDt.Rows[0]["SESSIONID"]),
                        },
                    };
                }
                //UserData
            }
            catch (Exception e)
            {
                MallDet.UserData.ZaBase.ErrorMsg = PLWM.Utils.CnvToSentenceCase(e.Message.ToLower().Replace("plerror", "").Replace("plerror", "").Trim());
            }
            return MallDet;
        }

    }
}
