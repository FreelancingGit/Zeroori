USE [Zeroori]
GO
/****** Object:  StoredProcedure [dbo].[ZA2990_sel]    Script Date: 2/25/2020 6:39:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[ZA2990_sel]
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
	@as_sessionid  VARCHAR(100),
	@ai_usr_mast_id int
)
AS
DECLARE @errNum varchar(100);
DECLARE @errMsg varchar(2000);
declare @ai_firstName varchar(2000);
BEGIN
SET NOCOUNT ON

 
		if( @as_Email is not null and @as_Passwd is not null ) 
		begin
				Set @as_sessionid = null
				exec [ZA1000_iu] 'I',@as_Email,@as_Passwd
				Set @as_mode = 'I'
		END
		else
		begin
			Select @ai_usr_mast_id = usr_mast_id
			from ZA1000
			where sessionid = @as_sessionid
		END




		SELECT @ai_usr_mast_id = usr_mast_id ,
				@ai_firstName = usr_FistNam
		FROM ZA3000 
		WHERE ( @as_Email is null or usr_email = @as_Email ) 
		AND ( @as_Passwd is null or  usr_passwd = @as_Passwd )
	  	AND ( @ai_usr_mast_id is null or  usr_mast_id = @ai_usr_mast_id )
		and isActive like 'A'

		IF( @as_mode = 'SE'   ) 
		BEGIN -- Create New Record
				SELECT sessionid AS SESSIONID ,
						@ai_firstName  as FirstName,
						usr_FistNam,
						usr_LastNam,
						usr_email,
						usr_phno
				FROM ZA1000 
				inner join za3000 on za3000.usr_mast_id = ZA1000.usr_mast_id
				WHERE za3000. usr_mast_id = @ai_usr_mast_id
		END

		 
END
 















GO
