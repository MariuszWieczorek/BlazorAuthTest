  select x.PartCategory,x.PartCategoryName , x.part, x.PartName, MAX(x.cost) as lastCost, MAX(x.skladniki) as skladniki
  from (
select pr.ProductNumber 
  , parts.ProductNumber as part
  , parts.Name as PartName
  , co.Cost
  , b.PartQty
  , capa.CategoryNumber as PartCategory
  , capa.Name as PartCategoryName 
  ,(Select COUNT(*) from dbo.Boms as bb where bb.SetId = parts.Id)  as skladniki
  from dbo.Products as pr
  inner join dbo.ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  inner join dbo.ProductVersions as v
  on v.ProductId = pr.id
  inner join dbo.Boms as b
  on b.SetId = pr.Id and b.SetVersionId = v.Id
  inner join dbo.Products as parts
  on parts.Id = b.PartId
  inner join dbo.ProductCategories as capa
  on capa.Id = parts.ProductCategoryId
  left join dbo.ProductCosts as co
  on co.ProductId = parts.Id and co.AccountingPeriodId = 27

  where 1 = 1 
  -- and ca.CategoryNumber  IN('NAW','MIE')
  -- and capa.CategoryNumber  IN('SUR','ZAW-S','OME','PREP','OPK','OKS', 'BOL-KARKAS')
  and v.DefaultVersion = 1
  and pr.IsActive = 1
  -- and (co.Cost is null or co.Cost = 0) 
  -- order by pr.ProductNumber
  ) as x
   
   group by x.PartCategory, x.PartCategoryName, x.part, x.PartName
   having  MAX(x.skladniki) = 0
   and MAX(x.cost) is null 
   order by x.PartCategory, x.part
   



/*
select pr.ProductNumber , parts.ProductNumber as part , co.Cost, b.PartQty
  from dbo.Products as pr
  inner join dbo.ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  inner join dbo.ProductVersions as v
  on v.ProductId = pr.id
  inner join dbo.Boms as b
  on b.SetId = pr.Id and b.SetVersionId = v.Id
  inner join dbo.Products as parts
  on parts.Id = b.PartId
  left join dbo.ProductCosts as co
  on co.ProductId = parts.Id and co.AccountingPeriodId = 16
  where ca.CategoryNumber  IN('OKG','OKC','ODR','OBB','OBC','OBK','ODA','WW-PROF')
  and v.DefaultVersion = 1
  and (co.Cost is null or co.Cost = 0) 
  order by pr.ProductNumber
  */

  /*
  select pr.ProductNumber , parts.ProductNumber as part , co.Cost, b.PartQty
  from dbo.Products as pr
  inner join dbo.ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  inner join dbo.ProductVersions as v
  on v.ProductId = pr.id
  inner join dbo.Boms as b
  on b.SetId = pr.Id and b.SetVersionId = v.Id
  inner join dbo.Products as parts
  on parts.Id = b.PartId
  left join dbo.ProductCosts as co
  on co.ProductId = parts.Id and co.AccountingPeriodId = 16
  where ca.CategoryNumber  IN('WW-PROF','WW-PPROF','WW-AKL','WW-ORN')
  and v.DefaultVersion = 1
  and (co.Cost is null or co.Cost = 0) 
  order by parts.ProductNumber,pr.ProductNumber
  */
  
  /*
    select pr.ProductNumber , parts.ProductNumber as part , co.Cost, b.PartQty
  from dbo.Products as pr
  inner join dbo.ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  inner join dbo.ProductVersions as v
  on v.ProductId = pr.id
  inner join dbo.Boms as b
  on b.SetId = pr.Id and b.SetVersionId = v.Id
  inner join dbo.Products as parts
  on parts.Id = b.PartId
  left join dbo.ProductCosts as co
  on co.ProductId = parts.Id and co.AccountingPeriodId = 16
  where ca.CategoryNumber  IN('WF-AKC','WF-AKC-B','WF-AKC-POZ')
  and v.DefaultVersion = 1
  and (co.Cost is null or co.Cost = 0) 
  order by parts.ProductNumber,pr.ProductNumber
  */

  
  /*
  select x.part
  from (
  select pr.ProductNumber , parts.ProductNumber as part , co.Cost
  from dbo.Products as pr
  inner join dbo.ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  inner join dbo.ProductVersions as v
  on v.ProductId = pr.id
  inner join dbo.Boms as b
  on b.SetId = pr.Id and b.SetVersionId = v.Id
  inner join dbo.Products as parts
  on parts.Id = b.PartId
  left join dbo.ProductCosts as co
  on co.ProductId = parts.Id and co.AccountingPeriodId = 16
  where ca.CategoryNumber = 'MIE'
  and v.DefaultVersion = 1
  and (co.Cost is null or co.Cost = 0) 
 -- and parts.ProductNumber = 'SKS068'
 ) as x
 group by x.part

 */