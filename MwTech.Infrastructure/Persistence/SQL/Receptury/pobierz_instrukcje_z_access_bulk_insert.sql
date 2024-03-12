use MwTech;

-- w Access F11 aby zobaczyæ panel z tabelami i widokami
-- tabela instrukcja_wykonania_kw
-- export -> Excel
-- natêpnie w Excelu zapisaæ jako csv

IF OBJECT_ID('dbo.access_mieszanki_instrukcje','U') IS NOT NULL
	DROP TABLE dbo.access_mieszanki_instrukcje;

CREATE TABLE dbo.access_mieszanki_instrukcje
	( 
-- Id INT not null identity(1,1) 
   IDM INT
 , MIESZANKA  NVARCHAR(100)
 , CYKL INT
 , LP INT
 , DESCRIPTION NVARCHAR(MAX)
 , TEXTVAL  NVARCHAR(400)
)


GO


bulk insert dbo.access_mieszanki_instrukcje from 'c:\02\instrukcja_wykonania_kw_20231124.csv' 
-- with ( CODEPAGE = '65001', fieldterminator =';' ,rowterminator ='\n', FIRSTROW = 1, LASTROW = 2 )
with ( CODEPAGE = '65001', fieldterminator =';' ,rowterminator ='\n', FIRSTROW = 2)
-- with ( CODEPAGE = 'ACP', fieldterminator =';' ,rowterminator ='\n', FIRSTROW = 2)
-- with ( CODEPAGE = 'ACP', fieldterminator =';' ,rowterminator ='\n' ) fieldquote = '"'


