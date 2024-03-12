select pr.ProductNumber, u.UnitCode, v.ProductQty
from products as pr
inner join dbo.Units as u
on u.id = pr.UnitId
inner join dbo.ProductVersions as v
on v.ProductId = pr.Id
where u.UnitCode = 'szt.'
and v.ProductQty != 1