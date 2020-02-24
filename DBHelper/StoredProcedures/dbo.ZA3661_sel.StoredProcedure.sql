USE [Zeroori]
GO
/****** Object:  StoredProcedure [dbo].[ZA3661_sel]    Script Date: 24-02-2020 03:22:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ZA3661_sel]
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
	@ai_pageno int,
	@as_sessionid varchar(1000),
	@ai_dir_dtl_id int
)
AS
BEGIN
SET NOCOUNT ON
declare @ai_page_count numeric(18,3) = 6.00
declare @ai_startNo int
declare @ai_endNo int
declare @ai_start_ID int
declare @ai_end_ID int
declare @ai_total_Pages int

declare @ai_base_url varchar(1000)

	IF( @ai_pageno is null ) 
		SET @ai_pageno = 1
	
	SET @ai_endNo= ( @ai_pageno * @ai_page_count )+1
	SET @ai_startNo   = @ai_endNo - @ai_page_count  



	SELECT	@ai_start_ID =	MIN(dir_dtl_id), 
			@ai_end_ID	=	MAX(dir_dtl_id) 
	FROM (
			SELECT ROW_NUMBER() OVER( ORDER BY dir_dtl_id ASC ) AS Rno,
					dir_dtl_id
			FROM ZA3661  )iTBL
	WHERE RNO BETWEEN @ai_startNo AND @ai_endNo
	

	SELECT	@ai_total_Pages = ceiling((count(dir_dtl_id ) / @ai_page_count ))
	FROM ZA3661

	IF( @as_mode = 'LI' or @as_mode = 'LD' )
	begin
			Select @ai_base_url = dbo.getBaseUrl( )
			
			Select	ZA1000.usr_mast_id,
					usr_FistNam as  FirstName,
					@as_sessionid as SESSIONID
			from ZA1000
			inner join ZA3000 on ZA3000.usr_mast_id = ZA1000.usr_mast_id
			where sessionid = @as_sessionid


			-- tables 0
			select  dir_dtl_id,
					comp_name,
					addrs,
					phone_1, 
					Mobile,
					email 
					
			from ZA3661
			where  ZA3661.dir_dtl_id BETWEEN @ai_start_ID AND @ai_end_ID

			if( @ai_pageno + 5 >= @ai_total_Pages )
			begin
					Set @ai_pageno = 
							case
								when @ai_total_Pages -5 < 1 then 1
								else @ai_total_Pages -5
							end
			END
			-- tables 1
			select	@ai_pageno as Page_No ,
					@ai_total_Pages TotalPages

	END
	IF( @as_mode = 'DD' )
	begin
			Select @ai_base_url = dbo.getBaseUrl( )
			
			-- tables 0
			select  dir_dtl_id,
					comp_name,
					addrs,
					phone_1, 
					Mobile,
					email ,
					fax,
					web
					
			from ZA3661
			where  ZA3661.dir_dtl_id =@ai_dir_dtl_id

	END
END



GO
