USE [Zeroori]
GO
/****** Object:  StoredProcedure [dbo].[ZA3610_Del]    Script Date: 24-02-2020 03:22:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ZA3610_Del]
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
	@ai_prop_ad_mast_id INT
)
AS
BEGIN
SET NOCOUNT ON

				DELETE FROM ZA3610 
				where prop_ad_mast_id=@ai_prop_ad_mast_id 

				DELETE FROM ZA3611
				where Prop_ad_mast_id=@ai_prop_ad_mast_id 
END
 












GO
