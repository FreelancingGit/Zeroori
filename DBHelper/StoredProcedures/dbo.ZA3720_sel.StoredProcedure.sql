USE [Zeroori]
GO
/****** Object:  StoredProcedure [dbo].[ZA3720_sel]    Script Date: 2/25/2020 6:43:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[ZA3720_sel]
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
	@as_age int,
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

	SELECT	@ai_start_ID =	MIN(clasifd_ad_mast_id), 
			@ai_end_ID	=	MAX(clasifd_ad_mast_id) 
	FROM (
			SELECT ROW_NUMBER() OVER( ORDER BY crtd_dt desc  ) AS Rno,
					clasifd_ad_mast_id
			FROM ZA3620
			WHERE stats like 'A'  )iTBL
	WHERE RNO BETWEEN @ai_startNo AND @ai_endNo
	

	SELECT	@ai_total_Pages = ceiling((count(clasifd_ad_mast_id ) / @ai_page_count ))
			FROM ZA3620
			WHERE stats like 'A'  


	Select @ai_base_url = dbo.getBaseUrl( )

	Set @ai_defaultUlr = @ai_base_url + '/images/classifieds.jpg'

	IF( @as_mode = 'LO' or @as_mode = 'LD' )
	begin
			-- tables 0
			select clasifd_title,
					FORMAT(ZA3620.crtd_dt, 'dd MMMM yyyy') crtd_dt,
					Age.clasifd_value as Age,
					Usage.clasifd_value as Usage,
					Condition.clasifd_value as Condition,
					warrenty.clasifd_value as warrenty,
					UserMast.usr_email,
					UserMast.usr_phno ,
					case 
						when clasifd_img_fldr_full_path is null then @ai_defaultUlr
						else @ai_base_url+'/'+clasifd_img_fldr_full_path+'/'+clasifd_img_name 
					end as full_path,
					place_name as Location,
					ZA3620.Price,
					ZA3620.clasifd_ad_mast_id
			from ZA3620
			LEFT join (	
							Select * from (
							Select row_number() over( partition by ZA3621.clasifd_ad_mast_id order by ZA3621.clasifd_ad_mast_id ) as Rno ,
											clasifd_img_fldr_full_path,
											clasifd_img_name,
											ZA3621.clasifd_ad_mast_id
							from ZA3621
							inner join ZA3620 on ZA3620.clasifd_ad_mast_id = ZA3621.clasifd_ad_mast_id
							--where ZA3620.stats like 'A' 
							)Itbl 
							where Rno = 1
						)ZA3621			on ZA3621.clasifd_ad_mast_id =ZA3620.clasifd_ad_mast_id
			inner join ZA3221 Age		on Age.clasifd_dtl_id =  ZA3620.Age_id 
			inner join ZA3221 Usage		on Usage.clasifd_dtl_id =  ZA3620.Usage_id 
			inner join ZA3221 Condition on Condition.clasifd_dtl_id =  ZA3620.Condition_id 
			inner join ZA3221 warrenty  on warrenty.clasifd_dtl_id =  ZA3620.Warranty_id
			inner join ZA3000 UserMast	on UserMast.usr_mast_id = ZA3620.usr_mast_id
			left join ZA2000 on ZA2000.city_mast_id =  ZA3620.city_mast_id
			where  --ZA3620.clasifd_ad_mast_id BETWEEN @ai_start_ID AND @ai_end_ID
			 ZA3620.stats like 'A' 
			  AND (@as_age is null OR (ZA3620.Age_id < @as_age))
			  AND (@as_Option is null OR (ZA3620.Category_id = @as_Option))
			ORDER BY 
			case when @sortdirection='asc'then ZA3620.crtd_dt  end asc,
			case when @sortdirection='desc'then ZA3620.crtd_dt  end desc


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
			Select  ZA3221.clasifd_dtl_id,
					ZA3221.clasifd_value 
			from ZA3220 
			inner join ZA3221 on ZA3220.clasifd_mast_id = ZA3221.clasifd_mast_id
			where ZA3220.clasifd_nam like 'Item'

			-- tables 4
			Select	ZA3221.clasifd_dtl_id,
					ZA3221.clasifd_value
			from ZA3220
			inner join ZA3221 on ZA3220.clasifd_mast_id = ZA3221.clasifd_mast_id
			where clasifd_nam like 'Age'

			-- tables 5
			Select 'Date Ascending' as SortMode, 1 as SortValue
			union all 
			Select 'Date Decending' as SortMode, 2 as SortValue

			select clasifd_title,
					Condition.clasifd_value as Condition,
					place_name as Location
			from ZA3620
			LEFT join (	
							Select * from (
							Select row_number() over( partition by ZA3621.clasifd_ad_mast_id order by ZA3621.clasifd_ad_mast_id ) as Rno ,
											clasifd_img_fldr_full_path,
											clasifd_img_name,
											ZA3621.clasifd_ad_mast_id
							from ZA3621
							inner join ZA3620 on ZA3620.clasifd_ad_mast_id = ZA3621.clasifd_ad_mast_id
							--where ZA3620.stats like 'A' 
							)Itbl 
							where Rno = 1
						)ZA3621			on ZA3621.clasifd_ad_mast_id =ZA3620.clasifd_ad_mast_id
			inner join ZA3221 Age		on Age.clasifd_dtl_id =  ZA3620.Age_id 
			inner join ZA3221 Usage		on Usage.clasifd_dtl_id =  ZA3620.Usage_id 
			inner join ZA3221 Condition on Condition.clasifd_dtl_id =  ZA3620.Condition_id 
			inner join ZA3221 warrenty  on warrenty.clasifd_dtl_id =  ZA3620.Warranty_id
			inner join ZA3000 UserMast	on UserMast.usr_mast_id = ZA3620.usr_mast_id
			left join ZA2000 on ZA2000.city_mast_id =  ZA3620.city_mast_id
			where   ZA3620.stats like 'A' 
	END
END
GO
