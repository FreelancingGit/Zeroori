USE [Zeroori]
GO
/****** Object:  StoredProcedure [dbo].[ZA3700_sel]    Script Date: 24-02-2020 03:22:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

 
CREATE PROCEDURE [dbo].[ZA3700_sel]
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
	@ai_pageno int,
	@as_sessionid varchar(1000),
	@as_Option int,
	@as_location int,
	@as_sortby int,
    @ai_model varchar(50),
    @ai_color varchar(50),
    @ai_fuelType varchar(50),
    @ai_bodyType varchar(50)
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
declare @ai_defaultUlr  varchar(1000)
declare @ai_base_url varchar(1000)
declare @sortdirection varchar(30)


	IF( @ai_pageno is null ) 
		SET @ai_pageno = 1
	
	SET @ai_endNo= ( @ai_pageno * @ai_page_count )+1
	SET @ai_startNo   = @ai_endNo - @ai_page_count 

	IF( @as_sortby is null) SET @sortdirection = 'desc'
	if(@as_sortby=1) set @sortdirection = 'asc'
	if(@as_sortby=2) set @sortdirection = 'desc'

	SELECT	@ai_start_ID =	MIN(motors_ad_mast_id), 
			@ai_end_ID	=	MAX(motors_ad_mast_id) 
	FROM (
			SELECT ROW_NUMBER() OVER( ORDER BY crtd_dt desc ) AS Rno,
					motors_ad_mast_id
			FROM ZA3600
			WHERE stats like 'A'  )iTBL
	WHERE RNO BETWEEN @ai_startNo AND @ai_endNo
	

	SELECT	@ai_total_Pages = ceiling((count(motors_ad_mast_id ) / @ai_page_count ))
			FROM ZA3600
			WHERE stats like 'A'  

	Select @ai_base_url = dbo.getBaseUrl( )
	Set @ai_defaultUlr = @ai_base_url +'/images/cars.jpg'

	IF( @as_mode = 'LO' or @as_mode = 'LD' )
	begin
			
			
			-- tables 0
			select	mot_Title,
					
					FORMAT(ZA3600.crtd_dt, 'dd MMMM yyyy') crtd_dt,
					Years.motor_spec_value as Years,
					Kmters,
					Doors.motor_spec_value as Doors,
					Colors.motor_spec_value as Colors,
					FuelType.motor_spec_value as FuelType,
					UserMast.usr_email,
					UserMast.usr_phno ,
					case 
					when motors_img_fldr_full_path  is not  null then @ai_base_url +'/'+ motors_img_fldr_full_path+'/'+motors_img_name 
					else @ai_defaultUlr 
					end as full_path,
					place_name as Location,
					ZA3600.Price,
					ZA3600.motors_ad_mast_id
			from ZA3600
			LEFT join (	
							Select * from (
							Select row_number() over( partition by ZA3601.motors_ad_mast_id order by ZA3601.motors_ad_mast_id ) as Rno ,
											motors_img_fldr_full_path,
											motors_img_name,
											ZA3601.motors_ad_mast_id
							from ZA3601
							inner join ZA3600 on ZA3600.motors_ad_mast_id = ZA3601.motors_ad_mast_id
							--where ZA3600.stats like 'A' 
							)Itbl 
							where Rno = 1
						)ZA3601				on ZA3601.motors_ad_mast_id		=	ZA3600.motors_ad_mast_id
			inner join ZA3201 Years			on Years.motor_spec_dtl_id		=	ZA3600.Year_id 
			left join ZA3201 KmtersTbl		on KmtersTbl.motor_spec_dtl_id	=	ZA3600.Kmters 
			inner join ZA3201 Doors			on Doors.motor_spec_dtl_id		=	ZA3600.Doors_id 
			inner join ZA3201 Colors		on Colors.motor_spec_dtl_id		=	ZA3600.Colour_id
			inner join ZA3201 FuelType		on FuelType.motor_spec_dtl_id	=  ZA3600.FuelType_id
			inner join ZA3000 UserMast		on UserMast.usr_mast_id			=	ZA3600.usr_mast_id
			left join ZA2000 on ZA2000.city_mast_id =  ZA3600.city_mast_id
			where ZA3600.motors_ad_mast_id BETWEEN @ai_start_ID AND @ai_end_ID
			 and
			  (@as_location is null OR (ZA3600.city_mast_id = @as_location))
			 and
			 (@ai_bodyType is null OR (mot_Title Like '%'+@ai_bodyType+'%'))
			 and
			(@ai_color is null OR (Colors.motor_spec_value Like '%'+@ai_color+'%'))
			and
			(@ai_fuelType is null OR (FuelType.motor_spec_value Like '%'+@ai_fuelType+'%'))
			and
			(@ai_model is null OR (Years.motor_spec_value Like '%'+@ai_model+'%'))
			order by
			case when @sortdirection='asc'then ZA3600.crtd_dt  end asc,
			case when @sortdirection='desc'then ZA3600.crtd_dt  end desc
			--AND  ZA3600.stats like 'A'


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
			Select	[place_name],
					[city_mast_id]
			from [ZA2000]
			order by [place_name]


			-- tables 4
			Select 'Date Ascending' as SortMode, 1 as SortValue
			union all 
			Select 'Date Decending' as SortMode, 2 as SortValue

			-- tables 5
			Select  ZA3201.motor_spec_dtl_id,
					ZA3201.motor_spec_value 
			from ZA3200 
			inner join ZA3201 on ZA3200.motor_spec_mast_id = ZA3201.motor_spec_mast_id
			where ZA3200.motor_spec_nam like 'Body Type'

			Select  ZA3201.motor_spec_dtl_id,
					ZA3201.motor_spec_value ,
					ZA3201.motor_spec_img_path
			from ZA3200 
			inner join ZA3201 on ZA3200.motor_spec_mast_id = ZA3201.motor_spec_mast_id
			where ZA3200.motor_spec_nam like 'Brand'
			order by motor_spec_seq asc

			Select  ZA3201.motor_spec_dtl_id,
					ZA3201.motor_spec_value 
			from ZA3200 
			inner join ZA3201 on ZA3200.motor_spec_mast_id = ZA3201.motor_spec_mast_id
			where ZA3200.motor_spec_nam like 'Year'
			order by cast( ZA3201.motor_spec_value  as int ) desc

			Select  ZA3201.motor_spec_dtl_id,
					ZA3201.motor_spec_value 
			from ZA3200 
			inner join ZA3201 on ZA3200.motor_spec_mast_id = ZA3201.motor_spec_mast_id
			where ZA3200.motor_spec_nam like 'Fuel Type'

			Select  ZA3201.motor_spec_dtl_id,
					ZA3201.motor_spec_value 
			from ZA3200 
			inner join ZA3201 on ZA3200.motor_spec_mast_id = ZA3201.motor_spec_mast_id
			where ZA3200.motor_spec_nam like 'Colour'



	END
END
 











GO
