USE [Zeroori]
GO
/****** Object:  StoredProcedure [dbo].[ZA3200_sel]    Script Date: 24-02-2020 03:22:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ZA3200_sel]
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
	@as_mode VARCHAR(2),
	@as_Email VARCHAR(100),
	@as_Passwd  VARCHAR(100),
	@ai_usr_mast_id int
)
AS
DECLARE @errNum varchar(100);
DECLARE @errMsg varchar(2000);
BEGIN
SET NOCOUNT ON
 
		IF( @as_mode = 'LO'   ) 
		BEGIN -- Create New Record

				select	ZA3200.motor_spec_mast_id,
						motor_spec_value
				from ZA3200 
				inner join ZA3201 on  ZA3201.motor_spec_mast_id = ZA3200.motor_spec_mast_id
				where motor_spec_nam like 'Year'


				select	ZA3200.motor_spec_mast_id,
						motor_spec_value
				from ZA3200 
				inner join ZA3201 on  ZA3201.motor_spec_mast_id = ZA3200.motor_spec_mast_id
				where motor_spec_nam like 'Colour'


				select ZA3200.motor_spec_mast_id,
						motor_spec_value 
				from ZA3200 
				inner join ZA3201 on  ZA3201.motor_spec_mast_id = ZA3200.motor_spec_mast_id
				where motor_spec_nam like 'Doors'


				select ZA3200.motor_spec_mast_id,
						motor_spec_value 
				from ZA3200 
				inner join ZA3201 on  ZA3201.motor_spec_mast_id = ZA3200.motor_spec_mast_id
				where motor_spec_nam like 'Warranty'


				select ZA3200.motor_spec_mast_id,
						motor_spec_value 
				from ZA3200 
				inner join ZA3201 on  ZA3201.motor_spec_mast_id = ZA3200.motor_spec_mast_id
				where motor_spec_nam like 'Regional Specs'


				select ZA3200.motor_spec_mast_id,
						motor_spec_value 
				from ZA3200 
				inner join ZA3201 on  ZA3201.motor_spec_mast_id = ZA3200.motor_spec_mast_id
				where motor_spec_nam like 'Transmisson'


				select ZA3200.motor_spec_mast_id,
						motor_spec_value 
				from ZA3200 
				inner join ZA3201 on  ZA3201.motor_spec_mast_id = ZA3200.motor_spec_mast_id
				where motor_spec_nam like 'Body Type'


				select ZA3200.motor_spec_mast_id,
						motor_spec_value 
				from ZA3200 
				inner join ZA3201 on  ZA3201.motor_spec_mast_id = ZA3200.motor_spec_mast_id
				where motor_spec_nam like 'Fuel Type'


				select ZA3200.motor_spec_mast_id,
						motor_spec_value 

				from ZA3200 
				inner join ZA3201 on  ZA3201.motor_spec_mast_id = ZA3200.motor_spec_mast_id
				where motor_spec_nam like 'Cylinders' 
				order by motor_spec_seq desc


				select ZA3200.motor_spec_mast_id,
						motor_spec_value 
				from ZA3200 
				inner join ZA3201 on  ZA3201.motor_spec_mast_id = ZA3200.motor_spec_mast_id
				where motor_spec_nam like 'Seller Type'


				select ZA3200.motor_spec_mast_id,
						motor_spec_value 
				from ZA3200 
				inner join ZA3201 on  ZA3201.motor_spec_mast_id = ZA3200.motor_spec_mast_id
				where motor_spec_nam like 'Extras'


				select ZA3200.motor_spec_mast_id,
						motor_spec_value 
				from ZA3200 
				inner join ZA3201 on  ZA3201.motor_spec_mast_id = ZA3200.motor_spec_mast_id
				where motor_spec_nam like 'Techinal Features'


				select ZA3200.motor_spec_mast_id,
						motor_spec_value 
				from ZA3200 
				inner join ZA3201 on  ZA3201.motor_spec_mast_id = ZA3200.motor_spec_mast_id
				where motor_spec_nam like 'Hourse Power'

				select ZA3200.motor_spec_mast_id,
						motor_spec_value 
				from ZA3200 
				inner join ZA3201 on  ZA3201.motor_spec_mast_id = ZA3200.motor_spec_mast_id
				where motor_spec_nam like 'Brand'

		END

		 
END
 








GO
