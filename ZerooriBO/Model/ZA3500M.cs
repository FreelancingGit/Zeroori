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
    public class ZA3500M
    {
        public ZA3500D DoLoad(ZA3000D FilterData, String Mode)
        {
            ZA3500D SignUpV = new ZA3500D();

            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                new XElement("as_sessionid", FilterData.ZaBase.SessionId),
                new XElement("as_mode", Mode)));

                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                DataSet ds = dbObj.SelectSP("ZA3500_sel", XString, PLABSM.DbProvider.MSSql);

                System.Data.DataTable dtComn = PLWM.Utils.GetDataTable(ds, 0);

                if (dtComn.Rows.Count > 0)
                {
                    System.Data.DataRow dr1 = dtComn.Rows[0];
                    SignUpV = new ZA3500D()
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
            }
            catch (Exception e)
            {
                SignUpV.UserData.ZaBase.ErrorMsg = PLWM.Utils.CnvToSentenceCase(e.Message.ToLower().Replace("plerror", "").Replace("plerror", "").Trim());
            }

            return SignUpV;
        }
    }
}
