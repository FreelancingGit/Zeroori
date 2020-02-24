USE [Zeroori]
GO
/****** Object:  StoredProcedure [dbo].[ZAJobList_Sel]    Script Date: 24-02-2020 03:22:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[ZAJobList_Sel]
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
	@as_mode VARCHAR(2) ,
	@as_sessionid varchar(1000)
)
AS
BEGIN
SET NOCOUNT ON
DECLARE @ai_usr_mast_id INT

declare @ai_base_url varchar(1000),
		@ai_defaultUlrM varchar(1000),
		@ai_defaultUlrF varchar(1000)

	
	Select @ai_base_url = dbo.getBaseUrl( )

	Set @ai_defaultUlrM = @ai_base_url + '/images/classifieds.jpg'

	IF( @as_mode = 'LO' or @as_mode = 'LD' )
	begin


			SET @ai_usr_mast_id=(Select	ZA1000.usr_mast_id 
			from ZA1000
			inner join ZA3000 on ZA3000.usr_mast_id = ZA1000.usr_mast_id
			where sessionid = @as_sessionid)

			-- tables 0
			select emp_job_mast_id,
				   title,
				   Description as descrptn,
				   case
						when ZA3630.FldrName is not null then  @ai_base_url +'/'+ ZA3630.FldrName+'/ProPic.Jpg'
						else 
								case
									when gender like 'M' then @ai_defaultUlrM
									else @ai_defaultUlrF
								END
				   end as proimg ,
				   'JW' as jobtype
				   from ZA3630  
				   where  ZA3630.usr_mast_id=@ai_usr_mast_id 
				   
				   union all

			select compny_job_mast_id,
				   job_title as title,
				   descrpn_step_one as  descrptn,
				   compny_logo_img as proimg ,
				   'JH' as jobtype
				   from ZA3650 
				   where  ZA3650.usr_mast_id=@ai_usr_mast_id 
				   union all

			select frelnc_emp_job_mast_id,
				   title,Description as descrptn,
				    case
						when ZA3670.FldrName is not null then  @ai_base_url +'/'+ ZA3670.FldrName+'/ProPic.Jpg'
						else 
								case
									when gender like 'M' then @ai_defaultUlrM
									else @ai_defaultUlrF
								END
					end as proimg ,
				   'FW' as jobtype
				   from ZA3670
				   where  ZA3670.usr_mast_id=@ai_usr_mast_id 
				   union all

			select frelnc_comp_job_mast_id,
				   job_title as title,descrpn_step_one as  descrptn,
				   compny_logo_img as proimg ,
				   'FH' as jobtype
				   from ZA3680 
				   where  ZA3680.usr_mast_id=@ai_usr_mast_id 
			
			--AND ZA3620.stats like 'A'
			--ORDER BY crtd_dt desc

			Select	ZA1000.usr_mast_id,
					@as_sessionid as SESSIONID,
					usr_FistNam as  FirstName
			from ZA1000
			inner join ZA3000 on ZA3000.usr_mast_id = ZA1000.usr_mast_id
			where sessionid = @as_sessionid
			

	END

END
 












GO
