USE [Zeroori]
GO
/****** Object:  StoredProcedure [dbo].[ZA3700AD_Del]    Script Date: 24-02-2020 03:22:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ZA3700AD_Del]
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
	@ai_motors_ad_mast_id INT
)
AS
BEGIN
SET NOCOUNT ON

				DELETE FROM ZA3600
				where motors_ad_mast_id=@ai_motors_ad_mast_id

				DELETE FROM ZA3601 
				WHERE motors_ad_mast_id=@ai_motors_ad_mast_id

END

GO
