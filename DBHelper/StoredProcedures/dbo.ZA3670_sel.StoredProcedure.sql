USE [Zeroori]
GO
/****** Object:  StoredProcedure [dbo].[ZA3670_sel]    Script Date: 2/25/2020 6:41:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[ZA3670_sel]
/*------------------------------------------------------------------------------
* Name         : ac1234_iu
* Purpose      : 
* Author       : Rony
* Date Created : 08-Apr-2019
* C# Class Name: ac1234
* -------------------------------------------------------------------------------
* Modification History : 
* [Date]                 [Name OF The Person]    [What IS The Modification Done] 
* -------------------------------------------------------------------------------*/
(
	@as_mode VARCHAR(2),
	@as_sessionid  VARCHAR(100),
	@ai_pageno int,
	@ai_frelnc_emp_job_mast_id INT
)
AS
DECLARE @errNum varchar(100);
DECLARE @errMsg varchar(2000);
DECLARE @ai_usr_mast_id varchar(2000);
declare @ai_sid  varchar(200);

declare @ai_base_url varchar(1000),
		@ai_defaultUlrM varchar(1000),
		@ai_defaultUlrF varchar(1000);

declare @ai_page_count numeric(18,3) = 6.00
declare @ai_startNo int
declare @ai_endNo int
declare @ai_start_ID int
declare @ai_end_ID int
declare @ai_total_Pages int

