USE [Zeroori]
GO
/****** Object:  StoredProcedure [dbo].[ZA3011_SEL]    Script Date: 24-02-2020 03:22:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ZA3011_SEL]
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
	@ai_pack_deal_mast_id int,
	@ai_deal_mast_id int,
	@as_mode varchar(3),
	@as_sessionid varchar(200)=null,
	@ai_pageno int = null
)
AS
DECLARE @errNum varchar(100);
DECLARE @errMsg varchar(2000),
		@ai_page_count numeric(18,3) = 6.00,
		@ai_startNo int,
		@ai_endNo int,
		@ai_start_ID int,
		@ai_end_ID int,
		@ai_total_Pages int
declare @ai_base_url varchar(1000)

		Select @ai_base_url = dbo.getBaseUrl( )
		IF( @ai_pageno is null ) 
				SET @ai_pageno = 1
	
			SET @ai_endNo= ( @ai_pageno * @ai_page_count )+1
			SET @ai_startNo   = @ai_endNo - @ai_page_count  



			SELECT	@ai_start_ID =	MIN(deal_mast_id), 
					@ai_end_ID	=	MAX(deal_mast_id) 
			FROM (
					SELECT ROW_NUMBER() OVER( ORDER BY start_dt desc  ) AS Rno,
							deal_mast_id
					FROM ZA3011)iTBL
			WHERE RNO BETWEEN @ai_startNo AND @ai_endNo

			SELECT	@ai_total_Pages = ceiling((count(deal_mast_id ) / @ai_page_count ))
			FROM ZA3011

BEGIN
SET NOCOUNT ON
 
		 
		if(@as_mode='LO' )
		BEGIN


				SELECT   deal_mast_id,
						 pack_deal_mast_id,
						 deal_name ,
						 cast(price as numeric(18,0)) as price, 
						 descrptn, 
						 start_dt,
						 end_dt, 
						 usr_mast_id,
						 @ai_base_url+'\'+img_name1 as img_name1,
						  @ai_base_url+'\'+img_name2 as img_name2
				FROM ZA3011
				where deal_mast_id = @ai_deal_mast_id


				SELECT	@as_sessionid  as sessionid ,
						ZA3000.usr_FistNam
				FROM ZA1000 
						inner join ZA3000 on ZA3000.usr_mast_id =  ZA1000.usr_mast_id
				WHERE (@as_sessionid is null or  sessionid = @as_sessionid )
				
		END	 
		 
		ELSE IF(@as_mode='LC')
		BEGIN


				SELECT   deal_mast_id,
						 pack_deal_mast_id,
						 deal_name ,
						  @ai_base_url+'\'+img_name1 as img_name1,
						  @ai_base_url+'\'+img_name2 as img_name2
				FROM ZA3011
				where pack_deal_mast_id = @ai_pack_deal_mast_id
				
				SELECT	@as_sessionid  as sessionid ,
						ZA3000.usr_FistNam,
						ZA3000.usr_mast_id
				FROM ZA1000 
						inner join ZA3000 on ZA3000.usr_mast_id =  ZA1000.usr_mast_id
				WHERE (@as_sessionid is null or  sessionid = @as_sessionid )
						
		END	

		ELSE IF(@as_mode='LI' or @as_mode='LD')
		BEGIN


				SELECT   deal_mast_id,
						 ZA3011.pack_deal_mast_id,
						 deal_name ,
						  cast(price as numeric(18,0)) as price, 
						 descrptn,
						 ZA3010.geo_Location,
						 banner_img_url,
						  usr_folder,
						  @ai_base_url+'\'+img_name1 as img_name1,
						  @ai_base_url+'\'+img_name2 as img_name2
				FROM ZA3011
				INNER JOIN ZA3010 ON ZA3010.pack_deal_mast_id=ZA3011.pack_deal_mast_id
				where  ZA3011.deal_mast_id BETWEEN @ai_start_ID AND @ai_end_ID
				

				SELECT	@as_sessionid  as sessionid ,
						ZA3000.usr_FistNam,
						ZA3000.usr_mast_id
				FROM ZA1000 
						inner join ZA3000 on ZA3000.usr_mast_id =  ZA1000.usr_mast_id
				WHERE ( sessionid = @as_sessionid )

				 if( @ai_pageno + 5 >= @ai_total_Pages )
					begin
							Set @ai_pageno = 
									case
										when @ai_total_Pages -5 < 1 then 1
										else @ai_total_Pages -5
									end
					END
					-- tables 1
					select	@ai_pageno as Page_No ,
							@ai_total_Pages TotalPages
						
		END	

		ELSE IF(@as_mode='LS')
		BEGIN

				select  deal_mast_id,
						ZA3011.pack_deal_mast_id,
						deal_name,
						ZA3011.descrptn,
						ZA3010.busines_Name,
						ZA3010.geo_Location,
						price,
						FORMAT (start_dt, 'dd-MMMM-yyyy') as start_dt,
						FORMAT (end_dt, 'dd-MMMM-yyyy') as end_dt,
						 @ai_base_url+'\'+img_name1 as img_name1,
						  @ai_base_url+'\'+img_name2 as img_name2
				 from ZA3011
				 INNER JOIN ZA3010 on ZA3010.pack_deal_mast_id=ZA3011.pack_deal_mast_id
				 where deal_mast_id=@ai_deal_mast_id

				
				SELECT	@as_sessionid  as sessionid ,
						ZA3000.usr_FistNam,
						ZA3000.usr_mast_id
				FROM ZA1000 
						inner join ZA3000 on ZA3000.usr_mast_id =  ZA1000.usr_mast_id
				WHERE ( sessionid = @as_sessionid ) 
						
		END	

		ELSE IF(@as_mode='LB')
		BEGIN

				SELECT   pack_deal_mast_id,
						 plan_mast_id,
						 package_mast_id,
						 Phone_No,
						 Email,
						 Website,
						 geo_Location ,
						 Description as descrptn,
						 @ai_base_url+ '/'+banner_img_url as banner_img_url,
						 @ai_base_url+ '/'+ logo_img_url as logo_img_url
				FROM ZA3010
				where pack_deal_mast_id = @ai_pack_deal_mast_id



				SELECT   deal_mast_id,
						 ZA3011.pack_deal_mast_id,
						 deal_name,
						 price,
						 descrptn,
						 geo_Location
				FROM ZA3011
				JOIN ZA3010 ON ZA3010.pack_deal_mast_id=ZA3011.pack_deal_mast_id
				where ZA3011.pack_deal_mast_id = @ai_pack_deal_mast_id
				


				SELECT	@as_sessionid  as sessionid ,
						ZA3000.usr_FistNam,
						ZA3000.usr_mast_id
				FROM ZA1000 
						inner join ZA3000 on ZA3000.usr_mast_id =  ZA1000.usr_mast_id
				WHERE (   sessionid = @as_sessionid )
						
		END	
END
 











GO
