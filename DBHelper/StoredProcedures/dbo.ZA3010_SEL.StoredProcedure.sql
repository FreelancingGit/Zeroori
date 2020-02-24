USE [Zeroori]
GO
/****** Object:  StoredProcedure [dbo].[ZA3010_SEL]    Script Date: 24-02-2020 03:22:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ZA3010_SEL]
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
	@as_mode varchar(3),
	@as_sessionid varchar(200)=null,
	@ai_pageno int = null,
	@as_Option int,
	@as_location int,
	@as_sortby int
)
AS
DECLARE @errNum varchar(100);
DECLARE @errMsg varchar(2000); 

declare @ai_page_count numeric(18,3) = 6.00
declare @ai_startNo int
declare @ai_endNo int
declare @ai_start_ID int
declare @ai_end_ID int
declare @ai_total_Pages int
declare @sortdirection varchar(30)

declare @ai_base_url varchar(1000),
		@ai_defaultUlrM varchar(1000),
		@ai_defaultUlrF varchar(1000);

BEGIN
SET NOCOUNT ON
 	IF( @ai_pageno is null ) 
		SET @ai_pageno = 1
	
	SET @ai_endNo= ( @ai_pageno * @ai_page_count )+1
	SET @ai_startNo   = @ai_endNo - @ai_page_count  

		IF( @as_sortby is null) SET @sortdirection = 'desc'
	if(@as_sortby=1) set @sortdirection = 'asc'
	if(@as_sortby=2) set @sortdirection = 'desc'

	SELECT	@ai_start_ID =	MIN(pack_deal_mast_id), 
			@ai_end_ID	=	MAX(pack_deal_mast_id) 
	FROM (
			SELECT ROW_NUMBER() OVER( ORDER BY start_date desc  ) AS Rno,
					pack_deal_mast_id
			FROM ZA3010
			WHERE stats like 'A'  )iTBL
	WHERE RNO BETWEEN @ai_startNo AND @ai_endNo
	

	SELECT	@ai_total_Pages = ceiling((count(pack_deal_mast_id ) / @ai_page_count ))
			FROM ZA3010
			WHERE stats like 'A'  


	Select @ai_base_url = dbo.getBaseUrl( )
	Select @ai_defaultUlrM = @ai_base_url + '/images/male.jpg'
	Select @ai_defaultUlrF = @ai_base_url + '/images/female.jpg'
	
		 
		if(@as_mode='LD')
		BEGIN
				Select @ai_base_url = dbo.getBaseUrl( )

				SELECT   
								busines_Name ,
								busines_Url, 
								catgry_id, 
								@ai_base_url+ '/'+banner_img_url as banner_img_url,
								@ai_base_url+ '/'+ logo_img_url as logo_img_url, 
								fb_url, 
								Instagram_url, 
								Twitter_url, 
								Phone_No, 
								Email, 
								Website, 
								geo_Location, 
								Description
				FROM ZA3010
				where pack_deal_mast_id = @ai_deal_mast_id
				 
				SELECT	@as_sessionid  as sessionid ,
						ZA3000.usr_FistNam
				FROM ZA1000 
						inner join ZA3000 on ZA3000.usr_mast_id =  ZA1000.usr_mast_id
				WHERE (@as_sessionid is null or  sessionid = @as_sessionid )


				Select * from Categeries

				SELECT	[city_mast_id]
						,[place_name] +', '+city  as [place_name]
				FROM  [ZA2000]
				order by [place_name]
						
		END	
		
		ELSE IF(@as_mode='LP' OR @as_mode='LD')
		BEGIN


				SELECT   pack_deal_mast_id,
								plan_mast_id,
								busines_Name ,
								Email,
								geo_Location,
								@ai_base_url+ '/'+ logo_img_url as logo_img_url
				FROM ZA3010
				where  ZA3010.pack_deal_mast_id BETWEEN @ai_start_ID AND @ai_end_ID
					   AND ZA3010.stats like 'A'
					   --AND (@as_location is null OR (ZA3010.geo_Location = @as_location))
					   --AND (@as_Option is null OR (ZA3010.catgry_id = @as_Option))
				ORDER BY 
					case when @sortdirection='asc'then ZA3010.start_date  end asc,
					case when @sortdirection='desc'then ZA3010.start_date  end desc
				
				SELECT	@as_sessionid  as sessionid ,
						ZA3000.usr_FistNam
				FROM ZA1000 
						inner join ZA3000 on ZA3000.usr_mast_id =  ZA1000.usr_mast_id
				WHERE (  sessionid = @as_sessionid )


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

			Select	[place_name],
					[city_mast_id]
			from [ZA2000]
			order by [place_name]

			Select 'Date Ascending' as SortMode, 1 as SortValue
			union all 
			Select 'Date Decending' as SortMode, 2 as SortValue

			-- tables 3
			Select  ZA3221.clasifd_dtl_id,
					ZA3221.clasifd_value 
			from ZA3220 
			inner join ZA3221 on ZA3220.clasifd_mast_id = ZA3221.clasifd_mast_id
			where ZA3220.clasifd_nam like 'Item'
						
		END	 
			Else if(@as_mode='UD')
		Begin
		SELECT   
								busines_Name ,
								busines_Url, 
								catgry_id, 
								banner_img_url as banner_img_url,
								logo_img_url as logo_img_url, 
								fb_url, 
								Instagram_url, 
								Twitter_url, 
								Phone_No, 
								Email, 
								Website, 
								geo_Location, 
								Description
				FROM ZA3010
				where pack_deal_mast_id = @ai_deal_mast_id
		 
END
		 
END
 













GO
