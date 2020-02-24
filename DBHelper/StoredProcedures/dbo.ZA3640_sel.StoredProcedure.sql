USE [Zeroori]
GO
/****** Object:  StoredProcedure [dbo].[ZA3640_sel]    Script Date: 24-02-2020 03:22:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ZA3640_sel]
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
	@as_sessionid  VARCHAR(100) 

)
AS
DECLARE @errNum varchar(100);
DECLARE @errMsg varchar(2000);
DECLARE @ai_usr_mast_id int,
		@ai_usr_FistNam varchar(2000);
declare @ai_sid  varchar(200);
BEGIN
SET NOCOUNT ON
 
		 Select	@ai_usr_mast_id = ZA1000.usr_mast_id,
				@ai_usr_FistNam = usr_FistNam 
		from ZA1000
		inner join ZA3000 on ZA3000.usr_mast_id = ZA1000.usr_mast_id
		where sessionid = @as_sessionid


		IF( @as_mode = 'LO'  ) 
		BEGIN -- Create New Record

			Select	ZA1000.usr_mast_id,
					usr_FistNam as  FirstName
			from ZA1000
			inner join ZA3000 on ZA3000.usr_mast_id = ZA1000.usr_mast_id
			where sessionid = @as_sessionid


			Select	usr_plan_mast_id, 
					[ZA3010].plan_mast_id, 
					ZA3000.usr_mast_id, 
					start_date, 
					end_date, 
					stats, 
					catgry_id
			from [ZA3010]
			inner join ZA2010 on ZA2010.plan_mast_id = [ZA3010].plan_mast_id
			inner join ZA3000 on ZA3000.usr_mast_id = [ZA3010].usr_mast_id
			where end_date > GETDATE()
			and ZA3000.usr_mast_id = @ai_usr_mast_id



		END

		 
END
 










GO
