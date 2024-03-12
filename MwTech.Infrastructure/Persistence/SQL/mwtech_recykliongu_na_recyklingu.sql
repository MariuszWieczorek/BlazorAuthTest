/****** Script for SelectTopNRows command from SSMS  ******/
SELECT 
      [ProductNumber]
      ,[Name]
	  ,REPLACE(name,'recykliongu','RECYKLINGU')
  FROM [MwTech].[dbo].[Products]
  where name like '%recykliongu%'

  update p set 
  name = REPLACE(name,'recykliongu','RECYKLINGU')
  FROM [MwTech].[dbo].[Products] as p
  where name like '%recykliongu%'