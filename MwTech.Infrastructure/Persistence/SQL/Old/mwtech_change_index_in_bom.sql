/****** Script for SelectTopNRows command from SSMS  ******/


declare @z as varchar(20);
declare @na as varchar(20);

set @z = 'MIE.SL4/052-F';
set @na = 'MIE.SL4/922_2_F';




SELECT *
  FROM [MwTech].[dbo].[Boms]
  where PartId = (select Id from [MwTech].[dbo].[Products] where ProductNumber = @z)

  SELECT *
  FROM [MwTech].[dbo].[Boms]
  where PartId = (select Id from [MwTech].[dbo].[Products] where ProductNumber = @na)


  UPDATE [MwTech].[dbo].[Boms]
  SET PartId = (select Id from [MwTech].[dbo].[Products] where ProductNumber = @na)
  where PartId = (select Id from [MwTech].[dbo].[Products] where ProductNumber = @z)



