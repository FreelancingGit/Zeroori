USE [Zeroori]
GO
/****** Object:  StoredProcedure [dbo].[ZA3650_IU]    Script Date: 24-02-2020 03:22:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ZA3650_IU]
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
    @as_mode varchar(2),
	@as_sessionid varchar(1000),
    @ai_compny_job_mast_id int,
	@as_company_name varchar(1000), 
	@as_trade_lic varchar(1000), 
	@as_contact_name varchar(100), 
	@ai_industry int, 
	@as_phone varchar(100), 
	@ai_company_size int,
	@as_compny_website varchar(100), 
	@as_adrs varchar(100), 
	@as_dscrptn_step_one varchar(max), 


	@as_job_title varchar(50), 
	@ai_empl_type int, 
	@ai_monthly_salry int, 
	@as_neighbrhd varchar(50), 
	@ai_exprnce int, 
	@ai_eductn_lvl int, 
	@ai_listed_by int, 
	@ai_career_lvl int, 
	@as_location  varchar(50), 
	@as_dscrptn_step_two  varchar(max),
	@as_PhotoName Varchar(300)
	
)
AS

DECLARE @errNum varchar(100);
DECLARE @errMsg varchar(2000);
declare @ai_firstName varchar(2000);
declare @ai_usr_mast_id int
declare @as_PostID varchar(100)
declare @as_us_flrdr_nam_usr varchar(1000),
		@as_usr_ProfleID varchar(1000),@ai_company_logo varchar(100),@ai_img_fullPath varchar(Max)
BEGIN
SET NOCOUNT ON
BEGIN TRY
BEGIN TRANSACTION
 
		Select @ai_usr_mast_id = usr_mast_id
		from ZA1000
		where sessionid = @as_sessionid

		 Select  @as_us_flrdr_nam_usr	= ZA3000.usr_fldr_nam 
		from ZA1000
		inner join ZA3000 on ZA3000.usr_mast_id = ZA1000.usr_mast_id
		where  ZA3000.usr_mast_id = @ai_usr_mast_id

		Set @as_us_flrdr_nam_usr =  '/Ads/CLogos/'+@as_us_flrdr_nam_usr
		Set @as_usr_ProfleID = 'ZA'+format(getdate(),'ddHHmmsshh')
		

		if( @as_PhotoName is null)
			Set @as_us_flrdr_nam_usr = '/images/'

		 set @ai_company_logo =@as_PhotoName

		 if(@as_PhotoName is null)
		 set @ai_company_logo ='CompanyLogo.jpg'

		 if(@as_PhotoName is null)
		 begin
		 set @ai_img_fullPath = dbo.getBaseUrl()+'/images/CompanyLogo.jpg'
		 end
		 else
		 set @ai_img_fullPath = dbo.getBaseUrl()+@as_us_flrdr_nam_usr+'/'+@as_PhotoName

		
		if  exists( select @ai_compny_job_mast_id
					from ZA3650
					where compny_job_mast_id = @ai_compny_job_mast_id )
			BEGIN
			UPDATE ZA3650 SET   
								--compny_name=@as_company_name, 
								--trade_licns=@as_trade_lic, 
								--contct_name=@as_contact_name, 
								--indstry=@ai_industry, 
								--ph_num=@as_phone, 
								--compny_size=@ai_company_size, 
								--compny_websit=@as_compny_website, 
								--addrs=@as_adrs,
								--descrpn_step_one=@as_dscrptn_step_one,

								job_title=@as_job_title, 
								emplymnt_typ=@ai_empl_type, 
								monthly_salary=@ai_monthly_salry, 
								neighbrhd=@as_neighbrhd, 
								exprnce=@ai_exprnce, 
								eductn_lvl=@ai_eductn_lvl, 
								listed_by=@ai_listed_by, 
								career_lvl=@ai_career_lvl, 
								location=@as_location, 
								descrptn_step_two=@as_dscrptn_step_two
								
				WHERE compny_job_mast_id= @ai_compny_job_mast_id
				END
		ELSE
			BEGIN
				INSERT INTO ZA3650(	compny_name, 
									trade_licns, 
									contct_name, 
									indstry, 
									ph_num, 
									compny_size, 
									compny_websit, 
									addrs,
									descrpn_step_one, 
									job_title, 
									emplymnt_typ, 
									monthly_salary, 
									exprnce, 
									eductn_lvl, 
									listed_by, 
									career_lvl, 
									location, 
									descrptn_step_two,
									FldrName,
									usr_mast_id,
									compny_logo_img ,
									img_fullPath )
							Select	@as_company_name, 
									@as_trade_lic, 
									@as_contact_name, 
									@ai_industry, 
									@as_phone, 
									@ai_company_size, 
									@as_compny_website,
									@as_adrs, 
									@as_dscrptn_step_one, 
									@as_job_title, 
									@ai_empl_type, 
									@ai_monthly_salry, 
									@ai_exprnce, 
									@ai_eductn_lvl, 
									@ai_listed_by, 
									@ai_career_lvl,
									@as_location,
									@as_dscrptn_step_two,
									@as_us_flrdr_nam_usr,
									@ai_usr_mast_id,
									@ai_company_logo,
									@ai_img_fullPath

								SET  @ai_compny_job_mast_id = @@identity

			END
			if(@ai_compny_job_mast_id > 0)
			begin
			SELECT FldrName AS UsrFldrName, img_fullPath AS UsrFilename, compny_logo_img As img_name
			from ZA3650 where compny_job_mast_id = @ai_compny_job_mast_id
			SELECT  @ai_compny_job_mast_id AS compny_job_mast_id
			end
			else
			begin
			SELECT @as_us_flrdr_nam_usr AS UsrFldrName,@ai_img_fullPath AS UsrFilename,@ai_company_logo as img_name
			SELECT  @ai_compny_job_mast_id AS compny_job_mast_id
			end


			

		--Select @as_us_flrdr_nam_usr as UsrFldrName 
				

		--Select @as_clasifdADMastID = @@IDENTITY
		--Select	@as_clasifdADMastID as clasifdADMastID,
		--		@as_sessionid as sessionid
		COMMIT TRANSACTION
		END TRY
BEGIN CATCH
select @errNum = ERROR_NUMBER();
		select @ErrMsg = ERROR_MESSAGE();
		ROLLBACK TRANSACTION
		EXEC PL1000_SEL @errNum , @ErrMsg out,'ZA3650_IU',@as_mode
		RAISERROR(@ErrMsg, 16 , 1 )
		RETURN
		END CATCH;
		 
END
 










GO
