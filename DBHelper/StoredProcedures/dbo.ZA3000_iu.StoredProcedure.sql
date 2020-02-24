USE [Zeroori]
GO
/****** Object:  StoredProcedure [dbo].[ZA3000_iu]    Script Date: 24-02-2020 03:22:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
CREATE PROCEDURE [dbo].[ZA3000_iu]
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
	@as_FistNam VARCHAR(100),
	@as_LastNam VARCHAR(100),
	@as_Email VARCHAR(100),
	@as_Passwd  VARCHAR(100)=null,
	@as_Mob VARCHAR(100) ,
	@ai_usr_mast_id int,
	@as_OldPasswd  VARCHAR(100)=null
)
AS
DECLARE @errNum varchar(100);
DECLARE @errMsg varchar(2000);
declare @as_usr_fldr_nam  varchar(100);
declare @ai_microsecond varchar(2) = '',
		@ai_nanosecond varchar(2) = '',
		@as_usr_OTP varchar(100)

BEGIN

SET NOCOUNT ON
BEGIN TRY

BEGIN TRANSACTION

		Set @as_FistNam = dbo.ProperCase(@as_FistNam)

		if exists( Select usr_email
					from  ZA3000
					where usr_email = @as_Email
					and isActive like 'A'
					and @as_mode not like 'UP' 
					and @as_mode not like 'PU' )
		begin
			RAISERROR('The email address you have entered is already registered.', 16 , 1 )
		END

		if( @ai_usr_mast_id is null ) 
		begin
			Select @ai_usr_mast_id = usr_mast_id
			from ZA3000
			where usr_email = @as_Email
		end

		--if( @ai_usr_mast_id is not null ) 
		--begin
		--	Set @as_mode = 'UP'
		--END


		IF( @as_mode = 'UP'  ) 
		BEGIN
				-- Update Value
				 UPDATE ZA3000 SET	usr_FistNam		=	@as_FistNam,
									usr_LastNam		=	@as_LastNam,
									usr_email		=	@as_Email,
									usr_phno		=	@as_Mob
				 WHERE  ZA3000.usr_mast_id			=	@ai_usr_mast_id


				 Select				usr_FistNam		 ,
									usr_LastNam	 ,
									usr_email	 ,
									usr_phno	 
				from  ZA3000
				WHERE  ZA3000.usr_mast_id	=	 @ai_usr_mast_id

		END
		IF( @as_mode = 'PU'  ) 
		BEGIN

				if not exists(
								select * 
								from za3000
								where usr_mast_id = @ai_usr_mast_id
								and usr_passwd = @as_OldPasswd )
				begin
						RAISERROR( 'Invalid username or password' , 16 , 1 )
				END
				else 
				begin
						-- Update Value
						 UPDATE ZA3000 SET	usr_passwd		=	@as_Passwd 
						 WHERE  ZA3000.usr_mast_id			=	@ai_usr_mast_id


						 Select				usr_FistNam		 ,
											usr_LastNam	 ,
											usr_email	 ,
											usr_phno	 
						from  ZA3000
						WHERE  ZA3000.usr_mast_id	=	 @ai_usr_mast_id
				END

		END
		IF( @as_mode = 'I'   ) 
		BEGIN -- Create New Record
				
				Set @as_usr_fldr_nam = 'ZA'+format(getdate(),'ddHHmmsshh')
				
				select	@ai_microsecond =format(  getdate() , 'ss')    ,
						@ai_nanosecond = cast(DATEPART(nanosecond,getdate())  as varchar)
				Select  @as_usr_OTP = isnull(@ai_microsecond, '') + isnull( @ai_nanosecond, '')


				INSERT INTO ZA3000 (usr_FistNam,
									usr_LastNam,
									usr_email,
									usr_phno,
									usr_passwd,
									usr_fldr_nam,
									isActive)
				VALUES (			@as_FistNam,
									@as_LastNam,
									@as_Email,
									@as_Mob,
									@as_Passwd,
									@as_usr_fldr_nam,
									@as_usr_OTP);

				Set @ai_usr_mast_id = @@IDENTITY;

				exec ZA1000_iu 'I',@as_Email,@as_Passwd,null
				
				Select	@as_usr_OTP As OTP,
						@as_Mob as Mobno,
						@as_Email AS Email
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
