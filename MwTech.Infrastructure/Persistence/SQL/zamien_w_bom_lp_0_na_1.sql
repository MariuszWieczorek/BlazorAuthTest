select ca.CategoryNumber, pr.ProductNumber, b.OrdinalNumber, ko.ProductNumber,
(Select Id from dbo.Products where ProductNumber = 'MPD014') as id
from dbo.Boms as b
inner join dbo.Products as pr
on pr.Id = b.SetId
inner join dbo.ProductCategories as ca
on ca.Id = Pr.ProductCategoryId
inner join dbo.Products as ko
on ko.Id = b.PartId
where 1 = 1
and ca.CategoryNumber like 'WF-AKC%'
and ko.ProductNumber = '001-006-0069'
and b.OrdinalNumber = 0

/*
UPDATE b
set OrdinalNumber = 1
from dbo.Boms as b
inner join dbo.Products as pr
on pr.Id = b.SetId
inner join dbo.ProductCategories as ca
on ca.Id = Pr.ProductCategoryId
inner join dbo.Products as ko
on ko.Id = b.PartId
where 1 = 1
and ca.CategoryNumber like 'WF-AKC%'
and ko.ProductNumber like 'F-%'
and b.OrdinalNumber = 0
*/