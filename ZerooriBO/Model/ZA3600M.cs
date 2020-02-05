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
    public class ZA3600M
    {
        public ZA3600LD DoLoad(ZA3000D FilterData, String Mode)
        {
            ZA3600LD MotorSpecD = new ZA3600LD();

            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                new XElement("as_sessionid", FilterData.ZaBase.SessionId),
                new XElement("as_mode", Mode),
                new XElement("as_email", FilterData.Email),
                new XElement("as_passwd", FilterData.Passwd),
                new XElement("ai_motors_ad_mast_id", FilterData.ZaBase.PKID)
                ));


                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                DataSet ds = dbObj.SelectSP("ZA3600_sel", XString, PLABSM.DbProvider.MSSql);

                System.Data.DataTable YearDt = PLWM.Utils.GetDataTable(ds, 0);
                System.Data.DataTable ColourDt = PLWM.Utils.GetDataTable(ds, 1);
                System.Data.DataTable DoorsDt = PLWM.Utils.GetDataTable(ds, 2);
                System.Data.DataTable WarrantyDt = PLWM.Utils.GetDataTable(ds, 3);
                System.Data.DataTable RegionalSpecsDT = PLWM.Utils.GetDataTable(ds, 4);
                System.Data.DataTable TransmissonDT = PLWM.Utils.GetDataTable(ds, 5);
                System.Data.DataTable BodyTypeDT = PLWM.Utils.GetDataTable(ds, 6);
                System.Data.DataTable FuelTypeDT = PLWM.Utils.GetDataTable(ds, 7);
                System.Data.DataTable CylindersDT = PLWM.Utils.GetDataTable(ds, 8);
                System.Data.DataTable SellerTypeDT = PLWM.Utils.GetDataTable(ds, 9);
                System.Data.DataTable ExtrasDT = PLWM.Utils.GetDataTable(ds, 10);
                System.Data.DataTable TechinalFeaturesDT = PLWM.Utils.GetDataTable(ds, 11);
                System.Data.DataTable HoursePowerDT = PLWM.Utils.GetDataTable(ds, 12);
                System.Data.DataTable BrandColDT = PLWM.Utils.GetDataTable(ds, 13);
                System.Data.DataTable ConditionDT = PLWM.Utils.GetDataTable(ds, 14);
                System.Data.DataTable dtUser = PLWM.Utils.GetDataTable(ds, 15);
                System.Data.DataTable dtSelectedData = PLWM.Utils.GetDataTable(ds, 16);

                DataRow drUser = null;
                if (dtUser.Rows.Count > 0)
                {
                    drUser = dtUser.Rows[0];
                }

                MotorSpecD.YearCol = new ZA3200DCol();
                MotorSpecD.YearCol.Add(new ZA3200D() { MotorSpecDtlId = -1, MotorSpecValue = "Year" });
                foreach (DataRow dr in YearDt.Rows)
                {
                    MotorSpecD.YearCol.Add(new ZA3200D()
                    {
                        MotorSpecDtlId = PLWM.Utils.CnvToNullableInt(dr["motor_spec_dtl_id"]),
                        MotorSpecValue = PLWM.Utils.CnvToStr(dr["motor_spec_value"]),
                    });
                }



                MotorSpecD.ColourCol = new ZA3200DCol();
                MotorSpecD.ColourCol.Add(new ZA3200D() { MotorSpecDtlId = -1, MotorSpecValue = "Colour" });
                foreach (DataRow dr in ColourDt.Rows)
                {
                    MotorSpecD.ColourCol.Add(new ZA3200D()
                    {
                        MotorSpecDtlId = PLWM.Utils.CnvToNullableInt(dr["motor_spec_dtl_id"]),
                        MotorSpecValue = PLWM.Utils.CnvToStr(dr["motor_spec_value"]),
                    });
                }

                MotorSpecD.DoorsCol = new ZA3200DCol();
                MotorSpecD.DoorsCol.Add(new ZA3200D() { MotorSpecDtlId = -1, MotorSpecValue = "Doors" });
                foreach (DataRow dr in DoorsDt.Rows)
                {
                    MotorSpecD.DoorsCol.Add(new ZA3200D()
                    {
                        MotorSpecDtlId = PLWM.Utils.CnvToNullableInt(dr["motor_spec_dtl_id"]),
                        MotorSpecValue = PLWM.Utils.CnvToStr(dr["motor_spec_value"]),
                    });
                }


                MotorSpecD.WarrantyCol = new ZA3200DCol();
                MotorSpecD.WarrantyCol.Add(new ZA3200D() { MotorSpecDtlId = -1, MotorSpecValue = "Warranty" });
                foreach (DataRow dr in WarrantyDt.Rows)
                {
                    MotorSpecD.WarrantyCol.Add(new ZA3200D()
                    {
                        MotorSpecDtlId = PLWM.Utils.CnvToNullableInt(dr["motor_spec_dtl_id"]),
                        MotorSpecValue = PLWM.Utils.CnvToStr(dr["motor_spec_value"]),
                    });
                }

                MotorSpecD.RegionalSpecsCol = new ZA3200DCol();
                MotorSpecD.RegionalSpecsCol.Add(new ZA3200D() { MotorSpecDtlId = -1, MotorSpecValue = "Regional Specs" });
                foreach (DataRow dr in RegionalSpecsDT.Rows)
                {
                    MotorSpecD.RegionalSpecsCol.Add(new ZA3200D()
                    {
                        MotorSpecDtlId = PLWM.Utils.CnvToNullableInt(dr["motor_spec_dtl_id"]),
                        MotorSpecValue = PLWM.Utils.CnvToStr(dr["motor_spec_value"]),
                    });
                }


                MotorSpecD.TransmissonCol = new ZA3200DCol();
                MotorSpecD.TransmissonCol.Add(new ZA3200D() { MotorSpecDtlId = -1, MotorSpecValue = "Transmisson Type" });
                foreach (DataRow dr in TransmissonDT.Rows)
                {
                    MotorSpecD.TransmissonCol.Add(new ZA3200D()
                    {
                        MotorSpecDtlId = PLWM.Utils.CnvToNullableInt(dr["motor_spec_dtl_id"]),
                        MotorSpecValue = PLWM.Utils.CnvToStr(dr["motor_spec_value"]),
                    });
                }


                MotorSpecD.BodyTypeCol = new ZA3200DCol();
                MotorSpecD.BodyTypeCol.Add(new ZA3200D() { MotorSpecDtlId = -1, MotorSpecValue = "Body Type" });
                foreach (DataRow dr in BodyTypeDT.Rows)
                {
                    MotorSpecD.BodyTypeCol.Add(new ZA3200D()
                    {
                        MotorSpecDtlId = PLWM.Utils.CnvToNullableInt(dr["motor_spec_dtl_id"]),
                        MotorSpecValue = PLWM.Utils.CnvToStr(dr["motor_spec_value"]),
                    });
                }

                MotorSpecD.FuelTypeCol = new ZA3200DCol();
                MotorSpecD.FuelTypeCol.Add(new ZA3200D() { MotorSpecDtlId = -1, MotorSpecValue = "Fuel Type" });
                foreach (DataRow dr in FuelTypeDT.Rows)
                {
                    MotorSpecD.FuelTypeCol.Add(new ZA3200D()
                    {
                        MotorSpecDtlId = PLWM.Utils.CnvToNullableInt(dr["motor_spec_dtl_id"]),
                        MotorSpecValue = PLWM.Utils.CnvToStr(dr["motor_spec_value"]),
                    });
                }

                MotorSpecD.CylindersCol = new ZA3200DCol();
                MotorSpecD.CylindersCol.Add(new ZA3200D() { MotorSpecDtlId = -1, MotorSpecValue = " No. of Cylinders" });
                foreach (DataRow dr in CylindersDT.Rows)
                {
                    MotorSpecD.CylindersCol.Add(new ZA3200D()
                    {
                        MotorSpecDtlId = PLWM.Utils.CnvToNullableInt(dr["motor_spec_dtl_id"]),
                        MotorSpecValue = PLWM.Utils.CnvToStr(dr["motor_spec_value"]),
                    });
                }

                MotorSpecD.SellerTypeCol = new ZA3200DCol();
                MotorSpecD.SellerTypeCol.Add(new ZA3200D() { MotorSpecDtlId = -1, MotorSpecValue = "Listed by" });
                foreach (DataRow dr in SellerTypeDT.Rows)
                {
                    MotorSpecD.SellerTypeCol.Add(new ZA3200D()
                    {
                        MotorSpecDtlId = PLWM.Utils.CnvToNullableInt(dr["motor_spec_dtl_id"]),
                        MotorSpecValue = PLWM.Utils.CnvToStr(dr["motor_spec_value"]),
                    });
                }

                MotorSpecD.ExtrasCol = new ZA3200DCol();
                foreach (DataRow dr in ExtrasDT.Rows)
                {
                    MotorSpecD.ExtrasCol.Add(new ZA3200D()
                    {
                        MotorSpecDtlId = PLWM.Utils.CnvToNullableInt(dr["motor_spec_dtl_id"]),
                        MotorSpecValue = PLWM.Utils.CnvToStr(dr["motor_spec_value"]),
                    });
                }


                MotorSpecD.TechinalFeaturesCol = new ZA3200DCol();
                foreach (DataRow dr in TechinalFeaturesDT.Rows)
                {
                    MotorSpecD.TechinalFeaturesCol.Add(new ZA3200D()
                    {
                        MotorSpecDtlId = PLWM.Utils.CnvToNullableInt(dr["motor_spec_dtl_id"]),
                        MotorSpecValue = PLWM.Utils.CnvToStr(dr["motor_spec_value"]),
                    });
                }


                MotorSpecD.HoursePowerCol = new ZA3200DCol();
                MotorSpecD.HoursePowerCol.Add(new ZA3200D() { MotorSpecDtlId = -1, MotorSpecValue = "Horsepower" });

                foreach (DataRow dr in HoursePowerDT.Rows)
                {
                    MotorSpecD.HoursePowerCol.Add(new ZA3200D()
                    {
                        MotorSpecDtlId = PLWM.Utils.CnvToNullableInt(dr["motor_spec_dtl_id"]),
                        MotorSpecValue = PLWM.Utils.CnvToStr(dr["motor_spec_value"]),
                    });
                }


                MotorSpecD.BrandCol = new ZA3200DCol();
                MotorSpecD.BrandCol.Add(new ZA3200D() { MotorSpecDtlId = -1, MotorSpecValue = "Brand" });
                foreach (DataRow dr in BrandColDT.Rows)
                {
                    MotorSpecD.BrandCol.Add(new ZA3200D()
                    {
                        MotorSpecDtlId = PLWM.Utils.CnvToNullableInt(dr["motor_spec_dtl_id"]),
                        MotorSpecValue = PLWM.Utils.CnvToStr(dr["motor_spec_value"]),
                    });
                }

                MotorSpecD.ConditionCol = new ZA3200DCol();
                MotorSpecD.ConditionCol.Add(new ZA3200D() { MotorSpecDtlId = -1, MotorSpecValue = "Condition" });
                foreach (DataRow dr in ConditionDT.Rows)
                {
                    MotorSpecD.ConditionCol.Add(new ZA3200D()
                    {
                        MotorSpecDtlId = PLWM.Utils.CnvToNullableInt(dr["motor_spec_dtl_id"]),
                        MotorSpecValue = PLWM.Utils.CnvToStr(dr["motor_spec_value"]),
                    });
                }


                MotorSpecD.UserData = new ZA3000D()
                {
                    ZaBase = new BaseD()
                    {
                        SessionId = PLWM.Utils.CnvToStr(drUser["SessionId"]),
                        UserName = PLWM.Utils.CnvToStr(drUser["FirstName"]),
                        ErrorMsg = "",
                        ZaKey = Utils.GetKey()
                    }
                };

                if (dtSelectedData.Rows.Count > 0)
                {
                    MotorSpecD.SelectedData = new ZA3600SD()
                    {
                        BodyType = MotorSpecD.BodyTypeCol.FirstOrDefault(x => x.MotorSpecDtlId == PLWM.Utils.CnvToNullableInt(dtSelectedData.Rows[0]["BodyType_id"])),
                        Brand = MotorSpecD.BrandCol.FirstOrDefault(x => x.MotorSpecDtlId == PLWM.Utils.CnvToNullableInt(dtSelectedData.Rows[0]["Brand_id"])),
                        Colour = MotorSpecD.ColourCol.FirstOrDefault(x => x.MotorSpecDtlId == PLWM.Utils.CnvToNullableInt(dtSelectedData.Rows[0]["Colour_id"])),
                        Condition = MotorSpecD.ConditionCol.FirstOrDefault(x => x.MotorSpecDtlId == PLWM.Utils.CnvToNullableInt(dtSelectedData.Rows[0]["condition_id"])),
                        Cylinders = MotorSpecD.CylindersCol.FirstOrDefault(x => x.MotorSpecDtlId == PLWM.Utils.CnvToNullableInt(dtSelectedData.Rows[0]["Cylinders_id"])),
                        Doors = MotorSpecD.DoorsCol.FirstOrDefault(x => x.MotorSpecDtlId == PLWM.Utils.CnvToNullableInt(dtSelectedData.Rows[0]["Doors_id"])),
                        FuelType = MotorSpecD.FuelTypeCol.FirstOrDefault(x => x.MotorSpecDtlId == PLWM.Utils.CnvToNullableInt(dtSelectedData.Rows[0]["FuelType_id"])),
                        HoursePower = MotorSpecD.HoursePowerCol.FirstOrDefault(x => x.MotorSpecDtlId == PLWM.Utils.CnvToNullableInt(dtSelectedData.Rows[0]["HoursePower_id"])),
                        RegionalSpecs = MotorSpecD.RegionalSpecsCol.FirstOrDefault(x => x.MotorSpecDtlId == PLWM.Utils.CnvToNullableInt(dtSelectedData.Rows[0]["RegionalSpecs_id"])),
                        SellerType = MotorSpecD.SellerTypeCol.FirstOrDefault(x => x.MotorSpecDtlId == PLWM.Utils.CnvToNullableInt(dtSelectedData.Rows[0]["SellerType_id"])),
                        Year = MotorSpecD.YearCol.FirstOrDefault(x => x.MotorSpecDtlId == PLWM.Utils.CnvToNullableInt(dtSelectedData.Rows[0]["Year_id"])),
                        Warranty = MotorSpecD.WarrantyCol.FirstOrDefault(x => x.MotorSpecDtlId == PLWM.Utils.CnvToNullableInt(dtSelectedData.Rows[0]["Warranty_id"])),
                        Transmisson = MotorSpecD.TransmissonCol.FirstOrDefault(x => x.MotorSpecDtlId == PLWM.Utils.CnvToNullableInt(dtSelectedData.Rows[0]["Transmisson_id"])),

                        KiloMetrs = PLWM.Utils.CnvToStr(dtSelectedData.Rows[0]["Kmters"]),
                        Title = PLWM.Utils.CnvToStr(dtSelectedData.Rows[0]["mot_Title"]),
                        Description = PLWM.Utils.CnvToStr(dtSelectedData.Rows[0]["mot_Description"]),
                        MotorsADMastID = PLWM.Utils.CnvToNullableInt(dtSelectedData.Rows[0]["motors_ad_mast_id"]),
                        
                    };
                }
            }
            catch (Exception e)
            {
                MotorSpecD.UserData.ZaBase.ErrorMsg = PLWM.Utils.CnvToSentenceCase(e.Message.ToLower().Replace("plerror", "").Replace("plerror", "").Trim());
            }

            return MotorSpecD;
        }

        public ZA3600LD DoSave(ZA3600SD SaveData, String Mode)
        {
            ZA3600LD MotorSpecD = new ZA3600LD();

            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                new XElement("as_sessionid", SaveData.UserData.ZaBase.SessionId),
                new XElement("as_Year", SaveData.Year.MotorSpecDtlId),
                new XElement("as_Colour", SaveData.Colour.MotorSpecDtlId),
                new XElement("as_Doors", SaveData.Doors.MotorSpecDtlId),
                new XElement("as_Warranty", SaveData.Warranty.MotorSpecDtlId),
                new XElement("as_RegionalSpecs", SaveData.RegionalSpecs.MotorSpecDtlId),
                new XElement("as_Transmisson", SaveData.Transmisson.MotorSpecDtlId),
                new XElement("as_BodyType", SaveData.BodyType.MotorSpecDtlId),
                new XElement("as_FuelType", SaveData.FuelType.MotorSpecDtlId),
                new XElement("as_Cylinders", SaveData.Cylinders.MotorSpecDtlId),
                new XElement("as_SellerType", SaveData.SellerType.MotorSpecDtlId),
                new XElement("as_Brand", SaveData.Brand.MotorSpecDtlId),
                new XElement("as_HoursePower", SaveData.HoursePower.MotorSpecDtlId),
                new XElement("as_Title", SaveData.Title),
                new XElement("as_KiloMetrs", SaveData.KiloMetrs),
                new XElement("as_Description", SaveData.Description),
                new XElement("as_MotorsADMastID", SaveData.MotorsADMastID),
                new XElement("as_ConditionID", SaveData.Condition.MotorSpecDtlId) ));


                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                DataSet ds = dbObj.SelectSP("ZA3600_IU", XString, PLABSM.DbProvider.MSSql);

                System.Data.DataTable MotorData  = PLWM.Utils.GetDataTable(ds, 0);
                System.Data.DataTable MotorFileData = PLWM.Utils.GetDataTable(ds, 1);

                if (MotorData.Rows.Count > 0)
                {
                    DataRow Dr = MotorData.Rows[0] ;
                    MotorSpecD.MotorsADMastID = PLWM.Utils.CnvToNullableInt(Dr["MotorsADMastID"]);
                    MotorSpecD.UserData.ZaBase.SessionId = PLWM.Utils.CnvToStr(Dr["sessionid"]);
                    MotorSpecD.UserData.ZaBase.ErrorMsg = "";
                }
            }
            catch (Exception e)
            {
                MotorSpecD.UserData.ZaBase.ErrorMsg = PLWM.Utils.CnvToSentenceCase(e.Message.ToLower().Replace("plerror", "").Replace("plerror", "").Trim());
            }

            return MotorSpecD;
        }

    }
}
