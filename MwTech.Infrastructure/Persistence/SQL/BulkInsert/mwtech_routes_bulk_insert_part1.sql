/* import bomów na podstawie dbo.import_boms czêœæ 1
   tworzenie dbo.import_boms na podstawie pliku csv
   poprzez bulk insert 
 */

use MwTech;

IF OBJECT_ID('dbo.import_routes','U') IS NOT NULL
	DROP TABLE dbo.import_routes;

CREATE TABLE dbo.import_routes
	( 
	  umiejscowienie				VARCHAR(50)		-- A umiejscowienie
	, productNumber					VARCHAR(255)	-- B nr_pozycji
	, alternative					VARCHAR(50)		-- C wariant_struktury
	, alternativeName				VARCHAR(50)		-- D opis_wariantu
	, OrdinalNumber					INT				-- E numer_operacji
	, OperationNumber				VARCHAR(50)		-- F opis_operacji
	, WorkCenterNumber				VARCHAR(50)		-- G nr_gniazda
	, ChangeOverMachineConsumption	DECIMAL(14,10)  -- H przezbr_maszynochlonnosc	
	, ChangeOverLabourConsumption	DECIMAL(14,10)	-- I przezbr_pracochlonnosc
	, ChangeOverResourceNumber 	    VARCHAR(50)		-- J przezbr_kategoria_zaszeregownia
	, ChangeOverNumberOfEmployee	DECIMAL(14,10)	-- K przezbr_ilosc_pracownikow
	, OperationMachineConsumption	DECIMAL(14,10)	-- L maszynochlonnosc
	, OperationLabourConsumption	DECIMAL(14,10)	-- M pracoch³onnoœæ
	, UnitName						VARCHAR(50)		-- N JM
	, ResourceNumber  				VARCHAR(50)		-- O Kategoria_zaszeregowania
    , ResourceQty					DECIMAL(14,10)	-- P wielkosc_brygady
	, MoveTime						DECIMAL(14,10)	-- Q czas_transportu
	, Overlap						DECIMAL(14,10)	-- R zachodzenie
	, Field1						VARCHAR(50)		-- S jedn_zachodzenia
	, Field2						VARCHAR(50)		-- T operacja_rownolegla
	)


	go


bulk insert dbo.import_routes from 'c:\02\routes_detki_20230111b.csv' 
with ( CODEPAGE = '65001', fieldterminator =';' ,rowterminator ='\n' )

select i.umiejscowienie as umiejscowienie
	  ,i.ProductNumber as ProductNumber
	  ,s.id as ProductId
	  ,iif(i.alternative='*',0,i.alternative) as alt
	  ,i.alternativeName as altName
	  ,i.OperationNumber
	  ,o.id as OperationId
      ,i.Resourcenumber
	  ,r.Id as ResourceId
      ,i.OrdinalNumber as OrdinalNumber
      ,i.ResourceQty
	  ,i.WorkCenterNumber
      ,w.Id as WorkCenterId
      ,i.OperationLabourConsumption
      ,i.OperationMachineConsumption
      ,i.ChangeOverLabourConsumption
      ,i.ChangeOverMachineConsumption
      ,i.ChangeOverNumberOfEmployee
      ,i.ChangeOverResourceNumber
	  ,co.Id as ChangeOverResourceId
      ,0 as RouteVersionId
      ,i.MoveTime
      ,i.Overlap
		from dbo.import_routes as i
		left join dbo.Products as s
		on s.ProductNumber = i. productNumber
		left join dbo.Operations as o
		on o.OperationNumber = i.OperationNumber
		left join dbo.Resources as r
		on r.ResourceNumber = i.ResourceNumber
		left join dbo.Resources as w
		on w.ResourceNumber = i.WorkcenterNumber
		left join dbo.Resources as co
		on co.ResourceNumber = i.ChangeOverResourceNumber


/* Kontrola czy jest wszystko */

		select i.umiejscowienie as umiejscowienie
	  ,i.ProductNumber as ProductNumber
	  ,s.id as ProductId
	  ,iif(i.alternative='*',0,i.alternative) as alt
	  ,i.alternativeName as altName
	  ,i.OperationNumber
	  ,o.id as OperationId
      ,i.Resourcenumber
	  ,r.Id as ResourceId
      ,i.OrdinalNumber as OrdinalNumber
      ,i.ResourceQty
	  ,i.WorkCenterNumber
      ,w.Id as WorkCenterId
      ,i.OperationLabourConsumption
      ,i.OperationMachineConsumption
      ,i.ChangeOverLabourConsumption
      ,i.ChangeOverMachineConsumption
      ,i.ChangeOverNumberOfEmployee
      ,i.ChangeOverResourceNumber
	  ,co.Id as ChangeOverResourceId
      ,0 as RouteVersionId
      ,i.MoveTime
      ,i.Overlap
		from dbo.import_routes as i
		left join dbo.Products as s
		on s.ProductNumber = i.productNumber
		left join dbo.Operations as o
		on o.OperationNumber = i.OperationNumber
		left join dbo.Resources as r
		on r.ResourceNumber = i.ResourceNumber
		left join dbo.Resources as w
		on w.ResourceNumber = i.WorkcenterNumber
		left join dbo.Resources as co
		on co.ResourceNumber = i.ChangeOverResourceNumber
		where co.id = null or w.id = null or r.id = null or o.id = null or s.id = null



