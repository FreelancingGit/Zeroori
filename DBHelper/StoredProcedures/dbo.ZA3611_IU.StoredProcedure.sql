USE [Zeroori]
GO
/****** Object:  StoredProcedure [dbo].[ZA3611_IU]    Script Date: 24-02-2020 03:22:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ZA3611_IU]
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
	@as_PropADMastID int,
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
		@ai_Prop_img_full_path varchar(1000),
		@ai_Prop_img_name  varchar(1000),
		@ai_usr_FistNam varchar(1000),
		@as_ext_seq	int
 
BEGIN
SET NOCOUNT ON

		if( @as_mode = 'UP')
		begin
				update ZA3610 set Price = @as_Price,
									city_mast_id =@as_Location
				where Prop_ad_mast_id = @as_PropADMastID

		END
		else if( @as_mode = 'RI')
		begin
				Select ZA3611.Prop_img_fldr_full_path 
				from ZA1000
				inner join ZA3000 on ZA3000.usr_mast_id = ZA1000.usr_mast_id
				inner join ZA3610 on ZA3610.usr_mast_id = ZA1000.usr_mast_id
				inner join ZA3611 on ZA3611.Prop_ad_mast_id = ZA3610.Prop_ad_mast_id
				where  ZA3610.Prop_ad_mast_id = @as_PropADMastID 
				and  seqnc = @as_seq

				delete 
				from  ZA3611
				where seqnc = @as_seq
				and  Prop_ad_mast_id  = @as_PropADMastID

		END

		else
		begin
				Select	@ai_usr_mast_id			= ZA1000.usr_mast_id,
						@as_usr_fldr_nam_usr	= ZA3000.usr_fldr_nam,
						@ai_usr_fldr_nam_add	= ZA3610.usr_fldr_nam,
						@ai_usr_FistNam			= ZA3000.usr_FistNam,
						@as_sessionid			= SessionId
				from ZA1000
				inner join ZA3000 on ZA3000.usr_mast_id = ZA1000.usr_mast_id
				inner join ZA3610 on ZA3610.usr_mast_id = ZA1000.usr_mast_id
				where  ZA3610.Prop_ad_mast_id = @as_PropADMastID 
		 
		

				Select	@ai_Prop_img_full_path	= ZA3611.Prop_img_fldr_full_path,
						@ai_Prop_img_name			= ZA3611.Prop_img_name

				from ZA1000
				inner join ZA3000 on ZA3000.usr_mast_id = ZA1000.usr_mast_id
				inner join ZA3610 on ZA3610.usr_mast_id = ZA1000.usr_mast_id
				inner join ZA3611 on ZA3611.Prop_ad_mast_id = ZA3610.Prop_ad_mast_id
				where sessionid = @as_sessionid
				and ZA3610.Prop_ad_mast_id = @as_PropADMastID 
				and seqnc = @as_seq


				if( @ai_Prop_img_full_path is null )
				begin
				 
						if( @as_ext_seq < @as_seq )
						begin
								Set @as_seq = @as_ext_seq + 1
						END

						Set @ai_Prop_img_full_path =  '/Ads/'+@as_usr_fldr_nam_usr+'/PROP/'+@ai_usr_fldr_nam_add

						insert into ZA3611(	Prop_ad_mast_id,
											fldr_nam,
											Prop_img_fldr_full_path,
											Prop_img_name,
											stats,
											seqnc )
								select		@as_PropADMastID,
											@ai_usr_fldr_nam_add,
											@ai_Prop_img_full_path,
											dbo.getFileName(),
											'A',
											@as_seq 
				END
		
				Select	@as_sessionid	as SessionId,
						@ai_usr_FistNam as FirstName
					
				Select	ZA3611.seqnc,
						ZA3611.Prop_img_fldr_full_path + '/'+ZA3611.Prop_img_name as ImageUrl,
						dbo.getBaseUrl()+ZA3611.Prop_img_fldr_full_path+'/'+ZA3611.Prop_img_name as ImgFullPath
				from ZA1000
				inner join ZA3000 on ZA3000.usr_mast_id = ZA1000.usr_mast_id
				inner join ZA3610 on ZA3610.usr_mast_id = ZA1000.usr_mast_id
				inner join ZA3611 on ZA3611.Prop_ad_mast_id = ZA3610.Prop_ad_mast_id
				where sessionid = @as_sessionid
				and ZA3610.Prop_ad_mast_id = @as_PropADMastID 
				order by ZA3611.seqnc asc
		END
END
 










GO
