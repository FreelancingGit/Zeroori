USE [Zeroori]
GO
/****** Object:  StoredProcedure [dbo].[ZA2010_IU]    Script Date: 24-02-2020 03:22:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ZA2010_IU]
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
	@as_sessionid varchar(1000),
	@as_EmailID varchar(1000),
	@as_Pkg varchar(1000),
	 
	@ai_Otp varchar(100)
)
AS
DECLARE @errNum varchar(100);
DECLARE @errMsg varchar(2000);
DECLARE @ai_usr_mast_id int
declare @ai_package_mast_id int
declare @ai_plan_name  varchar(100)
declare @ai_plan_Period int
declare @ai_counter int
declare @ai_microsecond varchar(2) = '',
		@ai_nanosecond varchar(2) = '',
		@as_usr_OTP varchar(100),
		@ai_no_of_packs int

BEGIN
SET NOCOUNT ON
 
		Select @ai_usr_mast_id =usr_mast_id  
		from ZA3000
		where usr_email = @as_EmailID


		Select @ai_package_mast_id = isnull(max( package_mast_id ),0) + 1
		from ZA3010


		if( @as_Pkg = 'Sl' )
		begin
				Set @ai_plan_name = 'Silver'
		END
		else if( @as_Pkg = 'Gl' )
		begin
				Set @ai_plan_name = 'Gold'
		END
		else if( @as_Pkg = 'Pl' )
		begin
				Set @ai_plan_name = 'Platinum'
		END

		Select @ai_plan_Period =   plan_Period ,
				@ai_no_of_packs		= ( no_of_packs-1)
		from ZA2010
		where plan_name like @ai_plan_name


		select	@ai_microsecond =format(  getdate() , 'ss')    ,
				@ai_nanosecond = cast(DATEPART(nanosecond,getdate())  as varchar)
		Select  @as_usr_OTP = isnull(@ai_microsecond, '') + isnull( @ai_nanosecond, '')


		Set @ai_counter = 0
		IF(  @ai_usr_mast_id is not null and @as_mode = 'I' ) 
		BEGIN -- Create New Record
				WHILE @ai_counter <= @ai_no_of_packs
				BEGIN
				  		insert into ZA3010( plan_mast_id,
											package_mast_id,
											usr_mast_id,
											start_date,
											end_date,
											stats)
						Select				plan_mast_id,
											@ai_package_mast_id,
											@ai_usr_mast_id,
											getdate(),
											dateadd(year,plan_Period,getdate()),
											@as_usr_OTP
						from ZA2010
						where plan_name like @ai_plan_name
				   
					   SET @ai_counter = @ai_counter + 1;
				END;

				Select @as_usr_OTP as OTP, @as_EmailID as UserEmail

		END
		else if( @as_mode != 'A' )
		begin
				RAISERROR('Invalid User Name', 16 , 1 )
		END

		IF(  @ai_usr_mast_id is not null and @as_mode = 'A' ) 
		BEGIN -- Create New Record
				 
				 update ZA3010 set stats = 'A'
				 where stats like @ai_Otp 

				Select @as_usr_OTP as OTP, @as_EmailID as UserEmail

		END
		else if(  @as_mode != 'I' )
		begin
				RAISERROR('Invalid User Name', 16 , 1 )
		END
			 
END









GO
