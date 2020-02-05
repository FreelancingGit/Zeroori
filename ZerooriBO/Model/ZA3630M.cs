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
    public class ZA3630M
    {
        public ZA3630LD DoLoad(ZA3630SD FilterData, String Mode)
        {
            ZA3630LD UsageD = new ZA3630LD();

            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                new XElement("as_sessionid", FilterData.UserData.ZaBase.SessionId),
                new XElement("as_mode", Mode),
                new XElement("as_email", FilterData.Email),
                new XElement("as_passwd", FilterData.Passwd),
                new XElement("ai_emp_job_mast_id", FilterData.UserData.ZaBase.PKID)
                ));


                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                DataSet ds = dbObj.SelectSP("ZA3630_sel", XString, PLABSM.DbProvider.MSSql);

                System.Data.DataTable NationalityDt = PLWM.Utils.GetDataTable(ds, 0);
                System.Data.DataTable CurrentLocationDt = PLWM.Utils.GetDataTable(ds, 1);
                System.Data.DataTable VisaStatusDt = PLWM.Utils.GetDataTable(ds, 2);
                System.Data.DataTable CarrierLevelDt = PLWM.Utils.GetDataTable(ds, 3);
                System.Data.DataTable CurrentSalaryDT = PLWM.Utils.GetDataTable(ds, 4);
                System.Data.DataTable WorkExperienceDT = PLWM.Utils.GetDataTable(ds, 5);
                System.Data.DataTable EducationLevelDT = PLWM.Utils.GetDataTable(ds, 6);
                System.Data.DataTable CommitmentDT = PLWM.Utils.GetDataTable(ds, 7);
                System.Data.DataTable IndustryDT = PLWM.Utils.GetDataTable(ds, 8);
                System.Data.DataTable dtUser = PLWM.Utils.GetDataTable(ds, 9);
                System.Data.DataTable dtJob = PLWM.Utils.GetDataTable(ds, 10);


                DataRow drUser = null;
                if (dtUser.Rows.Count > 0)
                {
                    drUser = dtUser.Rows[0];
                }

                UsageD.NationalityCol = new ZA3230DCol();
                UsageD.NationalityCol.Add(new ZA3230D() {  EmpJobDtlId = -1,  EmpJobValue = "Nationality" });
                foreach (DataRow dr in NationalityDt.Rows)
                {
                    UsageD.NationalityCol.Add(new ZA3230D()
                    {
                        EmpJobDtlId = PLWM.Utils.CnvToNullableInt(dr["Emp_Job_Dtl_Id"]),
                        EmpJobValue = PLWM.Utils.CnvToStr(dr["Emp_Job_Value"]),
                    });
                }



                UsageD.CurrentLocCol = new ZA3230DCol();
                UsageD.CurrentLocCol.Add(new ZA3230D() { EmpJobDtlId = -1, EmpJobValue = "Current Location" });
                foreach (DataRow dr in CurrentLocationDt.Rows)
                {
                    UsageD.CurrentLocCol.Add(new ZA3230D()
                    {
                        EmpJobDtlId = PLWM.Utils.CnvToNullableInt(dr["Emp_Job_Dtl_Id"]),
                        EmpJobValue = PLWM.Utils.CnvToStr(dr["Emp_Job_Value"]),
                    });
                }

                UsageD.VisaStatusCol = new ZA3230DCol();
                UsageD.VisaStatusCol.Add(new ZA3230D() { EmpJobDtlId = -1, EmpJobValue = "Visa Status" });
                foreach (DataRow dr in VisaStatusDt.Rows)
                {
                    UsageD.VisaStatusCol.Add(new ZA3230D()
                    {
                        EmpJobDtlId = PLWM.Utils.CnvToNullableInt(dr["Emp_Job_Dtl_Id"]),
                        EmpJobValue = PLWM.Utils.CnvToStr(dr["Emp_Job_Value"]),
                    });
                }


                UsageD.CarrierLevelCol = new ZA3230DCol();
                UsageD.CarrierLevelCol.Add(new ZA3230D() { EmpJobDtlId = -1, EmpJobValue = "Carrier Level" });
                foreach (DataRow dr in CarrierLevelDt.Rows)
                {
                    UsageD.CarrierLevelCol.Add(new ZA3230D()
                    {
                        EmpJobDtlId = PLWM.Utils.CnvToNullableInt(dr["Emp_Job_Dtl_Id"]),
                        EmpJobValue = PLWM.Utils.CnvToStr(dr["Emp_Job_Value"]),
                    });
                }

                UsageD.CurrentSalaryCol = new ZA3230DCol();
                UsageD.CurrentSalaryCol.Add(new ZA3230D() { EmpJobDtlId = -1, EmpJobValue = "Current Salary" });
                foreach (DataRow dr in CurrentSalaryDT.Rows)
                {
                    UsageD.CurrentSalaryCol.Add(new ZA3230D()
                    {
                        EmpJobDtlId = PLWM.Utils.CnvToNullableInt(dr["Emp_Job_Dtl_Id"]),
                        EmpJobValue = PLWM.Utils.CnvToStr(dr["Emp_Job_Value"]),
                    });
                }


                UsageD.WorkExperianceCol = new ZA3230DCol();
                UsageD.WorkExperianceCol.Add(new ZA3230D() { EmpJobDtlId = -1, EmpJobValue = "Work Experience" });
                foreach (DataRow dr in WorkExperienceDT.Rows)
                {
                    UsageD.WorkExperianceCol.Add(new ZA3230D()
                    {
                        EmpJobDtlId = PLWM.Utils.CnvToNullableInt(dr["Emp_Job_Dtl_Id"]),
                        EmpJobValue = PLWM.Utils.CnvToStr(dr["Emp_Job_Value"]),
                    });
                }

                UsageD.EducationalLevelCol = new ZA3230DCol();
                UsageD.EducationalLevelCol.Add(new ZA3230D() { EmpJobDtlId = -1, EmpJobValue = "Education Level" });
                foreach (DataRow dr in EducationLevelDT.Rows)
                {
                    UsageD.EducationalLevelCol.Add(new ZA3230D()
                    {
                        EmpJobDtlId = PLWM.Utils.CnvToNullableInt(dr["Emp_Job_Dtl_Id"]),
                        EmpJobValue = PLWM.Utils.CnvToStr(dr["Emp_Job_Value"]),
                    });
                }

                UsageD.CommitmentCol  = new ZA3230DCol();
                UsageD.CommitmentCol.Add(new ZA3230D() { EmpJobDtlId = -1, EmpJobValue = "Commitment" });
                foreach (DataRow dr in CommitmentDT.Rows)
                {
                    UsageD.CommitmentCol.Add(new ZA3230D()
                    {
                        EmpJobDtlId = PLWM.Utils.CnvToNullableInt(dr["Emp_Job_Dtl_Id"]),
                        EmpJobValue = PLWM.Utils.CnvToStr(dr["Emp_Job_Value"]),
                    });
                }


                UsageD.IndustryCol = new ZA3230DCol();
                UsageD.IndustryCol.Add(new ZA3230D() { EmpJobDtlId = -1, EmpJobValue = "Industry" });
                foreach (DataRow dr in IndustryDT.Rows)
                {
                    UsageD.IndustryCol.Add(new ZA3230D()
                    {
                        EmpJobDtlId = PLWM.Utils.CnvToNullableInt(dr["Emp_Job_Dtl_Id"]),
                        EmpJobValue = PLWM.Utils.CnvToStr(dr["Emp_Job_Value"]),
                    });
                }

                //UsageD.IndustryCol = new ZA3230DCol();
               
                foreach (DataRow dr in dtJob.Rows)
                {
                    UsageD.JobMast = new ZA3630SD()
                    {
                        FirstName= PLWM.Utils.CnvToStr(dr["FirstName"]),
                        LastName = PLWM.Utils.CnvToStr(dr["LastName"]),
                        Gender = PLWM.Utils.CnvToStr(dr["Gender"]),
                        Title = PLWM.Utils.CnvToStr(dr["Title"]),
                        Description = PLWM.Utils.CnvToStr(dr["Description"]),
                        Mobile = PLWM.Utils.CnvToStr(dr["Mobile"]),
                        Email = PLWM.Utils.CnvToStr(dr["Email"]),
                        CurrentPos = PLWM.Utils.CnvToStr(dr["CurrentPos"]),
                        NoticePeriod = PLWM.Utils.CnvToStr(dr["NoticePeriod"]),
                        CurrentCompany = PLWM.Utils.CnvToStr(dr["CurrentCompany"]),
                       // PhotoPath = PLWM.Utils.CnvToStr(dr["Emp_Job_Value"]),
                       // CvPath = PLWM.Utils.CnvToStr(dr["Emp_Job_Value"]),
                        Nationality = UsageD.NationalityCol.FirstOrDefault(x => x.EmpJobDtlId ==
                                      PLWM.Utils.CnvToInt(dtJob.Rows[0]["Nationality_id"])),
                        Industry = UsageD.IndustryCol.FirstOrDefault(x => x.EmpJobDtlId ==
                                      PLWM.Utils.CnvToInt(dtJob.Rows[0]["industry_id"])),
                        CurrentLoc = UsageD.CurrentLocCol.FirstOrDefault(x => x.EmpJobDtlId ==
                                      PLWM.Utils.CnvToInt(dtJob.Rows[0]["CurrentLoc_id"])),
                        VisaStatus = UsageD.VisaStatusCol.FirstOrDefault(x => x.EmpJobDtlId ==
                                      PLWM.Utils.CnvToInt(dtJob.Rows[0]["VisaStatus_id"])),
                        CarrierLevel = UsageD.CarrierLevelCol.FirstOrDefault(x => x.EmpJobDtlId ==
                                     PLWM.Utils.CnvToInt(dtJob.Rows[0]["CarrierLevel_id"])),
                        CurrentSalary = UsageD.CurrentSalaryCol.FirstOrDefault(x => x.EmpJobDtlId ==
                                     PLWM.Utils.CnvToInt(dtJob.Rows[0]["CurrentSalary_id"])),
                        WorkExperiance = UsageD.WorkExperianceCol.FirstOrDefault(x => x.EmpJobDtlId ==
                                     PLWM.Utils.CnvToInt(dtJob.Rows[0]["WorkExperiance_id"])),
                        EducationalLevel = UsageD.EducationalLevelCol.FirstOrDefault(x => x.EmpJobDtlId ==
                                     PLWM.Utils.CnvToInt(dtJob.Rows[0]["EducationalLevel_id"])),
                        Commitment = UsageD.CommitmentCol.FirstOrDefault(x => x.EmpJobDtlId ==
                                     PLWM.Utils.CnvToInt(dtJob.Rows[0]["Commitment_id"])),
                    };
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
            }
            catch (Exception e)
            {
                UsageD.UserData.ZaBase.ErrorMsg = PLWM.Utils.CnvToSentenceCase(e.Message.ToLower().Replace("plerror", "").Replace("plerror", "").Trim());
            }

            return UsageD;
        }




        public ZA3630SD DoSave(ZA3630SD SaveData, String Mode, long FileLength)
        {
            ZA3630SD UsageD = new ZA3630SD();

            try
            {
                XDocument doc = new XDocument(new XElement("Root",
                new XElement("as_emp_job_mast_id", SaveData.EmpJobMastID),
                new XElement("as_sessionid", SaveData.UserData.ZaBase.SessionId),
                new XElement("as_FirstName", SaveData.FirstName),
                new XElement("as_LastName", SaveData.LastName),
                new XElement("as_Gender", SaveData.Gender  ),
                new XElement("as_Nationality_id", SaveData.Nationality.EmpJobDtlId),
                new XElement("as_Title", SaveData.Title),
                new XElement("as_Description", SaveData.Description),
                new XElement("as_Mobile", SaveData.Mobile),
                new XElement("as_Email", SaveData.Email),
                new XElement("as_CurrentLoc_id", SaveData.CurrentLoc.EmpJobDtlId),
                new XElement("as_CurrentCompany", SaveData.CurrentCompany),
                new XElement("as_CurrentPos", SaveData.CurrentPos),
                new XElement("as_NoticePeriod", SaveData.NoticePeriod),
                new XElement("as_VisaStatus_id", SaveData.VisaStatus.EmpJobDtlId),
                new XElement("as_CarrierLevel_id", SaveData.CarrierLevel.EmpJobDtlId),
                new XElement("as_CurrentSalary_id", SaveData.CurrentSalary.EmpJobDtlId),
                new XElement("as_WorkExperiance_id", SaveData.WorkExperiance.EmpJobDtlId),
                new XElement("as_EducationalLevel_id", SaveData.EducationalLevel.EmpJobDtlId),
                new XElement("as_Commitment_id", SaveData.Commitment.EmpJobDtlId),
                new XElement("as_Industry_id", SaveData.Industry.EmpJobDtlId),
                new XElement("as_PhotoLength", FileLength)

                ));


                String XString = doc.ToString();
                PLABSM.DAL dbObj = new PLABSM.DAL();
                dbObj.ConnectionMode = PLABSM.ConnectionModes.WebDB;
                DataSet ds = dbObj.SelectSP("ZA3630_IU", XString, PLABSM.DbProvider.MSSql);
                DataTable dt = PLWM.Utils.GetDataTable(ds, 0);

                if (dt.Rows.Count > 0)
                {
                    UsageD.PhotoPath = PLWM.Utils.CnvToStr(dt.Rows[0]["UsrFldrName"]);
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
