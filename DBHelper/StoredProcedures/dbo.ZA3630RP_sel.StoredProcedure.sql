USE [Zeroori]
GO
/****** Object:  StoredProcedure [dbo].[ZA3630RP_sel]    Script Date: 24-02-2020 03:22:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ZA3630RP_sel]
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
	@ai_pageno int,
	@as_jobfilter int
)
AS

BEGIN
declare @ai_base_url varchar(1000),
		@ai_defaultUlrM varchar(1000),
		@ai_defaultUlrF varchar(1000),

		@ai_page_count numeric(18,3) = 6.00,
		@ai_startNo int,
		@ai_endNo int,
		@ai_start_ID int,
		@ai_end_ID int,
		@ai_total_Pages int

SET NOCOUNT ON

		IF( @ai_pageno is null ) 
				SET @ai_pageno = 1
	
			SET @ai_endNo= ( @ai_pageno * @ai_page_count )+1
			SET @ai_startNo   = @ai_endNo - @ai_page_count  



			SELECT	@ai_start_ID =	MIN(emp_job_mast_id), 
					@ai_end_ID	=	MAX(emp_job_mast_id) 
			FROM (
					SELECT ROW_NUMBER() OVER( ORDER BY crtd_dt desc  ) AS Rno,
							emp_job_mast_id
					FROM ZA3630
					WHERE stats like 'A'  )iTBL
			WHERE RNO BETWEEN @ai_startNo AND @ai_endNo

			SELECT	@ai_total_Pages = ceiling((count(emp_job_mast_id ) / @ai_page_count ))
			FROM ZA3630
			WHERE stats like 'A'  

	Select @ai_base_url = dbo.getBaseUrl( )
	Select @ai_defaultUlrM = @ai_base_url + '/images/male.jpg'
	Select @ai_defaultUlrF = @ai_base_url + '/images/female.jpg'


	IF( @as_mode = 'LO' or @as_mode = 'LD' )
	begin
			Select	ZA1000.usr_mast_id,
					@as_sessionid as SESSIONID,
					usr_FistNam as  FirstName
			from ZA1000
			inner join ZA3000 on ZA3000.usr_mast_id = ZA1000.usr_mast_id
			where sessionid = @as_sessionid


			-- tables 0
			select  ZA3630.emp_job_mast_id,
					FirstName,
					Title,
					FORMAT (crtd_dt, 'dd-MMMM-yyyy') as crtd_dt,
					CurLoc.Emp_Job_Value as CurLoc,
					WorkExperiance.Emp_Job_Value as WorkExp,
					EducationalLevel.Emp_Job_Value as EduLevel,
					case
						when ZA3630.FldrName is not null then  @ai_base_url +'/'+ ZA3630.FldrName+'/ProPic.Jpg'
						else 
								case
									when gender like 'M' then @ai_defaultUlrM
									else @ai_defaultUlrF
								END
					end as proimg
			from ZA3630
			 inner join ZA3231 CurLoc on CurLoc.Emp_Job_Dtl_Id=ZA3630.industry_id
			 inner join ZA3231 WorkExperiance on WorkExperiance.Emp_Job_Dtl_Id=ZA3630.WorkExperiance_id
			 inner join ZA3231 EducationalLevel on EducationalLevel.Emp_Job_Dtl_Id=ZA3630.EducationalLevel_id
			 where  ZA3630.emp_job_mast_id BETWEEN @ai_start_ID AND @ai_end_ID
			 AND ZA3630.stats like 'A'
			  AND (@as_jobfilter is null OR (CurLoc.Emp_Job_Dtl_Id = @as_jobfilter))
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
			 
	END
	ELSE IF( @as_mode ='CLD' )
	begin
			Select	ZA1000.usr_mast_id,
					@as_sessionid as SESSIONID,
					usr_FistNam as  FirstName
			from ZA1000
			inner join ZA3000 on ZA3000.usr_mast_id = ZA1000.usr_mast_id
			where sessionid = @as_sessionid


			-- tables 0
			select  ZA3630.emp_job_mast_id,
					FirstName,
					Title,
					FORMAT (crtd_dt, 'dd-MMMM-yyyy') as crtd_dt,
					CurLoc.Emp_Job_Value as CurLoc,
					WorkExperiance.Emp_Job_Value as WorkExp,
					EducationalLevel.Emp_Job_Value as EduLevel,
					case
						when ZA3630.FldrName is not null then  @ai_base_url +'/'+ ZA3630.FldrName+'/ProPic.Jpg'
						else case
									when gender like 'M' then @ai_defaultUlrM
									else @ai_defaultUlrF
								END
					end as proimg
			from ZA3630
			 inner join ZA3231 CurLoc on CurLoc.Emp_Job_Dtl_Id=ZA3630.CurrentLoc_id
			 inner join ZA3231 WorkExperiance on WorkExperiance.Emp_Job_Dtl_Id=ZA3630.WorkExperiance_id
			 inner join ZA3231 EducationalLevel on EducationalLevel.Emp_Job_Dtl_Id=ZA3630.EducationalLevel_id
			 
	END

	
	if( @as_mode = 'LO' )
	begin
			-- tables 2
			Select	ZA1000.usr_mast_id,
					@as_sessionid as SESSIONID,
					usr_FistNam as  FirstName
			from ZA1000
			inner join ZA3000 on ZA3000.usr_mast_id = ZA1000.usr_mast_id
			where sessionid = @as_sessionid

			-- tables 3
			Select  ZA3211.Prop_dtl_id,
					ZA3211.Prop_value 
			from ZA3210 
			inner join ZA3211 on ZA3210.Prop_mast_id = ZA3211.Prop_mast_id
			where ZA3210.Prop_nam like 'Category'

			-- tables 4
			Select	[place_name],
					[city_mast_id]
			from [ZA2000]
			order by [place_name]


			-- tables 5
			Select 'Date Ascending' as SortMode, 1 as SortValue
			union all 
			Select 'Date Decending' as SortMode, 2 as SortValue

			-- tables 6
			select ZA3231.Emp_Job_Dtl_Id,
						Emp_Job_Value 
				from ZA3230 
				inner join ZA3231 on  ZA3230.Emp_Job_Mast_Id = ZA3231.Emp_Job_Mast_Id
				where emp_job_spec_nam in( 'Jobs')

	END
END



GO
