USE [Zeroori]
GO
/****** Object:  StoredProcedure [dbo].[ZA2990_IU]    Script Date: 24-02-2020 03:22:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ZA2990_IU]
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
	@as_mode VARCHAR(2) ,
	@as_Email VARCHAR(100),
	@as_Passwd  VARCHAR(100),
	@as_sessionid  VARCHAR(100),
	@ai_usr_mast_id int
)
AS
DECLARE @errNum varchar(100);
DECLARE @errMsg varchar(2000);
declare @ai_firstName varchar(2000);
declare @as_usr_Otp varchar(100);
BEGIN
SET NOCOUNT ON
 
		Select @ai_usr_mast_id = usr_mast_id
		from ZA1000
		where sessionid = @as_sessionid


		SELECT @ai_usr_mast_id = usr_mast_id ,
				@ai_firstName = usr_FistNam
		FROM ZA3000 
		WHERE ( @as_Email is null or usr_email = @as_Email ) 
		AND ( @as_Passwd is null or  usr_passwd = @as_Passwd )
	  	AND ( @ai_usr_mast_id is null or  usr_mast_id = @ai_usr_mast_id )


		IF( @as_mode = 'SO'   ) 
		BEGIN -- Create New Record
				delete 
				FROM ZA1000 
				WHERE usr_mast_id = @ai_usr_mast_id
		END
		else IF( @as_mode = 'SP'   ) 
		BEGIN -- Create New Record
				Set @as_usr_Otp = format(getdate(),'mmsshh')

				update za3000 set usr_passwd =  @as_usr_Otp
				WHERE usr_email like @as_Email 

				SELECT		usr_passwd ,
							usr_FistNam,
							usr_phno
				FROM ZA3000 
				WHERE usr_email like @as_Email
		END
		 
END
 








GO
