USE [Zeroori]
GO
/****** Object:  StoredProcedure [dbo].[ZA3680_sel]    Script Date: 24-02-2020 03:22:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ZA3680_sel]
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
	@ai_frelnc_comp_job_mast_id INT,
	@as_jobfilter int

)
AS
DECLARE @errNum varchar(100);
DECLARE @errMsg varchar(2000);
DECLARE @ai_usr_mast_id varchar(2000);
declare @ai_sid  varchar(200);


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



	SELECT	@ai_start_ID =	MIN(frelnc_comp_job_mast_id), 
			@ai_end_ID	=	MAX(frelnc_comp_job_mast_id) 
	FROM (
			SELECT ROW_NUMBER() OVER( ORDER BY crtd_dt ASC ) AS Rno,
					frelnc_comp_job_mast_id
			FROM ZA3680  )iTBL
	WHERE RNO BETWEEN @ai_startNo AND @ai_endNo
	

	SELECT	@ai_total_Pages = ceiling((count(frelnc_comp_job_mast_id ) / @ai_page_count ))
	FROM ZA3680


 
		--if( ( @as_email is not null and @as_passwd is not null ) or  @as_sessionid is not null)
		--begin
		--		Select @ai_usr_mast_id = usr_mast_id
		--		from ZA3000
		--		where ( @as_email is null or usr_email like @as_email )
		--		and ( @as_passwd is null or usr_passwd =@as_passwd )
		--		and isActive like 'A'
			
		--		if(@ai_usr_mast_id is null)
		--		begin
		--				RAISERROR('Invalid User Name OR Password', 16 , 1 )
		--		END
		--		else
		--		begin
		--				select @ai_sid = dbo.[ZA1000_GetSID]()

		--				if not exists(SELECT sessionid AS SESSIONID  
		--								FROM ZA1000 
		--								WHERE usr_mast_id = @ai_usr_mast_id )
		--				begin  
		--						INSERT INTO ZA1000 (sessionid,
		--											lst_lgn_time,
		--											usr_mast_id) 
		--						VALUES (			@ai_sid,
		--											GETDATE(),
		--											@ai_usr_mast_id);
		--				END
		--				else 
		--				begin
		--						update ZA1000 set lst_lgn_time = GETDATE()
		--						where usr_mast_id = @ai_usr_mast_id
		--				END

		--				SELECT	@as_sessionid  = sessionid ,
		--						@ai_usr_mast_id = ZA3000.usr_mast_id
		--				FROM ZA1000 
		--				inner join ZA3000 on ZA3000.usr_mast_id =  ZA1000.usr_mast_id
		--				WHERE (@ai_usr_mast_id is null or  ZA3000.usr_mast_id = @ai_usr_mast_id )
		--				and (@as_sessionid is null or  sessionid = @as_sessionid )
		--				and isActive like 'A'
		--		END
		--END


		IF( @as_mode = 'LO'  ) 
		BEGIN -- Create New Record


				--Industry Type
				select ZA3231.Emp_Job_Dtl_Id,
						Emp_Job_Value 
				from ZA3230 
				inner join ZA3231 on  ZA3230.Emp_Job_Mast_Id = ZA3231.Emp_Job_Mast_Id
				where emp_job_spec_nam in( 'Jobs','Community')
				 


				--Company Size
				select ZA3231.Emp_Job_Dtl_Id,
						Emp_Job_Value 
				from ZA3230 
				inner join ZA3231 on  ZA3230.Emp_Job_Mast_Id = ZA3231.Emp_Job_Mast_Id
				where emp_job_spec_nam like 'Company Size'



				--Emplomnt Type
				select ZA3231.Emp_Job_Dtl_Id,
						Emp_Job_Value 
				from ZA3230 
				inner join ZA3231 on  ZA3230.Emp_Job_Mast_Id = ZA3231.Emp_Job_Mast_Id
				where emp_job_spec_nam like 'Commitment'


				--Monthly Salary
				select ZA3231.Emp_Job_Dtl_Id,
						Emp_Job_Value 
				from ZA3230 
				inner join ZA3231 on  ZA3230.Emp_Job_Mast_Id = ZA3231.Emp_Job_Mast_Id
				where emp_job_spec_nam like 'Current Salary'


				--Exprnc 
				select ZA3231.Emp_Job_Dtl_Id,
						Emp_Job_Value 
				from ZA3230 
				inner join ZA3231 on  ZA3230.Emp_Job_Mast_Id = ZA3231.Emp_Job_Mast_Id
				where emp_job_spec_nam like 'Work Experience'


				--Eductn Level 
				select ZA3231.Emp_Job_Dtl_Id,
						Emp_Job_Value 
				from ZA3230 
				inner join ZA3231 on  ZA3230.Emp_Job_Mast_Id = ZA3231.Emp_Job_Mast_Id
				where emp_job_spec_nam like 'Education Level'


				--Listed By Level 
				select ZA3231.Emp_Job_Dtl_Id,
						Emp_Job_Value 
				from ZA3230 
				inner join ZA3231 on  ZA3230.Emp_Job_Mast_Id = ZA3231.Emp_Job_Mast_Id
				where emp_job_spec_nam like 'Listed By'


				--career Level
				select ZA3231.Emp_Job_Dtl_Id,
						Emp_Job_Value 
				from ZA3230 
				inner join ZA3231 on  ZA3230.Emp_Job_Mast_Id = ZA3231.Emp_Job_Mast_Id
				where emp_job_spec_nam like 'Career Level'


				--User Data			 
				Select	ZA1000.usr_mast_id,
						@as_sessionid as SESSIONID,
						usr_FistNam as  FirstName
				from ZA1000
				inner join ZA3000 on ZA3000.usr_mast_id = ZA1000.usr_mast_id
				where sessionid = @as_sessionid

				SELECT*FROM ZA3680 WHERE frelnc_comp_job_mast_id=@ai_frelnc_comp_job_mast_id
		END



		IF( @as_mode = 'LI' OR @as_mode='LD' ) 
		BEGIN -- Create New Record

				Select	ZA1000.usr_mast_id,
						@as_sessionid as SESSIONID,
						usr_FistNam as  FirstName
				from ZA1000
				inner join ZA3000 on ZA3000.usr_mast_id = ZA1000.usr_mast_id
				where sessionid = @as_sessionid
				


				select frelnc_comp_job_mast_id,
					   job_title as Title,
					   descrpn_step_one as descrpn,
					   comp_name,
					   FORMAT (crtd_dt, 'dd-MMMM-yyyy') as crtd_dt,
					   location
				from ZA3680
				where  ZA3680.frelnc_comp_job_mast_id BETWEEN @ai_start_ID AND @ai_end_ID
				AND (@as_jobfilter is null OR (ZA3680.indstry = @as_jobfilter))
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
