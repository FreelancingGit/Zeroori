USE [Zeroori]
GO
/****** Object:  StoredProcedure [dbo].[ZA3710AD_sel]    Script Date: 24-02-2020 03:22:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[ZA3710AD_sel]
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
	Set @ai_defaultUlr = @ai_base_url + '/images/qatar.jpg'


	IF( @as_mode = 'LO' or @as_mode = 'LD' )
	begin
			
			SET @ai_usr_mast_id=(Select	ZA1000.usr_mast_id 
			from ZA1000
			inner join ZA3000 on ZA3000.usr_mast_id = ZA1000.usr_mast_id
			where sessionid = @as_sessionid)

			-- tables 0
			select Prop_title,
					case 
						when Prop_img_fldr_full_path is null then @ai_defaultUlr
					else  @ai_base_url +'/'+ Prop_img_fldr_full_path+'/'+Prop_img_name 
					End as full_path,
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

			WHERE ZA3610.usr_mast_id=@ai_usr_mast_id AND  ZA3610.stats like 'A'
			ORDER BY crtd_dt desc
			

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
