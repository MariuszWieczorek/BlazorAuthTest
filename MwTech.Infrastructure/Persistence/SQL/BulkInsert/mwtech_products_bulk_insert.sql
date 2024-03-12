use MwTech;

IF OBJECT_ID('dbo.import_pg','U') IS NOT NULL
	DROP TABLE dbo.import_pg;

CREATE TABLE dbo.import_pg
	( 
	  indeks			VARCHAR(50)
	, opis				VARCHAR(255)
	, jm	 			VARCHAR(50)
	, grupa	 			VARCHAR(50)
	, matka				VARCHAR(50)
	, old				VARCHAR(50)
	, waz				VARCHAR(50)
	)

	go
-- 'c:\vs\WebApp\ASP.NET_CORE_CQRS\MwTechCqrs\MwTech.UI\ExcelFiles\Detki\pg_detki_marki_obce.csv' 

bulk insert dbo.import_pg from 'c:\02\pg_detki_marki_obce.csv' 
with ( CODEPAGE = '65001', fieldterminator =';' ,rowterminator ='\n' )

select i.indeks as productNumber, 
i.opis as Name,
i.matka as idx1,
i.waz as idx2,
i.old as oldProductNumber,
u.Id as UnitId,
ca.id as ProductCategoryId
from dbo.import_pg as i
left join dbo.Units as u
on u.UnitCode = i.jm
left join dbo.ProductCategories as ca
on ca.CategoryNumber = i.grupa
where u.Id is not null

/*
insert into [MwTech].[dbo].[Products]
  (
       ProductNumber
      ,Name
      ,Idx01
      ,Idx02
      ,OldProductNumber
      ,UnitId
      ,ProductCategoryId
      ,Client
	  ,CreatedDate
	  ,CreatedByUserId
  )
  (
  select i.indeks as productNumber, 
	i.opis as Name,
	i.matka as idx1,
	i.waz as idx2,
	i.old as oldProductNumber,
	u.Id as UnitId,
	ca.id as ProductCategoryId,
	1 as Client,
	GETDATE() as CreatedDate,
	'7f2bacf6-564a-4272-a4bb-f76832476024' as CreatedByUserId
	from dbo.import_pg as i
	left join dbo.Units as u
	on u.UnitCode = i.jm
	left join dbo.ProductCategories as ca
	on ca.CategoryNumber = i.grupa
	where u.Id is not null
  )
  */

