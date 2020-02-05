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
    public class ZA3700M
    {
        public ZA3700ILD DoInit(ZA3000D FilterData, String Mode)
        {
            ZA3700ILD UsageD = new ZA3700ILD();

            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                new XElement("as_mode", Mode),
                new XElement("ai_pageno", ""),
                new XElement("as_sessionid", FilterData.ZaBase.SessionId),
                new XElement("as_Option", ""),
                new XElement("as_location",""),
                new XElement("as_sortby",2),
				new XElement("ai_model", ""),
				new XElement("ai_fuelType", ""),
				new XElement("ai_color", ""),
				new XElement("ai_bodyType", "")
				));

                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                DataSet ds = dbObj.SelectSP("ZA3700_sel", XString, PLABSM.DbProvider.MSSql);

                
                System.Data.DataTable MotorDt = PLWM.Utils.GetDataTable(ds, 0);
                System.Data.DataTable PageNoDt = PLWM.Utils.GetDataTable(ds, 1);

                System.Data.DataTable UserDataDt = PLWM.Utils.GetDataTable(ds, 2);
                
                System.Data.DataTable LocationDt = PLWM.Utils.GetDataTable(ds, 3);
                System.Data.DataTable SortByDt = PLWM.Utils.GetDataTable(ds, 4);

                System.Data.DataTable BodyTypeDt = PLWM.Utils.GetDataTable(ds, 5);
                System.Data.DataTable BrandDt = PLWM.Utils.GetDataTable(ds, 6);
                System.Data.DataTable ModelDt = PLWM.Utils.GetDataTable(ds, 7);
                System.Data.DataTable FuelTypeDt  = PLWM.Utils.GetDataTable(ds, 8);
                System.Data.DataTable ColorDt = PLWM.Utils.GetDataTable(ds, 9);


                if (UserDataDt.Rows.Count > 0)
                {
                    UsageD.UserData = new ZA3000D()
                    {
                        FistNam = PLWM.Utils.CnvToStr(UserDataDt.Rows[0]["FirstName"]),
                        ZaBase = new BaseD()
                        {
                            ZaKey = Utils.GetKey(),
                            SessionId = PLWM.Utils.CnvToStr(UserDataDt.Rows[0]["SESSIONID"]),
                            ErrorMsg = "",
                        }
                    };
                }
                else
                {
                    UsageD.UserData = new ZA3000D()
                    {
                        FistNam = "",
                        ZaBase = new BaseD()
                        {
                            ZaKey = Utils.GetKey(),
                            SessionId = "",
                            ErrorMsg = "",
                        }
                    };
                }

                UsageD.MotorDataCol = new ZA3700BDCol();
                foreach (DataRow Dr in MotorDt.Rows)
                {
                    UsageD.MotorDataCol.Add(new ZA3700BD
                    {
                        Title = PLWM.Utils.CnvToStr(Dr["mot_Title"]),
                        Years = PLWM.Utils.CnvToStr(Dr["Years"]),
                        Kmters = PLWM.Utils.CnvToStr(Dr["Kmters"]),
                        Email = PLWM.Utils.CnvToStr(Dr["usr_email"]),
                        Location = PLWM.Utils.CnvToStr(Dr["Location"]),
                        PhNo = PLWM.Utils.CnvToStr(Dr["usr_phno"]),
                        PostDate = PLWM.Utils.CnvToStr(Dr["crtd_dt"]),
                        ProductImage = PLWM.Utils.CnvToStr(Dr["full_path"]),
                        Rate = PLWM.Utils.CnvToStr(Dr["Price"]),
                        Colors = PLWM.Utils.CnvToStr(Dr["Colors"]),
                        Doors = PLWM.Utils.CnvToStr(Dr["Doors"]),
                        MotorsAdMastId = PLWM.Utils.CnvToStr(Dr["motors_ad_mast_id"])
                    });
                }

                UsageD.LocationCol = new ComDisValDCol();
                UsageD.LocationCol.Add(new ComDisValD() { DisPlyMembr = "Location", ValMembr = -1 });
                foreach (DataRow Dr in LocationDt.Rows)
                {
                    UsageD.LocationCol.Add(new ComDisValD()
                    {
                      DisPlyMembr = PLWM.Utils.CnvToStr(Dr["place_name"]),
                      ValMembr = PLWM.Utils.CnvToInt(Dr["city_mast_id"]),
                    });
                }


                UsageD.SortByCol = new ComDisValDCol();
                UsageD.SortByCol.Add(new ComDisValD() { DisPlyMembr = "Sort By", ValMembr = -1 });
                foreach (DataRow Dr in SortByDt.Rows)
                {
                    UsageD.SortByCol.Add(new ComDisValD()
                    {
                        DisPlyMembr = PLWM.Utils.CnvToStr(Dr["SortMode"]),
                        ValMembr = PLWM.Utils.CnvToInt(Dr["SortValue"]),
                    });
                }

                UsageD.BrandCol = new ZA3200DCol();
                foreach (DataRow Dr in BrandDt.Rows)
                {
                    UsageD.BrandCol.Add(new ZA3200D()
                    {
                        MotorSpecDtlId = PLWM.Utils.CnvToInt(Dr["motor_spec_dtl_id"]),
                        MotorSpecValue = PLWM.Utils.CnvToStr(Dr["motor_spec_value"]),
                        MotorSpecImgPath = PLWM.Utils.CnvToStr(Dr["motor_spec_img_path"])
                    });
                }

                UsageD.ModelCol = new ZA3200DCol();
                UsageD.ModelCol.Add(new ZA3200D() { MotorSpecValue = "Model", MotorSpecDtlId = -1 });
                foreach (DataRow Dr in ModelDt.Rows)
                {
                    UsageD.ModelCol.Add(new ZA3200D()
                    {
                        MotorSpecDtlId = PLWM.Utils.CnvToInt(Dr["motor_spec_dtl_id"]),
                        MotorSpecValue = PLWM.Utils.CnvToStr(Dr["motor_spec_value"]),
                    });
                }

                UsageD.FuelTypeCol = new ZA3200DCol();
                UsageD.FuelTypeCol.Add(new ZA3200D() { MotorSpecValue = "Fuel Type", MotorSpecDtlId = -1 });
                foreach (DataRow Dr in FuelTypeDt.Rows)
                {
                    UsageD.FuelTypeCol.Add(new ZA3200D()
                    {
                        MotorSpecDtlId = PLWM.Utils.CnvToInt(Dr["motor_spec_dtl_id"]),
                        MotorSpecValue = PLWM.Utils.CnvToStr(Dr["motor_spec_value"]),
                    });
                }

                UsageD.BodyTypeCol = new ZA3200DCol();
                UsageD.BodyTypeCol.Add(new ZA3200D() { MotorSpecValue = "Body Type", MotorSpecDtlId = -1 });
                foreach (DataRow Dr in BodyTypeDt.Rows)
                {
                    UsageD.BodyTypeCol.Add(new ZA3200D()
                    {
                        MotorSpecDtlId = PLWM.Utils.CnvToInt(Dr["motor_spec_dtl_id"]),
                        MotorSpecValue = PLWM.Utils.CnvToStr(Dr["motor_spec_value"]),
                    });
                }

                UsageD.ColorCol = new ZA3200DCol();
                UsageD.ColorCol.Add(new ZA3200D() { MotorSpecValue = "Colour", MotorSpecDtlId = -1 });
                foreach (DataRow Dr in ColorDt.Rows)
                {
                    UsageD.ColorCol.Add(new ZA3200D()
                    {
                        MotorSpecDtlId = PLWM.Utils.CnvToInt(Dr["motor_spec_dtl_id"]),
                        MotorSpecValue = PLWM.Utils.CnvToStr(Dr["motor_spec_value"]),
                    });
                }

                UsageD.PageNoCol = new ComDisValDCol();
                foreach (DataRow Dr in PageNoDt.Rows)
                {
                    UsageD.PageNoCol.Add(new ComDisValD()
                    {
                        ValMembr = PLWM.Utils.CnvToInt(Dr["TotalPages"]),
                        DisPlyMembr = PLWM.Utils.CnvToStr(Dr["Page_No"]),
                    });
                }


            }
            catch (Exception e)
            {
                UsageD.UserData.ZaBase.ErrorMsg = PLWM.Utils.CnvToSentenceCase(e.Message.ToLower().Replace("plerror", "").Replace("plerror", "").Trim());
            }
            return UsageD;
        }



        public ZA3700ILD DoLoad(ZA3700LFD FilterData, String Mode)
        {
            ZA3700ILD UsageD = new ZA3700ILD();

            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                new XElement("as_mode", "LD"),
                new XElement("ai_pageno", FilterData.PageNo),
                new XElement("as_sessionid",""),
                new XElement("as_Option", FilterData.Catagory.MotorSpecDtlId),
                new XElement("as_location", FilterData.Location.ValMembr<=0?null: FilterData.Location.ValMembr),
                new XElement("as_sortby", FilterData.SortBy.ValMembr),
				new XElement("ai_model", ""),
				new XElement("ai_fuelType", ""),
				new XElement("ai_color", ""),
				new XElement("ai_bodyType", "")
				));

                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                DataSet ds = dbObj.SelectSP("ZA3700_sel", XString, PLABSM.DbProvider.MSSql);

                System.Data.DataTable ClasifiedDt = PLWM.Utils.GetDataTable(ds, 0);
                System.Data.DataTable PageNoDt = PLWM.Utils.GetDataTable(ds, 1);

                UsageD.UserData = new ZA3000D()
                {
                    ZaBase = new BaseD()
                    {
                        ZaKey = Utils.GetKey(),
                        ErrorMsg = ""
                    }
                };


                UsageD.MotorDataCol = new ZA3700BDCol();
                foreach (DataRow Dr in ClasifiedDt.Rows)
                {
                    UsageD.MotorDataCol.Add(new ZA3700BD
                    {
                        Title = PLWM.Utils.CnvToStr(Dr["mot_Title"]),
                        Years = PLWM.Utils.CnvToStr(Dr["Years"]),
                        Kmters = PLWM.Utils.CnvToStr(Dr["Kmters"]),
                        Email = PLWM.Utils.CnvToStr(Dr["usr_email"]),
                        Location = PLWM.Utils.CnvToStr(Dr["Location"]),
                        PhNo = PLWM.Utils.CnvToStr(Dr["usr_phno"]),
                        PostDate = PLWM.Utils.CnvToStr(Dr["crtd_dt"]),
                        ProductImage = PLWM.Utils.CnvToStr(Dr["full_path"]),
                        Rate = PLWM.Utils.CnvToStr(Dr["Price"]),
                        Colors = PLWM.Utils.CnvToStr(Dr["Colors"]),
                        Doors = PLWM.Utils.CnvToStr(Dr["Doors"]),
                        MotorsAdMastId = PLWM.Utils.CnvToStr(Dr["motors_ad_mast_id"])
                    });
                }


                UsageD.PageNoCol = new ComDisValDCol();
                foreach (DataRow Dr in PageNoDt.Rows)
                {
                    UsageD.PageNoCol.Add(new ComDisValD()
                    {
                        ValMembr = PLWM.Utils.CnvToInt(Dr["TotalPages"]),
                        DisPlyMembr = PLWM.Utils.CnvToStr(Dr["Page_No"]),
                    });
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
