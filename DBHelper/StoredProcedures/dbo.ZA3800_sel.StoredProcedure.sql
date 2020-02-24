USE [Zeroori]
GO
/****** Object:  StoredProcedure [dbo].[ZA3800_sel]    Script Date: 24-02-2020 03:22:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ZA3800_sel]
/*------------------------------------------------------------------------------
* Name         : ac1234_iu
* Purpose      : 9ic
* Author       : Rony
* Date Created : 08-Apr-2019
* C# Class Name: ac1234
* -------------------------------------------------------------------------------
* Modification History : 
* [Date]                 [Name OF The Person]    [What IS The Modification Done] 
* -------------------------------------------------------------------------------*/
(
	@as_mode VARCHAR(3) ,
	@as_sessionid varchar(1000)
)
AS
DECLARE @ai_usr_mast_id INT

DECLARE @ai_motors_count INT=0
DECLARE @ai_prop_count INT=0
DECLARE @ai_clasfds_count INT=0
DECLARE @ai_jobs_JW_counts INT=0
DECLARE @ai_jobs_JH_counts INT=0
DECLARE @ai_jobs_FW_counts INT=0
DECLARE @ai_jobs_FH_counts INT=0

BEGIN


SET NOCOUNT ON


	IF( @as_mode = 'LO' or @as_mode = 'LD' )
	begin

			SET @ai_usr_mast_id=(Select	ZA1000.usr_mast_id 
			from ZA1000
			inner join ZA3000 on ZA3000.usr_mast_id = ZA1000.usr_mast_id
			where sessionid = @as_sessionid)


			SET @ai_motors_count = (SELECT COUNT(*) FROM ZA3600 where ZA3600.usr_mast_id=@ai_usr_mast_id )
			SET @ai_prop_count = (SELECT COUNT(*) FROM ZA3610 where ZA3610.usr_mast_id=@ai_usr_mast_id )
			SET @ai_clasfds_count = (SELECT COUNT(*) FROM ZA3620 where ZA3620.usr_mast_id=@ai_usr_mast_id )

			SET @ai_jobs_JW_counts = (SELECT COUNT(*) FROM ZA3630 where ZA3630.usr_mast_id=@ai_usr_mast_id )
			SET @ai_jobs_JH_counts = (SELECT COUNT(*) FROM ZA3650 where ZA3650.usr_mast_id=@ai_usr_mast_id )
			SET @ai_jobs_FW_counts = (SELECT COUNT(*) FROM ZA3670 where ZA3670.usr_mast_id=@ai_usr_mast_id )
			SET @ai_jobs_FH_counts = (SELECT COUNT(*) FROM ZA3680 where ZA3680.usr_mast_id=@ai_usr_mast_id )

			SELECT @ai_motors_count AS motors_count,
				   @ai_prop_count AS prop_count,
				   @ai_clasfds_count AS clasfds_count,
				   @ai_jobs_JW_counts AS jobs_JW_counts,
				   @ai_jobs_JH_counts AS jobs_JH_counts,
				   @ai_jobs_FW_counts AS jobs_FW_counts,
				   @ai_jobs_FH_counts AS jobs_FH_counts
			
	END
	
END





GO
