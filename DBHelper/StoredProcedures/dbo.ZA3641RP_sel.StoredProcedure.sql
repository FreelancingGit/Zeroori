USE [Zeroori]
GO
/****** Object:  StoredProcedure [dbo].[ZA3641RP_sel]    Script Date: 24-02-2020 03:22:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ZA3641RP_sel]
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
	@ai_com_job_mast_id int
)
AS
BEGIN
SET NOCOUNT ON

declare @ai_base_url varchar(1000)

Select @ai_base_url = dbo.getBaseUrl( )

	IF( @as_mode = 'LO' or @as_mode = 'LD' )
	begin

			Select	ZA1000.usr_mast_id,
							@as_sessionid as SESSIONID,
							usr_FistNam as  FirstName
					from ZA1000
					inner join ZA3000 on ZA3000.usr_mast_id = ZA1000.usr_mast_id
					where sessionid = @as_sessionid

			select  compny_job_mast_id,
								compny_name,
								job_title,
								descrptn_step_two as Descr,
								FORMAT (crtd_dt, 'dd-MMMM-yyyy') as Crtd_dt,
								location ,EmpTyp.Emp_Job_Value as EmpTyp ,
								neighbrhd,
								MinWorkExp.Emp_Job_Value as MinWorkExp,
								MinEduLvl.Emp_Job_Value as MinEduLvl,
								ListedBy.Emp_Job_Value as ListedBy,
								CompnySize.Emp_Job_Value as CompnySize,
								CarierLvl.Emp_Job_Value as CarierLvl ,
								ph_num,
								compny_websit,
								img_fullPath,
								location ,
								case
								when ZA3650.FldrName is null then  @ai_base_url +'/'+ ZA3650.FldrName+'/ProPic.Jpg'
								else @ai_base_url +'/'+'images/job-seeker.jpg'
								end as proimg
						from ZA3650
						inner join ZA3231 EmpTyp on ZA3650.emplymnt_typ=EmpTyp.Emp_Job_Dtl_Id
						inner join ZA3231 MinWorkExp on MinWorkExp.Emp_Job_Dtl_Id=ZA3650.exprnce
						inner join ZA3231 MinEduLvl on MinEduLvl.Emp_Job_Dtl_Id=ZA3650.eductn_lvl
						inner join ZA3231 ListedBy on ListedBy.Emp_Job_Dtl_Id=ZA3650.listed_by
						inner join ZA3231 CompnySize on CompnySize.Emp_Job_Dtl_Id=ZA3650.compny_size
						inner join ZA3231 CarierLvl on CarierLvl.Emp_Job_Dtl_Id=ZA3650.career_lvl

			where ZA3650.compny_job_mast_id=@ai_com_job_mast_id
			
	END

	IF( @as_mode = 'CLD'  )
	begin

			Select	ZA1000.usr_mast_id,
							@as_sessionid as SESSIONID,
							usr_FistNam as  FirstName
					from ZA1000
					inner join ZA3000 on ZA3000.usr_mast_id = ZA1000.usr_mast_id
					where sessionid = @as_sessionid

			select  Company_job_mast_id,
								CompanyName,
								Title,
								Descriptn as Descr,
								FORMAT (Crtd_dt, 'dd-MMMM-yyyy') as Crtd_dt,
								ZA3231.Emp_Job_Value as CurLoc ,
								EmpType,
								Neighbr,
								MinWorkExp,
								MinEduLvl,
								ListedBy,
								CompnySize,
								CarierLvl,
								phone_no,
								email --
								--case
								--when ZA3630.FldrName is null then  @ai_base_url +'/'+ ZA3630.FldrName+'/ProPic.Jpg'
								--else @ai_base_url +'/'+'images/job-seeker.jpg'
								--end as proimg
						from ZA3640
						inner join ZA3231 on ZA3231.Emp_Job_Dtl_Id=ZA3640.CurrentLoc_id

			where ZA3640.Company_job_mast_id=@ai_com_job_mast_id
			
	END
END




GO
