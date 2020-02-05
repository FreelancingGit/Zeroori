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
    public class ZA3011M
    {
        public ZA3011D DoSave(ZA3011D FilterData, String Mode)
        {
            ZA3011D SaveDataV = new ZA3011D();

            if(FilterData.DealMastId>0)
            {
                Mode = "U";
            }
            try
            {
              
                XDocument doc = new XDocument(new XElement("Root",
                    new XElement("as_mode", Mode),
                    new XElement("as_sessionid", FilterData.UserData.ZaBase.SessionId),
                    new XElement("ai_packdeal_mast_id", FilterData.PackDealMastId),
                    new XElement("ai_deal_mast_id", FilterData.DealMastId),
                    new XElement("as_deal_name", FilterData.DealName),
                    new XElement("an_price", FilterData.Price),
                    new XElement("as_descrptn", FilterData.Descrptn),
                    new XElement("ad_start_dt", FilterData.StartDt),
                    new XElement("ad_end_dt", FilterData.EndDt)
                ));


                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                DataSet ds = dbObj.SelectSP("ZA3011_IU", XString, PLABSM.DbProvider.MSSql);
                

                System.Data.DataTable dtComn = PLWM.Utils.GetDataTable(ds, 0);
                if(dtComn.Rows.Count>0)
                {
                    SaveDataV.DealName = PLWM.Utils.CnvToStr(dtComn.Rows[0]["deal_name"]);
                    SaveDataV.Price = PLWM.Utils.CnvToStr(dtComn.Rows[0]["price"]);
                    SaveDataV.Descrptn = PLWM.Utils.CnvToStr(dtComn.Rows[0]["descrptn"]);
                    SaveDataV.StartDt = PLWM.Utils.CnvToStr(dtComn.Rows[0]["start_dt"]);
                    SaveDataV.EndDt = PLWM.Utils.CnvToStr(dtComn.Rows[0]["end_dt"]);
                }
                

            }
            catch (Exception e)
            {
                SaveDataV.UserData.ZaBase.ErrorMsg = PLWM.Utils.CnvToSentenceCase(e.Message.ToLower().Replace("plerror", "").Replace("plerror", "").Trim());
            }

            return SaveDataV;
        }

        public ZA3011LD DoLoadM(ZA3011D FilterData, String Mode)
        {
            ZA3011LD SaveDataV = new ZA3011LD();

            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                    //new XElement("as_mode", Mode),
                    new XElement("as_sessionid", FilterData.UserData.ZaBase.SessionId),
                    new XElement("ai_pack_deal_mast_id", FilterData.PackDealMastId),
                    new XElement("ai_deal_mast_id", FilterData.DealMastId),
                    new XElement("ai_pageno", ""),
                    new XElement("as_mode", Mode)
                ));


                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                DataSet ds = dbObj.SelectSP("ZA3011_SEL", XString, PLABSM.DbProvider.MSSql);

                System.Data.DataTable DealcolDt = PLWM.Utils.GetDataTable(ds, 0);
                System.Data.DataTable UserDt = PLWM.Utils.GetDataTable(ds, 1);

                if (UserDt.Rows.Count > 0)
                {
                    SaveDataV.UserData = new ZA3000D()
                    {
                        UsrMastID = PLWM.Utils.CnvToInt(UserDt.Rows[0]["usr_mast_id"]),
                        FistNam = PLWM.Utils.CnvToStr(UserDt.Rows[0]["usr_FistNam"]),
                        ZaBase = new BaseD()
                        {
                            SessionId = PLWM.Utils.CnvToStr(UserDt.Rows[0]["SESSIONID"]),
                        },
                    };
                }

                foreach (DataRow dr in DealcolDt.Rows)
                {
                    SaveDataV.Dealcol.Add(new ZA3011D()
                    {
                        DealMastId = PLWM.Utils.CnvToInt(dr["deal_mast_id"]),
                        PackDealMastId = PLWM.Utils.CnvToInt(dr["pack_deal_mast_id"]),
                        DealName = PLWM.Utils.CnvToStr(dr["deal_name"]),
                       
                    });

                }
                
            }
            catch (Exception e)
            {
                SaveDataV.UserData.ZaBase.ErrorMsg = PLWM.Utils.CnvToSentenceCase(e.Message.ToLower().Replace("plerror", "").Replace("plerror", "").Trim());
            }
            return SaveDataV;

        }

        public ZA3011LD DoLoadBrand(ZA3011LD FilterData, String Mode)
        {
            ZA3011LD brandDet = new ZA3011LD();

            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                    new XElement("as_sessionid", FilterData.UserData.ZaBase.SessionId),
                    new XElement("ai_pack_deal_mast_id", FilterData.PackDealMast.PackDealMastID),
                    new XElement("ai_deal_mast_id", FilterData.PackDealMast.DealMastID),
                    //@ai_pageno
                    new XElement("ai_pageno", ""),
                    new XElement("as_mode", Mode)
                ));


                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                DataSet ds = dbObj.SelectSP("ZA3011_SEL", XString, PLABSM.DbProvider.MSSql);

                System.Data.DataTable PackDealcolDt = PLWM.Utils.GetDataTable(ds, 0);
                System.Data.DataTable DealcolDt = PLWM.Utils.GetDataTable(ds, 1);
                System.Data.DataTable UserDt = PLWM.Utils.GetDataTable(ds, 2);

                foreach (DataRow dr in PackDealcolDt.Rows)
                {
                    brandDet.PackDealMast = new ZA3010D()
                    {
                        PackDealMastID = PLWM.Utils.CnvToInt(dr["pack_deal_mast_id"]),
                        PlanMastId = PLWM.Utils.CnvToInt(dr["plan_mast_id"]),
                        PackageMastId = PLWM.Utils.CnvToInt(dr["package_mast_id"]),
                        PhoneNo = PLWM.Utils.CnvToStr(dr["Phone_No"]),
                        Email = PLWM.Utils.CnvToStr(dr["Email"]),
                        Website = PLWM.Utils.CnvToStr(dr["Website"]),
                        Location = PLWM.Utils.CnvToStr(dr["geo_Location"]),
                        Description = PLWM.Utils.CnvToStr(dr["descrptn"]),
                        BannerImage= PLWM.Utils.CnvToStr(dr["banner_img_url"]),
                        CompanyLogo= PLWM.Utils.CnvToStr(dr["logo_img_url"]),
                    };

                }

                foreach (DataRow dr in DealcolDt.Rows)
                {
                    brandDet.Dealcol.Add(new ZA3011D()
                    {
                        DealMastId= PLWM.Utils.CnvToInt(dr["deal_mast_id"]),
                        PackDealMastId = PLWM.Utils.CnvToInt(dr["pack_deal_mast_id"]),
                        DealName = PLWM.Utils.CnvToStr(dr["deal_name"]),
                        Price = PLWM.Utils.CnvToStr(dr["price"]),
                        Descrptn= PLWM.Utils.CnvToStr(dr["descrptn"]),
                        Location= PLWM.Utils.CnvToStr(dr["geo_Location"]),
                    });

                }

                

                if (UserDt.Rows.Count > 0)
                {
                    brandDet.UserData = new ZA3000D()
                    {
                        UsrMastID = PLWM.Utils.CnvToInt(UserDt.Rows[0]["usr_mast_id"]),
                        FistNam = PLWM.Utils.CnvToStr(UserDt.Rows[0]["usr_FistNam"]),
                        ZaBase = new BaseD()
                        {
                            SessionId = PLWM.Utils.CnvToStr(UserDt.Rows[0]["SESSIONID"]),
                        },
                    };
                }
                
            }
            catch (Exception e)
            {
                brandDet.UserData.ZaBase.ErrorMsg = PLWM.Utils.CnvToSentenceCase(e.Message.ToLower().Replace("plerror", "").Replace("plerror", "").Trim());
            }
            return brandDet;

        }

        public ZA3011LD DoLoadA(ZA3011D FilterData, String Mode)
        {
            ZA3011LD SaveDataV = new ZA3011LD();

            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                    //new XElement("as_mode", Mode),
                    new XElement("as_sessionid", FilterData.UserData.ZaBase.SessionId),
                    new XElement("ai_pack_deal_mast_id", FilterData.PackDealMastId),
                    new XElement("ai_deal_mast_id", FilterData.DealMastId),
                    new XElement("as_mode", Mode)
                ));


                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                DataSet ds = dbObj.SelectSP("ZA3011_SEL", XString, PLABSM.DbProvider.MSSql);

                System.Data.DataTable DealcolDt = PLWM.Utils.GetDataTable(ds, 0);
                System.Data.DataTable UserDt = PLWM.Utils.GetDataTable(ds, 1);

                if (UserDt.Rows.Count > 0)
                {
                    SaveDataV.UserData = new ZA3000D()
                    {
                        //UsrMastID = PLWM.Utils.CnvToInt(UserDt.Rows[0]["usr_mast_id"]),
                        FistNam = PLWM.Utils.CnvToStr(UserDt.Rows[0]["usr_FistNam"]),
                        ZaBase = new BaseD()
                        {
                            SessionId = PLWM.Utils.CnvToStr(UserDt.Rows[0]["SESSIONID"]),
                        },
                    };
                }

                foreach (DataRow dr in DealcolDt.Rows)
                {
                    SaveDataV.Dealcol.Add(new ZA3011D()
                    {
                        DealMastId = PLWM.Utils.CnvToInt(dr["deal_mast_id"]),
                        PackDealMastId = PLWM.Utils.CnvToInt(dr["pack_deal_mast_id"]),
                        DealName = PLWM.Utils.CnvToStr(dr["deal_name"]),
                        Price = PLWM.Utils.CnvToStr(dr["price"]),
                        StartDt = PLWM.Utils.CnvToStr(dr["start_dt"]),
                        EndDt = PLWM.Utils.CnvToStr(dr["end_dt"]),
                        Descrptn = PLWM.Utils.CnvToStr(dr["descrptn"]),
                    });

                }
                
            }
            catch (Exception e)
            {
                SaveDataV.UserData.ZaBase.ErrorMsg = PLWM.Utils.CnvToSentenceCase(e.Message.ToLower().Replace("plerror", "").Replace("plerror", "").Trim());
            }
            return SaveDataV;

        }

        //DoLoadDealDetail
        public ZA3011LDD DoLoadDealDetail(ZA3011D FilterData, String Mode)
        {
            ZA3011LDD DealDet = new ZA3011LDD();

            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                    //new XElement("as_mode", Mode),
                    new XElement("as_sessionid", FilterData.UserData.ZaBase.SessionId),
                    new XElement("ai_pack_deal_mast_id", FilterData.PackDealMastId),
                    new XElement("ai_deal_mast_id", FilterData.DealMastId),
                    new XElement("ai_pageno", ""),
                    new XElement("as_mode", Mode)
                ));


                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                DataSet ds = dbObj.SelectSP("ZA3011_SEL", XString, PLABSM.DbProvider.MSSql);

                System.Data.DataTable DealcolDt = PLWM.Utils.GetDataTable(ds, 0);
                System.Data.DataTable UserDt = PLWM.Utils.GetDataTable(ds, 1);

                if (UserDt.Rows.Count > 0)
                {
                    DealDet.UserData = new ZA3000D()
                    {
                        //UsrMastID = PLWM.Utils.CnvToInt(UserDt.Rows[0]["usr_mast_id"]),
                        FistNam = PLWM.Utils.CnvToStr(UserDt.Rows[0]["usr_FistNam"]),
                        ZaBase = new BaseD()
                        {
                            SessionId = PLWM.Utils.CnvToStr(UserDt.Rows[0]["SESSIONID"]),
                        },
                    };
                }

                foreach (DataRow dr in DealcolDt.Rows)
                {
                    DealDet.DealMast=new ZA3011D()
                    {
                        DealMastId = PLWM.Utils.CnvToInt(dr["deal_mast_id"]),
                        PackDealMastId = PLWM.Utils.CnvToInt(dr["pack_deal_mast_id"]),
                        DealName = PLWM.Utils.CnvToStr(dr["deal_name"]),
                        Price = PLWM.Utils.CnvToStr(dr["price"]),
                        Descrptn = PLWM.Utils.CnvToStr(dr["descrptn"]),
                        StartDt= PLWM.Utils.CnvToStr(dr["start_dt"]),
                        EndDt= PLWM.Utils.CnvToStr(dr["end_dt"]),
                    };

                }

                foreach (DataRow dr in DealcolDt.Rows)
                {
                    DealDet.DealM = new ZA3010D
                    {
                        BusinessName = PLWM.Utils.CnvToStr(dr["busines_Name"]),
                        Location = PLWM.Utils.CnvToStr(dr["geo_Location"]),
                    };

                }

            }
            catch (Exception e)
            {
                DealDet.UserData.ZaBase.ErrorMsg = PLWM.Utils.CnvToSentenceCase(e.Message.ToLower().Replace("plerror", "").Replace("plerror", "").Trim());
            }
            return DealDet;

        }
        public ZA3011LD DoLoadDealList(ZA3011D FilterData, String Mode)
        {
            ZA3011LD SaveDataV = new ZA3011LD();

            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                    //new XElement("as_mode", Mode),
                    new XElement("as_sessionid", FilterData.UserData.ZaBase.SessionId),
                    new XElement("ai_pack_deal_mast_id", FilterData.PackDealMastId),
                    new XElement("ai_deal_mast_id", FilterData.DealMastId),
                    new XElement("ai_pageno", FilterData.PageNo),
                    new XElement("as_mode", Mode)
                ));


                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                DataSet ds = dbObj.SelectSP("ZA3011_SEL", XString, PLABSM.DbProvider.MSSql);

                System.Data.DataTable DealcolDt = PLWM.Utils.GetDataTable(ds, 0);
                System.Data.DataTable UserDt = PLWM.Utils.GetDataTable(ds, 1);
                System.Data.DataTable PageNoDt = PLWM.Utils.GetDataTable(ds, 2);

                if (UserDt.Rows.Count > 0)
                {
                    SaveDataV.UserData = new ZA3000D()
                    {
                        //UsrMastID = PLWM.Utils.CnvToInt(UserDt.Rows[0]["usr_mast_id"]),
                        FistNam = PLWM.Utils.CnvToStr(UserDt.Rows[0]["usr_FistNam"]),
                        ZaBase = new BaseD()
                        {
                            SessionId = PLWM.Utils.CnvToStr(UserDt.Rows[0]["SESSIONID"]),
                        },
                    };
                }

                foreach (DataRow dr in DealcolDt.Rows)
                {
                    SaveDataV.Dealcol.Add(new ZA3011D()
                    {
                        DealMastId = PLWM.Utils.CnvToInt(dr["deal_mast_id"]),
                        PackDealMastId = PLWM.Utils.CnvToInt(dr["pack_deal_mast_id"]),
                        DealName = PLWM.Utils.CnvToStr(dr["deal_name"]),
                        Price = PLWM.Utils.CnvToStr(dr["price"]),
                        Descrptn = PLWM.Utils.CnvToStr(dr["descrptn"]),
                        Location= PLWM.Utils.CnvToStr(dr["geo_Location"]),
                        BannerImg= PLWM.Utils.CnvToStr(dr["banner_img_url"]),
                    });

                }

                SaveDataV.PageNoCol = new ComDisValDCol();
                foreach (DataRow Dr in PageNoDt.Rows)
                {
                    SaveDataV.PageNoCol.Add(new ComDisValD()
                    {
                        ValMembr = PLWM.Utils.CnvToInt(Dr["TotalPages"]),
                        DisPlyMembr = PLWM.Utils.CnvToStr(Dr["Page_No"]),
                    });
                }
            }
            catch (Exception e)
            {
                SaveDataV.UserData.ZaBase.ErrorMsg = PLWM.Utils.CnvToSentenceCase(e.Message.ToLower().Replace("plerror", "").Replace("plerror", "").Trim());
            }
            return SaveDataV;

        }

        public ZA3011LD DoLoadDeal(ZA3011D FilterData, String Mode)
        {
            ZA3011LD SaveDataV = new ZA3011LD();

            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                    new XElement("as_mode", Mode),
                    new XElement("as_sessionid", FilterData.UserData.ZaBase.SessionId),
                    new XElement("ai_deal_mast_id", FilterData.DealMastId),
                    new XElement("ai_pack_deal_mast_id", ""),
                    new XElement("ai_pageno", FilterData.PageNo)

                ));


                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                DataSet ds = dbObj.SelectSP("ZA3011_SEL", XString, PLABSM.DbProvider.MSSql);

                System.Data.DataTable DealcolDt = PLWM.Utils.GetDataTable(ds, 0);
               

                foreach (DataRow dr in DealcolDt.Rows)
                {
                    SaveDataV.Dealcol.Add(new ZA3011D()
                    {
                        DealMastId = PLWM.Utils.CnvToInt(dr["deal_mast_id"]),
                        PackDealMastId = PLWM.Utils.CnvToInt(dr["pack_deal_mast_id"]),
                        DealName = PLWM.Utils.CnvToStr(dr["deal_name"]),
                        Descrptn = PLWM.Utils.CnvToStr(dr["descrptn"]),
                        Price = PLWM.Utils.CnvToStr(dr["price"]),
                        StartDt = PLWM.Utils.CnvToStr(dr["start_dt"]),
                        EndDt = PLWM.Utils.CnvToStr(dr["end_dt"]),
                    });

                }

                //System.Data.DataTable dtComn = PLWM.Utils.GetDataTable(ds, 0);
                //System.Data.DataTable dtUser = PLWM.Utils.GetDataTable(ds, 1);

                //if (dtComn.Rows.Count > 0)
                //{
                //    SaveDataV.DealName = PLWM.Utils.CnvToStr(dtComn.Rows[0]["deal_name"]);
                //    SaveDataV.Price = PLWM.Utils.CnvToStr(dtComn.Rows[0]["price"]);
                //    SaveDataV.Descrptn = PLWM.Utils.CnvToStr(dtComn.Rows[0]["descrptn"]);
                //    SaveDataV.StartDt = PLWM.Utils.CnvToStr(dtComn.Rows[0]["start_dt"]);
                //    SaveDataV.EndDt = PLWM.Utils.CnvToStr(dtComn.Rows[0]["end_dt"]);
                //}
                //if (dtUser.Rows.Count > 0)
                //{
                //    SaveDataV.UserData = new ZA3000D()
                //    {
                //        FistNam = PLWM.Utils.CnvToStr(dtUser.Rows[0]["usr_FistNam"]),
                //        ZaBase = new BaseD()
                //        {
                //            SessionId = PLWM.Utils.CnvToStr(dtUser.Rows[0]["sessionid"]),
                //        }
                //    };

                //}


            }
            catch (Exception e)
            {
                SaveDataV.UserData.ZaBase.ErrorMsg = PLWM.Utils.CnvToSentenceCase(e.Message.ToLower().Replace("plerror", "").Replace("plerror", "").Trim());
            }
            return SaveDataV;

        }

        public ZA3011LD DoDelete(ZA3011D FilterData, String Mode)
        {
            ZA3011LD SaveDataV = new ZA3011LD();

            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                    new XElement("as_sessionid", FilterData.UserData.ZaBase.SessionId),
                    new XElement("ai_deal_mast_id", FilterData.DealMastId)
                ));
                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                DataSet ds = dbObj.SelectSP("ZA3011_DEL", XString, PLABSM.DbProvider.MSSql);
            }
            catch (Exception e)
            {
                SaveDataV.UserData.ZaBase.ErrorMsg = PLWM.Utils.CnvToSentenceCase(e.Message.ToLower().Replace("plerror", "").Replace("plerror", "").Trim());
            }
            return SaveDataV;

        }
    }
}
