USE [Zeroori]
GO
/****** Object:  StoredProcedure [dbo].[ZA3610_IU]    Script Date: 24-02-2020 03:22:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ZA3610_IU]
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
	@as_bed_room  int,
	@as_bath_room  int,
	@as_size  int,
	@as_is_Furnished  int,
	@as_apartment_for  int,
	@as_Rent_Is_Paid  int,
	@as_listed_by  int,
	@as_Category  int, 
	@as_Description varchar(1000) ,
	@as_title varchar(1000) ,
	@as_propADMastID int
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
				
		 if  exists( select @as_propADMastID
					from ZA3610
					where prop_ad_mast_id = @as_propADMastID )
		begin
			
					Update ZA3610 set	usr_mast_id=@ai_usr_mast_id, 
								bed_room_id=@as_bed_room, 
								bath_room_id=@as_bath_room, 
								size_id=@as_size, 
								is_Furnished_id=@as_is_Furnished, 
								apartment_for_id=@as_apartment_for, 
								Rent_Is_Paid_id=@as_Rent_Is_Paid, 
								listed_by_id=@as_listed_by, 
								Category_id=@as_Category,  
								prop_Description=@as_Description,
								usr_fldr_nam=@as_usr_fldr_nam,
								prop_title=@as_title,
								Stats='A'
					where prop_ad_mast_id = @as_propADMastID 
		END
		ELSE
		BEGIN
		 insert into ZA3610(	usr_mast_id, 
								bed_room_id, 
								bath_room_id, 
								size_id, 
								is_Furnished_id, 
								apartment_for_id, 
								Rent_Is_Paid_id, 
								listed_by_id, 
								Category_id,  
								prop_Description,
								usr_fldr_nam,
								prop_title,
								Stats )
						Select	@ai_usr_mast_id , 
								@as_bed_room ,
								@as_bath_room, 
								@as_size ,
								@as_is_Furnished, 
								@as_apartment_for, 
								@as_Rent_Is_Paid ,
								@as_listed_by ,
								@as_Category  ,
								@as_Description ,
								@as_usr_fldr_nam,
								@as_title,
								'A'

		Select @as_propADMastID = @@IDENTITY
		END
		Select	@as_propADMastID as PropADMastID,
				@as_sessionid as sessionid
		 
END
 










GO
