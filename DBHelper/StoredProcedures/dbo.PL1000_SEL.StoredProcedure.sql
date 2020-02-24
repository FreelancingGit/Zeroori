USE [Zeroori]
GO
/****** Object:  StoredProcedure [dbo].[PL1000_SEL]    Script Date: 24-02-2020 03:22:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PL1000_SEL]
/*------------------------------------------------------------------------------
* Name : PL20050_IU
* Purpose : ERROR LOG 
* Author : Rony John
* Date alterd : 03-APR-2012
* C# Class Name: ERROR LOG INSERTION SP
* -------------------------------------------------------------------------------
* Modification History : 
* [Date] [Name OF The Person] [What IS The Modification Done] 
* -------------------------------------------------------------------------------*/
( 
	 @ERRNum INT,
	 @ERRMSG VARCHAR(1000) out,
	 @SP_NAME VARCHAR(1000),
	 @MODE VARCHAR(2),
	 @ErrorContent varchar(1000) = null
	 --XXXXXX
 
)
AS
BEGIN
SET NOCOUNT ON;
 DECLARE @StartIndex INT;
 DECLARE @EndIndex INT;
		
		Set @ERRMSG = upper( @ERRMSG )
		SELECT @StartIndex =  CHARINDEX('CK_', @ERRMSG);

		if(@StartIndex > 0 )
		BEGIN
				if( len( @ERRMSG ) < 9 )
				begin 
						set @ERRMSG =replace( @ERRMSG,'CK_', '') ;
						set @ERRMSG =replace( @ERRMSG,'CK', '') ;
						set @ERRMSG =replace( @ERRMSG,'_', '')  ;
				END
				ELSE
				begin
						SELECT @StartIndex +=3
						SELECT @EndIndex =  CHARINDEX('_', @ERRMSG,@StartIndex)+1;
						set @ERRMSG =SUBSTRING( @ERRMSG,@EndIndex, 4 ) ;
				END

				Select  @ERRMSG =error_msg+' [ '+isnull( @ErrorContent,'')+' ] '
				From PL1022
				where error_num = @ERRMSG
		end
		ELSE IF( @ERRNum = 2627 )
				set @ERRMSG = 'PLErrorRecord Already Exist';
		else IF( @ERRNum = 547 and      CHARINDEX('The INSERT statement conflicted with the CHECK constraint' , @ERRMSG) > 0   )
		begin	
	
			-- SELECT  @ERRMSG = RIGHT(@ERRMSG, CHARINDEX(' ', REVERSE(' ' + @ERRMSG)) - 1)  
				set @ERRMSG = 'PLErrorEnter A valid data in ' + @ERRMSG;
 
		END
		ELSE IF(@ERRNum =515)
		BEGIN
				INSERT INTO dbo.PL1000(err_msg,err_num,gui_name,as_mode)
				VALUES (@ERRMSG,@ERRNum,@SP_NAME,@MODE)
				set @ERRMSG = 'PLErrorEmpty Field Not Allowed'; 
		END
		ELSE IF(@ERRNum =547)
		BEGIN
				INSERT INTO dbo.PL1000(err_msg,err_num,gui_name,as_mode)
				VALUES (@ERRMSG,@ERRNum,@SP_NAME,@MODE)
				set @ERRMSG = 'PLErrorForiengn Key Violation'; 
		END 
		ELSE IF(@ERRNum =8152)
		BEGIN
				INSERT INTO dbo.PL1000(err_msg,err_num,gui_name,as_mode)
				VALUES (@ERRMSG,@ERRNum,@SP_NAME,@MODE)
				set @ERRMSG = 'PLErrorField Size Limit Exceeds'; 
		END 
		ELSE IF(@ERRNum =50000)
		BEGIN
				SET @ERRMSG='PLError'+@ERRMSG
		END 
		ELSE IF(@ERRNum =2601)
		BEGIN
				SET @ERRMSG='Record Already Exists'
		END 
		ELSE
		BEGIN
				INSERT INTO dbo.PL1000(err_msg,err_num,gui_name,as_mode)
				VALUES (@ERRMSG,@ERRNum,@SP_NAME,@MODE)
				SET @ERRMSG='PLError'+CAST (@ERRNUM AS VARCHAR)+@ERRMSG 
		END
	 
			Set @ERRMSG = replace( @ERRMSG,'XXXXXX', isnull( @ErrorContent,'')) ;
END 













GO
