USE [Zeroori]
GO
/****** Object:  StoredProcedure [dbo].[ZA3620AD_sel]    Script Date: 24-02-2020 03:22:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[ZA3620AD_sel]
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
	@as_sessionid varchar(1000)
)
AS
BEGIN
SET NOCOUNT ON
DECLARE @ai_usr_mast_id INT

declare @ai_defaultUlr varchar(1000)
declare @ai_base_url varchar(1000)

	
	Select @ai_base_url = dbo.getBaseUrl( )

	Set @ai_defaultUlr = @ai_base_url + '/images/classifieds.jpg'

	IF( @as_mode = 'LO' or @as_mode = 'LD' )
	begin


			SET @ai_usr_mast_id=(Select	ZA1000.usr_mast_id 
			from ZA1000
			inner join ZA3000 on ZA3000.usr_mast_id = ZA1000.usr_mast_id
			where sessionid = @as_sessionid)

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
			where  ZA3620.usr_mast_id=@ai_usr_mast_id
			AND ZA3620.stats like 'A'
			ORDER BY crtd_dt desc

			Select	ZA1000.usr_mast_id,
					@as_sessionid as SESSIONID,
					usr_FistNam as  FirstName
			from ZA1000
			inner join ZA3000 on ZA3000.usr_mast_id = ZA1000.usr_mast_id
			where sessionid = @as_sessionid
			

	END

END
 












GO
