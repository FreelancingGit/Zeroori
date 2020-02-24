USE [Zeroori]
GO
/****** Object:  StoredProcedure [dbo].[ZA3010_IU]    Script Date: 24-02-2020 03:22:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ZA3010_IU]
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
	@as_busines_Name varchar(500),
	@as_busines_Url varchar(500), 
	@ai_catgry_id int, 
	@as_banner_img_url varchar(500),
	@as_logo_img_url varchar(500), 
	@as_fb_url varchar(500), 
	@as_Instagram_url varchar(500), 
	@as_Twitter_url varchar(500), 
	@as_Phone_No varchar(500), 
	@as_Email varchar(500), 
	@as_Website varchar(500), 
	@as_geo_Location varchar(500), 
	@as_Description varchar(500),
	@as_sessionid varchar(500),
	@as_PhotoLength  varchar(1000)
	
)
AS
DECLARE @errNum varchar(100);
DECLARE @errMsg varchar(2000); 
declare @as_banr_img_fldr_Pth varchar(Max);
declare @ai_usr_mast_id int
BEGIN
SET NOCOUNT ON
 
		Select @ai_usr_mast_id = usr_mast_id
		from ZA1000
		where sessionid = @as_sessionid

		Select  @as_banr_img_fldr_Pth	= ZA3000.usr_fldr_nam 
		from ZA1000
		inner join ZA3000 on ZA3000.usr_mast_id = ZA1000.usr_mast_id
		where  ZA3000.usr_mast_id = @ai_usr_mast_id

		Set @as_banr_img_fldr_Pth =  '/Ads/Clogs/'+@as_banr_img_fldr_Pth
 

		update ZA3010 Set 
						busines_Name = @as_busines_Name,
						busines_Url=@as_busines_Url, 
						catgry_id=@ai_catgry_id, 
						banner_img_url=@as_banr_img_fldr_Pth+'/Banner.Jpg',
						logo_img_url=@as_banr_img_fldr_Pth+'/CompanyLogo.Jpg', 
						fb_url=@as_fb_url, 
						Instagram_url=@as_Instagram_url, 
						Twitter_url=@as_Twitter_url, 
						Phone_No=@as_Phone_No, 
						Email=@as_Email, 
						Website=@as_Website, 
						geo_Location=@as_geo_Location, 
						Description=@as_Description,
						banr_img_fldr_Pth = @as_banr_img_fldr_Pth
		where pack_deal_mast_id = @ai_deal_mast_id


				
		exec ZA3010_SEL @ai_deal_mast_id,'UD',null,null,null,null,null	 
		 
END
 










GO
