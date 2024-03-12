use MWTech;
/* 
   Porównanie Marszrut MWTech -> IFS
   Najpierw generujemy csv po stronie Oracle
   potem importujemy za pomoc¹ bulk insert do dbo.IFS_marszruty
   nastêpnie porównujemy z widokiem dbo.IFS_marszruty
   w tym widoku mamy marszruty z MWTech przygotowane do zaimportowania do IFS
*/


IF OBJECT_ID('dbo.IFS_marszruty','U') IS NOT NULL
	DROP TABLE dbo.IFS_marszruty;

CREATE TABLE dbo.IFS_marszruty
	( 
   LP INT
 , CONTRACT VARCHAR(5)
 , PART_NO VARCHAR(255)
 , ALTERNATIVE_NO VARCHAR(5)
 , ROUTING_REVISION VARCHAR(5)
--
 , OPERATION_NO VARCHAR(5)
 , OPERATION_DESCRIPTION VARCHAR(255)
--
 , WORK_CENTER_NO VARCHAR(25)
 , MACH_RUN_FACTOR DECIMAL(10,5)
--
 , LABOR_CLASS_NO  VARCHAR(25)
 , LABOR_RUN_FACTOR  DECIMAL(10,5)
 , CREW_SIZE DECIMAL(10,5)
 --
 , RUN_TIME_CODE VARCHAR(25)
 --
 , SETUP_LABOR_CLASS_NO VARCHAR(25) 
 , MACH_SETUP_TIME  DECIMAL(10,5)
 , LABOR_SETUP_TIME  DECIMAL(10,5)
 , SETUP_CREW_SIZE  DECIMAL(10,5)
 --
 , MOVE_TIME  DECIMAL(10,5)
 , OVERLAP  DECIMAL(10,5)
 --
 -- , EFFICIENCY_FACTOR  DECIMAL(10,5)
 -- , LOT_QTY  DECIMAL(10,5)

	)

	go

-- 'c:\vs\WebApp\ASP.NET_CORE_CQRS\MWTechCqrs\MWTech.UI\ExcelFiles\Detki\pg_detki_marki_obce.csv' 

bulk insert dbo.IFS_marszruty from 'c:\02\IFS_marszruty.csv' 
-- with ( CODEPAGE = '65001', fieldterminator =';' ,rowterminator ='\n', FIRSTROW = 1, LASTROW = 2 )
with ( CODEPAGE = 'ACP', fieldterminator =';' ,rowterminator ='\n', FIRSTROW = 2 )
-- with ( CODEPAGE = 'ACP', fieldterminator =';' ,rowterminator ='\n' )



