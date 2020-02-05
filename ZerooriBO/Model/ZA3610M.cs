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
    public class ZA3610M
    {
        public ZA3610LD DoLoad(ZA3000D FilterData, String Mode)
        {
            ZA3610LD PropSpecD = new ZA3610LD();

            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                new XElement("as_sessionid", FilterData.ZaBase.SessionId),
                new XElement("as_mode", Mode),
                new XElement("as_email", FilterData.Email),
                new XElement("as_passwd", FilterData.Passwd),
                new XElement("ai_prop_ad_mast_id", FilterData.ZaBase.PKID)
                ));


                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                DataSet ds = dbObj.SelectSP("ZA3610_sel", XString, PLABSM.DbProvider.MSSql);

                System.Data.DataTable BedroomDt = PLWM.Utils.GetDataTable(ds, 0);
                System.Data.DataTable BathRoomDt = PLWM.Utils.GetDataTable(ds, 1);
                System.Data.DataTable SizeDt = PLWM.Utils.GetDataTable(ds, 2);
                System.Data.DataTable PropSpecDt = PLWM.Utils.GetDataTable(ds, 3);
                System.Data.DataTable ApartmentForDT = PLWM.Utils.GetDataTable(ds, 4);
                System.Data.DataTable RentIsPaidDT = PLWM.Utils.GetDataTable(ds, 5);
                System.Data.DataTable ListedByDT = PLWM.Utils.GetDataTable(ds, 6);
                System.Data.DataTable CategoryDT = PLWM.Utils.GetDataTable(ds, 7);
 
                System.Data.DataTable dtUser = PLWM.Utils.GetDataTable(ds, 8);
                System.Data.DataTable dtSel = PLWM.Utils.GetDataTable(ds, 9);

                DataRow drUser = null;
                if (dtUser.Rows.Count > 0)
                {
                    drUser = dtUser.Rows[0];
                }

                PropSpecD.BedroomCol = new ZA3210DCol();
                PropSpecD.BedroomCol.Add(new ZA3210D() { PropSpecDtlId = -1, PropSpecValue = "Bed Room" });
                foreach (DataRow dr in BedroomDt.Rows)
                {
                    PropSpecD.BedroomCol.Add(new ZA3210D()
                    {
                        PropSpecDtlId = PLWM.Utils.CnvToNullableInt(dr["Prop_dtl_id"]),
                        PropSpecValue = PLWM.Utils.CnvToStr(dr["Prop_value"]),
                    });
                }



                PropSpecD.BathRoomCol = new ZA3210DCol();
                PropSpecD.BathRoomCol.Add(new ZA3210D() { PropSpecDtlId = -1, PropSpecValue = "Bath Room" });
                foreach (DataRow dr in BathRoomDt.Rows)
                {
                    PropSpecD.BathRoomCol.Add(new ZA3210D()
                    {
                        PropSpecDtlId = PLWM.Utils.CnvToNullableInt(dr["Prop_dtl_id"]),
                        PropSpecValue = PLWM.Utils.CnvToStr(dr["Prop_value"]),
                    });
                }

                PropSpecD.SizeCol = new ZA3210DCol();
                PropSpecD.SizeCol.Add(new ZA3210D() { PropSpecDtlId = -1, PropSpecValue = "Size ( Sq.Mtr)" });
                foreach (DataRow dr in SizeDt.Rows)
                {
                    PropSpecD.SizeCol.Add(new ZA3210D()
                    {
                        PropSpecDtlId = PLWM.Utils.CnvToNullableInt(dr["Prop_dtl_id"]),
                        PropSpecValue = PLWM.Utils.CnvToStr(dr["Prop_value"]),
                    });
                }


                PropSpecD.FurnishedCol = new ZA3210DCol();
                PropSpecD.FurnishedCol.Add(new ZA3210D() { PropSpecDtlId = -1, PropSpecValue = "Furnished" });
                foreach (DataRow dr in PropSpecDt.Rows)
                {
                    PropSpecD.FurnishedCol.Add(new ZA3210D()
                    {
                        PropSpecDtlId = PLWM.Utils.CnvToNullableInt(dr["Prop_dtl_id"]),
                        PropSpecValue = PLWM.Utils.CnvToStr(dr["Prop_value"]),
                    });
                }

                PropSpecD.ApartmentForCol = new ZA3210DCol();
                PropSpecD.ApartmentForCol.Add(new ZA3210D() { PropSpecDtlId = -1, PropSpecValue = "Apartment For" });
                foreach (DataRow dr in ApartmentForDT.Rows)
                {
                    PropSpecD.ApartmentForCol.Add(new ZA3210D()
                    {
                        PropSpecDtlId = PLWM.Utils.CnvToNullableInt(dr["Prop_dtl_id"]),
                        PropSpecValue = PLWM.Utils.CnvToStr(dr["Prop_value"]),
                    });
                }


                PropSpecD.RentIsPaidCol = new ZA3210DCol();
                PropSpecD.RentIsPaidCol.Add(new ZA3210D() { PropSpecDtlId = -1, PropSpecValue = "Rent Is Paid" });
                foreach (DataRow dr in RentIsPaidDT.Rows)
                {
                    PropSpecD.RentIsPaidCol.Add(new ZA3210D()
                    {
                        PropSpecDtlId = PLWM.Utils.CnvToNullableInt(dr["Prop_dtl_id"]),
                        PropSpecValue = PLWM.Utils.CnvToStr(dr["Prop_value"]),
                    });
                }


                PropSpecD.ListedByCol = new ZA3210DCol();
                PropSpecD.ListedByCol.Add(new ZA3210D() { PropSpecDtlId = -1, PropSpecValue = "Listed By" });
                foreach (DataRow dr in ListedByDT.Rows)
                {
                    PropSpecD.ListedByCol.Add(new ZA3210D()
                    {
                        PropSpecDtlId = PLWM.Utils.CnvToNullableInt(dr["Prop_dtl_id"]),
                        PropSpecValue = PLWM.Utils.CnvToStr(dr["Prop_value"]),
                    });
                }

                PropSpecD.CategoryCol = new ZA3210DCol();
                PropSpecD.CategoryCol.Add(new ZA3210D() { PropSpecDtlId = -1, PropSpecValue = "Category" });
                foreach (DataRow dr in CategoryDT.Rows)
                {
                    PropSpecD.CategoryCol.Add(new ZA3210D()
                    {
                        PropSpecDtlId = PLWM.Utils.CnvToNullableInt(dr["Prop_dtl_id"]),
                        PropSpecValue = PLWM.Utils.CnvToStr(dr["Prop_value"]),
                    });
                }
 
                PropSpecD.UserData = new ZA3000D()
                {
                    ZaBase = new BaseD()
                    {
                        SessionId = PLWM.Utils.CnvToStr(drUser["SessionId"]),
                        UserName = PLWM.Utils.CnvToStr(drUser["FirstName"]),
                        ErrorMsg = "",
                        ZaKey = Utils.GetKey()
                    }
                };

                if (dtSel.Rows.Count > 0)
                {
                    PropSpecD.SelectedData = new ZA3610SD()
                    {
                        BathRoom = PropSpecD.BathRoomCol.FirstOrDefault(x => x.PropSpecDtlId == PLWM.Utils.CnvToNullableInt(dtSel.Rows[0]["bath_room_id"])),

                        Bedroom = PropSpecD.BedroomCol.FirstOrDefault(x => x.PropSpecDtlId == PLWM.Utils.CnvToNullableInt(dtSel.Rows[0]["bed_room_id"])),

                        Size = PropSpecD.SizeCol.FirstOrDefault(x => x.PropSpecDtlId == PLWM.Utils.CnvToNullableInt(dtSel.Rows[0]["size_id"])),

                        Furnished = PropSpecD.FurnishedCol.FirstOrDefault(x => x.PropSpecDtlId == PLWM.Utils.CnvToNullableInt(dtSel.Rows[0]["is_Furnished_id"])),

                        ApartmentFor = PropSpecD.ApartmentForCol.FirstOrDefault(x => x.PropSpecDtlId == PLWM.Utils.CnvToNullableInt(dtSel.Rows[0]["apartment_for_id"])),

                        RentIsPaid = PropSpecD.RentIsPaidCol.FirstOrDefault(x => x.PropSpecDtlId == PLWM.Utils.CnvToNullableInt(dtSel.Rows[0]["Rent_Is_Paid_id"])),

                        ListedBy = PropSpecD.ListedByCol.FirstOrDefault(x => x.PropSpecDtlId == PLWM.Utils.CnvToNullableInt(dtSel.Rows[0]["listed_by_id"])),

                        Category = PropSpecD.CategoryCol.FirstOrDefault(x => x.PropSpecDtlId == PLWM.Utils.CnvToNullableInt(dtSel.Rows[0]["Category_id"])),

                        Description = PLWM.Utils.CnvToStr(dtSel.Rows[0]["prop_Description"]),

                        Title = PLWM.Utils.CnvToStr(dtSel.Rows[0]["prop_title"]),

                        PropADMastID = PLWM.Utils.CnvToNullableInt(dtSel.Rows[0]["prop_ad_mast_id"]),

                    };
                }

            }
            catch (Exception e)
            {
                PropSpecD.UserData.ZaBase.ErrorMsg = PLWM.Utils.CnvToSentenceCase(e.Message.ToLower().Replace("plerror", "").Replace("plerror", "").Trim());
            }

            return PropSpecD;
        }




        public ZA3610LD DoSave(ZA3610SD SaveData, String Mode)
        {
            ZA3610LD PropSpecD = new ZA3610LD();

            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                new XElement("as_sessionid", SaveData.UserData.ZaBase.SessionId),
                new XElement("as_bed_room", SaveData.Bedroom.PropSpecDtlId),
                new XElement("as_bath_room", SaveData.BathRoom.PropSpecDtlId),
                new XElement("as_size", SaveData.Size.PropSpecDtlId),
                new XElement("as_is_Furnished", SaveData.Furnished.PropSpecDtlId),
                new XElement("as_apartment_for", SaveData.ApartmentFor.PropSpecDtlId),
                new XElement("as_Rent_Is_Paid", SaveData.RentIsPaid.PropSpecDtlId),
                new XElement("as_listed_by", SaveData.ListedBy.PropSpecDtlId),
                new XElement("as_Category", SaveData.Category.PropSpecDtlId),
                new XElement("as_title", SaveData.Title),
                new XElement("as_Description", SaveData.Description),
                new XElement("as_PropADMastID", SaveData.PropADMastID )
                
                ));


                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                DataSet ds = dbObj.SelectSP("ZA3610_IU", XString, PLABSM.DbProvider.MSSql);

                System.Data.DataTable MotorData  = PLWM.Utils.GetDataTable(ds, 0);
                System.Data.DataTable MotorFileData = PLWM.Utils.GetDataTable(ds, 1);

                if (MotorData.Rows.Count > 0)
                {
                    DataRow Dr = MotorData.Rows[0] ;
                    PropSpecD.PropADMastID = PLWM.Utils.CnvToNullableInt(Dr["PropADMastID"]);
                    PropSpecD.UserData.ZaBase.SessionId = PLWM.Utils.CnvToStr(Dr["sessionid"]);
                    PropSpecD.UserData.ZaBase.ErrorMsg = "";
                }
            }
            catch (Exception e)
            {
                PropSpecD.UserData.ZaBase.ErrorMsg = PLWM.Utils.CnvToSentenceCase(e.Message.ToLower().Replace("plerror", "").Replace("plerror", "").Trim());
            }

            return PropSpecD;
        }
    }
}
