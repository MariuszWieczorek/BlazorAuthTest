use MwTech;

IF OBJECT_ID('dbo.ifs_inventory_part_in_stock','U') IS NOT NULL
	DROP TABLE dbo.ifs_inventory_part_in_stock;

CREATE TABLE dbo.ifs_inventory_part_in_stock
	( 
	  productNo			VARCHAR(255)
	)

	go

-- 'c:\vs\WebApp\ASP.NET_CORE_CQRS\MwTechCqrs\MwTech.UI\ExcelFiles\Detki\pg_detki_marki_obce.csv' 

bulk insert dbo.ifs_inventory_part_in_stock from 'c:\02\stany_stany_a.csv' 
-- with ( CODEPAGE = '65001', fieldterminator =';' ,rowterminator ='\n', FIRSTROW = 1, LASTROW = 2 )
with ( CODEPAGE = '65001', fieldterminator =';' ,rowterminator ='\n' )
-- with ( CODEPAGE = 'ACP', fieldterminator =';' ,rowterminator ='\n' )

select * from dbo.ifs_inventory_part_in_stock

