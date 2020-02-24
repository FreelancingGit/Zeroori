USE [Zeroori]
GO
/****** Object:  StoredProcedure [dbo].[ZA1000_sel]    Script Date: 24-02-2020 03:22:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ZA1000_sel]
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
	@as_Passwd  VARCHAR(100) 
)
AS
DECLARE @errNum varchar(100);
DECLARE @errMsg varchar(2000);
BEGIN

SET NOCOUNT ON
BEGIN TRY

BEGIN TRANSACTION

		 

 
		IF( @as_mode = 'SE'   ) 
		BEGIN -- Create New Record
				 Select 2
		END

		COMMIT TRANSACTION
		END TRY
		BEGIN CATCH
				select @errNum = ERROR_NUMBER();
				select @ErrMsg = ERROR_MESSAGE();
				ROLLBACK TRANSACTION
				EXEC PL1000_SEL @errNum , @ErrMsg out,'ac1234_IU',@as_mode
				RAISERROR(@ErrMsg, 16 , 1 )
				RETURN
		END CATCH;
END
 








GO
