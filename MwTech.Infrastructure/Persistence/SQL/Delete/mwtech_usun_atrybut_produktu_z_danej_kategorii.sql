/****** Script for SelectTopNRows command from SSMS  ******/
select ca.CategoryNumber,pr.ProductNumber
from dbo.ProductProperties as pp
inner join dbo.Products as pr
on pr.Id = pp.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.Properties as ppp
on ppp.Id = pp.PropertyId
where 1=1
and ca.CategoryNumber in ('DMA')
-- and ca.CategoryNumber in ('DST','DET','DKJ','DWU','DAP','DOB','DWY','DET-KBK')
and ppp.PropertyNumber = 'nazwa_mieszanki'


delete pp
from dbo.ProductProperties as pp
inner join dbo.Products as pr
on pr.Id = pp.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.Properties as ppp
on ppp.Id = pp.PropertyId
where 1=1
and ca.CategoryNumber in ('DMA')
-- and ca.CategoryNumber in ('DST','DET','DKJ','DWU','DAP','DOB','DWY','DET-KBK')
and ppp.PropertyNumber = 'nazwa_mieszanki'
