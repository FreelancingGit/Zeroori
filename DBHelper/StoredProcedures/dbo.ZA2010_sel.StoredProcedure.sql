USE [Zeroori]
GO
/****** Object:  StoredProcedure [dbo].[ZA2010_sel]    Script Date: 24-02-2020 03:22:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ZA2010_sel]
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
	@as_sessionid varchar(1000)
)
AS
DECLARE @errNum varchar(100);
DECLARE @errMsg varchar(2000);
BEGIN
SET NOCOUNT ON
 
		IF( @as_mode = 'LO'   ) 
		BEGIN -- Create New Record

				SELECT *
				FROM ZA2010 WHERE plan_name= 'Silver'

				SELECT *
				FROM ZA2010 WHERE plan_name= 'Gold'

				SELECT *
				FROM ZA2010 WHERE plan_name= 'Platinum'


				
				SELECT	ZA1000.usr_mast_id,
					    @as_sessionid as SESSIONID,
					    usr_FistNam as  FirstName
				FROM ZA1000
				inner join ZA3000 on ZA3000.usr_mast_id = ZA1000.usr_mast_id
				WHERE sessionid = @as_sessionid
		END

		IF( @as_mode = 'LD'   ) 
		BEGIN -- Load Data

				SELECT ZA2011.plan_mast_id, plan_value 
				FROM ZA2011
				INNER JOIN ZA2010 ON ZA2010.plan_mast_id=ZA2011.plan_mast_id
				WHERE ZA2010.plan_name='Silver'

				SELECT ZA2011.plan_mast_id, plan_value 
				FROM ZA2011
				INNER JOIN ZA2010 ON ZA2010.plan_mast_id=ZA2011.plan_mast_id
				WHERE ZA2010.plan_name='Gold'

				SELECT ZA2011.plan_mast_id, plan_value 
				FROM ZA2011
				INNER JOIN ZA2010 ON ZA2010.plan_mast_id=ZA2011.plan_mast_id
				WHERE ZA2010.plan_name='Platinum'
		END
END








GO
