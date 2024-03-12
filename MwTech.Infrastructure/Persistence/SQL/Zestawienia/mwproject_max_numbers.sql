/****** Script for SelectTopNRows command from SSMS  ******/
use MwProjects;

SELECT 
       [CategoryId]
	   ,ca.Name as kategoria
	   ,YEAR(CreatedDate) as rok
      ,max(No) + 1 as wolny_numer
 FROM [MwProjects].[dbo].[Projects] as pr
 inner join dbo.Categories as ca
 on ca.Id = pr.CategoryId
 group by CategoryId, year(CreatedDate), ca.Name
 order by CategoryId, year(CreatedDate), ca.Name

