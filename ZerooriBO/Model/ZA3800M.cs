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
    public class ZA3800M
    {
        public ZA3800D DoLoad(ZA3800D FilterData, String Mode)
        {
            ZA3800D UsageD = new ZA3800D();

            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                new XElement("as_sessionid", FilterData.UserData.ZaBase.SessionId),
                new XElement("as_mode", Mode)
                ));


                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                DataSet ds = dbObj.SelectSP("ZA3800_sel", XString, PLABSM.DbProvider.MSSql);

                System.Data.DataTable Dt = PLWM.Utils.GetDataTable(ds, 0);
                
                foreach (DataRow dr in Dt.Rows)
                {
                    UsageD=new ZA3800D()
                    {
                        MotorsCount= PLWM.Utils.CnvToNullableInt(dr["motors_count"]),
                        PropertiesCount= PLWM.Utils.CnvToNullableInt(dr["prop_count"]),
                        ClasiifiedsCount= PLWM.Utils.CnvToNullableInt(dr["clasfds_count"]),
                        JobCountJW= PLWM.Utils.CnvToNullableInt(dr["jobs_JW_counts"]),
                        JobCountJH = PLWM.Utils.CnvToNullableInt(dr["jobs_JH_counts"]),
                        JobCountFW = PLWM.Utils.CnvToNullableInt(dr["jobs_FW_counts"]),
                        JobCountFH = PLWM.Utils.CnvToNullableInt(dr["jobs_FH_counts"]),
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
