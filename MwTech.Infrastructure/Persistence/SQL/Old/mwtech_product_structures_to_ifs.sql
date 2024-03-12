/* Przygotowanie BOM do IFS'a */

DECLARE @umiejscowienie as varchar(20)
DECLARE @wariant as char(10)
DECLARE @wariant_opis as varchar(20)
DECLARE @metoda_wydan as char(10)
SET @umiejscowienie = 'KT1'
SET @wariant = '*'
SET @wariant_opis = ' '
SET @metoda_wydan = 'A'

Select @umiejscowienie as umiejscowienie,
CAST(REPLACE(x.SetProductNumber,'_','-') as varchar(50)) as numer_pozycji_nadrzednej,
@wariant as wariant,
@wariant_opis as wariant_opis,
x.ComponentOrdinalNo as nr_pozycji_w_linii,
CAST(REPLACE(x.ComponentProductNumber,'_','-') as varchar(50)) as numer_komponentu,
x.PartQty as norma_zuzycia,
x.Excess as wsp_proc_nadmiaru,
iif(x.OnProductionOrder=1,'T','N') as zuzycie_komponentu_w_zleceniu,
@metoda_wydan as metoda_wydan,
CategoryNumber,
CategoryName
FROM (
SELECT zpr.ProductNumber as SetProductNumber,
	   zpr.Name as SetProductName,
	   zpc.Name as SetCategoryName,
       b.SetId
      ,b.SetVersionId
	  ,b.PartId
      ,b.PartQty
	  ,cpr.ProductNumber as ComponentProductNumber
	  ,cpr.Name as ComponentProductName
      ,b.OrdinalNumber as ComponentOrdinalNo
      ,b.Excess
      ,b.OnProductionOrder
	  ,zpc.OrdinalNumber as SetCategoryOrdinalNo
	  ,zpc.CategoryNumber
	  ,zpc.Name as CategoryName
  FROM [MwTech].[dbo].[Boms] as b
  inner join [MwTech].[dbo].[ProductVersions] as zpv
  on zpv.ProductId = b.SetId and zpv.Id = b.SetVersionId
  inner join [MwTech].[dbo].[Products] as zpr
  on zpr.Id = zpv.ProductId
  inner join [MwTech].[dbo].[ProductCategories] as zpc
  on zpc.Id = zpr.ProductCategoryId
  inner join [MwTech].[dbo].[Products] as cpr
  on cpr.Id = b.PartId
  where zpv.DefaultVersion = 1
  and zpc.CategoryNumber not in ('MIE')
  ) AS x
  order by x.SetCategoryOrdinalNo,x.SetProductNumber, x.ComponentOrdinalNo
