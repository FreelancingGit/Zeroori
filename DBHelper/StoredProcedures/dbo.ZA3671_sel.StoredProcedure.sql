USE [Zeroori]
GO
/****** Object:  StoredProcedure [dbo].[ZA3671_sel]    Script Date: 24-02-2020 03:22:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ZA3671_sel]
/*------------------------------------------------------------------------------
* Name         : ac1234_iu
* Purpose      : 9ic
* Author       : Rony
* Date Created : 08-Apr-2019
* C# Class Name: ac1234
* -------------------------------------------------------------------------------
* Modification History : 
* [Date]                 [Name OF The Person]    [What IS The Modification Done] 
* -------------------------------------------------------------------------------*/
(
	@as_mode VARCHAR(3) ,
	@as_sessionid varchar(1000),
	@ai_emp_job_mast_id int
)
AS
BEGIN
SET NOCOUNT ON
declare @ai_base_url varchar(200)

	IF( @as_mode = 'LO' or @as_mode = 'LD' )
	begin
			Select @ai_base_url = dbo.getBaseUrl( )

			Select	ZA1000.usr_mast_id,
							@as_sessionid as SESSIONID,
							usr_FistNam as  FirstName
					from ZA1000
					inner join ZA3000 on ZA3000.usr_mast_id = ZA1000.usr_mast_id
					where sessionid = @as_sessionid

			select   ZA3670.frelnc_emp_job_mast_id,
					 FirstName,
					 CASE
						WHEN Gender ='M'  THEN 'Male'
						ELSE 'Female'
					 END AS Gender,
					 Title,
					 crtd_dt,
					 Description as descr,
					 Mobile,
					 Email,
					 CurrentCompany,
					 CurrentPos,
					 NoticePeriod,
					 Nat.Emp_Job_Value as Nationality,
					 CurLoc.Emp_Job_Value as CurLoc,
					 Visa.Emp_Job_Value as VisaStatus,
					 CarrierLevel.Emp_Job_Value as CarrierLevl,
					 CurrentSalary.Emp_Job_Value as CurrentSal,
					 WorkExperiance.Emp_Job_Value as WorkExp,

					 EducationalLevel.Emp_Job_Value as EduLevel,
					 Commitment.Emp_Job_Value as Commitment,
					 case
						when ZA3670.FldrName is null then  @ai_base_url +'/'+ ZA3670.FldrName+'/ProPic.Jpg'
						else @ai_base_url +'/'+'images/job-seeker.jpg'
					  end as proimg
			from ZA3670
					 inner join ZA3231 Nat on Nat.Emp_Job_Dtl_Id=ZA3670.Nationality_id
					 inner join ZA3231 CurLoc on CurLoc.Emp_Job_Dtl_Id=ZA3670.CurrentLoc_id
					 inner join ZA3231 Visa on Visa.Emp_Job_Dtl_Id=ZA3670.VisaStatus_id
					 inner join ZA3231 CarrierLevel on CarrierLevel.Emp_Job_Dtl_Id=ZA3670.CarrierLevel_id
					 inner join ZA3231 CurrentSalary on CurrentSalary.Emp_Job_Dtl_Id=ZA3670.CurrentSalary_id
					 inner join ZA3231 WorkExperiance on WorkExperiance.Emp_Job_Dtl_Id=ZA3670.WorkExperiance_id
					 inner join ZA3231 EducationalLevel on EducationalLevel.Emp_Job_Dtl_Id=ZA3670.EducationalLevel_id
					 inner join ZA3231 Commitment on Commitment.Emp_Job_Dtl_Id=ZA3670.Commitment_id

			where ZA3670.frelnc_emp_job_mast_id=@ai_emp_job_mast_id
			
	END

	
END




GO
