use MwTech;

IF OBJECT_ID('dbo.ifs_struktury','U') IS NOT NULL
	DROP TABLE dbo.ifs_struktury;

CREATE TABLE dbo.ifs_struktury
	( 
   LP VARCHAR(10)
 , CONTRACT VARCHAR(5)
 , PART_NO VARCHAR(50)
 , REVISION VARCHAR(10)
 , REVISION_NAME VARCHAR(255)
 , ALTERNATIVE VARCHAR(20)
 , ALTERNATIVE_DESCRIPTION VARCHAR(255)
 , STATE VARCHAR(255)
--
, LINE_ITEM_NO  VARCHAR(10)
, LINE_SEQUENCE VARCHAR(10)
, COMPONENT_PART VARCHAR(50)
, QTY_PER_ASSEMBLY DECIMAL(10,5)
, CONSUMPTION_ITEM_DB VARCHAR(25)
, PRINT_UNIT  VARCHAR(25)
, EFF_PHASE_IN_DATE DATETIME
, EFF_PHASE_OUT_DATE  DATETIME
, COMPONENT_SCRAP   DECIMAL(10,5)
, SHRINKAGE_FACTOR   DECIMAL(10,5)
, PART_STATUS VARCHAR(10)
)


GO

bulk insert dbo.ifs_struktury from 'c:\02\ifs_struktury.csv' 
-- with ( CODEPAGE = '65001', fieldterminator =';' ,rowterminator ='\n', FIRSTROW = 1, LASTROW = 2 )
with ( CODEPAGE = 'APC', fieldterminator =';' ,rowterminator ='\n', FIRSTROW = 2)
-- with ( CODEPAGE = 'ACP', fieldterminator =';' ,rowterminator ='\n' ) fieldquote = '"'


