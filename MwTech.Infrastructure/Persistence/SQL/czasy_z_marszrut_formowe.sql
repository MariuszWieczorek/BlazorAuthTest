use MwTech;

select y.CategoryNumber, y.matka, y.waz
, y.indeksy, y.przykl_indeks
, y.waz_dwy
, y.waz_dob
, y.mat_dap
, y.mat_dst
, y.mat_dwu
, y.mat_dkj 
,( CASE y.CategoryNumber when 'DET' then y.mat_det  when 'DET-KBK' then y.mat_det_kbk ELSE 0 END ) as pakowanie
, waz_dwy + waz_dob + mat_dap + mat_dst + mat_dwu + mat_dkj + ( CASE y.CategoryNumber when 'DET' then y.mat_det when 'DET-KBK' then y.mat_det_kbk ELSE 0 END ) as czas_razem
, dbo.getProductWeight (( select id from dbo.Products where ProductNumber = y.przykl_indeks)) as waga
FROM (
select x.CategoryNumber, x.Idx02 as waz, x.Idx01 as matka, x.indeksy
--
	,CAST(ROUND(( select SUM(r.OperationLabourConsumption * r.ResourceQty * 3600)
	from dbo.RouteVersions as v
	inner join dbo.ManufactoringRoutes as r
	on r.RouteVersionId = v.id
	inner join dbo.ProductCategories as vca 
	on vca.Id = v.ProductCategoryId
	where v.ProductId = waz.id
	and v.IsActive = 1 and v.DefaultVersion = 1
	and vca.CategoryNumber = 'DWY'
	),2) as numeric(10,2)) as waz_dwy
--
	,CAST(ROUND(( select SUM(r.OperationLabourConsumption * r.ResourceQty * 3600)
	from dbo.RouteVersions as v
	inner join dbo.ManufactoringRoutes as r
	on r.RouteVersionId = v.id
	inner join dbo.ProductCategories as vca 
	on vca.Id = v.ProductCategoryId
	where v.ProductId = waz.id
	and v.IsActive = 1 and v.DefaultVersion = 1
	and vca.CategoryNumber = 'DOB'
	) ,2) as numeric(10,2)) as waz_dob
	--
	,CAST(ROUND(( select SUM(r.OperationLabourConsumption * r.ResourceQty * 3600)
	from dbo.RouteVersions as v
	inner join dbo.ManufactoringRoutes as r
	on r.RouteVersionId = v.id
	inner join dbo.ProductCategories as vca 
	on vca.Id = v.ProductCategoryId
	where v.ProductId = matka.id
	and v.IsActive = 1 and v.DefaultVersion = 1
	and vca.CategoryNumber = 'DAP'
	) ,2) as numeric(10,2)) as mat_dap
	--
	,CAST(ROUND(( select SUM(r.OperationLabourConsumption * r.ResourceQty * 3600)
	from dbo.RouteVersions as v
	inner join dbo.ManufactoringRoutes as r
	on r.RouteVersionId = v.id
	inner join dbo.ProductCategories as vca 
	on vca.Id = v.ProductCategoryId
	where v.ProductId = matka.id
	and v.IsActive = 1 and v.DefaultVersion = 1
	and vca.CategoryNumber = 'DST'
	) ,2) as numeric(10,2)) as mat_dst
	--
	,CAST(ROUND(( select SUM(r.OperationLabourConsumption * r.ResourceQty * 3600)
	from dbo.RouteVersions as v
	inner join dbo.ManufactoringRoutes as r
	on r.RouteVersionId = v.id
	inner join dbo.ProductCategories as vca 
	on vca.Id = v.ProductCategoryId
	where v.ProductId = matka.id
	and v.IsActive = 1 and v.DefaultVersion = 1
	and vca.CategoryNumber = 'DWU'
	) ,2) as numeric(10,2)) as mat_dwu
	--
	,CAST(ROUND(( select SUM(r.OperationLabourConsumption * r.ResourceQty * 3600)
	from dbo.RouteVersions as v
	inner join dbo.ManufactoringRoutes as r
	on r.RouteVersionId = v.id
	inner join dbo.ProductCategories as vca 
	on vca.Id = v.ProductCategoryId
	where v.ProductId = matka.id
	and v.IsActive = 1 and v.DefaultVersion = 1
	and vca.CategoryNumber = 'DKJ'
	) ,2) as numeric(10,2)) as mat_dkj
	--
	,CAST(ROUND(( select SUM(r.OperationLabourConsumption * r.ResourceQty * 3600)
	from dbo.RouteVersions as v
	inner join dbo.ManufactoringRoutes as r
	on r.RouteVersionId = v.id
	inner join dbo.ProductCategories as vca 
	on vca.Id = v.ProductCategoryId
	where v.ProductId = matka.id
	and v.IsActive = 1 and v.DefaultVersion = 1
	and vca.CategoryNumber = 'DET'
	) ,2) as numeric(10,2)) as mat_det
	--
	,CAST(ROUND(( select SUM(r.OperationLabourConsumption * r.ResourceQty * 3600)
	from dbo.RouteVersions as v
	inner join dbo.ManufactoringRoutes as r
	on r.RouteVersionId = v.id
	inner join dbo.ProductCategories as vca 
	on vca.Id = v.ProductCategoryId
	where v.ProductId = matka.id
	and v.IsActive = 1 and v.DefaultVersion = 1
	and vca.CategoryNumber = 'DET-LUZ'
	) ,2) as numeric(10,2)) as mat_det_luz
	--
	,CAST(ROUND(( select SUM(r.OperationLabourConsumption * r.ResourceQty * 3600)
	from dbo.RouteVersions as v
	inner join dbo.ManufactoringRoutes as r
	on r.RouteVersionId = v.id
	inner join dbo.ProductCategories as vca 
	on vca.Id = v.ProductCategoryId
	where v.ProductId = matka.id
	and v.IsActive = 1 and v.DefaultVersion = 1
	and vca.CategoryNumber = 'DET-KBK'
	) ,2) as numeric(10,2)) as mat_det_kbk
	
	,(SELECT min(ppr.ProductNumber)
	  from dbo.Products as ppr
	  inner join dbo.ProductCategories as pca
	  on pca.Id = ppr.ProductCategoryId
	  where ppr.Idx01 = x.Idx01
	  and ppr.IsActive = 1
	  and pca.CategoryNumber = x.CategoryNumber
	  -- order by ppr.ProductNumber
	  ) as przykl_indeks
	  /*
	  ,(SELECT min(ppr.id)
	  from dbo.Products as ppr
	  inner join dbo.ProductCategories as pca
	  on pca.Id = ppr.ProductCategoryId
	  where ppr.Idx01 = x.Idx01
	  and ppr.IsActive = 1
	  and pca.CategoryNumber = x.CategoryNumber
	  -- order by ppr.ProductNumber
	  ) as przykl_indeksa
	 */
from (
select ca.CategoryNumber, pr.Idx02, pr.Idx01, COUNT(*) indeksy
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
where ca.CategoryNumber in ('WF-AKC')
and pr.IsActive = 1
group by ca.CategoryNumber, pr.Idx02, pr.Idx01
) as x
inner join dbo.Products as matka
on matka.ProductNumber = x.Idx01
inner join dbo.Products as waz
on waz.ProductNumber = x.Idx02
) as y
order by y.matka, y.CategoryNumber