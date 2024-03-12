
select p.id, ca.CategoryNumber ,p.ProductNumber
from dbo.Products as p
  inner join dbo.ProductCategories as ca
  on ca.Id = p.ProductCategoryId
  where ca.CategoryNumber = 'DKJ' -- IN ('DWU','DKJ','DET','DET-KBK')
  and p.Id in 
  (
  select b.SetId
  from dbo.Boms as b
  inner join dbo.Products as p
  on p.Id = b.PartId
  inner join dbo.ProductCategories as ca
  on ca.Id = p.ProductCategoryId
  where ca.CategoryNumber = 'DWU-B'
  )


 update p
 set ProductCategoryId = (select Id from dbo.ProductCategories where CategoryNumber = 'DKJ-B' )
from dbo.Products as p
  inner join dbo.ProductCategories as ca
  on ca.Id = p.ProductCategoryId
  where ca.CategoryNumber = 'DKJ' -- IN ('DWU','DKJ','DET','DET-KBK')
  and p.Id in 
  (
  select b.SetId
  from dbo.Boms as b
  inner join dbo.Products as p
  on p.Id = b.PartId
  inner join dbo.ProductCategories as ca
  on ca.Id = p.ProductCategoryId
  where ca.CategoryNumber = 'DWU-B'
  )
