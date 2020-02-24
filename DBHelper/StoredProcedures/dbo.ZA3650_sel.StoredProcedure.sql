USE [Zeroori]
GO
/****** Object:  StoredProcedure [dbo].[ZA3650_sel]    Script Date: 24-02-2020 03:22:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ZA3650_sel]
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
	@ai_compny_job_mast_id INT
)
AS
DECLARE @errNum varchar(100);
DECLARE @errMsg varchar(2000);
DECLARE @ai_usr_mast_id varchar(2000);
declare @ai_sid  varchar(200);
BEGIN
SET NOCOUNT ON
 

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

				SELECT*FROM ZA3650 WHERE compny_job_mast_id=@ai_compny_job_mast_id 

				Select	[place_name],
					[city_mast_id]
			from [ZA2000]
			order by [place_name]

			-- job Titles
			select Id,Title
			from [JobTitles]
			order by Title
		END

		 
END
 













GO
