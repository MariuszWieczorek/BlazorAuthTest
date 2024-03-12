/* Export w³aœciwoœci do IFS */
CREATE OR ALTER VIEW mwtech_product_properties_2
AS
(
select *
from
(
select ca.CategoryNumber,
ca.Name as CategoryName,
pr.ProductNumber,
pr.Name as ProductName,
atr.PropertyNumber,
atr.PropertyName,
atr.text,
atr.MinValue,
atr.Value,
atr.MaxValue,
atr.UnitName,
pr.OldProductNumber,
pr.Idx01,
pr.Idx02
from mwtech_product_properties as atr
inner join dbo.Products as pr
on pr.Idx01 = atr.ProductNumber
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
where pr.ProductCategoryId = atr.PropertyProductCategoryId
union all
select ca.CategoryNumber,
ca.Name as CategoryName,
pr.ProductNumber,
pr.Name as ProductName,
atr.PropertyNumber,
atr.PropertyName,
atr.text,
atr.MinValue,
atr.Value,
atr.MaxValue,
atr.UnitName,
pr.OldProductNumber,
pr.Idx01,
pr.Idx02
from mwtech_product_properties as atr
inner join dbo.Products as pr
on pr.Idx02 = atr.ProductNumber
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
where pr.ProductCategoryId = atr.PropertyProductCategoryId
) as x
)