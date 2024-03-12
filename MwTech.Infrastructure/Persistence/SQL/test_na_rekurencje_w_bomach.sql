select b.SetId, b.PartID, pr.ProductNumber, pp.ProductNumber
from dbo.Boms as b
inner join dbo.Products as pr
on pr.Id = b.SetId
inner join dbo.Products as pp
on pp.Id = b.PartId
where pr.ProductNumber = 'AKC300'

select b.SetId, b.PartID, pr.ProductNumber, pp.ProductNumber
from dbo.Boms as b
inner join dbo.Products as pr
on pr.Id = b.SetId
inner join dbo.Products as pp
on pp.Id = b.PartId
where b.SetId = 95690

select b.SetId, b.PartID, pr.ProductNumber, pp.ProductNumber
from dbo.Boms as b
inner join dbo.Products as pr
on pr.Id = b.SetId
inner join dbo.Products as pp
on pp.Id = b.PartId
where b.SetId = 96375

select b.SetId, b.PartID, pr.ProductNumber, pp.ProductNumber
from dbo.Boms as b
inner join dbo.Products as pr
on pr.Id = b.SetId
inner join dbo.Products as pp
on pp.Id = b.PartId
where b.SetId = 96375 and b.PartId = 95690

delete b
from dbo.Boms as b
inner join dbo.Products as pr
on pr.Id = b.SetId
inner join dbo.Products as pp
on pp.Id = b.PartId
where b.SetId = 96375 and b.PartId = 95690
