/****** Script for SelectTopNRows command from SSMS  ******/
/* Prêdkoœæ przeliczania TKW */
select y.CategoryNumber, y.mi, y.mx, y.diff, y.razem, y.ile
,y.razem - y.ile as rest
,y.speed
,cast(  round(  iif(y.speed <> 0, (y.razem - y.ile) / y.speed,0),2) as numeric(10,2))  as czas
from
(
  select x.OrdinalNumber, x.CategoryNumber, mi,mx, DATEDIFF(minute,mi,mx) as diff
  , ( select COUNT(*) from dbo.Products as p inner join dbo.ProductCategories as ca on ca.Id = p.ProductCategoryId where ca.CategoryNumber = x.CategoryNumber ) as razem
  , ile 
  , iif(DATEDIFF(minute,mi,mx) > 0, ile / DATEDIFF(minute,mi,mx),0) as speed
  from(
  SELECT 
     ca.OrdinalNumber,ca.CategoryNumber, MAX(c.CalculatedDate) as mx,MIN(c.CalculatedDate) as mi, COUNT(*) as ile
  FROM [MwTech].[dbo].[ProductCosts] as c
  inner join dbo.Products as p
  on p.Id = c.ProductId
  inner join dbo.Currencies as cu
  on cu.Id = c.CurrencyId
  inner join dbo.ProductCategories as ca
  on ca.Id = p.ProductCategoryId
  where c.AccountingPeriodId = (select Id from dbo.AccountingPeriods where PeriodNumber = '2022-12')
  and ca.CategoryNumber IN ('DWY','DOB','DAP','DST','DWU','DWU-B','DKJ','DKJ-B','DET','DET-B','DET-KBK','WW-PROF','WW-PPROF','WW-ORN','WW-AKL','WF-AKC','WF-AKC-B','WF-AKC-POZ')
  and datediff(minute,c.CalculatedDate,GETDATE()) < 2000
  group by ca.CategoryNumber,ca.OrdinalNumber
  ) as x
  ) as y
  order by y.OrdinalNumber



  





