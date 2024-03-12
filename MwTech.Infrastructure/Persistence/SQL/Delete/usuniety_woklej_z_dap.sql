select *
from dbo.Boms as b
inner join dbo.Products  as ps
on ps.Id = b.SetId
inner join dbo.ProductCategories as cas
on cas.Id = ps.ProductCategoryId
inner join dbo.Products  as pp
on pp.Id = b.PartId
where cas.CategoryNumber = 'DAP'
and pp.ProductNumber = 'WOK.KLEJ-KLE-19'

delete b
from dbo.Boms as b
inner join dbo.Products  as ps
on ps.Id = b.SetId
inner join dbo.ProductCategories as cas
on cas.Id = ps.ProductCategoryId
inner join dbo.Products  as pp
on pp.Id = b.PartId
where cas.CategoryNumber = 'DAP'
and pp.ProductNumber = 'WOK.KLEJ-KLE-19'
