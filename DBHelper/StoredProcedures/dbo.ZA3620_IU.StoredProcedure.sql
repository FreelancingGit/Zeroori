USE [Zeroori]
GO
/****** Object:  StoredProcedure [dbo].[ZA3620_IU]    Script Date: 24-02-2020 03:22:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ZA3620_IU]
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
	@as_Category  int,
	@as_Sub_Category  int,
	@as_Age  int,
	@as_Usage  int,
	@as_Condition  int,
	@as_Warranty  int,
	@as_Description varchar(1000) ,
	@as_title varchar(1000) ,
	@as_clasifdADMastID int
)
AS
DECLARE @errNum varchar(100);
DECLARE @errMsg varchar(2000);
declare @ai_firstName varchar(2000);
declare @ai_usr_mast_id int
declare @as_usr_fldr_nam varchar(100);
BEGIN
SET NOCOUNT ON
 
		Select @ai_usr_mast_id = usr_mast_id
		from ZA1000
		where sessionid = @as_sessionid

		Set @as_usr_fldr_nam = 'ZA'+format(getdate(),'ddHHmmsshh')
				
		 if  exists( select @as_clasifdADMastID
					from ZA3620
					where clasifd_ad_mast_id = @as_clasifdADMastID )
				
				
		begin

			update ZA3620 set usr_mast_id=@ai_usr_mast_id, 
									Category_id=@as_Category, 
									Sub_Category_id=@as_Sub_Category, 
									Age_id=@as_Age, 
									Usage_id=@as_Usage, 
									Condition_id=@as_Condition,
									Warranty_id=@as_Warranty, 
									clasifd_Description=@as_Description,
									usr_fldr_nam=@as_usr_fldr_nam,
									clasifd_title=@as_title,
									Stats ='A'
			where clasifd_ad_mast_id=@as_clasifdADMastID
		end


		else
		begin
		 insert into ZA3620(	usr_mast_id, 
								Category_id, 
								Sub_Category_id, 
								Age_id, 
								Usage_id, 
								Condition_id, 
								Warranty_id, 
								clasifd_Description,
								usr_fldr_nam,
								clasifd_title,
								Stats )
						Select	@ai_usr_mast_id , 
								@as_Category ,
								@as_Sub_Category, 
								@as_Age ,
								@as_Usage, 
								@as_Condition, 
								@as_Warranty ,
								@as_Description ,
								@as_usr_fldr_nam,
								@as_title,
								'A'

				Select @as_clasifdADMastID = @@IDENTITY
		end


		Select	@as_clasifdADMastID as clasifdADMastID,
				@as_sessionid as sessionid
		
		 
END
 









GO
