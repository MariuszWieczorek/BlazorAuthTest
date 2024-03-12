
--update t
--set Ean13Code = b.ean13
SELECT b.indeksHandlowy  ,b.ean13
  FROM [mwbase].[prdkabat].[opony_wulk] as b
  inner join mwtech.dbo.Products as t
  on t.ProductNumber = b.indeksHandlowy

         