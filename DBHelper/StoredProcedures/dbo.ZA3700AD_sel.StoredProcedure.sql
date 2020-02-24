USE [Zeroori]
GO
/****** Object:  StoredProcedure [dbo].[ZA3700AD_sel]    Script Date: 24-02-2020 03:22:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
CREATE PROCEDURE [dbo].[ZA3700AD_sel]
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

declare @ai_defaultUlr  varchar(1000)
declare @ai_base_url varchar(1000)
DECLARE @ai_usr_mast_id INT
	


	Select @ai_base_url = dbo.getBaseUrl( )
	Set @ai_defaultUlr = @ai_base_url +'/images/cars.jpg'

	IF( @as_mode = 'LO' or @as_mode = 'LD' )
	begin
			
			SET @ai_usr_mast_id=(Select	ZA1000.usr_mast_id 
			from ZA1000
			inner join ZA3000 on ZA3000.usr_mast_id = ZA1000.usr_mast_id
			where sessionid = @as_sessionid)


			-- tables 0
			select	mot_Title,
					case 
					when motors_img_fldr_full_path  is not  null then @ai_base_url +'/'+ motors_img_fldr_full_path+'/'+motors_img_name 
					else @ai_defaultUlr 
					end as full_path,
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

						where ZA3600.usr_mast_id=@ai_usr_mast_id AND  ZA3600.stats like 'A'
			order by ZA3600.crtd_dt desc
			--AND  ZA3600.stats like 'A'


			
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

	END
END
 











GO
