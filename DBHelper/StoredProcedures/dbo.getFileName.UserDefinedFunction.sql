USE [Zeroori]
GO
/****** Object:  UserDefinedFunction [dbo].[getFileName]    Script Date: 24-02-2020 03:22:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[getFileName](  )
 RETURNS varchar(100)
begin

declare @ai_fileName varchar(100)
 
		select @ai_fileName ='Z'+format(getdate(),'mmssHH')+'A.jpg'

return @ai_fileName;
end










GO
