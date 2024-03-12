/* import bomów na podstawie dbo.import_boms czêœæ 1
   tworzenie dbo.import_boms na podstawie pliku csv
   poprzez bulk insert 
 */

use MwTech;

IF OBJECT_ID('dbo.import_pg','U') IS NOT NULL
	DROP TABLE dbo.import_pg;

CREATE TABLE dbo.import_pg
	( 
	  ProductNumber					VARCHAR(255)	-- A nr_pozycji
	, ProductName					VARCHAR(255)	-- B nazwa
	, UnitCode						VARCHAR(50)		-- C jm
	, ProductCategoryNumber			VARCHAR(50)		-- D kategoria
	, Idx01							VARCHAR(50)		-- E matka
	, OldProductNumber				VARCHAR(50)		-- F 
	, Idx02							VARCHAR(50)		-- G 
	)


	go


bulk insert dbo.import_pg from 'c:\02\pg_detki_update.csv' 
with ( CODEPAGE = '65001', fieldterminator =';' ,rowterminator ='\n' )

select   i.ProductNumber
		,i.ProductName
		,i.UnitCode				
		,i.ProductCategoryNumber	
		,i.Idx01					
		,i.OldProductNumber		
		,i.Idx02					
	    ,p.id as ProductId
		,u.id as UnitId
		from dbo.import_pg as i
		left join dbo.Products as p
		on p.ProductNumber = i. productNumber
		left join dbo.Units as u
		on u.UnitCode = i.UnitCode


		update p
		set OldProductNumber = i.OldProductNumber,
		Idx01 = i.Idx01,
		Idx02 = i.Idx02
		from dbo.import_pg as i
		left join dbo.Products as p
		on p.ProductNumber = i. productNumber
		left join dbo.Units as u
		on u.UnitCode = i.UnitCode


