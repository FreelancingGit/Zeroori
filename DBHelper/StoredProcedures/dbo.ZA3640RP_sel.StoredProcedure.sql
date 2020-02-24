USE [Zeroori]
GO
/****** Object:  StoredProcedure [dbo].[ZA3640RP_sel]    Script Date: 24-02-2020 03:22:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ZA3640RP_sel]
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

declare @ai_page_count numeric(18,3) = 6.00,
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



			SELECT	@ai_start_ID =	MIN(compny_job_mast_id), 
					@ai_end_ID	=	MAX(compny_job_mast_id) 
			FROM (
					SELECT ROW_NUMBER() OVER( ORDER BY crtd_dt desc  ) AS Rno,
							compny_job_mast_id
					FROM ZA3650
					--WHERE stats like 'A'  
					)iTBL
			WHERE RNO BETWEEN @ai_startNo AND @ai_endNo

			SELECT	@ai_total_Pages = ceiling((count(compny_job_mast_id ) / @ai_page_count ))
			FROM ZA3650
			WHERE stats like 'A'  


	IF( @as_mode = 'LO' or @as_mode = 'LD' )
	begin
			Select	ZA1000.usr_mast_id,
					@as_sessionid as SESSIONID,
					usr_FistNam as  FirstName
			from ZA1000
			inner join ZA3000 on ZA3000.usr_mast_id = ZA1000.usr_mast_id
			where sessionid = @as_sessionid


			-- tables 0
			select 
					compny_job_mast_id,
					compny_name,
					job_title,
					descrpn_step_one as Descr,
					FORMAT (crtd_dt, 'dd-MMMM-yyyy') as Crtd_dt,
					location,
					img_fullPath
			from ZA3650
			where  ZA3650.compny_job_mast_id BETWEEN @ai_start_ID AND @ai_end_ID
			AND (@as_jobfilter is null OR (ZA3650.indstry = @as_jobfilter))
			AND ZA3650.job_title is not null
			order by crtd_dt desc
			--and stats like'A'

			IF( @ai_pageno + 5 >= @ai_total_Pages )
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

	IF( @as_mode = 'CLD')
	begin
			Select	ZA1000.usr_mast_id,
					@as_sessionid as SESSIONID,
					usr_FistNam as  FirstName
			from ZA1000
			inner join ZA3000 on ZA3000.usr_mast_id = ZA1000.usr_mast_id
			where sessionid = @as_sessionid


			-- tables 0
			select 
					Company_job_mast_id,
					CompanyName,
					Title,
					Descriptn as Descr,
					FORMAT (Crtd_dt, 'dd-MMMM-yyyy') as Crtd_dt,
					ZA3231.Emp_Job_Value as CurLoc 
				
			from ZA3640
			inner join ZA3231 on ZA3231.Emp_Job_Dtl_Id=ZA3640.CurrentLoc_id
	END

	if( @as_mode = 'LO' )
	begin
		select ZA3231.Emp_Job_Dtl_Id,
						Emp_Job_Value 
				from ZA3230 
				inner join ZA3231 on  ZA3230.Emp_Job_Mast_Id = ZA3231.Emp_Job_Mast_Id
				where emp_job_spec_nam in( 'Jobs')
	end
END



GO
