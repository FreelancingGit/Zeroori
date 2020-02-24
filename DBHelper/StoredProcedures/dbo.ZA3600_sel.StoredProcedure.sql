USE [Zeroori]
GO
/****** Object:  StoredProcedure [dbo].[ZA3600_sel]    Script Date: 24-02-2020 03:22:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ZA3600_sel]
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
	@as_email   VARCHAR(100),
	@as_passwd   VARCHAR(100),
	@ai_motors_ad_mast_id int = null
)
AS
DECLARE @errNum varchar(100);
DECLARE @errMsg varchar(2000);
DECLARE @ai_usr_mast_id varchar(2000);
declare @ai_sid  varchar(200);
BEGIN
SET NOCOUNT ON
 
		if( ( @as_email is not null and @as_passwd is not null ) or  @as_sessionid is not null)
		begin
				Select @ai_usr_mast_id = usr_mast_id
				from ZA3000
				where ( @as_email is null or usr_email like @as_email )
				and ( @as_passwd is null or usr_passwd =@as_passwd )
				and isActive like 'A'
			
				if(@ai_usr_mast_id is null)
				begin
						RAISERROR('Invalid User Name OR Password', 16 , 1 )
				END
				else
				begin
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

						SELECT	@as_sessionid  = sessionid ,
								@ai_usr_mast_id = ZA3000.usr_mast_id
						FROM ZA1000 
						inner join ZA3000 on ZA3000.usr_mast_id =  ZA1000.usr_mast_id
						WHERE (@ai_usr_mast_id is null or  ZA3000.usr_mast_id = @ai_usr_mast_id )
						and (@as_sessionid is null or  sessionid = @as_sessionid )
						and isActive like 'A'
				END
		END


		IF( @as_mode = 'LO' and @ai_usr_mast_id is not null) 
		BEGIN -- Create New Record

				select	ZA3201.motor_spec_dtl_id,
						motor_spec_value
				from ZA3200 
				inner join ZA3201 on  ZA3200.motor_spec_mast_id = ZA3201.motor_spec_mast_id
				where motor_spec_nam like 'Year'
				ORDER BY CAST( motor_spec_value AS INT) DESC


				select	ZA3201.motor_spec_dtl_id,
						motor_spec_value
				from ZA3200 
				inner join ZA3201 on  ZA3200.motor_spec_mast_id = ZA3201.motor_spec_mast_id
				where motor_spec_nam like 'Colour'


				select ZA3201.motor_spec_dtl_id,
						motor_spec_value 
				from ZA3200 
				inner join ZA3201 on  ZA3200.motor_spec_mast_id = ZA3201.motor_spec_mast_id
				where motor_spec_nam like 'Doors'


				select ZA3201.motor_spec_dtl_id,
						motor_spec_value 
				from ZA3200 
				inner join ZA3201 on  ZA3200.motor_spec_mast_id = ZA3201.motor_spec_mast_id
				where motor_spec_nam like 'Warranty'


				select ZA3201.motor_spec_dtl_id,
						motor_spec_value 
				from ZA3200 
				inner join ZA3201 on  ZA3200.motor_spec_mast_id = ZA3201.motor_spec_mast_id
				where motor_spec_nam like 'Regional Specs'


				select ZA3201.motor_spec_dtl_id,
						motor_spec_value 
				from ZA3200 
				inner join ZA3201 on  ZA3200.motor_spec_mast_id = ZA3201.motor_spec_mast_id
				where motor_spec_nam like 'Transmisson'


				select ZA3201.motor_spec_dtl_id,
						motor_spec_value 
				from ZA3200 
				inner join ZA3201 on  ZA3200.motor_spec_mast_id = ZA3201.motor_spec_mast_id
				where motor_spec_nam like 'Body Type'


				select ZA3201.motor_spec_dtl_id,
						motor_spec_value 
				from ZA3200 
				inner join ZA3201 on  ZA3200.motor_spec_mast_id = ZA3201.motor_spec_mast_id
				where motor_spec_nam like 'Fuel Type'


				select ZA3201.motor_spec_dtl_id,
						motor_spec_value 
				from ZA3200 
				inner join ZA3201 on  ZA3200.motor_spec_mast_id = ZA3201.motor_spec_mast_id
				where motor_spec_nam like 'Cylinders'
				 
				order by motor_spec_seq


				select ZA3201.motor_spec_dtl_id,
						motor_spec_value 
				from ZA3200 
				inner join ZA3201 on  ZA3200.motor_spec_mast_id = ZA3201.motor_spec_mast_id
				where motor_spec_nam like 'Seller Type'


				select ZA3201.motor_spec_dtl_id,
						motor_spec_value 
				from ZA3200 
				inner join ZA3201 on  ZA3200.motor_spec_mast_id = ZA3201.motor_spec_mast_id
				where motor_spec_nam like 'Extras'


				select ZA3201.motor_spec_dtl_id,
						motor_spec_value 
				from ZA3200 
				inner join ZA3201 on  ZA3200.motor_spec_mast_id = ZA3201.motor_spec_mast_id
				where motor_spec_nam like 'Techinal Features'


				select ZA3201.motor_spec_dtl_id,
						motor_spec_value 
				from ZA3200 
				inner join ZA3201 on  ZA3200.motor_spec_mast_id = ZA3201.motor_spec_mast_id
				where motor_spec_nam like 'Hourse Power'

				select ZA3201.motor_spec_dtl_id,
						motor_spec_value 
				from ZA3200 
				inner join ZA3201 on  ZA3200.motor_spec_mast_id = ZA3201.motor_spec_mast_id
				where motor_spec_nam like 'Brand'

				select ZA3201.motor_spec_dtl_id,
						motor_spec_value 
				from ZA3200 
				inner join ZA3201 on  ZA3200.motor_spec_mast_id = ZA3201.motor_spec_mast_id
				where motor_spec_nam like 'Condition'

				Select	ZA1000.usr_mast_id,
						@as_sessionid as SESSIONID,
						usr_FistNam as  FirstName
				from ZA1000
				inner join ZA3000 on ZA3000.usr_mast_id = ZA1000.usr_mast_id
				where sessionid = @as_sessionid


				Select Year_id,
						Colour_id,
						Doors_id,
						Warranty_id,
						RegionalSpecs_id,
						Transmisson_id,
						BodyType_id,
						FuelType_id,
						Cylinders_id,
						SellerType_id,
						HoursePower_id,
						Brand_id,
						condition_id,
						mot_Title,
						mot_Description,
						Kmters,
						motors_ad_mast_id
				from ZA3600
				where motors_ad_mast_id = @ai_motors_ad_mast_id


		END

		 
END
 









GO
