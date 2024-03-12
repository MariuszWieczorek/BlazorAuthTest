
select p.id, ca.CategoryNumber ,p.ProductNumber
from dbo.Products as p
  inner join dbo.ProductCategories as ca
  on ca.Id = p.ProductCategoryId
  where ca.CategoryNumber IN ('WF-AKC','WF-AKC-POZ')
  and p.Id in 
  (
  select w.ProductId
  from dbo.ManufactoringRoutes as r
  inner join dbo.RouteVersions as w
  on r.RouteVersionId = w.id
 inner join dbo.Resources as k
 on k.Id = r.ResourceId
  where k.ResourceNumber in ('PC.BO.POB','PC.BO.POP','PC.BO.WF','PC.BO.KJ')
  )

  
 update p
 set ProductCategoryId = (select Id from dbo.ProductCategories where CategoryNumber = 'WF-AKC-B' )
from dbo.Products as p
  inner join dbo.ProductCategories as ca
  on ca.Id = p.ProductCategoryId
  where ca.CategoryNumber IN ('WF-AKC','WF-AKC-POZ')
  and p.Id in 
  (
  select w.ProductId
  from dbo.ManufactoringRoutes as r
  inner join dbo.RouteVersions as w
  on r.RouteVersionId = w.id
 inner join dbo.Resources as k
 on k.Id = r.ResourceId
  where k.ResourceNumber in ('PC.BO.POB','PC.BO.POP','PC.BO.WF','PC.BO.KJ')
  )

  