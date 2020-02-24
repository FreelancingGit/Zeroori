USE [Zeroori]
GO
/****** Object:  StoredProcedure [dbo].[ZA3240_sel]    Script Date: 24-02-2020 03:22:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ZA3240_sel]
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
	@as_sessionid  VARCHAR(100) ,
	@ai_deal_mast_id int
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


		IF( @as_mode = 'LM'  ) 
		BEGIN -- Create New Record

			Select	ZA1000.usr_mast_id,
					usr_FistNam as  FirstName,
					@as_sessionid as SessionId
			from ZA1000
			inner join ZA3000 on ZA3000.usr_mast_id = ZA1000.usr_mast_id
			where sessionid = @as_sessionid


			Select	pack_deal_mast_id, 
					busines_Name,
					plan_name,
					Category
			from [ZA3010]
			inner join ZA2010 on ZA2010.plan_mast_id = [ZA3010].plan_mast_id
			inner join ZA3000 on ZA3000.usr_mast_id = [ZA3010].usr_mast_id
			left join Categeries on catgry_id = [Categeries].Id
			where end_date > GETDATE()
			and ZA3000.usr_mast_id = @ai_usr_mast_id
			and stats like 'A' 



		END

		 
END
 










GO
