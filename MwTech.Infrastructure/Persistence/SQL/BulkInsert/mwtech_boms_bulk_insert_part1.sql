/* import bomów na podstawie dbo.import_boms czêœæ 1
   tworzenie dbo.import_boms na podstawie pliku csv
   poprzez bulk insert 
 */

use MwTech;

IF OBJECT_ID('dbo.import_boms','U') IS NOT NULL
	DROP TABLE dbo.import_boms;

CREATE TABLE dbo.import_boms
	( 
	  umiejscowienie	VARCHAR(50)
	, productNo			VARCHAR(255)
	, alternative		VARCHAR(50)
	, alternativeName	VARCHAR(50)
	, No				int
	, partNo			VARCHAR(50)
	, partQty			DECIMAL(14,10)
	, excess			DECIMAL(14,10)
	, onProdOrder		VARCHAR(50)
	, metoda			VARCHAR(50)
	)

	go
-- 'c:\vs\WebApp\ASP.NET_CORE_CQRS\MwTechCqrs\MwTech.UI\ExcelFiles\Detki\pg_detki_marki_obce.csv' 

bulk insert dbo.import_boms from 'c:\02\boms_profile.csv' 
with ( CODEPAGE = '65001', fieldterminator =';' ,rowterminator ='\n' )



select i.umiejscowienie
,i.productNo as setProductNo
,s.id as setProductId
,iif(i.alternative='*',0,i.alternative) as alt
,i.alternativeName as altName
,i.No
,i.partNo as partProductNo
,p.id as partProductId
,i.partQty
,i.excess
,i.onProdOrder
,i.metoda
from dbo.import_boms as i
left join dbo.Products as s
on s.ProductNumber = i. productNo
left join dbo.Products as p
on p.ProductNumber = i.partNo
where s.id is null or p.id is null




