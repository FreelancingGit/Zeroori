USE [Zeroori]
GO
/****** Object:  StoredProcedure [dbo].[ZA3011_DEL]    Script Date: 24-02-2020 03:22:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ZA3011_DEL]
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
	@as_sessionid varchar(200)=null
)
AS
DECLARE @errNum varchar(100);
DECLARE @errMsg varchar(2000); 
BEGIN
SET NOCOUNT ON
 
	DELETE FROM ZA3011 
	WHERE deal_mast_id=@ai_deal_mast_id
	
END
 









GO
