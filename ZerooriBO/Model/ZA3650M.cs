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
    public class ZA3650M
    {
        public ZA3650LD DoLoad(ZA3650SD FilterData, String Mode)
        {
            ZA3650LD UsageD = new ZA3650LD();

            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                new XElement("as_sessionid", FilterData.UserData.ZaBase.SessionId),
                new XElement("ai_compny_job_mast_id", FilterData.UserData.ZaBase.PKID),
                new XElement("as_mode", Mode)
                ));


                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                DataSet ds = dbObj.SelectSP("ZA3650_sel", XString, PLABSM.DbProvider.MSSql);

                System.Data.DataTable IndstryTypDt = PLWM.Utils.GetDataTable(ds, 0);
                System.Data.DataTable CompnySizeDt = PLWM.Utils.GetDataTable(ds, 1);
                System.Data.DataTable EmplymntTypeDt = PLWM.Utils.GetDataTable(ds, 2);
                System.Data.DataTable MonthlySlryDt = PLWM.Utils.GetDataTable(ds, 3);
                System.Data.DataTable ExprnceDt = PLWM.Utils.GetDataTable(ds, 4);
                System.Data.DataTable EductnLvlDt = PLWM.Utils.GetDataTable(ds, 5);
                System.Data.DataTable ListedByDt = PLWM.Utils.GetDataTable(ds, 6);
                System.Data.DataTable CareerLvlDt = PLWM.Utils.GetDataTable(ds, 7);
                System.Data.DataTable dtUser = PLWM.Utils.GetDataTable(ds, 8);
                System.Data.DataTable dtSel = PLWM.Utils.GetDataTable(ds, 9);
				System.Data.DataTable dtLoc = PLWM.Utils.GetDataTable(ds, 10);
				var dt_Jobtitles = PLWM.Utils.GetDataTable(ds, 11);




				DataRow drUser = null;
                if (dtUser.Rows.Count > 0)
                {
                    drUser = dtUser.Rows[0];
                }

                UsageD.IndstryCol = new ZA3230DCol();
                UsageD.IndstryCol.Add(new ZA3230D() {  EmpJobDtlId = -1,  EmpJobValue = "Jobs" });
                foreach (DataRow dr in IndstryTypDt.Rows)
                {
                    UsageD.IndstryCol.Add(new ZA3230D()
                    {
                        EmpJobDtlId = PLWM.Utils.CnvToNullableInt(dr["Emp_Job_Dtl_Id"]),
                        EmpJobValue = PLWM.Utils.CnvToStr(dr["Emp_Job_Value"]),
                    });
                }
				UsageD.JobsCol= new ZA3230DCol();
				UsageD.JobsCol.Add(new ZA3230D() { EmpJobDtlId = -1, EmpJobValue = "Jobs Title" });
				foreach (DataRow dr in dt_Jobtitles.Rows)
				{
					UsageD.JobsCol.Add(new ZA3230D()
					{
						EmpJobDtlId = PLWM.Utils.CnvToNullableInt(dr["Id"]),
						EmpJobValue = PLWM.Utils.CnvToStr(dr["Title"]),
					});
				}


				UsageD.CompnySizeCol = new ZA3230DCol();
                UsageD.CompnySizeCol.Add(new ZA3230D() { EmpJobDtlId = -1, EmpJobValue = "Company Size" });
                foreach (DataRow dr in CompnySizeDt.Rows)
                {
                    UsageD.CompnySizeCol.Add(new ZA3230D()
                    {
                        EmpJobDtlId = PLWM.Utils.CnvToNullableInt(dr["Emp_Job_Dtl_Id"]),
                        EmpJobValue = PLWM.Utils.CnvToStr(dr["Emp_Job_Value"]),
                    });
                }

                UsageD.EmploymntTypeCol = new ZA3230DCol();
                UsageD.EmploymntTypeCol.Add(new ZA3230D() { EmpJobDtlId = -1, EmpJobValue = "Commitment" });
                foreach (DataRow dr in EmplymntTypeDt.Rows)
                {
                    UsageD.EmploymntTypeCol.Add(new ZA3230D()
                    {
                        EmpJobDtlId = PLWM.Utils.CnvToNullableInt(dr["Emp_Job_Dtl_Id"]),
                        EmpJobValue = PLWM.Utils.CnvToStr(dr["Emp_Job_Value"]),
                    });
                }


                UsageD.MonthlySalaryCol = new ZA3230DCol();
                UsageD.MonthlySalaryCol.Add(new ZA3230D() { EmpJobDtlId = -1, EmpJobValue = "Current Salary" });
                foreach (DataRow dr in MonthlySlryDt.Rows)
                {
                    UsageD.MonthlySalaryCol.Add(new ZA3230D()
                    {
                        EmpJobDtlId = PLWM.Utils.CnvToNullableInt(dr["Emp_Job_Dtl_Id"]),
                        EmpJobValue = PLWM.Utils.CnvToStr(dr["Emp_Job_Value"]),
                    });
                }

                UsageD.ExprnceCol = new ZA3230DCol();
                UsageD.ExprnceCol.Add(new ZA3230D() { EmpJobDtlId = -1, EmpJobValue = "Work Experience" });
                foreach (DataRow dr in ExprnceDt.Rows)
                {
                    UsageD.ExprnceCol.Add(new ZA3230D()
                    {
                        EmpJobDtlId = PLWM.Utils.CnvToNullableInt(dr["Emp_Job_Dtl_Id"]),
                        EmpJobValue = PLWM.Utils.CnvToStr(dr["Emp_Job_Value"]),
                    });
                }


                UsageD.EductnLevlCol = new ZA3230DCol();
                UsageD.EductnLevlCol.Add(new ZA3230D() { EmpJobDtlId = -1, EmpJobValue = "Education Level" });
                foreach (DataRow dr in EductnLvlDt.Rows)
                {
                    UsageD.EductnLevlCol.Add(new ZA3230D()
                    {
                        EmpJobDtlId = PLWM.Utils.CnvToNullableInt(dr["Emp_Job_Dtl_Id"]),
                        EmpJobValue = PLWM.Utils.CnvToStr(dr["Emp_Job_Value"]),
                    });
                }

                UsageD.ListedBycol = new ZA3230DCol();
                UsageD.ListedBycol.Add(new ZA3230D() { EmpJobDtlId = -1, EmpJobValue = "Listed By" });
                foreach (DataRow dr in ListedByDt.Rows)
                {
                    UsageD.ListedBycol.Add(new ZA3230D()
                    {
                        EmpJobDtlId = PLWM.Utils.CnvToNullableInt(dr["Emp_Job_Dtl_Id"]),
                        EmpJobValue = PLWM.Utils.CnvToStr(dr["Emp_Job_Value"]),
                    });
                }

				UsageD.LocationCol = new ComDisValDCol();
				UsageD.LocationCol.Add(new ComDisValD() { DisPlyMembr = "Location", ValMembr = -1 });
				foreach (DataRow Dr in dtLoc.Rows)
				{
					UsageD.LocationCol.Add(new ComDisValD()
					{
						DisPlyMembr = PLWM.Utils.CnvToStr(Dr["place_name"]),
						ValMembr = PLWM.Utils.CnvToInt(Dr["city_mast_id"]),
					});
				}

				UsageD.CareervLevelCol  = new ZA3230DCol();
                UsageD.CareervLevelCol.Add(new ZA3230D() { EmpJobDtlId = -1, EmpJobValue = "Career Level" });
                foreach (DataRow dr in CareerLvlDt.Rows)
                {
                    UsageD.CareervLevelCol.Add(new ZA3230D()
                    {
                        EmpJobDtlId = PLWM.Utils.CnvToNullableInt(dr["Emp_Job_Dtl_Id"]),
                        EmpJobValue = PLWM.Utils.CnvToStr(dr["Emp_Job_Value"]),
                    });
                }

                if (drUser != null)
                {
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
                }

                foreach (DataRow dr in dtSel.Rows)
                {
                    UsageD.FrelncMast=new ZA3650SD()
                    {
                        CompnyJobMastId = PLWM.Utils.CnvToInt(dr["compny_job_mast_id"]),
                        CompnyName= PLWM.Utils.CnvToStr(dr["compny_name"]),
                        TradeLicns= PLWM.Utils.CnvToStr(dr["trade_licns"]),
                        ContctName= PLWM.Utils.CnvToStr(dr["contct_name"]),
                        Phone= PLWM.Utils.CnvToStr(dr["ph_num"]),
                        CompnyWebsit= PLWM.Utils.CnvToStr(dr["compny_websit"]),
                        Addrs=PLWM.Utils.CnvToStr(dr["addrs"]),
                        DescrpnStepOne= PLWM.Utils.CnvToStr(dr["descrpn_step_one"]),
                        Indstry = UsageD.IndstryCol.FirstOrDefault(x => x.EmpJobDtlId ==
                                      PLWM.Utils.CnvToInt(dtSel.Rows[0]["indstry"])),
                        CompnySize = UsageD.CompnySizeCol.FirstOrDefault(x => x.EmpJobDtlId ==
                                      PLWM.Utils.CnvToInt(dtSel.Rows[0]["compny_size"])),

                        //StepTwo
                        JobTitle= PLWM.Utils.CnvToStr(dr["job_title"]),
                        Neighbrhd= PLWM.Utils.CnvToStr(dr["neighbrhd"]),
                        DescrptnStepTwo= PLWM.Utils.CnvToStr(dr["descrptn_step_two"]),

                        EmplymntTyp = UsageD.EmploymntTypeCol.FirstOrDefault(x => x.EmpJobDtlId ==
                                      PLWM.Utils.CnvToInt(dtSel.Rows[0]["emplymnt_typ"])),

                        MonthlySalary = UsageD.MonthlySalaryCol.FirstOrDefault(x => x.EmpJobDtlId ==
                                      PLWM.Utils.CnvToInt(dtSel.Rows[0]["monthly_salary"])),

                        Exprnce = UsageD.ExprnceCol.FirstOrDefault(x => x.EmpJobDtlId ==
                                      PLWM.Utils.CnvToInt(dtSel.Rows[0]["exprnce"])),

                        EductnLvl = UsageD.EductnLevlCol.FirstOrDefault(x => x.EmpJobDtlId ==
                                      PLWM.Utils.CnvToInt(dtSel.Rows[0]["eductn_lvl"])),

                        ListedBy = UsageD.ListedBycol.FirstOrDefault(x => x.EmpJobDtlId ==
                                      PLWM.Utils.CnvToInt(dtSel.Rows[0]["listed_by"])),

                        CareerLvl = UsageD.CareervLevelCol.FirstOrDefault(x => x.EmpJobDtlId ==
                                      PLWM.Utils.CnvToInt(dtSel.Rows[0]["career_lvl"])),
						Location = UsageD.LocationCol.FirstOrDefault(x => x.DisPlyMembr ==
									  PLWM.Utils.CnvToStr(dtSel.Rows[0]["place_name"])),

					};
                }
            }
            catch (Exception e)
            {
                UsageD.UserData.ZaBase.ErrorMsg = PLWM.Utils.CnvToSentenceCase(e.Message.ToLower().Replace("plerror", "").Replace("plerror", "").Trim());
            }

            return UsageD;
        }

        public ZA3650SD DoSave(ZA3650SD SaveData, String Mode, long FileName)
        {
            ZA3650SD UsageD = new ZA3650SD();

            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                new XElement("as_mode", Mode),
                new XElement("as_sessionid", SaveData.UserData.ZaBase.SessionId),
                new XElement("ai_compny_job_mast_id", SaveData.CompnyJobMastId),
                new XElement("as_company_name", SaveData.CompnyName),
                new XElement("as_trade_lic", SaveData.TradeLicns),
                new XElement("as_contact_name", SaveData.ContctName),
                new XElement("ai_industry", SaveData.Indstry.EmpJobDtlId),
                new XElement("as_phone", SaveData.Phone),
                new XElement("ai_company_size", SaveData.CompnySize.EmpJobDtlId),
                new XElement("as_compny_website", SaveData.CompnyWebsit),
                new XElement("as_adrs", SaveData.Addrs),
                new XElement("as_dscrptn_step_one", SaveData.DescrpnStepOne),

                new XElement("as_job_title", SaveData.JobTitle),
                new XElement("ai_empl_type", SaveData.EmplymntTyp.EmpJobDtlId),
                new XElement("ai_monthly_salry", SaveData.MonthlySalary.EmpJobDtlId),
                new XElement("as_neighbrhd", SaveData.Neighbrhd),
                new XElement("ai_exprnce", SaveData.Exprnce.EmpJobDtlId),
                new XElement("ai_eductn_lvl", SaveData.EductnLvl.EmpJobDtlId),
                new XElement("ai_listed_by", SaveData.ListedBy.EmpJobDtlId),
                 new XElement("ai_career_lvl", SaveData.CareerLvl.EmpJobDtlId),
                new XElement("as_location", SaveData.Location.DisPlyMembr),
                new XElement("as_dscrptn_step_two", SaveData.DescrptnStepTwo), 
                new XElement("as_PhotoName", SaveData.filename)

                ));


                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                DataSet ds = dbObj.SelectSP("ZA3650_IU", XString, PLABSM.DbProvider.MSSql);
                DataTable dt = PLWM.Utils.GetDataTable(ds, 0);
                DataTable dt_id = PLWM.Utils.GetDataTable(ds, 1);
				DataTable dt_file = PLWM.Utils.GetDataTable(ds, 2);

                if (dt.Rows.Count > 0)
                {
                    UsageD.PhotoPath = PLWM.Utils.CnvToStr(dt.Rows[0]["UsrFldrName"]);
					UsageD.filename = PLWM.Utils.CnvToStr(dt.Rows[0]["UsrFilename"]);
					UsageD.imgName = PLWM.Utils.CnvToStr(dt.Rows[0]["img_name"]);

				}
                if (dt_id.Rows.Count > 0)
                {
                    UsageD.CompnyJobMastId = PLWM.Utils.CnvToInt(dt_id.Rows[0]["compny_job_mast_id"]);
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
