USE [Zeroori]
GO
/****** Object:  StoredProcedure [dbo].[ZA3011_IU]    Script Date: 24-02-2020 03:22:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ZA3011_IU]
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
	@ai_deal_mast_id int,
	@ai_packdeal_mast_id INT,
	@as_deal_name varchar(500), 
	@an_price NUMERIC(18,3), 
	@as_descrptn varchar(500),
	@ad_start_dt DATE, 
	@ad_end_dt DATE, 
	@as_sessionid varchar(500),
	@as_mode VARCHAR(3)
	
)
AS
DECLARE @errNum varchar(100);
DECLARE @errMsg varchar(2000); 
declare @ai_usr_mast_id int
declare @as_banr_img_fldr_Pth varchar(Max);
declare @filename1 varchar(50)
declare @filename2 varchar(50)
declare @lastId int
BEGIN
SET NOCOUNT ON
 
		Select @ai_usr_mast_id = usr_mast_id
		from ZA1000
		where sessionid = @as_sessionid

		Select  @as_banr_img_fldr_Pth	= ZA3000.usr_fldr_nam 
		from ZA1000
		inner join ZA3000 on ZA3000.usr_mast_id = ZA1000.usr_mast_id
		where  ZA3000.usr_mast_id = @ai_usr_mast_id
		set @as_deal_name = REPLACE(@as_deal_name,' ','');
		Set @as_banr_img_fldr_Pth =  '/Ads/Deals/'+@as_banr_img_fldr_Pth
		set @filename1 = @as_banr_img_fldr_Pth+'/'+'file1'+@as_deal_name+'.jpg'
		set @filename2 = @as_banr_img_fldr_Pth+'/'+'file2'+@as_deal_name+'.jpg'

		IF(@as_mode='I')
		BEGIN
		SET NOCOUNT ON;
				INSERT INTO  ZA3011 
							( pack_deal_mast_id,
							deal_name,
							price,
							descrptn,
							start_dt,
							end_dt,
							usr_mast_id,
							 usr_folder,
							  img_name1,
							  img_name2)
					VALUES
							(@ai_packdeal_mast_id,
							@as_deal_name,
							@an_price,
							@as_descrptn,
							@ad_start_dt,
							@ad_end_dt,
							@ai_usr_mast_id,
							@as_banr_img_fldr_Pth,
							@filename1,
							@filename2)
				
			set @lastId = SCOPE_IDENTITY();
			select * from ZA3011 where deal_mast_id = @lastId
		END

		IF(@as_mode='U')
		BEGIN
					 UPDATE ZA3011 SET
									pack_deal_mast_id = @ai_packdeal_mast_id,
									deal_name = @as_deal_name,
									descrptn=@as_descrptn,
									start_dt=@ad_start_dt,
									end_dt=@ad_end_dt,
									usr_folder = @as_banr_img_fldr_Pth,
							        img_name1 = @filename1,
							       img_name2 =@filename2

					WHERE deal_mast_id = @ai_deal_mast_id

					select * from ZA3011 where deal_mast_id = @ai_deal_mast_id

		END
		 
END
 









GO
