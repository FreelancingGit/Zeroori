USE [Zeroori]
GO
/****** Object:  StoredProcedure [dbo].[ZA3600_IU]    Script Date: 24-02-2020 03:22:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ZA3600_IU]
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
	@as_Year  int,
	@as_Colour  int,
	@as_Doors  int,
	@as_Warranty  int,
	@as_RegionalSpecs  int,
	@as_Transmisson  int,
	@as_BodyType  int,
	@as_FuelType  int,
	@as_Cylinders  int,
	@as_SellerType  int,
	@as_Brand  int,
	@as_HoursePower int, 
	@as_Title  varchar(1000),
	@as_Description varchar(1000) ,
	@as_MotorsADMastID int,
	@as_ConditionID int,
	@as_KiloMetrs numeric(18,3)
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
				
		if  exists( select @as_MotorsADMastID
					from ZA3600
					where motors_ad_mast_id = @as_MotorsADMastID )
		begin
					Update ZA3600 set	usr_mast_id = @ai_usr_mast_id, 
										Year_id = @as_Year, 
										Colour_id = @as_Colour, 
										Doors_id = @as_Doors, 
										Warranty_id = @as_Warranty, 
										RegionalSpecs_id = @as_RegionalSpecs, 
										Transmisson_id = @as_Transmisson, 
										BodyType_id = @as_BodyType, 
										FuelType_id = @as_FuelType, 
										Cylinders_id = @as_Cylinders, 
										SellerType_id = @as_SellerType, 
										Brand_id =@as_Brand, 
										HoursePower_id = @as_HoursePower, 
										mot_Title = @as_Title, 
										mot_Description = @as_Description,
										usr_fldr_nam = @as_usr_fldr_nam,
										Stats = 'A',
										condition_id = @as_ConditionID,
										Kmters = @as_KiloMetrs
					where motors_ad_mast_id = @as_MotorsADMastID 
		END
		else
		begin
				 insert into ZA3600(	usr_mast_id, 
										Year_id, 
										Colour_id, 
										Doors_id, 
										Warranty_id, 
										RegionalSpecs_id, 
										Transmisson_id, 
										BodyType_id, 
										FuelType_id, 
										Cylinders_id, 
										SellerType_id, 
										Brand_id, 
										HoursePower_id, 
										mot_Title, 
										mot_Description,
										usr_fldr_nam,
										Stats,
										condition_id,
										Kmters )
								Select	@ai_usr_mast_id , 
										@as_Year ,
										@as_Colour, 
										@as_Doors ,
										@as_Warranty, 
										@as_RegionalSpecs, 
										@as_Transmisson ,
										@as_BodyType ,
										@as_FuelType ,
										@as_Cylinders ,
										@as_SellerType ,
										@as_Brand ,
										@as_HoursePower, 
										@as_Title ,
										@as_Description ,
										@as_usr_fldr_nam,
										'A',
										@as_ConditionID,
										@as_KiloMetrs

				Select @as_MotorsADMastID = @@IDENTITY
		END

		Select	@as_MotorsADMastID as MotorsADMastID,
				@as_sessionid as sessionid
		 
END
 









GO
