USE [Zeroori]
GO
/****** Object:  StoredProcedure [dbo].[ZA3711_sel]    Script Date: 24-02-2020 03:22:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ZA3711_sel]
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
	@ai_prop_ad_mast_id int,
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
			select	prop_title,
					FORMAT(ZA3610.crtd_dt, 'dd MMMM yyyy') crtd_dt,
					BedRoom.prop_value as bedRoom,
					BathRoom.prop_value as bathRoom ,
					Size.prop_value as size,
					IsFurnished.prop_value as isFurnished,
					Appartment.prop_value as appartment,
					RentIsPaid.prop_value as rentIsPaid,
					ListedBy	.prop_value as listedBy,
					Category.prop_value	as category,	
					ZA2000.city	as city,		
					UserMast.usr_email,
					UserMast.usr_phno ,
					ZA3610.prop_Description,
					place_name as Location,
					ZA3610.Price
			from ZA3610
			inner join ZA3211 BedRoom			on BedRoom.prop_dtl_id		=	ZA3610.bed_room_id 
			left join  ZA3211 BathRoom			on BathRoom.prop_dtl_id	=	ZA3610.bath_room_id
			inner join ZA3211 Size				on Size.prop_dtl_id		=	ZA3610.size_id 
			inner join ZA3211 IsFurnished		on IsFurnished.prop_dtl_id	=	ZA3610.is_Furnished_id
			inner join ZA3211 Appartment		on Appartment.prop_dtl_id		=	ZA3610.apartment_for_id
			inner join ZA3211 RentIsPaid		on RentIsPaid.prop_dtl_id	=	ZA3610.Rent_Is_Paid_id
			inner join ZA3211 ListedBy			on ListedBy.prop_dtl_id		=	ZA3610.listed_by_id
			inner join ZA3211 Category			on Category.prop_dtl_id			=	ZA3610.Category_id
			inner join ZA3000 UserMast			on UserMast.usr_mast_id			=	ZA3610.usr_mast_id
			left join ZA2000 on ZA2000.city_mast_id =  ZA3610.city_mast_id
			where Prop_ad_mast_id = @ai_prop_ad_mast_id
			 

			Select  @ai_base_url +'/'+ Prop_img_fldr_full_path+'/'+Prop_img_name as full_path 
			from ZA3611
			inner join ZA3610 on ZA3610.Prop_ad_mast_id = ZA3611.Prop_ad_mast_id
			where ZA3610.Prop_ad_mast_id = @ai_prop_ad_mast_id

			Select	ZA1000.usr_mast_id,
					@as_sessionid as SESSIONID,
					usr_FistNam as  FirstName
			from ZA1000
			inner join ZA3000 on ZA3000.usr_mast_id = ZA1000.usr_mast_id
			where sessionid = @as_sessionid

	END

	 
END
 











GO
