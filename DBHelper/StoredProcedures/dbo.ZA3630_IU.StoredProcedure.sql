USE [Zeroori]
GO
/****** Object:  StoredProcedure [dbo].[ZA3630_IU]    Script Date: 24-02-2020 03:22:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ZA3630_IU]
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
	@as_sessionid varchar(1000),
	@as_emp_job_mast_id int = null, 
	@as_FirstName varchar(1000), 
	@as_LastName varchar(1000), 
	@as_Gender varchar(10), 
	@as_Title varchar(MAX), 
	@as_Description varchar(MAX), 
	@as_Mobile varchar(1000), 
	@as_Email varchar(1000), 
	@as_CurrentCompany varchar(1000), 
	@as_CurrentPos varchar(1000), 
	@as_NoticePeriod varchar(1000), 
	@as_Nationality_id int, 
	@as_CurrentLoc_id int, 
	@as_VisaStatus_id int, 
	@as_CarrierLevel_id int, 
	@as_CurrentSalary_id int, 
	@as_WorkExperiance_id int, 
	@as_EducationalLevel_id int, 
	@as_Commitment_id  int,
	@as_Industry_id int,
	@as_PhotoLength Varchar(300)
	
)
AS
DECLARE @errNum varchar(100);
DECLARE @errMsg varchar(2000);
declare @ai_firstName varchar(2000);
declare @ai_usr_mast_id int
declare  @as_PostID varchar(100)
declare @as_us_flrdr_nam_usr varchar(1000),
		@as_usr_ProfleID varchar(1000)
BEGIN
SET NOCOUNT ON
 
		Select @ai_usr_mast_id = usr_mast_id
		from ZA1000
		where sessionid = @as_sessionid

		Select  @as_us_flrdr_nam_usr	= ZA3000.usr_fldr_nam 
		from ZA1000
		inner join ZA3000 on ZA3000.usr_mast_id = ZA1000.usr_mast_id
		where  ZA3000.usr_mast_id = @ai_usr_mast_id

		Set @as_us_flrdr_nam_usr =  '/Ads/Cvs/'+@as_us_flrdr_nam_usr
		Set @as_usr_ProfleID = 'ZA'+format(getdate(),'ddHHmmsshh')

		if( @as_PhotoLength is null )
			Set @as_us_flrdr_nam_usr = null

			if  exists( select @as_emp_job_mast_id
					from ZA3630
					where emp_job_mast_id = @as_emp_job_mast_id )
				
			BEGIN					
			update ZA3630 set FirstName    =@as_FirstName,            
								LastName =@as_LastName,
								Gender=@as_Gender,
								Title =@as_Title,
								[Description]=@as_Description,
								Mobile=@as_Mobile,
								Email= @as_Email,
								CurrentCompany =@as_CurrentCompany,
								CurrentPos=@as_CurrentPos,
								NoticePeriod =@as_NoticePeriod,
								Nationality_id =@as_Nationality_id,
								CurrentLoc_id =@as_CurrentLoc_id,
								VisaStatus_id =@as_VisaStatus_id,
								CarrierLevel_id =@as_CarrierLevel_id,
								CurrentSalary_id =@as_CurrentSalary_id,
								WorkExperiance_id =@as_WorkExperiance_id,
								EducationalLevel_id =@as_EducationalLevel_id,
								Commitment_id =@as_Commitment_id,
								usr_mast_id=@ai_usr_mast_id,
								PostID=@as_usr_ProfleID,
								FldrName=@as_us_flrdr_nam_usr,
								industry_id=@as_Industry_id
								where emp_job_mast_id=@as_emp_job_mast_id
			END	
		else
		begin
		 insert into ZA3630(	 
								FirstName, 
								LastName, 
								Gender, 
								Title, 
								[Description], 
								Mobile, 
								Email, 
								CurrentCompany, 
								CurrentPos, 
								NoticePeriod, 
								Nationality_id, 
								CurrentLoc_id, 
								VisaStatus_id, 
								CarrierLevel_id, 
								CurrentSalary_id, 
								WorkExperiance_id, 
								EducationalLevel_id, 
								Commitment_id, 
								usr_mast_id, 
								PostID,
								FldrName,
								industry_id  )
						Select	 @as_FirstName, 
								@as_LastName, 
								@as_Gender, 
								@as_Title, 
								@as_Description, 
								@as_Mobile, 
								@as_Email, 
								@as_CurrentCompany, 
								@as_CurrentPos, 
								@as_NoticePeriod, 
								@as_Nationality_id, 
								@as_CurrentLoc_id, 
								@as_VisaStatus_id, 
								@as_CarrierLevel_id, 
								@as_CurrentSalary_id, 
								@as_WorkExperiance_id, 
								@as_EducationalLevel_id, 
								@as_Commitment_id, 
								@ai_usr_mast_id, 
								@as_usr_ProfleID,
								@as_us_flrdr_nam_usr,
								@as_Industry_id

		Select @as_us_flrdr_nam_usr as UsrFldrName 
			end	
				 
		 
END
 











GO