BEGIN
SET NOCOUNT ON
 
 	IF( @ai_pageno is null ) 
		SET @ai_pageno = 1
	
	SET @ai_endNo= ( @ai_pageno * @ai_page_count )+1
	SET @ai_startNo   = @ai_endNo - @ai_page_count  



	SELECT	@ai_start_ID =	MIN(frelnc_emp_job_mast_id), 
			@ai_end_ID	=	MAX(frelnc_emp_job_mast_id) 
	FROM (
			SELECT ROW_NUMBER() OVER( ORDER BY frelnc_emp_job_mast_id ASC ) AS Rno,
					frelnc_emp_job_mast_id
			FROM ZA3670  )iTBL
	WHERE RNO BETWEEN @ai_startNo AND @ai_endNo
	

	SELECT	@ai_total_Pages = ceiling((count(frelnc_emp_job_mast_id ) / @ai_page_count ))
	FROM ZA3670
	 inner join ZA3231 CurLoc on CurLoc.Emp_Job_Dtl_Id=ZA3670.CurrentLoc_id
			 inner join ZA3231 WorkExperiance on WorkExperiance.Emp_Job_Dtl_Id=ZA3670.WorkExperiance_id
			 inner join ZA3231 EducationalLevel on EducationalLevel.Emp_Job_Dtl_Id=ZA3670.EducationalLevel_id
			 where  --ZA3670.frelnc_emp_job_mast_id BETWEEN @ai_start_ID AND @ai_end_ID
			 ZA3670.stats like 'A'

	Select @ai_base_url = dbo.getBaseUrl( )
	Select @ai_defaultUlrM = @ai_base_url + '/images/male.jpg'
	Select @ai_defaultUlrF = @ai_base_url + '/images/female.jpg'

		IF( @as_mode = 'LO'  ) 
		BEGIN -- Create New Record

				

				select	ZA3231.Emp_Job_Dtl_Id,
						Emp_Job_Value
				from ZA3230 
				inner join ZA3231 on  ZA3230.Emp_Job_Mast_Id = ZA3231.Emp_Job_Mast_Id
				where emp_job_spec_nam like 'Nationality'


				select	ZA3231.Emp_Job_Dtl_Id,
						Emp_Job_Value
				from ZA3230 
				inner join ZA3231 on  ZA3230.Emp_Job_Mast_Id = ZA3231.Emp_Job_Mast_Id
				where emp_job_spec_nam like 'Current Location'


				select ZA3231.Emp_Job_Dtl_Id,
						Emp_Job_Value 
				from ZA3230 
				inner join ZA3231 on  ZA3230.Emp_Job_Mast_Id = ZA3231.Emp_Job_Mast_Id
				where emp_job_spec_nam like 'Visa Status'


				select ZA3231.Emp_Job_Dtl_Id,
						Emp_Job_Value 
				from ZA3230 
				inner join ZA3231 on  ZA3230.Emp_Job_Mast_Id = ZA3231.Emp_Job_Mast_Id
				where emp_job_spec_nam like 'Career Level'


				select ZA3231.Emp_Job_Dtl_Id,
						Emp_Job_Value 
				from ZA3230 
				inner join ZA3231 on  ZA3230.Emp_Job_Mast_Id = ZA3231.Emp_Job_Mast_Id
				where emp_job_spec_nam like 'Current Salary'


				select ZA3231.Emp_Job_Dtl_Id,
						Emp_Job_Value 
				from ZA3230 
				inner join ZA3231 on  ZA3230.Emp_Job_Mast_Id = ZA3231.Emp_Job_Mast_Id
				where emp_job_spec_nam like 'Work Experience'

				select ZA3231.Emp_Job_Dtl_Id,
						Emp_Job_Value 
				from ZA3230 
				inner join ZA3231 on  ZA3230.Emp_Job_Mast_Id = ZA3231.Emp_Job_Mast_Id
				where emp_job_spec_nam like 'Education Level'


				select ZA3231.Emp_Job_Dtl_Id,
						Emp_Job_Value 
				from ZA3230 
				inner join ZA3231 on  ZA3230.Emp_Job_Mast_Id = ZA3231.Emp_Job_Mast_Id
				where emp_job_spec_nam like 'Commitment'

				select ZA3231.Emp_Job_Dtl_Id,
						Emp_Job_Value 
				from ZA3230 
				inner join ZA3231 on  ZA3230.Emp_Job_Mast_Id = ZA3231.Emp_Job_Mast_Id
				where emp_job_spec_nam in( 'Jobs','Community')

				 Select	ZA1000.usr_mast_id,
						@as_sessionid as SESSIONID,
						usr_FistNam as  FirstName
				from ZA1000
				inner join ZA3000 on ZA3000.usr_mast_id = ZA1000.usr_mast_id
				where sessionid = @as_sessionid

				--Select	ZA1000.usr_mast_id,
				--		@as_sessionid as SESSIONID,
				--		usr_FistNam as  FirstName
				--from ZA1000
				--inner join ZA3000 on ZA3000.usr_mast_id = ZA1000.usr_mast_id
				--where sessionid = @as_sessionid
				select*from ZA3670 where frelnc_emp_job_mast_id=@ai_frelnc_emp_job_mast_id

		END

		 IF( @as_mode = 'LI' or @as_mode='LD' ) 
		begin
			Select	ZA1000.usr_mast_id,
					@as_sessionid as SESSIONID,
					usr_FistNam as  FirstName
			from ZA1000
			inner join ZA3000 on ZA3000.usr_mast_id = ZA1000.usr_mast_id
			where sessionid = @as_sessionid


			-- tables 0
			select  ZA3670.frelnc_emp_job_mast_id,
					FirstName,
					Title,
					FORMAT (crtd_dt, 'dd-MMMM-yyyy') as crtd_dt,
					CurLoc.Emp_Job_Value as CurLoc,
					WorkExperiance.Emp_Job_Value as WorkExp,
					EducationalLevel.Emp_Job_Value as EduLevel,
					case
						when ZA3670.FldrName is not null then  @ai_base_url +'/'+ ZA3670.FldrName+'/ProPic.Jpg'
						else 
								case
									when gender like 'M' then @ai_defaultUlrM
									else @ai_defaultUlrF
								END
					end as proimg
			from ZA3670
			 inner join ZA3231 CurLoc on CurLoc.Emp_Job_Dtl_Id=ZA3670.CurrentLoc_id
			 inner join ZA3231 WorkExperiance on WorkExperiance.Emp_Job_Dtl_Id=ZA3670.WorkExperiance_id
			 inner join ZA3231 EducationalLevel on EducationalLevel.Emp_Job_Dtl_Id=ZA3670.EducationalLevel_id
			 where  --ZA3670.frelnc_emp_job_mast_id BETWEEN @ai_start_ID AND @ai_end_ID
			 ZA3670.stats like 'A'
			 ORDER BY crtd_dt desc


			 if( @ai_pageno + 5 >= @ai_total_Pages )
			begin
					Set @ai_pageno = 
							case
								when @ai_total_Pages -5 < 1 then 1
								else @ai_total_Pages -5
							end
			END
			-- tables 1
			select	@ai_pageno as Page_No ,
					@ai_total_Pages TotalPages
			 
			 select ZA3231.Emp_Job_Dtl_Id,
						Emp_Job_Value 
				from ZA3230 
				inner join ZA3231 on  ZA3230.Emp_Job_Mast_Id = ZA3231.Emp_Job_Mast_Id
				where emp_job_spec_nam in( 'Jobs')
	END
END
 













GO
