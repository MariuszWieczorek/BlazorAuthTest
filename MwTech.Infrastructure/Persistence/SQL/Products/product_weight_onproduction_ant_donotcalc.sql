select pr.ProductNumber, b.DoNotIncludeInWeight, b.OnProductionOrder
from dbo.Boms as b
inner join dbo.Products as pr
on pr.Id = b.setid
where 1 = 1
and
(
(
    b.OnProductionOrder = 1
and b.DoNotIncludeInTkw = 1
)
)
