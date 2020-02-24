USE [Zeroori]
GO
/****** Object:  StoredProcedure [dbo].[ZA3000_sel]    Script Date: 24-02-2020 03:22:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ZA3000_sel]
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
	@as_Email VARCHAR(100),
	@as_Passwd  VARCHAR(100),
	@ai_usr_mast_id int,
	@as_otp VARCHAR(100) = null, 
	@ai_SessionId VARCHAR(200) 
)
AS
DECLARE @errNum varchar(100);
DECLARE @errMsg varchar(2000);
BEGIN
SET NOCOUNT ON
 
		IF( @as_mode = 'SE'   ) 
		BEGIN -- Create New Record

				if( @ai_SessionId is not null )
				begin
						Select	@as_Passwd  =  usr_passwd,
								@as_Email	=usr_email 
						from ZA3000
						inner join ZA1000 on ZA1000.usr_mast_id = ZA3000.usr_mast_id
						where sessionid  = @ai_SessionId
				END

				EXEC [ZA1000_iu] null, @as_Email, @as_Passwd, null

				Select	ZA3000.usr_FistNam,
						ZA3000.usr_LastNam,
						ZA3000.usr_email,
						ZA3000.usr_phno,
						ZA3000.usr_passwd ,
						ZA3000.usr_fldr_nam,
						ZA3000.usr_mast_id,
						ZA1000.sessionid
				from ZA3000
				inner join ZA1000 on ZA1000.usr_mast_id = ZA3000.usr_mast_id
				where usr_email = @as_Email
				and usr_passwd = @as_Passwd


		END
		else IF( @as_mode = 'SU'   ) 
		BEGIN -- Create New Record
				Select	usr_FistNam,
						usr_LastNam,
						usr_email,
						usr_phno,
						usr_passwd ,
						usr_fldr_nam
				from ZA3000
				where usr_email = @as_Email 
				and isActive like 'A'

		END
		else IF( @as_mode = 'VP'   )
		BEGIN 
				if not exists(Select	sessionid AS SESSIONID ,
										usr_FistNam  as FirstName,
										usr_fldr_nam
								from ZA3000
								inner join ZA1000 on ZA1000.usr_mast_id = ZA3000.usr_mast_id
								where 	sessionid  = @ai_SessionId
								and isActive = @as_otp)
				begin
						RAISERROR('Invalid OTP', 16 , 1 )
				END
				else
				BEGIN
						Select	sessionid AS SESSIONID ,
								usr_FistNam  as FirstName,
								usr_fldr_nam
						from ZA3000
						inner join ZA1000 on ZA1000.usr_mast_id = ZA3000.usr_mast_id
						where sessionid  = @ai_SessionId

						Update ZA3000 set isActive = 'A'
						from ZA3000
						inner join ZA1000 on ZA1000.usr_mast_id = ZA3000.usr_mast_id
						where sessionid  = @ai_SessionId
				END
		END
END
 









GO
