USE [Zeroori]
GO
/****** Object:  UserDefinedFunction [dbo].[ZA1000_GetSID]    Script Date: 24-02-2020 03:22:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[ZA1000_GetSID] (  )
 RETURNS varchar(100)
begin

declare @sidVal varchar(100)
declare @sidsamp varchar(30)
declare @secCut int
declare @Cut  int

declare @sidVal1 varchar(30)
declare @sidsamp1 varchar(30)
declare @secCut1 int
declare @Cut1  int


set @Cut = 5;
set @secCut = 6 + @Cut

	select @sidVal = ltrim(rtrim( replace(replace(replace(replace(convert(varchar(30), getdate(),21),' ', '' ),':',''),'-',''),'.','')))
	select @sidsamp = substring( @sidVal, 5 , @Cut) 
	select @sidVal = substring( @sidVal, 1 , @Cut) + substring( @sidVal,6 , @Cut)  + @sidsamp +substring( @sidVal, @secCut , len(@sidVal ) -  @secCut )  
  

  
set @Cut1 = 3;
set @secCut1 = 7 + @Cut1

select @sidVal1 = ltrim(rtrim( replace(replace(replace(replace(convert(varchar(30), getdate(),21),' ', '' ),':',''),'-',''),'.','')))
select @sidsamp1 = substring( @sidVal1, 5 , @Cut1) 
select @sidVal1 = substring( @sidVal1, 1 , @Cut1) + substring( @sidVal1, 6 , @Cut1)  + @sidsamp1 +substring( @sidVal1, @secCut1 , len(@sidVal1 ) -  @secCut1 )  
  
select @sidVal =  @sidVal +    @sidVal1  

return @sidVal;
end










GO
