USE [Zeroori]
GO
/****** Object:  StoredProcedure [dbo].[ZA3621_sel]    Script Date: 24-02-2020 03:22:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ZA3621_sel]
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
	@ai_clasifd_ad_mast_id int,
	@as_mode VARCHAR(2),
	@as_sessionid  VARCHAR(100),
	@as_email   VARCHAR(100),
	@as_passwd   VARCHAR(100)
)
AS
DECLARE @errNum varchar(100);
DECLARE @errMsg varchar(2000);
DECLARE @ai_usr_mast_id varchar(2000);
declare @ai_sid  varchar(200);
BEGIN
SET NOCOUNT ON
 
		 
		IF( @as_mode = 'LO' ) 
		BEGIN -- Create New Record
		
				Select	ZA1000.usr_mast_id,
						@as_sessionid as SESSIONID,
						usr_FistNam as  FirstName
				from ZA1000
				inner join ZA3000 on ZA3000.usr_mast_id = ZA1000.usr_mast_id
				where sessionid = @as_sessionid

				SELECT	[city_mast_id]
						,[place_name] +', '+city  as [place_name]
				FROM  [ZA2000]
				order by [place_name]


		END

		 
END
 









GO
