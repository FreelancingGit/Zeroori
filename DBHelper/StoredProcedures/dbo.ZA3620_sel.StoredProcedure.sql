USE [Zeroori]
GO
/****** Object:  StoredProcedure [dbo].[ZA3620_sel]    Script Date: 24-02-2020 03:22:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
CREATE PROCEDURE [dbo].[ZA3620_sel]
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
	@ai_clasifd_ad_mast_id int
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

				select	ZA3221.clasifd_dtl_id,
						clasifd_value
				from ZA3220 
				inner join ZA3221 on  ZA3220.clasifd_mast_id = ZA3221.clasifd_mast_id
				where clasifd_nam like 'Item'
				ORDER BY clasifd_value asc


				select	ZA3221.clasifd_dtl_id,
						clasifd_value
				from ZA3220 
				inner join ZA3221 on  ZA3220.clasifd_mast_id = ZA3221.clasifd_mast_id
				where clasifd_nam like 'SubCategory'
				ORDER BY clasifd_value asc


				select ZA3221.clasifd_dtl_id,
						clasifd_value 
				from ZA3220 
				inner join ZA3221 on  ZA3220.clasifd_mast_id = ZA3221.clasifd_mast_id
				where clasifd_nam like 'Age'
				ORDER BY clasifd_value asc


				select ZA3221.clasifd_dtl_id,
						clasifd_value 
				from ZA3220 
				inner join ZA3221 on  ZA3220.clasifd_mast_id = ZA3221.clasifd_mast_id
				where clasifd_nam like 'Usage'
				ORDER BY clasifd_value asc


				select ZA3221.clasifd_dtl_id,
						clasifd_value 
				from ZA3220 
				inner join ZA3221 on  ZA3220.clasifd_mast_id = ZA3221.clasifd_mast_id
				where clasifd_nam like 'Condition'
				ORDER BY clasifd_value asc


				select ZA3221.clasifd_dtl_id,
						clasifd_value 
				from ZA3220 
				inner join ZA3221 on  ZA3220.clasifd_mast_id = ZA3221.clasifd_mast_id
				where clasifd_nam like 'Warranty'
				ORDER BY clasifd_value asc

				 
				Select	ZA1000.usr_mast_id,
						@as_sessionid as SESSIONID,
						usr_FistNam as  FirstName
				from ZA1000
				inner join ZA3000 on ZA3000.usr_mast_id = ZA1000.usr_mast_id
				where sessionid = @as_sessionid

				SELECT @ai_clasifd_ad_mast_id as clasifd_ad_mast_id ,
						crtd_dt,
						start_dt,
						end_dt,
						ad_pay_mod,
						ad_prty,
						stats,
						aprv_dt,
						lic_id,
						usr_id_aprv_by,
						Category_id,
						Sub_Category_id,
						Age_id,
						Usage_id,
						Condition_id,
						Warranty_id,
						clasifd_Description,
						usr_fldr_nam,
						Price,
						clasifd_title,
						city_mast_id
				FROM ZA3620
				where clasifd_ad_mast_id = @ai_clasifd_ad_mast_id
		END

		 
END
 









GO
