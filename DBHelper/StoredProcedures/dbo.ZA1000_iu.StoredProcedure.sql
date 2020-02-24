USE [Zeroori]
GO
/****** Object:  StoredProcedure [dbo].[ZA1000_iu]    Script Date: 24-02-2020 03:22:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ZA1000_iu]
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
	@as_Passwd  VARCHAR(100) ,
	@ai_isOtp VARCHAR(1) = 'N'
)
AS
DECLARE @errNum varchar(100);
DECLARE @errMsg varchar(2000);
DECLARE @ai_sid varchar(1000);
declare @ai_usr_mast_id int,
		@ai_firstName varchar(1000)
BEGIN

SET NOCOUNT ON
BEGIN TRY
BEGIN TRANSACTION

		SELECT @ai_usr_mast_id = usr_mast_id ,
				@ai_firstName = usr_FistNam
		FROM ZA3000 
		WHERE usr_email = @as_Email 
		AND usr_passwd = @as_Passwd  
		and (@ai_isOtp is null or isActive like 'A' )

		

		IF  ( @ai_usr_mast_id is not null  )
		BEGIN 
				select @ai_sid = dbo.[ZA1000_GetSID]()
					
				if not exists(SELECT sessionid AS SESSIONID  
								FROM ZA1000 
								WHERE usr_mast_id = @ai_usr_mast_id )
				begin  
						INSERT INTO ZA1000 (sessionid,
											lst_lgn_time,
											usr_mast_id) 
						VALUES (			@ai_sid,
											GETDATE(),
											@ai_usr_mast_id);
				END
				else 
				begin
						update ZA1000 set lst_lgn_time = GETDATE()
						where usr_mast_id = @ai_usr_mast_id
				END

				SELECT	sessionid AS SESSIONID ,
						@ai_firstName  as FirstName,
						usr_fldr_nam
				FROM ZA1000 
				inner join ZA3000 on ZA3000.usr_mast_id =  ZA1000.usr_mast_id
				WHERE ZA3000.usr_mast_id = @ai_usr_mast_id
				and (@ai_isOtp is null or isActive like 'A' )
		END
		else
		begin
				SELECT	null AS SESSIONID ,
						null  as FirstName,
						null as usr_fldr_nam
				 
		END

		COMMIT TRANSACTION
		END TRY
		BEGIN CATCH
				select @errNum = ERROR_NUMBER();
				select @ErrMsg = ERROR_MESSAGE();
				ROLLBACK TRANSACTION
				EXEC PL1000_SEL @errNum , @ErrMsg out,'ac1234_IU',@as_mode
				RAISERROR(@ErrMsg, 16 , 1 )
				RETURN
		END CATCH;
END
 









GO
