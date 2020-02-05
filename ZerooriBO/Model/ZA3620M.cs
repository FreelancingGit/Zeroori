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
    public class ZA3620M
    {
        public ZA3620LD DoLoad(ZA3000D FilterData, String Mode)
        {
            ZA3620LD UsageD = new ZA3620LD();

            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                new XElement("as_sessionid", FilterData.ZaBase.SessionId),
                new XElement("as_mode", Mode),
                new XElement("as_email", FilterData.Email),
                new XElement("as_passwd", FilterData.Passwd), 
                new XElement("ai_clasifd_ad_mast_id", FilterData.ZaBase.PKID)
                ));


                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                DataSet ds = dbObj.SelectSP("ZA3620_sel", XString, PLABSM.DbProvider.MSSql);

                System.Data.DataTable CategoryDt = PLWM.Utils.GetDataTable(ds, 0);
                System.Data.DataTable SubCategoryDt = PLWM.Utils.GetDataTable(ds, 1);
                System.Data.DataTable AgeDt = PLWM.Utils.GetDataTable(ds, 2);
                System.Data.DataTable UsageDt = PLWM.Utils.GetDataTable(ds, 3);
                System.Data.DataTable ConditionDT = PLWM.Utils.GetDataTable(ds, 4);
                System.Data.DataTable WarrantyDT = PLWM.Utils.GetDataTable(ds, 5);
                System.Data.DataTable dtUser = PLWM.Utils.GetDataTable(ds, 6);
                System.Data.DataTable dtSelData = PLWM.Utils.GetDataTable(ds, 7);

                DataRow drUser = null;
                if (dtUser.Rows.Count > 0)
                {
                    drUser = dtUser.Rows[0];
                }

                UsageD.CategoryCol = new ZA3220DCol();
                UsageD.CategoryCol.Add(new ZA3220D() { ClasifdSpecDtlId = -1, ClasifdSpecValue = "Category" });
                foreach (DataRow dr in CategoryDt.Rows)
                {
                    UsageD.CategoryCol.Add(new ZA3220D()
                    {
                        ClasifdSpecDtlId = PLWM.Utils.CnvToNullableInt(dr["clasifd_dtl_id"]),
                        ClasifdSpecValue = PLWM.Utils.CnvToStr(dr["clasifd_value"]),
                    });
                }



                UsageD.SubCategoryCol = new ZA3220DCol();
                UsageD.SubCategoryCol.Add(new ZA3220D() { ClasifdSpecDtlId = -1, ClasifdSpecValue = "Sub Category" });
                foreach (DataRow dr in SubCategoryDt.Rows)
                {
                    UsageD.SubCategoryCol.Add(new ZA3220D()
                    {
                        ClasifdSpecDtlId = PLWM.Utils.CnvToNullableInt(dr["clasifd_dtl_id"]),
                        ClasifdSpecValue = PLWM.Utils.CnvToStr(dr["clasifd_value"]),
                    });
                }

                UsageD.AgeCol = new ZA3220DCol();
                UsageD.AgeCol.Add(new ZA3220D() { ClasifdSpecDtlId = -1, ClasifdSpecValue = "Age"});
                foreach (DataRow dr in AgeDt.Rows)
                {
                    UsageD.AgeCol.Add(new ZA3220D()
                    {
                        ClasifdSpecDtlId = PLWM.Utils.CnvToNullableInt(dr["clasifd_dtl_id"]),
                        ClasifdSpecValue = PLWM.Utils.CnvToStr(dr["clasifd_value"]),
                    });
                }


                UsageD.UsageCol = new ZA3220DCol();
                UsageD.UsageCol.Add(new ZA3220D() { ClasifdSpecDtlId = -1, ClasifdSpecValue = "Usage" });
                foreach (DataRow dr in UsageDt.Rows)
                {
                    UsageD.UsageCol.Add(new ZA3220D()
                    {
                        ClasifdSpecDtlId = PLWM.Utils.CnvToNullableInt(dr["clasifd_dtl_id"]),
                        ClasifdSpecValue = PLWM.Utils.CnvToStr(dr["clasifd_value"]),
                    });
                }

                UsageD.ConditionCol = new ZA3220DCol();
                UsageD.ConditionCol.Add(new ZA3220D() { ClasifdSpecDtlId = -1, ClasifdSpecValue = "Condition" });
                foreach (DataRow dr in ConditionDT.Rows)
                {
                    UsageD.ConditionCol.Add(new ZA3220D()
                    {
                        ClasifdSpecDtlId = PLWM.Utils.CnvToNullableInt(dr["clasifd_dtl_id"]),
                        ClasifdSpecValue = PLWM.Utils.CnvToStr(dr["clasifd_value"]),
                    });
                }


                UsageD.WarrantyCol = new ZA3220DCol();
                UsageD.WarrantyCol.Add(new ZA3220D() { ClasifdSpecDtlId = -1, ClasifdSpecValue = "Warranty" });
                foreach (DataRow dr in WarrantyDT.Rows)
                {
                    UsageD.WarrantyCol.Add(new ZA3220D()
                    {
                        ClasifdSpecDtlId = PLWM.Utils.CnvToNullableInt(dr["clasifd_dtl_id"]),
                        ClasifdSpecValue = PLWM.Utils.CnvToStr(dr["clasifd_value"]),
                    });
                }

 
                UsageD.UserData = new ZA3000D()
                {
                    ZaBase = new BaseD()
                    {
                        SessionId = PLWM.Utils.CnvToStr(drUser["SessionId"]),
                        UserName = PLWM.Utils.CnvToStr(drUser["FirstName"]),
                        ErrorMsg = "",
                        ZaKey = Utils.GetKey()
                    }
                };

                foreach (DataRow dr in dtSelData.Rows)
                {
                    UsageD.SelectedDataDtl =new ZA3621SD()
                    {

                        Location = UsageD.LocationCol.FirstOrDefault(x => x.CityMastID == PLWM.Utils.CnvToStr(dtSelData.Rows[0]["city_mast_id"])),
                        Price = PLWM.Utils.CnvToInt(dtSelData.Rows[0]["Price"]),
                    };
                }

                foreach (DataRow dr in dtSelData.Rows)
                {
                    UsageD.SelectedData = new ZA3620SD()
                    {
                        ClasifdADMastID = PLWM.Utils.CnvToInt(dtSelData.Rows[0]["clasifd_ad_mast_id"]),
                        Title = PLWM.Utils.CnvToStr(dtSelData.Rows[0]["clasifd_title"]),
                        Description = PLWM.Utils.CnvToStr(dtSelData.Rows[0]["clasifd_Description"]),

                        Category = UsageD.CategoryCol.FirstOrDefault(x => x.ClasifdSpecDtlId == PLWM.Utils.CnvToNullableInt(dtSelData.Rows[0]["Category_id"])),

                        Age = UsageD.AgeCol.FirstOrDefault(x => x.ClasifdSpecDtlId == PLWM.Utils.CnvToNullableInt(dtSelData.Rows[0]["Age_id"])),

                        Usage = UsageD.UsageCol.FirstOrDefault(x => x.ClasifdSpecDtlId == PLWM.Utils.CnvToNullableInt(dtSelData.Rows[0]["Usage_id"])),

                        Condition = UsageD.ConditionCol.FirstOrDefault(x => x.ClasifdSpecDtlId == PLWM.Utils.CnvToNullableInt(dtSelData.Rows[0]["Condition_id"])),

                        Warranty = UsageD.WarrantyCol.FirstOrDefault(x => x.ClasifdSpecDtlId == PLWM.Utils.CnvToNullableInt(dtSelData.Rows[0]["Warranty_id"])),
                    };
                    }
                
            }
            catch (Exception e)
            {
                UsageD.UserData.ZaBase.ErrorMsg = PLWM.Utils.CnvToSentenceCase(e.Message.ToLower().Replace("plerror", "").Replace("plerror", "").Trim());
            }

            return UsageD;
        }




        public ZA3620LD DoSave(ZA3620SD SaveData, String Mode)
        {
            ZA3620LD UsageD = new ZA3620LD();

            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                new XElement("as_sessionid", SaveData.UserData.ZaBase.SessionId),
                new XElement("as_Category", SaveData.Category.ClasifdSpecDtlId),
                new XElement("as_Sub_Category", SaveData.SubCategory.ClasifdSpecDtlId),
                new XElement("as_Age", SaveData.Age.ClasifdSpecDtlId),
                new XElement("as_Usage", SaveData.Usage.ClasifdSpecDtlId),
                new XElement("as_Condition", SaveData.Condition.ClasifdSpecDtlId),
                new XElement("as_Warranty", SaveData.Warranty.ClasifdSpecDtlId),
                 
                new XElement("as_title", SaveData.Title),
                new XElement("as_Description", SaveData.Description),
                new XElement("as_clasifdADMastID", SaveData.ClasifdADMastID )
                
                ));


                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                DataSet ds = dbObj.SelectSP("ZA3620_IU", XString, PLABSM.DbProvider.MSSql);

                System.Data.DataTable MotorData  = PLWM.Utils.GetDataTable(ds, 0);
                System.Data.DataTable MotorFileData = PLWM.Utils.GetDataTable(ds, 1);

                if (MotorData.Rows.Count > 0)
                {
                    DataRow Dr = MotorData.Rows[0] ;
                    UsageD.ClasifdADMastID = PLWM.Utils.CnvToNullableInt(Dr["clasifdADMastID"]);
                    UsageD.UserData.ZaBase.SessionId = PLWM.Utils.CnvToStr(Dr["sessionid"]);
                    UsageD.UserData.ZaBase.ErrorMsg = "";
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
