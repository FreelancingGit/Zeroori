USE [Zeroori]
GO
/****** Object:  StoredProcedure [dbo].[ZA3601_IU]    Script Date: 24-02-2020 03:22:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ZA3601_IU]
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
	@as_MotorsADMastID int,
	@as_seq  int,
	@as_sessionid  varchar(1000) ,
	@as_PHNo varchar(1000) = null,
	@as_Price numeric(18,3)= null,
	@as_mode varchar(3) = null,
	@as_Location int
)
AS
DECLARE @errNum varchar(100);
DECLARE @errMsg varchar(2000);
declare @ai_usr_mast_id int,
		@as_usr_fldr_nam_usr varchar(100),
		@ai_usr_fldr_nam_add varchar(100),
		@ai_motors_img_full_path varchar(1000),
		@ai_motors_img_name  varchar(1000),
		@ai_usr_FistNam varchar(1000),
		@as_ext_seq	int
 
BEGIN
SET NOCOUNT ON

		if( @as_mode = 'UP')
		begin
				update ZA3600 set Price = @as_Price,
									city_mast_id = @as_Location
				where motors_ad_mast_id = @as_MotorsADMastID

		END
		else if( @as_mode = 'RI')
		begin
				Select ZA3601.motors_img_fldr_full_path 
				from ZA1000
				inner join ZA3000 on ZA3000.usr_mast_id = ZA1000.usr_mast_id
				inner join ZA3600 on ZA3600.usr_mast_id = ZA1000.usr_mast_id
				inner join ZA3601 on ZA3601.motors_ad_mast_id = ZA3600.motors_ad_mast_id
				where  ZA3600.motors_ad_mast_id = @as_MotorsADMastID 
				and  seqnc = @as_seq

				delete 
				from  ZA3601
				where seqnc = @as_seq
				and  motors_ad_mast_id  = @as_MotorsADMastID

		END

		else
		begin
				Select	@ai_usr_mast_id			= ZA1000.usr_mast_id,
						@as_usr_fldr_nam_usr	= ZA3000.usr_fldr_nam,
						@ai_usr_fldr_nam_add	= ZA3600.usr_fldr_nam,
						@ai_usr_FistNam			= ZA3000.usr_FistNam,
						@as_sessionid			= SessionId
				from ZA1000
				inner join ZA3000 on ZA3000.usr_mast_id = ZA1000.usr_mast_id
				inner join ZA3600 on ZA3600.usr_mast_id = ZA1000.usr_mast_id
				where  ZA3600.motors_ad_mast_id = @as_MotorsADMastID 
		 
		

				Select	@ai_motors_img_full_path	= ZA3601.motors_img_fldr_full_path,
						@ai_motors_img_name			= ZA3601.motors_img_name

				from ZA1000
				inner join ZA3000 on ZA3000.usr_mast_id = ZA1000.usr_mast_id
				inner join ZA3600 on ZA3600.usr_mast_id = ZA1000.usr_mast_id
				inner join ZA3601 on ZA3601.motors_ad_mast_id = ZA3600.motors_ad_mast_id
				where sessionid = @as_sessionid
				and ZA3600.motors_ad_mast_id = @as_MotorsADMastID 
				and seqnc = @as_seq


				if( @ai_motors_img_full_path is null )
				begin
				 
						if( @as_ext_seq < @as_seq )
						begin
								Set @as_seq = @as_ext_seq + 1
						END

						Set @ai_motors_img_full_path =  '/Ads/'+@as_usr_fldr_nam_usr+'/MOTORS/'+@ai_usr_fldr_nam_add

						insert into ZA3601(	motors_ad_mast_id,
											fldr_nam,
											motors_img_fldr_full_path,
											motors_img_name,
											stats,
											seqnc)
								select		@as_MotorsADMastID,
											@ai_usr_fldr_nam_add,
											@ai_motors_img_full_path,
											dbo.getFileName(),
											'A',
											@as_seq
				END
		
				Select	@as_sessionid	as SessionId,
						@ai_usr_FistNam as FirstName
					
				Select	ZA3601.seqnc,
						ZA3601.motors_img_fldr_full_path + '/'+ZA3601.motors_img_name as ImageUrl,
						dbo.getBaseUrl()+ZA3601.motors_img_fldr_full_path+'/'+ZA3601.motors_img_name as ImgFullPath
				from ZA1000
				inner join ZA3000 on ZA3000.usr_mast_id = ZA1000.usr_mast_id
				inner join ZA3600 on ZA3600.usr_mast_id = ZA1000.usr_mast_id
				inner join ZA3601 on ZA3601.motors_ad_mast_id = ZA3600.motors_ad_mast_id
				where sessionid = @as_sessionid
				and ZA3600.motors_ad_mast_id = @as_MotorsADMastID 
				order by ZA3601.seqnc asc
		END
END
 









GO
