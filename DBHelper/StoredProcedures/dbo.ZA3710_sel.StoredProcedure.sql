USE [Zeroori]
GO
/****** Object:  StoredProcedure [dbo].[ZA3710_sel]    Script Date: 24-02-2020 03:22:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ZA3710_sel]
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
	@as_mode VARCHAR(2) ,
	@ai_pageno int,
	@as_sessionid varchar(1000),
	@as_Option int,
	@as_location int,
	@as_sortby int
)
AS
BEGIN
SET NOCOUNT ON
declare @ai_page_count numeric(18,3) = 6.00
declare @ai_startNo int
declare @ai_endNo int
declare @ai_start_ID int
declare @ai_end_ID int
declare @ai_total_Pages int
declare @ai_defaultUlr varchar(1000)
declare @ai_base_url varchar(1000)
declare @sortdirection varchar(30)

	IF( @ai_pageno is null ) 
		SET @ai_pageno = 1
	
	SET @ai_endNo= ( @ai_pageno * @ai_page_count )+1
	SET @ai_startNo   = @ai_endNo - @ai_page_count  

		IF( @as_sortby is null) SET @sortdirection = 'desc'
	if(@as_sortby=1) set @sortdirection = 'asc'
	if(@as_sortby=2) set @sortdirection = 'desc'

	SELECT	@ai_start_ID =	MIN(Prop_ad_mast_id), 
			@ai_end_ID	=	MAX(Prop_ad_mast_id) 
	FROM (
			SELECT ROW_NUMBER() OVER( ORDER BY crtd_dt desc ) AS Rno,
					Prop_ad_mast_id
			FROM ZA3610
			WHERE stats like 'A'  )iTBL
	WHERE RNO BETWEEN @ai_startNo AND @ai_endNo
	

	SELECT	@ai_total_Pages = ceiling((count(Prop_ad_mast_id ) / @ai_page_count ))
			FROM ZA3610
			WHERE stats like 'A'  

	Select @ai_base_url = dbo.getBaseUrl( )

	Set @ai_defaultUlr = @ai_base_url + '/images/qatar.jpg'


	IF( @as_mode = 'LO' or @as_mode = 'LD' )
	begin
			
			-- tables 0
			select Prop_title,
					FORMAT(ZA3610.crtd_dt, 'dd MMMM yyyy') crtd_dt,
					BedRoom.Prop_value as BedRoom,
					BathRoom.Prop_value as BathRoom,
					size.Prop_value as size,
					Furnished.Prop_value as Furnished,
					UserMast.usr_email,
					UserMast.usr_phno ,
					case 
						when Prop_img_fldr_full_path is null then @ai_defaultUlr
					else  @ai_base_url +'/'+ Prop_img_fldr_full_path+'/'+Prop_img_name 
					End as full_path,
					place_name as Location,
					ZA3610.Price,
					ZA3610.prop_ad_mast_id
			from ZA3610
			LEFT join (	
							Select * from (
							Select row_number() over( partition by ZA3611.Prop_ad_mast_id order by ZA3611.Prop_ad_mast_id ) as Rno ,
											Prop_img_fldr_full_path,
											Prop_img_name,
											ZA3611.Prop_ad_mast_id
							from ZA3611
							inner join ZA3610 on ZA3610.Prop_ad_mast_id = ZA3611.Prop_ad_mast_id
							--where ZA3610.stats like 'A' 
							)Itbl 
							where Rno = 1
						)ZA3611				on ZA3611.Prop_ad_mast_id =ZA3610.Prop_ad_mast_id
			inner join ZA3211 BedRoom		on BedRoom.Prop_dtl_id =  ZA3610.bed_room_id 
			inner join ZA3211 BathRoom		on BathRoom.Prop_dtl_id =  ZA3610.bath_room_id 
			inner join ZA3211 size			on size.Prop_dtl_id =  ZA3610.size_id 
			inner join ZA3211 Furnished		on Furnished.Prop_dtl_id =  ZA3610.is_Furnished_id
			inner join ZA3000 UserMast		on UserMast.usr_mast_id = ZA3610.usr_mast_id
			left join ZA2000 on ZA2000.city_mast_id =  ZA3610.city_mast_id
			where ZA3610.Prop_ad_mast_id BETWEEN @ai_start_ID AND @ai_end_ID
			 AND (@as_location is null OR (ZA3610.city_mast_id = @as_location))
			  AND (@as_Option is null OR (ZA3610.Category_id = @as_Option))
			ORDER BY 
			case when @sortdirection='asc'then ZA3610.crtd_dt  end asc,
			case when @sortdirection='desc'then ZA3610.crtd_dt  end desc
			--AND  ZA3610.stats like 'A'


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


	if( @as_mode = 'LO' )
	begin
			-- tables 2
			Select	ZA1000.usr_mast_id,
					@as_sessionid as SESSIONID,
					usr_FistNam as  FirstName
			from ZA1000
			inner join ZA3000 on ZA3000.usr_mast_id = ZA1000.usr_mast_id
			where sessionid = @as_sessionid

			-- tables 3
			Select  ZA3211.Prop_dtl_id,
					ZA3211.Prop_value 
			from ZA3210 
			inner join ZA3211 on ZA3210.Prop_mast_id = ZA3211.Prop_mast_id
			where ZA3210.Prop_nam like 'Category'

			-- tables 4
			Select	[place_name],
					[city_mast_id]
			from [ZA2000]
			order by [place_name]


			-- tables 5
			Select 'Date Ascending' as SortMode, 1 as SortValue
			union all 
			Select 'Date Decending' as SortMode, 2 as SortValue

	END
END
GO
