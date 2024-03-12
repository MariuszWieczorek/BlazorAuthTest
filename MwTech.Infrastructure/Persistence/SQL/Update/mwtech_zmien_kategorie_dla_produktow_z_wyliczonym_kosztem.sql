select p.id, ca.CategoryNumber ,p.ProductNumber
from dbo.Products as p
  inner join dbo.ProductCategories as ca
  on ca.Id = p.ProductCategoryId
  where ca.CategoryNumber = 'DET' -- IN ('DWU','DKJ','DET','DET-KBK')
  and p.Id in 
  (
  select c.ProductId
  from dbo.ProductCosts as c
  inner join dbo.Products as p
  on p.Id = c.ProductId
  inner join dbo.ProductCategories as ca
  on ca.Id = p.ProductCategoryId
  where ca.CategoryNumber = 'DET'
  )


 update p
 set ProductCategoryId = (select Id from dbo.ProductCategories where CategoryNumber = 'DET-B' )
 from dbo.Products as p
  inner join dbo.ProductCategories as ca
  on ca.Id = p.ProductCategoryId
  where ca.CategoryNumber = 'DET' -- IN ('DWU','DKJ','DET','DET-KBK')
  and p.Id in 
  (
  select c.ProductId
  from dbo.ProductCosts as c
  inner join dbo.Products as p
  on p.Id = c.ProductId
  inner join dbo.ProductCategories as ca
  on ca.Id = p.ProductCategoryId
  where ca.CategoryNumber = 'DET'
  )


  /*
  from dbo.Products as p
  inner join dbo.ProductCategories as ca
  on ca.Id = p.ProductCategoryId
  where ca.CategoryNumber = 'DET' -- IN ('DWU','DKJ','DET','DET-KBK')
  and p.Id in 
  (
  select c.ProductId
  from dbo.ProductCosts as c
  inner join dbo.Products as p
  on p.Id = c.ProductId
  inner join dbo.ProductCategories as ca
  on ca.Id = p.ProductCategoryId
  where ca.CategoryNumber = 'DET'
  )
  */