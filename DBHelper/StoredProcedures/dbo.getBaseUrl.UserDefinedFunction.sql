USE [Zeroori]
GO
/****** Object:  UserDefinedFunction [dbo].[getBaseUrl]    Script Date: 24-02-2020 03:22:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[getBaseUrl](  )
 RETURNS varchar(100)
begin

declare @ai_URL varchar(100)
 
		select @ai_URL = 'http://localhost:51612'
		--select @ai_URL = 'http://za.skewedinfo.com'
		--select @ai_URL = 'https://zeroori.com'


return @ai_URL;
end











GO
