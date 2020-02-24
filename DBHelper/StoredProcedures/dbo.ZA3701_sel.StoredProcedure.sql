USE [Zeroori]
GO
/****** Object:  StoredProcedure [dbo].[ZA3701_sel]    Script Date: 24-02-2020 03:22:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ZA3701_sel]
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
	@ai_motors_ad_mast_id int,
	@as_sessionid varchar(200)= null
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

declare @ai_base_url varchar(1000)

  



	IF( @as_mode = 'LO' or @as_mode = 'LD' )
	begin
			Select @ai_base_url = dbo.getBaseUrl( )

			
			-- tables 0
			select	mot_Title,
					FORMAT(ZA3600.crtd_dt, 'dd MMMM yyyy') crtd_dt,
					Years.motor_spec_value as Years,
					ZA3600.Kmters as Kmters ,
					Colors.motor_spec_value as Colors,
					Doors.motor_spec_value as Doors,
					Warenty.motor_spec_value as Warenty,
					RegionalSpecs.motor_spec_value as RegionalSpecs,
					Transmisson	.motor_spec_value as Transmisson,
					BodyType.motor_spec_value	as BodyType,	
					Brand.motor_spec_value	as Brand,		
					FuelType.motor_spec_value as FuelType	,	
					SellerType.motor_spec_value	as SellerType,
					Cylinders.motor_spec_value	as Cylinders	,
					HoursePower.motor_spec_value	as HoursePower,
					condition.motor_spec_value as condition,		
					UserMast.usr_email,
					UserMast.usr_phno ,
					ZA3600.mot_Description,
					place_name as Location,
					ZA3600.Price
			from ZA3600
			inner join ZA3201 Years				on Years.motor_spec_dtl_id		=	ZA3600.Year_id 
			--left join  ZA3201 KmtersTbl			on KmtersTbl.motor_spec_dtl_id	=	ZA3600.Kmters 
			inner join ZA3201 Colors			on Colors.motor_spec_dtl_id		=	ZA3600.Colour_id
			inner join ZA3201 Doors				on Doors.motor_spec_dtl_id		=	ZA3600.Doors_id 
			inner join ZA3201 Warenty			on Warenty.motor_spec_dtl_id	=	ZA3600.Warranty_id
			inner join ZA3201 RegionalSpecs		on RegionalSpecs.motor_spec_dtl_id		=	ZA3600.RegionalSpecs_id
			inner join ZA3201 Transmisson		on Transmisson.motor_spec_dtl_id	=	ZA3600.Transmisson_id
			inner join ZA3201 BodyType			on BodyType.motor_spec_dtl_id		=	ZA3600.BodyType_id
			inner join ZA3201 Brand				on Brand.motor_spec_dtl_id			=	ZA3600.Brand_id
			inner join ZA3201 FuelType			on FuelType.motor_spec_dtl_id		=	ZA3600.FuelType_id
			inner join ZA3201 SellerType		on SellerType.motor_spec_dtl_id		=	ZA3600.SellerType_id
			inner join ZA3201 Cylinders			on Cylinders.motor_spec_dtl_id		=	ZA3600.Cylinders_id
			inner join ZA3201 HoursePower		on HoursePower.motor_spec_dtl_id	=	ZA3600.HoursePower_id
			left join ZA3201 condition			on condition.motor_spec_dtl_id		=	ZA3600.condition_id
			inner join ZA3000 UserMast			on UserMast.usr_mast_id			=	ZA3600.usr_mast_id
			left join ZA2000 on ZA2000.city_mast_id =  ZA3600.city_mast_id
			where motors_ad_mast_id = @ai_motors_ad_mast_id
			 

			Select  @ai_base_url +'/'+ motors_img_fldr_full_path+'/'+motors_img_name as full_path 
			from ZA3601
			inner join ZA3600 on ZA3600.motors_ad_mast_id = ZA3601.motors_ad_mast_id
			where ZA3600.motors_ad_mast_id = @ai_motors_ad_mast_id

			Select	ZA1000.usr_mast_id,
					@as_sessionid as SESSIONID,
					usr_FistNam as  FirstName
			from ZA1000
			inner join ZA3000 on ZA3000.usr_mast_id = ZA1000.usr_mast_id
			where sessionid = @as_sessionid

	END

	 
END
 











GO
