USE [Zeroori]
GO
/****** Object:  StoredProcedure [dbo].[ZA3610_sel]    Script Date: 24-02-2020 03:22:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
CREATE PROCEDURE [dbo].[ZA3610_sel]
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
	@ai_prop_ad_mast_id INT
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

				select	ZA3211.Prop_dtl_id,
						Prop_value
				from ZA3210 
				inner join ZA3211 on  ZA3210.Prop_mast_id = ZA3211.Prop_mast_id
				where Prop_nam like 'Bedrooms'
				order by cast( Prop_value as int) asc

				select	ZA3211.Prop_dtl_id,
						Prop_value
				from ZA3210 
				inner join ZA3211 on  ZA3210.Prop_mast_id = ZA3211.Prop_mast_id
				where Prop_nam like 'Bathrooms'
				order by cast( Prop_value as int) asc

				select ZA3211.Prop_dtl_id,
						Prop_value 
				from ZA3210 
				inner join ZA3211 on  ZA3210.Prop_mast_id = ZA3211.Prop_mast_id
				where Prop_nam like 'Size(SqFt)'
				order by cast( Prop_value as int) asc

				select ZA3211.Prop_dtl_id,
						Prop_value 
				from ZA3210 
				inner join ZA3211 on  ZA3210.Prop_mast_id = ZA3211.Prop_mast_id
				where Prop_nam like 'Furnished'
				order by   Prop_value  asc

				select ZA3211.Prop_dtl_id,
						Prop_value 
				from ZA3210 
				inner join ZA3211 on  ZA3210.Prop_mast_id = ZA3211.Prop_mast_id
				where Prop_nam like 'Apartment For'
				order by   Prop_value  asc

				select ZA3211.Prop_dtl_id,
						Prop_value 
				from ZA3210 
				inner join ZA3211 on  ZA3210.Prop_mast_id = ZA3211.Prop_mast_id
				where Prop_nam like 'Rent is Paid'
				order by   Prop_value  asc

				select ZA3211.Prop_dtl_id,
						Prop_value 
				from ZA3210 
				inner join ZA3211 on  ZA3210.Prop_mast_id = ZA3211.Prop_mast_id
				where Prop_nam like 'Listed By'
				order by   Prop_value  asc

				select ZA3211.Prop_dtl_id,
						Prop_value 
				from ZA3210 
				inner join ZA3211 on  ZA3210.Prop_mast_id = ZA3211.Prop_mast_id
				where Prop_nam like 'Category'
				order by   Prop_value  asc

				Select	ZA1000.usr_mast_id,
						@as_sessionid as SESSIONID,
						usr_FistNam as  FirstName
				from ZA1000
				inner join ZA3000 on ZA3000.usr_mast_id = ZA1000.usr_mast_id
				where sessionid = @as_sessionid

				SELECT  prop_ad_mast_id,
						usr_mast_id,
						bed_room_id,
						bath_room_id,
						size_id,
						is_Furnished_id,
						apartment_for_id,
						Rent_Is_Paid_id,
						listed_by_id,
						Category_id,
						prop_Description,
						prop_title
						
						FROM ZA3610 
				WHERE prop_ad_mast_id=@ai_prop_ad_mast_id
		END

		 
END
 










GO
