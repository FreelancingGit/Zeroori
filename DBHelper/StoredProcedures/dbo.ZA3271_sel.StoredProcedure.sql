USE [Zeroori]
GO
/****** Object:  StoredProcedure [dbo].[ZA3271_sel]    Script Date: 24-02-2020 03:22:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[ZA3271_sel]
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
	@as_mode VARCHAR(2) ,
	@ai_mallMastId int,
	@as_sessionid varchar(1000)
)
AS
BEGIN
SET NOCOUNT ON
--declare @ai_page_count numeric(18,3) = 6.00
--declare @ai_startNo int
--declare @ai_endNo int
--declare @ai_start_ID int
--declare @ai_end_ID int
--declare @ai_total_Pages int

declare @ai_base_url varchar(1000)

	--IF( @ai_pageno is null ) 
	--	SET @ai_pageno = 1
	
	--SET @ai_endNo= ( @ai_pageno * @ai_page_count )+1
	--SET @ai_startNo   = @ai_endNo - @ai_page_count  



	--SELECT	@ai_start_ID =	MIN(mall_mast_id), 
	--		@ai_end_ID	=	MAX(mall_mast_id) 
	--FROM (
	--		SELECT ROW_NUMBER() OVER( ORDER BY mall_mast_id ASC ) AS Rno,
	--				mall_mast_id
	--		FROM ZA3270  )iTBL
	--WHERE RNO BETWEEN @ai_startNo AND @ai_endNo
	

	--SELECT	@ai_total_Pages = ceiling((count(mall_mast_id ) / @ai_page_count ))
	--FROM ZA3270

	IF( @as_mode = 'LO' or @as_mode = 'LD' )
	begin

		Select	ZA1000.usr_mast_id,
					@as_sessionid as SESSIONID,
					usr_FistNam as  FirstName
			from ZA1000
			inner join ZA3000 on ZA3000.usr_mast_id = ZA1000.usr_mast_id
			where sessionid = @as_sessionid

			
			-- tables 0
			select  ZA3270.mall_mast_id,
					mall_name,
					mall_location,
					mall_start_timing, 
					mall_end_timing,
					mall_phone as Phone,
					mall_emaild as email,
					mall_url,
					mall_decrp
			from ZA3270
			where mall_mast_id=@ai_mallMastId


			select mall_mast_img_path 
			from ZA3271 
			where  mall_mast_id=@ai_mallMastId


			--LEFT join (	
			--				Select * from (
			--				Select row_number() over( order by ZA3271.mall_mast_id ) as Rno ,
			--								ZA3271.mall_mast_id,
			--								mall_mast_img_path
			--				from ZA3271
			--				inner join ZA3270 on ZA3270.mall_mast_id = ZA3271.mall_mast_id
			--				--where ZA3600.stats like 'A' 
			--				)Itbl 
			--				where Rno = 1
			--			)ZA3271				on ZA3271.mall_mast_id		=	ZA3270.mall_mast_id
			--where  ZA3270.mall_mast_id =@ai_pageno

			

	END
END





GO
