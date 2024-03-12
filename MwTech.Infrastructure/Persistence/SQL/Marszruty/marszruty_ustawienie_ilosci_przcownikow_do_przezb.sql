--select pr.ProductNumber, rv.AlternativeNo
delete rv
from dbo.RouteVersions as rv
--inner join dbo.ManufactoringRoutes as r
--on r.RouteVersionId = rv.id
inner join dbo.Products as pr
on pr.Id = rv.ProductId
inner join dbo.ProductCategories as ca
on ca.id = pr.ProductCategoryId
where 1=1
and rv.AlternativeNo = 5
and pr.ProductNumber in
('DWU18438TR218DPA'
,'DWU18438TR218AK'
,'DWU18438TR218AT'
,'DWU18438TR218C00'
,'DWU18438TR218CET'
,'DWU18438TR218CEU'
,'DWU18438TR218CKB'
,'DWU18438TR218CNT'
,'DWU18438TR218CPA'
,'DWU18438TR218DEU'
,'DWU18438TR218DFD'
,'DWU18438TR218DKB'
,'DWU18438TR218DNT'
,'DWU18438TR218DO'
,'DWU18438TR218ES'
,'DWU18438TR218ET'
,'DWU18438TR218EU'
,'DWU18438TR218FD'
,'DWU18438TR218FO'
,'DWU18438TR218GR'
,'DWU18438TR218GW'
,'DWU18438TR218HT'
,'DWU18438TR218KB'
,'DWU18438TR218LL'
,'DWU18438TR218MI'
,'DWU18438TR218MT'
,'DWU18438TR218NT'
,'DWU18438TR218PA'
,'DWU18438TR218PC'
,'DWU18438TR218PL'
,'DWU18438TR218PN'
,'DWU18438TR218PR'
,'DWU18438TR218PT'
,'DWU18438TR218RD'
,'DWU18438TR218RG'
,'DWU18438TR218RN'
,'DWU18438TR218RW'
,'DWU18438TR218SF'
,'DWU18438TR218SO'
,'DWU18438TR218TA'
,'DWU18438TR218TG'
,'DWU18438TR218WE'
,'DWU18438TR218WZ')




/*
update r
set ChangeOverNumberOfEmployee = 1
from dbo.RouteVersions as rv
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = rv.id
inner join dbo.Products as pr
on pr.Id = rv.ProductId
inner join dbo.ProductCategories as ca
on ca.id = pr.ProductCategoryId
where ca.CategoryNumber = 'DOB'
and r.ChangeOverNumberOfEmployee = 0
*/