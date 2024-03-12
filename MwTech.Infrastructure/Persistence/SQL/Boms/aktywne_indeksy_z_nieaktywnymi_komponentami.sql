select ca.CategoryNumber, pr1.ProductNumber as ProductNumber, pr.ProductNumber as Part, v.VersionNumber, v.AlternativeNo , v.DefaultVersion
--update pr1
--set IsActive = 0
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.id = pr.ProductCategoryId
inner join dbo.Boms as b
on b.PartId = pr.Id
inner join dbo.Products as pr1
on pr1.id = b.SetId
inner join dbo.ProductVersions as v
on v.Id = b.SetVersionId
where pr1.IsActive = 1 and pr.IsActive = 0 and v.IsActive = 1


select pr1.ProductNumber as ProductNumber, pr.ProductNumber as Part, v.VersionNumber, v.AlternativeNo , v.DefaultVersion
--update pr1
--set IsActive = 0
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.id = pr.ProductCategoryId
inner join dbo.Boms as b
on b.PartId = pr.Id
inner join dbo.Products as pr1
on pr1.id = b.SetId
inner join dbo.ProductVersions as v
on v.Id = b.SetVersionId
where pr1.IsActive = 1 and pr.IsActive = 0 and v.IsActive = 1 and pr1.ProductNumber like 'D%'
group by pr1.ProductNumber , pr.ProductNumber , v.VersionNumber, v.AlternativeNo , v.DefaultVersion




/*
inner join dbo.ProductVersions as v
on v.ProductId = pr.Id
inner join dbo.Boms as b
on b.SetVersionId = v.id
*/