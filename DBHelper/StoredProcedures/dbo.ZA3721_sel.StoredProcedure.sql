USE [Zeroori]
GO
/****** Object:  StoredProcedure [dbo].[ZA3721_sel]    Script Date: 24-02-2020 03:22:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ZA3721_sel]
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
	@ai_clasfd_ad_mast_id int,
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
			select	clasifd_title,
					FORMAT(ZA3620.crtd_dt, 'dd MMMM yyyy') crtd_dt,
					Category.clasifd_value as Category,
					SubCategory.clasifd_value as SubCategory ,
					Age.clasifd_value as Age,
					Usage.clasifd_value as Usage,
					Condition.clasifd_value as Condition,
					Warranty.clasifd_value as Warranty,
					cityMast.clasifd_value as cityMast,
					UserMast.usr_email,
					UserMast.usr_phno ,
					ZA3620.clasifd_Description,
					place_name as Location,
					ZA3620.Price
			from ZA3620
			inner join ZA3221 Category			on Category.clasifd_dtl_id		=	ZA3620.Category_id 
			left join  ZA3221 SubCategory		on SubCategory.clasifd_dtl_id	=	ZA3620.Sub_Category_id
			inner join ZA3221 Age				on Age.clasifd_dtl_id		=	ZA3620.Age_id 
			inner join ZA3221 Usage				on Usage.clasifd_dtl_id	=	ZA3620.Usage_id
			inner join ZA3221 Condition			on Condition.clasifd_dtl_id		=	ZA3620.Condition_id
			inner join ZA3221 Warranty			on Warranty.clasifd_dtl_id	=	ZA3620.Warranty_id
			left join ZA3221 cityMast			on cityMast.clasifd_dtl_id		=	ZA3620.city_mast_id
			left join ZA3000 UserMast			on UserMast.usr_mast_id			=	ZA3620.usr_mast_id
			left join ZA2000 on ZA2000.city_mast_id =  ZA3620.city_mast_id
			where clasifd_ad_mast_id = @ai_clasfd_ad_mast_id
			 

			Select  @ai_base_url +'/'+ clasifd_img_fldr_full_path+'/'+clasifd_img_name as full_path 
			from ZA3621
			inner join ZA3620 on ZA3620.clasifd_ad_mast_id = ZA3621.clasifd_ad_mast_id
			where ZA3620.clasifd_ad_mast_id = @ai_clasfd_ad_mast_id

			Select	ZA1000.usr_mast_id,
					@as_sessionid as SESSIONID,
					usr_FistNam as  FirstName
			from ZA1000
			inner join ZA3000 on ZA3000.usr_mast_id = ZA1000.usr_mast_id
			where sessionid = @as_sessionid

	END

	 
END
 











GO
